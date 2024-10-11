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

namespace CLIClassLibrary.RoleHandlers.RESTHandlers
{
    public partial class GuestRESTCLIHandler : RESTRoleHandlerBase
    {
        public GuestRESTCLIHandler(string restEndpoint)
            : base(restEndpoint)
        {
        
        }

        public override string Handle(string invoke, string data, string where, Int32 maxPages, String view)
        {
            if (string.IsNullOrEmpty(data)) data = "{}";
            string result = HandlerFactory(invoke, data, where, maxPages).GetAwaiter().GetResult();
            return result;
        }
    }
}