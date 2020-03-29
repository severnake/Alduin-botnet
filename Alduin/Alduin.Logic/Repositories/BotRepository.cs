using Alduin.DataAccess.Entities;
using Alduin.Logic.Interfaces.Repositories;
using Alduin.Shared.DTOs;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alduin.Logic.Repositories
{
    public class BotRepository : RepositoryBase<BotEntity, BotDTO>, IBotRepository
    {
        public BotRepository(ISession session, IMapper mapper) : base(session, mapper)
        {
            
        }
        public BotDTO[] FindAddressByAvailable(bool force)
        {
            var result = _session.QueryOver<BotEntity>();
            DateTime DateNowUTC = DateTime.UtcNow;
            DateNowUTC = DateNowUTC.AddMinutes(-5);

            if (!force)
            {
                result = result.Where(x => x.LastLoggedInUTC >= DateNowUTC);
            }
            var entities = result.List();
            var dto = _mapper.Map<BotDTO[]>(entities);
            return dto;
        }

        public BotDTO FindBotByKeyUnique(string keyunique)
        {
            var result = _session.QueryOver<BotEntity>()
                .Where(x => x.KeyUnique == keyunique)
                .List()
                .FirstOrDefault();

            if (result == null)
                return null;

            var dto = _mapper.Map<BotDTO>(result);
            return dto;
        }
    }
}
