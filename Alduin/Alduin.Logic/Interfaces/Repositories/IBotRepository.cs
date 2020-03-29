using Alduin.DataAccess.Entities;
using Alduin.Shared.DTOs;
using Alduin.Shared.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
namespace Alduin.Logic.Interfaces.Repositories
{
    public interface IBotRepository : IRepository<BotEntity, BotDTO>
    {
        BotDTO[] FindAddressByAvailable(bool force);
        BotDTO FindBotByKeyUnique(string keyunique);
    }
}
