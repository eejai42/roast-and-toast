
using AirtableDirect.CLI.Lib.DataClasses;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace YP.SassyMQ.Lib.RabbitMQ
{
    public partial class StandardPayload
    {
        public const int DEFAULT_TIMEOUT = 30;
        private ATDActorBase actor;
        private bool v;

        public StandardPayload()
            : this(default(ATDActorBase))
        {

        }

        public StandardPayload(ATDActorBase actor)
            : this(actor, String.Empty)
        {

        }
        
        public StandardPayload(ATDActorBase actor, string content)
            : this(actor, content, true)
        {
        }

        public StandardPayload(ATDActorBase actor, string content, bool v)
        {
            this.actor = actor;
            Content = content;
            this.v = v;
        }

        public string PayloadId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string AccessToken { get; set; }
        public string Content { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsHandled { get; set; }
        

        private ATDActorBase __Actor { get; set; }
        private bool TimedOutWaiting { get; set; }
        internal StandardPayload ReplyPayload { get; set; }
        private BasicDeliverEventArgs ReplyBDEA { get; set; }
        private bool ReplyRecieved { get; set; }
        
        public DateTime OnlineSince { get; set; }
        public string EmailAddress { get; set; }
        public string DemoPassword { get; set; }
        public string AirtableWhere { get; set; }
        public object SingletonAppUser { get; set; }
        public string WhoAreYouRelativeUrl { get; set; }
        public string WhoAreYouTextFileContents { get; set; }
        public byte[] WhoAreYouBinaryFileContents { get; set; }
        public int MaxPages { get; set; }
        public string View { get; set; }
        public string ApiKey { get; set; }
        public string BaseId { get; set; }

        public void SetActor(ATDActorBase actor) 
        {
            this.__Actor = actor;
        }

        internal bool HasNoErrors(BasicDeliverEventArgs bdea)
        {
            if (!String.IsNullOrEmpty(this.ErrorMessage)) return false;
            else return true;
        }
    }
}