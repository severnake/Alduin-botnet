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
    }
}
