﻿using System.Threading.Tasks;
using MediatR;
using MoneyFox.Application.Accounts.Queries.GetIfAccountWithNameExists;
using MoneyFox.Application.Tests.Infrastructure;
using MoneyFox.Domain.Entities;
using MoneyFox.Persistence;
using Should;
using Xunit;

namespace MoneyFox.Application.Tests.Accounts.Queries.GetIfAccountWithNameExists
{
    public class GetIfAccountWithNameExistsQueryTests : IRequest<bool>
    {
        private readonly EfCoreContext context;

        public GetIfAccountWithNameExistsQueryTests()
        {
            context = TestEfCoreContextFactory.Create();
        }

        public void Dispose()
        {
            TestEfCoreContextFactory.Destroy(context);
        }

        [Theory]
        [InlineData("Foo", true)]
        [InlineData("foo", true)]
        [InlineData("Foo212", false)]
        public async Task GetExcludedAccountQuery_CorrectNumberLoaded(string name, bool expectedResult)
        {
            // Arrange
            var accountExcluded = new Account("Foo", 80, isExcluded: true);
            var accountIncluded = new Account("test", 80);
            await context.AddAsync(accountExcluded);
            await context.AddAsync(accountIncluded);
            await context.SaveChangesAsync();

            // Act
            bool result = await new GetIfAccountWithNameExistsQuery.Handler(context).Handle(new GetIfAccountWithNameExistsQuery{AccountName = name}, default);

            // Assert
            result.ShouldEqual(expectedResult);
        }
    }
}
