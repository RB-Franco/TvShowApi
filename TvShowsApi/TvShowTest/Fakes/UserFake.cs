using Entity.Entity;
using System.Collections.Generic;

namespace TvShowTest.Fakes
{
    public static class UserFake
    {
        public static User FakeUser01 => new User
        {
            Id = "11a-2b2-33c",
            Name = "User Fake",
            Email = "userFake@email.com",
            PasswordHash = "11a-2b2-33c"
        };

        public static User FakeUser02 => new User
        {
            Id = "12a-2b3-34c",
            Name = "User Fake 02",
            Email = "userFake02@email.com",
            PasswordHash = "12a-2b3-34cc"
        };

        public static List<User> AllUsers => new List<User> { FakeUser01, FakeUser02 };
    }
}
