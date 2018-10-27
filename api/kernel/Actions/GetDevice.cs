using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Kernel.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;

namespace Kernel.Actions
{

    public class GetDeviceHandler : IRequestHandler<GetDeviceRequest, Phone>
    {
        private readonly ExternalSourceOptions _options;
        private readonly ILogger<GetDeviceHandler> _logger;

        public GetDeviceHandler(
            ILogger<GetDeviceHandler> logger,
            IOptions<ExternalSourceOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Phone> Handle(GetDeviceRequest request, CancellationToken cancellationToken)
        {
            var phones = await Wrap(
                _options.RateLimitPauseSeconds,
                () => {
                    return _options.GetDeviceUri
                        .SetQueryParam("token", _options.Token)
                        .SetQueryParam("device", request.DeviceName)
                        .GetJsonAsync<List<PhoneDto>>(cancellationToken);
                });
            var phoneDto = phones?.FirstOrDefault(x => x.DeviceName == request.DeviceName);
            if (phoneDto == null) return null;

            var result = new Phone
            {
                Model = phoneDto.DeviceName,
                Tech = phoneDto.Technology,
                Bands = new Dictionary<string, string>()
            };
            if (!string.IsNullOrWhiteSpace(phoneDto._2g_bands))
            {
                result.Bands.Add("g2", phoneDto._2g_bands);
            }
            if (!string.IsNullOrWhiteSpace(phoneDto._3g_bands))
            {
                result.Bands.Add("g3", phoneDto._3g_bands);
            }
            if (!string.IsNullOrWhiteSpace(phoneDto._4g_bands))
            {
                result.Bands.Add("g4", phoneDto._4g_bands);
            }
            return result;
        }

        private async Task<T> Wrap<T>(long timeoutSeconds, Func<Task<T>> action)
        {
            while (true)
            {
                try
                {
                    return await action();
                }
                catch (FlurlHttpTimeoutException fhte)
                {
                    _logger.LogError(fhte.ToString());
                    await Task.Delay(TimeSpan.FromSeconds(timeoutSeconds));
                }
                catch (FlurlParsingException fpe)
                {
                    var response = await fpe.GetResponseStringAsync();
                    _logger.LogDebug($"Cannot parse result: {response}");
                    await Task.Delay(TimeSpan.FromSeconds(timeoutSeconds));
                }
                catch (FlurlHttpException fhe)
                {
                    int errorCode = fhe.Call.HttpStatus.HasValue ? (int)fhe.Call.HttpStatus.Value : -1;
                    if (errorCode == 404)
                    {
                        return default(T);
                    }
                    if (errorCode == 429)
                    {
                        _logger.LogDebug("Rate limit exceeded. Pausing for 10 seconds");
                        await Task.Delay(TimeSpan.FromSeconds(timeoutSeconds));
                    }
                }
            }
        }
    }

    public class GetDeviceRequest : IRequest<Phone>
    {
        public string DeviceName { get; }

        public GetDeviceRequest(string device)
        {
            DeviceName = device;
        }
    }
}