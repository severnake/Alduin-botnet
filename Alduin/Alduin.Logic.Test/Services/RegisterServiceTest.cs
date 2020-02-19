using Alduin.Logic.Mediator.Queries;
using Alduin.Logic.Services;
using Alduin.Shared.DTOs;
using AutoMapper;
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
        [Fact]
        public async Task RegisterService_RegisterWidthUsedInvitation_ReturnsFalseActionResult()
        {
            var mediatorMock = new Mock<IMediator>();
            var registerService = new RegisterService(mediatorMock.Object);
            var key = "Key";
            var user = new UserDTO
            {
                Name = "Test",
                Email = "Test@test.hu",
                UserName = "Test@test.hu",
                NormalizedUserName = "TEST@TEST.HU"
            };
            var deriveduser = new UserDTO
            {
                Name = "derivedTest",
                Email = "derivedTest@test.hu",
                UserName = "derivedTest@test.hu",
                NormalizedUserName = "derivedTEST@TEST.HU",
                Id = 35,
                PasswordHash = "PasswordHash"
            };
            var invitation = new InvitationDTO
            {
                Id = 1,
                Used = true,
                UserId = 35,
                invitationKey = "Key",
                User = deriveduser
            };
            var password = "Password";

            mediatorMock
                .Setup(x => x.Send(It.Is<GetInvitationByKeyQuery>(y => y.invitationKey == key), default))
                .Returns(Task.FromResult(invitation));



            var actionResult = await registerService.Register(user, password, key);

            Assert.False(actionResult.Suceeded);

        }
    }
}
