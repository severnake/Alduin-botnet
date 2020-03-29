using Alduin.DataAccess.Entities.Base;

namespace Alduin.DataAccess.Entities
{
    public class BotLogEntity : EntityBase
    {
        public virtual BotEntity Bot { get; set; }
        public virtual string Message { get; set; }
        public virtual string Type { get; set; }
    }
}
