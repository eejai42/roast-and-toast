using CLIClassLibrary.AirtableAPI;
using SSoTme.Default.Lib.CLIHandler;
using System;
using System.Collections.Generic;
using System.Text;
using CLIClassLibrary.RoleHandlers.ATDHandlers;
using CLIClassLibrary.RoleHandlers.RESTHandlers;

namespace CLIClassLibrary.RoleHandlers
{
    public static partial class RoleHandlerFactory
    {
        public static RoleHandlerBase CreateHandler(string runAs, string restEndpoint)
        {        
            if (String.IsNullOrEmpty(runAs)) runAs = EAPICLIHandler.GetCurrentUserRoleName();
            if (String.IsNullOrEmpty(restEndpoint)) return RoleHandlerFactory.CallATD(runAs);
            else return RoleHandlerFactory.InvokeRest(runAs, restEndpoint);
        }
        
        public static RoleHandlerBase InvokeRest(string runAs, string restEndpoint)
        {
            switch (runAs.ToLower())
            {
                
                    
                case "guest":
                    return new GuestRESTCLIHandler(restEndpoint);

                    
                case "admin":
                    return new AdminRESTCLIHandler(restEndpoint);

                    
                case "player":
                    return new PlayerRESTCLIHandler(restEndpoint);

                    
                case "npc":
                    return new NPCRESTCLIHandler(restEndpoint);


                default:
                    throw new Exception($"Can't find CLIHandler for {runAs} actor.");
            }
        }
        
        public static RoleHandlerBase CallATD(string runAs) {
            ATDKey.ProjectName = "appmrdAZppf3BM8LL";
            var atdKey = EAPICLIHandler.GetCurrentKey(runAs);
            switch (runAs.ToLower())
            {
                
                    
                case "guest":
                    return new GuestCLIHandler(atdKey);

                    
                case "admin":
                    return new AdminCLIHandler(atdKey);

                    
                case "player":
                    return new PlayerCLIHandler(atdKey);

                    
                case "npc":
                    return new NPCCLIHandler(atdKey);


                default:
                    throw new Exception($"Can't find CLIHandler for {runAs} actor.");
            }
        }

        public static void ListRoles(StringBuilder sbHelpBuilder)
        {
           
            sbHelpBuilder.AppendLine(" - Guest");
            sbHelpBuilder.AppendLine(" - Admin");
            sbHelpBuilder.AppendLine(" - Player");
            sbHelpBuilder.AppendLine(" - NPC");
        }
    }
}
