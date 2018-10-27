using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models;

namespace Kernel.Actions
{
    public class ReleasePhoneHandler : IRequestHandler<ReleasePhoneRequest, bool>
    {
        private readonly IDataProviderFactory _factory;

        public ReleasePhoneHandler(IDataProviderFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public Task<bool> Handle(ReleasePhoneRequest request, CancellationToken cancellationToken)
        {
            using (var db = _factory.Create())
            {
                var phones = db.GetCollection<Phone>();
                var phone = phones.FindOne(x => x.Model == request.Model);
                if (phone == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(request), request.Model, "There is no phone of request model");
                }
                if (!phone.BookedAt.HasValue)
                {
                    return Task.FromResult(false);
                }
                phone.BookedAt = null;
                phone.BookedBy = null;
                phones.Upsert(phone);
                return Task.FromResult(true);
            }
        }
    }

    public class ReleasePhoneRequest : IRequest<bool>
    {
        public string Model { get; }

        public ReleasePhoneRequest(string model)
        {
            Model = model;
        }
    }
}