using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models;

namespace Kernel.Actions
{
    public class GetAllPhonesHandler : IRequestHandler<GetAllPhonesRequest, IEnumerable<Phone>>
    {
        private readonly IDataProviderFactory _factory;

        public GetAllPhonesHandler(IDataProviderFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public Task<IEnumerable<Phone>> Handle(GetAllPhonesRequest request, CancellationToken cancellationToken)
        {
            using (var db = _factory.Create())
            {
                var phonesCollection = db.GetCollection<Phone>();
                var result = phonesCollection.FindAll();
                return Task.FromResult(result);
            }
        }
    }

    public class GetAllPhonesRequest : IRequest<IEnumerable<Phone>>
    {

    }
}