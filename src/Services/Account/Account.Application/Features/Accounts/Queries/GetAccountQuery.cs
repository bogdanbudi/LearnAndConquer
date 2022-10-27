using MediatR;

namespace Account.Application.Features.Accounts.Queries
{
    public  class GetAccountQuery : IRequest<AccountByEmailRequest>
    {
        public string Email { get; set; }

        public GetAccountQuery(string email)
        {
            Email = email;
        }
    }
}
