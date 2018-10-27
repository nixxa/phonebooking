using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models;

namespace Kernel.Actions
{
    public class BookPhoneHandler : IRequestHandler<BookPhoneRequest, bool>
    {
        private readonly IDataProviderFactory _factory;

        public BookPhoneHandler(IDataProviderFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public Task<bool> Handle(BookPhoneRequest request, CancellationToken cancellationToken)
        {
            using (var db = _factory.Create())
            {
                var phones = db.GetCollection<Phone>();
                var phone = phones.FindOne(x => x.Model == request.Model);
                if (phone == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(request), request.Model, "There is no phone of request model");
                }
                if (phone.BookedAt.HasValue)
                {
                    return Task.FromResult(false);
                }
                phone.BookedAt = DateTime.UtcNow;
                phone.BookedBy = request.Email;
                phones.Upsert(phone);
                return Task.FromResult(true);
            }
        }
    }

    public class BookPhoneRequest : IRequest<bool>
    {
        public string Model { get; }
        public string Email { get; }

        public BookPhoneRequest(string model, string email)
        {
            Model = model;
            Email = email;
        }
    }
}