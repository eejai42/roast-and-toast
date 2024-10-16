using CLIClassLibrary.AirtableAPI; 
using Newtonsoft.Json;
using static SSoT.me.AirtableToDotNetLib.AirtableExtensions;

namespace YP.SassyMQ.Lib.RabbitMQ
{
    public class ATDActorBase
    {
        public string EmailAddress { get; set; }
        static ATDActorBase()
        {
            AirtableKey.CurrentKey = new AirtableKey()
            {
                EmailAddress = "your-email-address@domain.com"
            };

            StageInfo.CurrentStage = new StageInfo()
            {
                ApiKeyName = "your-email-address@domain.com",
                BaseID = "appmrdAZppf3BM8LL",
                Stage = "develop"
            };
        }

        public ATDActorBase()
        {

        }
        public string AccessToken { get; set; }

        public StandardPayload CreatePayload(string payloadString = "{}", string airtableWhere = "", string view = null, int maxPages = 5)
        {
            var payload = JsonConvert.DeserializeObject<StandardPayload>(payloadString, new JsonSerializerSettings()
            {
                FloatParseHandling = FloatParseHandling.Decimal,
                ContractResolver = new CustomContractResolver<StandardPayload>()
            });
            payload.SetActor(this);
            payload.AccessToken = this.AccessToken;
            payload.View = view;
            payload.ApiKey = AirtableKey.CurrentKey?.APIKey;
            payload.BaseId = StageInfo.CurrentStage?.BaseID;
            payload.AirtableWhere = airtableWhere;
            payload.MaxPages = maxPages < 1 ? 5 : maxPages;
            return payload;
        }
    }
}