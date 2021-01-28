using Innoloft.Models;
using System;
using Xunit;

namespace Innoloft.Tests
{
    public class UserDTOTest
    {
        [Fact]
        public async void UserDTOFetchTest()
        {
            var obj = await UserDTO.fetchUserAsync(1);
            Assert.True(obj.name == "Leanne Graham");
        }

        [Fact]
        public async void UserDTONullFetchTest()
        {
            var obj = await UserDTO.fetchUserAsync(-1);
            Assert.True(obj.name == null);
        }
    }
}
