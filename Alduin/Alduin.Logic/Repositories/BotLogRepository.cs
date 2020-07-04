using Alduin.DataAccess.Entities;
using Alduin.Logic.Interfaces.Repositories;
using Alduin.Shared.DTOs;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Logic.Repositories
{
    public class BotLogRepository : RepositoryBase<BotLogEntity, BotLogDTO>, IBotLogRepository
    {
        public BotLogRepository(ISession session, IMapper mapper) : base(session, mapper)
        {
        }

        public BotLogDTO[] FindByBotId(int botid)
        {
            var result = _session.QueryOver<BotLogEntity>()
                .Where(x => x.Bot.Id == botid)
                .List();

            if (result == null)
                return null;

            var dto = _mapper.Map<BotLogDTO[]>(result);
            return dto;
        }
    }
}
