using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using AirtableDirect.CLI.Lib.DataClasses;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using CLIClassLibrary.AirtableAPI;
using static SSoT.me.AirtableToDotNetLib.AirtableExtensions;

namespace CLIClassLibrary.RoleHandlers.ATDHandlers
{
    public partial class AdminCLIHandler
    {
        public override void AddHelp(StringBuilder sb, string helpTerm)
        {
            sb.AppendLine($"Help for Admin.");
            
            helpTerm = helpTerm.ToLower();
            var found = helpTerm == "general";
            
            if (helpTerm == "general")
            {
                sb.AppendLine();
                
                sb.AppendLine($"Game: GetGames");
                sb.AppendLine($"Game: AddGame");
                sb.AppendLine($"Game: UpdateGame");
                sb.AppendLine($"void: DeleteGame");
                sb.AppendLine($"GameDetail: GetGameDetails");
                sb.AppendLine($"GameDetail: AddGameDetail");
                sb.AppendLine($"GameDetail: UpdateGameDetail");
                sb.AppendLine($"void: DeleteGameDetail");
                sb.AppendLine($"Character: GetCharacters");
                sb.AppendLine($"Character: AddCharacter");
                sb.AppendLine($"Character: UpdateCharacter");
                sb.AppendLine($"void: DeleteCharacter");
                sb.AppendLine($"AppUser: GetAppUsers");
                sb.AppendLine($"AppUser: AddAppUser");
                sb.AppendLine($"AppUser: UpdateAppUser");
                sb.AppendLine($"void: DeleteAppUser");                                            
            }
            
            sb.AppendLine($"{Environment.NewLine}Available Actions Matching: {helpTerm}");
            
            if ("getgames".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - GetGames");
                if ("getgames".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintGetGamesHelp(sb);
                }
                found = true;
            }
            if ("addgame".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - AddGame");
                if ("addgame".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintAddGameHelp(sb);
                }
                found = true;
            }
            if ("updategame".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - UpdateGame");
                if ("updategame".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintUpdateGameHelp(sb);
                }
                found = true;
            }
            if ("deletegame".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - DeleteGame");
                if ("deletegame".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintDeleteGameHelp(sb);
                }
                found = true;
            }
            if ("getgamedetails".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - GetGameDetails");
                if ("getgamedetails".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintGetGameDetailsHelp(sb);
                }
                found = true;
            }
            if ("addgamedetail".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - AddGameDetail");
                if ("addgamedetail".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintAddGameDetailHelp(sb);
                }
                found = true;
            }
            if ("updategamedetail".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - UpdateGameDetail");
                if ("updategamedetail".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintUpdateGameDetailHelp(sb);
                }
                found = true;
            }
            if ("deletegamedetail".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - DeleteGameDetail");
                if ("deletegamedetail".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintDeleteGameDetailHelp(sb);
                }
                found = true;
            }
            if ("getcharacters".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - GetCharacters");
                if ("getcharacters".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintGetCharactersHelp(sb);
                }
                found = true;
            }
            if ("addcharacter".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - AddCharacter");
                if ("addcharacter".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintAddCharacterHelp(sb);
                }
                found = true;
            }
            if ("updatecharacter".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - UpdateCharacter");
                if ("updatecharacter".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintUpdateCharacterHelp(sb);
                }
                found = true;
            }
            if ("deletecharacter".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - DeleteCharacter");
                if ("deletecharacter".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintDeleteCharacterHelp(sb);
                }
                found = true;
            }
            if ("getappusers".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - GetAppUsers");
                if ("getappusers".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintGetAppUsersHelp(sb);
                }
                found = true;
            }
            if ("addappuser".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - AddAppUser");
                if ("addappuser".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintAddAppUserHelp(sb);
                }
                found = true;
            }
            if ("updateappuser".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - UpdateAppUser");
                if ("updateappuser".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintUpdateAppUserHelp(sb);
                }
                found = true;
            }
            if ("deleteappuser".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - DeleteAppUser");
                if ("deleteappuser".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.PrintDeleteAppUserHelp(sb);
                }
                found = true;
            }
                       
            if (!found)
            {
                sb.AppendLine();
                sb.AppendLine($"{Environment.NewLine}UNABLE TO FIND COMMAND: {helpTerm} not found.");
            }
        }

        private string HandlerFactory(string invokeRequest, string payloadString, string where, Int32 maxPages)
        {
            var result = "";
            var payload = JsonConvert.DeserializeObject<StandardPayload>(payloadString, new JsonSerializerSettings() { 
                    FloatParseHandling = FloatParseHandling.Decimal, 
                    ContractResolver = new CustomContractResolver<StandardPayload>()
            });
            payload.SetActor(this.ATDActor);
            payload.AccessToken = this.ATDActor.AccessToken;
            payload.ApiKey = AirtableKey.CurrentKey.APIKey;
            payload.BaseId = StageInfo.CurrentStage.BaseID;
            payload.AirtableWhere = where;
            payload.MaxPages = maxPages < 1 ? 5 : maxPages;
            Object reply = new Object();
			
            switch (invokeRequest.ToLower())
            {
                case "getgames":
				    reply = this.ATDActor.GetGames(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "addgame":
				    reply = this.ATDActor.AddGame(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "updategame":
				    reply = this.ATDActor.UpdateGame(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "deletegame":
				    this.ATDActor.DeleteGame(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                case "getgamedetails":
				    reply = this.ATDActor.GetGameDetails(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "addgamedetail":
				    reply = this.ATDActor.AddGameDetail(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "updategamedetail":
				    reply = this.ATDActor.UpdateGameDetail(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "deletegamedetail":
				    this.ATDActor.DeleteGameDetail(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                case "getcharacters":
				    reply = this.ATDActor.GetCharacters(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "addcharacter":
				    reply = this.ATDActor.AddCharacter(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "updatecharacter":
				    reply = this.ATDActor.UpdateCharacter(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "deletecharacter":
				    this.ATDActor.DeleteCharacter(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                case "getappusers":
				    reply = this.ATDActor.GetAppUsers(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "addappuser":
				    reply = this.ATDActor.AddAppUser(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "updateappuser":
				    reply = this.ATDActor.UpdateAppUser(payload);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);
                    break;                   

                case "deleteappuser":
				    this.ATDActor.DeleteAppUser(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    break;                   

                default:
                    throw new Exception($"Invalid request: {invokeRequest}");
            }

            return result;
        }
        
        
        public void PrintGetGamesHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: Game     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - GameId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - LoadingScreen");
                    sb.AppendLine($"CRUD      - Notes");
                    sb.AppendLine($"CRUD      - IsActive");
                    sb.AppendLine($"CRUD      - GameDetails");
                    sb.AppendLine($"CRUD      - Characters");
                    sb.AppendLine($"CRUD      - IsDisabled");
                
            
        }
        
        public void PrintAddGameHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: Game     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - GameId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - LoadingScreen");
                    sb.AppendLine($"CRUD      - Notes");
                    sb.AppendLine($"CRUD      - IsActive");
                    sb.AppendLine($"CRUD      - GameDetails");
                    sb.AppendLine($"CRUD      - Characters");
                    sb.AppendLine($"CRUD      - IsDisabled");
                
            
        }
        
        public void PrintUpdateGameHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: Game     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - GameId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - LoadingScreen");
                    sb.AppendLine($"CRUD      - Notes");
                    sb.AppendLine($"CRUD      - IsActive");
                    sb.AppendLine($"CRUD      - GameDetails");
                    sb.AppendLine($"CRUD      - Characters");
                    sb.AppendLine($"CRUD      - IsDisabled");
                
            
        }
        
        public void PrintDeleteGameHelp(StringBuilder sb)
        {
            
        }
        
        public void PrintGetGameDetailsHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: GameDetail     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - GameDetailId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - Value");
                    sb.AppendLine($"CRUD      - GameLoadingScreen");
                    sb.AppendLine($"CRUD      - Game");
                    sb.AppendLine($"CRUD      - GameIsActive");
                    sb.AppendLine($"CRUD      - GameIsDisabled");
                
            
        }
        
        public void PrintAddGameDetailHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: GameDetail     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - GameDetailId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - Value");
                    sb.AppendLine($"CRUD      - GameLoadingScreen");
                    sb.AppendLine($"CRUD      - Game");
                    sb.AppendLine($"CRUD      - GameIsActive");
                    sb.AppendLine($"CRUD      - GameIsDisabled");
                
            
        }
        
        public void PrintUpdateGameDetailHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: GameDetail     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - GameDetailId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - Value");
                    sb.AppendLine($"CRUD      - GameLoadingScreen");
                    sb.AppendLine($"CRUD      - Game");
                    sb.AppendLine($"CRUD      - GameIsActive");
                    sb.AppendLine($"CRUD      - GameIsDisabled");
                
            
        }
        
        public void PrintDeleteGameDetailHelp(StringBuilder sb)
        {
            
        }
        
        public void PrintGetCharactersHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: Character     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - CharacterId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - Avatar");
                    sb.AppendLine($"CRUD      - Type");
                    sb.AppendLine($"CRUD      - IsMissing");
                    sb.AppendLine($"CRUD      - IntroducedAtLevel");
                    sb.AppendLine($"CRUD      - Description");
                    sb.AppendLine($"CRUD      - Skills");
                    sb.AppendLine($"CRUD      - Game");
                    sb.AppendLine($"CRUD      - GameIsActive");
                
            
        }
        
        public void PrintAddCharacterHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: Character     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - CharacterId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - Avatar");
                    sb.AppendLine($"CRUD      - Type");
                    sb.AppendLine($"CRUD      - IsMissing");
                    sb.AppendLine($"CRUD      - IntroducedAtLevel");
                    sb.AppendLine($"CRUD      - Description");
                    sb.AppendLine($"CRUD      - Skills");
                    sb.AppendLine($"CRUD      - Game");
                    sb.AppendLine($"CRUD      - GameIsActive");
                
            
        }
        
        public void PrintUpdateCharacterHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: Character     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - CharacterId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - Avatar");
                    sb.AppendLine($"CRUD      - Type");
                    sb.AppendLine($"CRUD      - IsMissing");
                    sb.AppendLine($"CRUD      - IntroducedAtLevel");
                    sb.AppendLine($"CRUD      - Description");
                    sb.AppendLine($"CRUD      - Skills");
                    sb.AppendLine($"CRUD      - Game");
                    sb.AppendLine($"CRUD      - GameIsActive");
                
            
        }
        
        public void PrintDeleteCharacterHelp(StringBuilder sb)
        {
            
        }
        
        public void PrintGetAppUsersHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: AppUser     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - AppUserId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - EmailAddress");
                    sb.AppendLine($"CRUD      - Roles");
                    sb.AppendLine($"CRUD      - Notes");
                
            
        }
        
        public void PrintAddAppUserHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: AppUser     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - AppUserId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - EmailAddress");
                    sb.AppendLine($"CRUD      - Roles");
                    sb.AppendLine($"CRUD      - Notes");
                
            
        }
        
        public void PrintUpdateAppUserHelp(StringBuilder sb)
        {
            
                
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: AppUser     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                
                    sb.AppendLine($"CRUD      - AppUserId");
                    sb.AppendLine($"CRUD      - Name");
                    sb.AppendLine($"CRUD      - EmailAddress");
                    sb.AppendLine($"CRUD      - Roles");
                    sb.AppendLine($"CRUD      - Notes");
                
            
        }
        
        public void PrintDeleteAppUserHelp(StringBuilder sb)
        {
            
        }
        

    }
}
