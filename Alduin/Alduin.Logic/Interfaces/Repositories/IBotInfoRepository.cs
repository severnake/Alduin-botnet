using Alduin.DataAccess.Entities;
using Alduin.Shared.DTOs;
using Alduin.Shared.Interfaces.Repositories;

namespace Alduin.Logic.Interfaces.Repositories
{
    public interface IBotInfoRepository : IRepository<BotInfoEntity, BotInfoDTO>
    {
        BotInfoDTO[] FindBotInfoByBotId(int BotId);
    }
}
