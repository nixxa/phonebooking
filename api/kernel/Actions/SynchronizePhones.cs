using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models;

namespace Kernel.Actions
{
    public class SynchronizePhonesHandler : IRequestHandler<SynchronizePhonesRequest, IEnumerable<Phone>>
    {
        private readonly IMediator _mediator;

        public SynchronizePhonesHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IEnumerable<Phone>> Handle(SynchronizePhonesRequest request, CancellationToken cancellationToken)
        {
            if (request.ModelNames == null || !request.ModelNames.Any())
            {
                return null;
            }
            var phones = new List<Task<Phone>>();
            foreach (var model in request.ModelNames)
            {
                phones.Add(_mediator.Send(new GetDeviceRequest(model))); 
            }
            var result = await Task.WhenAll(phones).ConfigureAwait(false);
            return result;
        }
    }

    public class SynchronizePhonesRequest : IRequest<IEnumerable<Phone>>
    {
        public IEnumerable<string> ModelNames { get; }

        public SynchronizePhonesRequest(IEnumerable<string> models)
        {
            ModelNames = models;
        }
    }
}