using System;
using System.Threading;
using System.Threading.Tasks;
using Kernel;
using Kernel.Actions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Tests
{
    public class TestGettingDevice
    {
        private readonly ExternalSourceOptions _options;

        public TestGettingDevice()
        {
            _options = new ExternalSourceOptions
            {
                GetDeviceUri = "https://fonoapi.freshpixl.com/v1/getdevice",
                Token = "a528483f2e5ba46c5cb6769ddc2c533ae1d95e33570ee1f0",
                CacheExpirationHours = 12,
                RateLimitPauseSeconds = 10,
                ModelNames = new []
                {
                    "Samsung Galaxy S9",
                    "Samsung Galaxy S8",
                    "Samsung Galaxy S7",
                    "Motorola Nexus 6",      
                    "LG Nexus 5X",
                    "Apple iPhone X",
                    "Apple iPhone 8",
                    "Apple iPhone 4s",
                    "Nokia 3310"
                }
            };
        }

        [Fact]
        public async Task Should_get_device_by_name()
        {
            var options = Options.Create(_options);
            var logger = Substitute.For<ILogger<GetDeviceHandler>>();
            var handler = new GetDeviceHandler(logger, options);
            var response = await handler.Handle(new GetDeviceRequest("Nokia 3310"), CancellationToken.None);
            Assert.NotNull(response);
            Assert.Equal("Nokia 3310", response.Model);
            Assert.DoesNotContain("g3", response.Bands.Keys);
        }
    }
}
