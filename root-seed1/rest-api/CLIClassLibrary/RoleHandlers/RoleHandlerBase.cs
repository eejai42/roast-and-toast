using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YP.SassyMQ.Lib.RabbitMQ;

namespace CLIClassLibrary.RoleHandlers
{
    public abstract class RoleHandlerBase
    {
        public abstract string Handle(string invoke, string data, string where, int maxPages, string view);

        protected string SerializePayload(StandardPayload reply)
        {
            return JsonConvert.SerializeObject(reply, Formatting.Indented, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public abstract void AddHelp(StringBuilder sb, string helpTerm);
    }
}
