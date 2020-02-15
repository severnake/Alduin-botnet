using Alduin.Logic.Interfaces.Repositories;
using Alduin.Shared.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alduin.Logic.Test.Repositories
{
    public class BotRepositoryTest
    {
        [Fact]
        public void BotRepository_GetsBotsWithStatus_GetsAnExistingBots()
        {
            var botRepository = new Mock<IBotRepository>();

            // Arrange
            var botDto1 = new BotDTO
            {
                Id = 1,
                Domain = "dasdsadasd",
                LastLoggedInUTC = DateTime.UtcNow,

            };
            var botDto2 = new BotDTO
            {
                Id = 1,
                Domain = "dasdsadasd",
                LastLoggedInUTC = DateTime.UtcNow.AddMinutes(-10),
            };
            botRepository
                .Setup(x => x.FindAddressByAvailable(It.Is<bool>(i => i == true)));

            // Act

            var bot = botRepository.Object.FindAddressByAvailable(false);

            // Assert
            Assert.NotNull(bot);
        }
    }
}
