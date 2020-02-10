using Alduin.Shared.Interfaces.UnitOfWork;
using AutoMapper;
using Moq;
using NHibernate;
using Xunit;
using Alduin.Logic.Managers;
using Alduin.Shared.DTOs;
using Alduin.DataAccess.Entities;

namespace Alduin.Logic.Test.Managers
{
    
    public class InvitationManagerTest
    {
        [Fact]
        public void InvitationManager_SaveAInvitationWithEmptyInvitationKey_ThrowsValidationError()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var mapperMock = new Mock<IMapper>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var invitationManager = new InvitationManager(sessionMock.Object, mapperMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock
                .SetupGet(x => x.IsManagedTransaction)
                .Returns(true);

            var invitationDTO = new InvitationDTO
            {
                Used = false,
                UserId = 1
            };
            var invitationEntity = new InvitationEntity
            {
                Used = false,
            };
            mapperMock
                .Setup(x => x.Map(It.IsAny<InvitationDTO>(), It.IsAny<InvitationEntity>()))
                .Returns(invitationEntity);
            // Act
            var saveResult = invitationManager.Save(invitationDTO);
            // Assert
            Assert.False(saveResult.Succeeded);
            Assert.True(saveResult.ErrorMessages.Count > 0);
        }
        [Fact]
        public void InvitationManager_SaveAInvitationWithUsed_ThrowsValidationError()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var mapperMock = new Mock<IMapper>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var invitationManager = new InvitationManager(sessionMock.Object, mapperMock.Object, unitOfWorkMock.Object);

            unitOfWorkMock
                .SetupGet(x => x.IsManagedTransaction)
                .Returns(true);

            var invitationDTO = new InvitationDTO
            {
                Used = true,
                UserId = 1,
                invitationKey = "fsdfsdfdsfdsfdfs"
            };
            var invitationEntity = new InvitationEntity
            {
                Used = true,
                invitationKey = "fsdfsdfdsfdsfdfs"
            };
            mapperMock
                .Setup(x => x.Map(It.IsAny<InvitationDTO>(), It.IsAny<InvitationEntity>()))
                .Returns(invitationEntity);
            // Act
            var saveResult = invitationManager.Save(invitationDTO);
            // Assert
            Assert.False(saveResult.Succeeded);
            Assert.True(saveResult.ErrorMessages.Count > 0);
        }
    }
}
