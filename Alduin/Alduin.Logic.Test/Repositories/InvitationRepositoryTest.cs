using Alduin.Logic.Interfaces.Repositories;
using Alduin.Shared.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alduin.Logic.Test.Repositories
{
    public class InvitationRepositoryTest
    {
        [Fact]
        public void InvitationRepository_GetsInvitationWithExistingId_GetsAnExistingUser()
        {
            var invitationRepository = new Mock<IInvitationRepository>();

            // Arrange
            var invitationDto = new InvitationDTO {
                Id = 1,
                invitationKey = "fsdfsdfsdf",
                Used = false
            };

            invitationRepository
                .Setup(x => x.Get(It.Is<int>(i => i == 1)))
                .Returns(invitationDto);

            invitationRepository
                .Setup(x => x.Get(It.Is<int>(i => i != 1)))
                .Returns((InvitationDTO)null);

            // Act

            var invitation = invitationRepository.Object.Get(1);

            // Assert
            Assert.NotNull(invitation);
            Assert.Equal(invitation.Id, invitationDto.Id);
        }
        [Fact]
        public void InvitationRepository_GetsInvitationWithExistingKey_GetsAnExistingUser()
        {
            var invitationRepository = new Mock<IInvitationRepository>();

            // Arrange
            var invitationDto = new InvitationDTO
            {
                Id = 1,
                invitationKey = "fsdfsdfsdf",
                Used = false
            };

            invitationRepository
                .Setup(x => x.FindByInvitationKey(It.Is<string>(i => i == "fsdfsdfsdf")))
                .Returns(invitationDto);

            invitationRepository
                .Setup(x => x.FindByInvitationKey(It.Is<string>(i => i != "fsdfsdfsdf")))
                .Returns((InvitationDTO)null);

            // Act

            var invitation = invitationRepository.Object.FindByInvitationKey("fsdfsdfsdf");

            // Assert
            Assert.NotNull(invitation);
            Assert.Equal(invitation.Id, invitationDto.Id);
        }
        [Fact]
        public void InvitationRepository_ListInvitationWithExistingUserId_GetsAnExistingUser()
        {
            var invitationRepository = new Mock<IInvitationRepository>();

            // Arrange
            var invitationDto = new InvitationDTO
            {
                UserId = 1,
                invitationKey = "fsdfsdfsdf",
                Used = false
            };
            
            invitationRepository
                .Setup(x => x.FindByUserId(It.Is<int>(i => i != 1)))
                .Returns((InvitationDTO[])null);

            // Act

            var invitation = invitationRepository.Object.FindByUserId(1);

            // Assert
            Assert.NotNull(invitation);
        }
        [Fact]
        public void InvitationRepository_ListInvitationWithExistingUserId_GetsAnNotExistingUser()
        {
            var invitationRepository = new Mock<IInvitationRepository>();

            // Arrange
            var invitationDto = new InvitationDTO
            {
                UserId = 1,
                invitationKey = "fsdfsdfsdf",
                Used = false
            };

            invitationRepository
                .Setup(x => x.FindByUserId(It.Is<int>(i => i != 1)))
                .Returns((InvitationDTO[])null);

            // Act

            var invitation = invitationRepository.Object.FindByUserId(2);

            // Assert
            Assert.Null(invitation);
        }
    }
}
