﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyFox.Application.Accounts.Queries.GetAccountById;
using MoneyFox.Application.Interfaces;
using MoneyFox.Domain.Entities;

namespace MoneyFox.Application.Accounts.Queries.GetAccountNameById
{
    public class GetAccountNameByIdQuery : IRequest<string>
    {
        public GetAccountNameByIdQuery(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; }

        public class Handler : IRequestHandler<GetAccountNameByIdQuery, string>
        {
            private readonly IEfCoreContext context;

            public Handler(IEfCoreContext context)
            {
                this.context = context;
            }

            public async Task<string> Handle(GetAccountNameByIdQuery request, CancellationToken cancellationToken)
            {
                return await context.Accounts
                                    .Where(x =>  x.Id == request.AccountId)
                                    .Select(x => x.Name)
                                    .FirstAsync(cancellationToken);
            }
        }
    }
}
