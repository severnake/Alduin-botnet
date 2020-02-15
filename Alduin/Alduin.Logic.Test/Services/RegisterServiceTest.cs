using Alduin.Logic.Mediator.Queries;
using Alduin.Logic.Services;
using Alduin.Shared.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alduin.Logic.Test.Services
{
    public class RegisterServiceTest
    {
        [Fact]
        public async Task RegisterService_RegisterWithInvalidKey_ReturnsFalseActionResult()
        {
            var mediatorMock = new Mock<IMediator>();
            var registerService = new RegisterService(mediatorMock.Object);
            var key = "InvalidKey";
            var user = new UserDTO
            {
                Name = "Test",
                Email = "Test@test.hu",
                UserName = "Test@test.hu",
                NormalizedUserName = "TEST@TEST.HU"
            };
            var password = "asdasd";

            mediatorMock
                .Setup(x => x.Send(It.Is<GetInvitationByKeyQuery>(y => y.invitationKey == key), default))
                .Returns(Task.FromResult((InvitationDTO)null));
            
            var actionResult = await registerService.Register(user, password, key);

            Assert.False(actionResult.Suceeded);
        }
    }
}
