using Account.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Features.Accounts.Queries
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountByEmailRequest>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountByEmailRequest> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account =  await _accountRepository.GetUserByEmail(request.Email);
           return _mapper.Map<AccountByEmailRequest>(account);
        }
    }
}
