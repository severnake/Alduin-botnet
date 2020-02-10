using Xunit;
using AutoMapper;
using Moq;
using NHibernate;
using Alduin.Shared.Interfaces.UnitOfWork;
using Alduin.Logic.Managers;
using Alduin.Shared.DTOs;
using Alduin.DataAccess.Entities;

namespace Alduin.Logic.Test.Managers
{
    public class UserManagerTest
    {
        [Fact]
        public void UserManager_SavesAUserWithEmptyEmail_ThrowsValidationError()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var mapperMock = new Mock<IMapper>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userManager = new UserManager(sessionMock.Object, mapperMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock
                .SetupGet(x => x.IsManagedTransaction)
                .Returns(true);

            var userDto = new UserDTO
            {
                PasswordHash = "",
                Name = "Test User",
                UserName = "Test User",
                NormalizedUserName = "TEST USER"
            };
            var userEntity = new UserEntity
            {
                PasswordHash = "",
                Name = "Test User",
                UserName = "Test User",
                NormalizedUserName = "TEST USER"
            };

            mapperMock
                .Setup(x => x.Map(It.IsAny<UserDTO>(), It.IsAny<UserEntity>()))
                .Returns(userEntity);

            // Act
            var saveResult = userManager.Save(userDto);

            // Assert
            Assert.False(saveResult.Succeeded);
            Assert.True(saveResult.ErrorMessages.Count > 0);
        }
        [Fact]
        public void UserManager_SavesAValidNewUser_UserGetsId()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var mapperMock = new Mock<IMapper>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userManager = new UserManager(sessionMock.Object, mapperMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock
                .SetupGet(x => x.IsManagedTransaction)
                .Returns(true);

            var userDto = new UserDTO
            {
                Email = "test@email.com",
                PasswordHash = "",
                Name = "Test User",
                UserName = "Test User",
                NormalizedUserName = "TEST USER"
            };
            var userEntity = new UserEntity
            {
                Email = "test@email.com",
                PasswordHash = "",
                Name = "Test User",
                UserName = "Test User",
                NormalizedUserName = "TEST USER"
            };

            var expectedId = 1;

            mapperMock
                .Setup(x => x.Map(It.IsAny<UserDTO>(), It.IsAny<UserEntity>()))
                .Returns(userEntity);

            sessionMock
                .Setup(x => x.Save(It.IsAny<UserEntity>()))
                .Callback((object user) =>
                {
                    ((UserEntity)user).Id = expectedId;
                });

            // Act
            var saveResult = userManager.Save(userDto);

            // Assert
            Assert.True(saveResult.Succeeded);
            Assert.Equal(saveResult.Id, expectedId);
        }
    }
}
