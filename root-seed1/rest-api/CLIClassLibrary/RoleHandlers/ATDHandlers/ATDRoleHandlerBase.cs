using CLIClassLibrary.AirtableAPI;
using Newtonsoft.Json;
using SSoTme.Default.Lib.CLIHandler;
using System;
using System.Collections.Generic;
using System.Text;
using YP.SassyMQ.Lib.RabbitMQ;

namespace CLIClassLibrary.RoleHandlers.ATDHandlers
{   
    public abstract class ATDRoleHandlerBase<T> : RoleHandlerBase
        where T : ATDActorBase
    {
        private T _atdActor;

        public T ATDActor
        {
            get
            {
                if (_atdActor is null)
                {
                    _atdActor = Activator.CreateInstance(typeof(T)) as T;
                }
                return _atdActor;
            }
        }

        public ATDRoleHandlerBase(string accessToken)
        {
            AccessToken = accessToken;
        }

        protected ATDRoleHandlerBase(ATDKey atdKey)
        {
            ATDKey = atdKey;
        }

        public string AccessToken { get; }
        public ATDKey ATDKey { get; }
    }
}
