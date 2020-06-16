using Alduin.DataAccess.Entities;
using Alduin.Logic.Interfaces.Repositories;
using Alduin.Shared.DTOs;
using AutoMapper;
using NHibernate;

namespace Alduin.Logic.Repositories
{
    public class BotInfoRepository : RepositoryBase<BotInfoEntity, BotInfoDTO>, IBotInfoRepository
    {
        public BotInfoRepository(ISession session, IMapper mapper) : base(session, mapper)
        {

        }

        public BotInfoDTO[] FindBotInfoByBotId(int BotId)
        {
            var result = _session.QueryOver<BotInfoEntity>()
                .Where(x => x.Bot.Id == BotId)
                .List();

            if (result == null)
                return null;

            var dto = _mapper.Map<BotInfoDTO[]>(result);
            return dto;
        }
    }
}
