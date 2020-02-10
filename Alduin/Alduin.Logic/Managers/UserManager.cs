using AutoMapper;
using NHibernate;
using Alduin.DataAccess.Entities;
using Alduin.Logic.Interfaces.Managers;
using Alduin.Shared.DTOs;
using Alduin.Shared.Interfaces.UnitOfWork;
using Alduin.Shared.Transaction;

namespace Alduin.Logic.Managers
{
    public class UserManager : ManagerBase<UserEntity, UserDTO>, IUserManager
    {
        public UserManager(ISession session, IMapper mapper, IUnitOfWork unitOfWork) : base(session, mapper, unitOfWork)
        { }
        protected override TransactionResult ValidateSaving(UserEntity entity)
        {
            var result = base.ValidateSaving(entity);

            if (string.IsNullOrEmpty(entity.Email))
            {
                result.ErrorMessages.Add(new TransactionErrorMessage
                {
                    IsPublic = true,
                    Message = "Email is required!"
                });
                result.Succeeded = false;
            }

            if (string.IsNullOrEmpty(entity.Name))
            {
                result.ErrorMessages.Add(new TransactionErrorMessage
                {
                    IsPublic = true,
                    Message = "Name is required!"
                });
                result.Succeeded = false;
            }

            return result;
        }
    }
}
