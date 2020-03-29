
using Alduin.DataAccess.Entities;
using Alduin.Logic.Interfaces.Managers;
using Alduin.Shared.DTOs;
using Alduin.Shared.Interfaces.UnitOfWork;
using AutoMapper;
using NHibernate;
namespace Alduin.Logic.Managers
{
    public class BotLogManager : ManagerBase<BotLogEntity, BotLogDTO>, IBotLogManager
    {
        public BotLogManager(ISession session, IMapper mapper, IUnitOfWork unitOfWork) : base(session, mapper, unitOfWork)
        {
        }
    }
}
