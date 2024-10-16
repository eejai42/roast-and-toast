using Newtonsoft.Json;
using AirtableDirect.CLI.Lib.DataClasses;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Text;
using System;
using AirtableToDotNet.APIWrapper;
using System.Collections.Generic;
using System.Threading;
using CLIClassLibrary.ATDMQ;
using CLIClassLibrary.AirtableAPI;

namespace CLIClassLibrary.RoleHandlers.ATDHandlers
{
    public partial class GuestCLIHandler : ATDRoleHandlerBase<ATDGuest>
    {

        public GuestCLIHandler(ATDKey atdKey)
            : base(atdKey)
        {
        }

        public override string Handle(string invoke, string data, string where, Int32 maxPages, String view)
        {
            if (string.IsNullOrEmpty(data)) data = "{}";
            string result = HandlerFactory(invoke, data, where, maxPages);
            return result;
        }
    }
}