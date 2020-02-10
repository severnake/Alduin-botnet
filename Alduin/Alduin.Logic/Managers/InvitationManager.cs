using Alduin.DataAccess.Entities;
using Alduin.Logic.Interfaces.Managers;
using Alduin.Shared.DTOs;
using Alduin.Shared.Interfaces.UnitOfWork;
using Alduin.Shared.Transaction;
using AutoMapper;
using NHibernate;

namespace Alduin.Logic.Managers
{
    public class InvitationManager : ManagerBase<InvitationEntity, InvitationDTO>, IInvitationManager
    {
        public InvitationManager(ISession session, IMapper mapper, IUnitOfWork unitOfWork) : base(session, mapper, unitOfWork)
        {}
        protected override TransactionResult ValidateSaving(InvitationEntity entity)
        {
            var result = base.ValidateSaving(entity);

            if (string.IsNullOrEmpty(entity.invitationKey))
            {
                result.ErrorMessages.Add(new TransactionErrorMessage
                {
                    IsPublic = true,
                    Message = "Invitation Key is required!"
                });
                result.Succeeded = false;
            }
            if (entity.Used)
            {
                result.ErrorMessages.Add(new TransactionErrorMessage
                {
                    IsPublic = true,
                    Message = "Key is Used!"
                });
                result.Succeeded = false;
            }
            return result;
        }
    }
}
