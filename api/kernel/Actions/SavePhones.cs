using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models;

namespace Kernel.Actions
{
    public class SavePhonesHandler : IRequestHandler<SavePhonesRequest, IEnumerable<Phone>>
    {
        private readonly IDataProviderFactory _factory;

        public SavePhonesHandler(IDataProviderFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public Task<IEnumerable<Phone>> Handle(SavePhonesRequest request, CancellationToken cancellationToken)
        {
            if (request.Phones == null)
            {
                return Task.FromResult<IEnumerable<Phone>>(null);
            }
            using (var db = _factory.Create())
            {
                var collection = db.GetCollection<Phone>();
                foreach (var phone in request.Phones)
                {
                    var existing = collection.FindOne(x => x.Model == phone.Model);
                    if (existing != null)
                    {
                        phone.Id = existing.Id;
                    }
                    collection.Upsert(phone);
                }
                return Task.FromResult(request.Phones);
            }
        }
    }

    public class SavePhonesRequest : IRequest<IEnumerable<Phone>>
    {
        public IEnumerable<Phone> Phones { get; }

        public SavePhonesRequest(IEnumerable<Phone> phones)
        {
            Phones = phones;
        }
    }
}