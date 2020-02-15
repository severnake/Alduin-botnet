using Alduin.DataAccess.Entities;
using Alduin.Logic.Interfaces.Repositories;
using Alduin.Shared.DTOs;
using AutoMapper;
using NHibernate;
using System;

namespace Alduin.Logic.Repositories
{
    public class BotRepository : RepositoryBase<BotEntity, BotDTO>, IBotRepository
    {
        public BotRepository(ISession session, IMapper mapper) : base(session, mapper)
        {
            
        }
        public BotDTO[] FindAddressByAvailable(bool force)
        {
            var query = _session.QueryOver<BotEntity>();
            DateTime DateNowUTC = DateTime.UtcNow;
            DateNowUTC = DateNowUTC.AddMinutes(-5);

            if (!force)
            {
               query = query.Where(x => x.LastLoggedInUTC >= DateNowUTC);
            }
            var entities = query.List();
            var dto = _mapper.Map<BotDTO[]>(entities);
            return dto;
        }
    }
}
