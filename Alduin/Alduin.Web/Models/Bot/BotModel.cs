using Alduin.Web.Models.Bot;
using Alduin.Web.Models.Commands.Commands;

namespace Alduin.Web.Models
{
    public class BotModel
    {
        public GetImgJsonModel newImagesJsonModel{get; set;}
        public BotDeatilsInquiryModel newBotDeatilsInquiryModel { get; set; }
        public BotDeatilsModel[] newBotDeatilsModel { get; set; }
        public string getImagesStatus { get; set; }
    }
}
