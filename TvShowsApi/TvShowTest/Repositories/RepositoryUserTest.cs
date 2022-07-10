using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TvShowTest.DataLoader;
using TvShowTest.Fakes;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;

namespace TvShowTest.Repositories
{
    public class RepositoryUserTest
    {
        private RepositoryUser _repositoryUser;

        public RepositoryUserTest()
        {
            Setup().Wait();
        }

        [Theory]
        [MemberData(nameof(CreateCasesUsers))]
        public async Task Should_return_user_by_email(string email, string value)
        {
            var result = await _repositoryUser.ReturnIdUser(email);
            result.Should().BeEquivalentTo(value);
        }

        [Theory]
        [MemberData(nameof(CreateCasesAddUsers))]
        public async Task Should_add_new_user(string name, string email, string password, bool value)
        {
            var result = await _repositoryUser.AddUser(name, email, password);
            result.Should().Be(value);
        }

        public static IEnumerable<object[]> CreateCasesUsers()
        {
            return new List<object[]>
            {
                new object[] { UserFake.FakeUser01.Email, UserFake.FakeUser01.Id},
                new object[] { "teste@email.com", string.Empty},
            };
        }

        public static IEnumerable<object[]> CreateCasesAddUsers()
        {
            return new List<object[]>
            {
                new object[] { UserFake.FakeUser02.Name, UserFake.FakeUser02.Email, UserFake.FakeUser02.PasswordHash, true},
                new object[] { UserFake.FakeUser02.Name, null, null, false},
            };
        }

        private async Task Setup()
        {
            using var fixture = new TvShowContextFixture();
            var context = fixture.Context;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            _repositoryUser = await GetDataLoader(context);
        }

        private async Task<RepositoryUser> GetDataLoader(Context ctx)
        {
            var _dataLoader = new UserRepositoryTestDataLoader(ctx);
            await _dataLoader.LoadData();
            ctx.ChangeTracker.Clear();
            return new RepositoryUser();
        }
    }
}
