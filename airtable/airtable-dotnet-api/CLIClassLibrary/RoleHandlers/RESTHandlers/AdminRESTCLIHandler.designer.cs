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
using System.Net.Http;

namespace CLIClassLibrary.RoleHandlers.RESTHandlers
{
    public partial class AdminRESTCLIHandler
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

        private async Task<string> HandlerFactory(string invokeRequest, string payloadString, string where, Int32 maxPages)
        {
            string result = null;
            var payload = JsonConvert.DeserializeObject<StandardPayload>(payloadString, new JsonSerializerSettings() { 
                    FloatParseHandling = FloatParseHandling.Decimal, 
                    ContractResolver = new CustomContractResolver<StandardPayload>()
            });
            payload.AirtableWhere = where;
            payload.MaxPages = maxPages < 1 ? 5 : maxPages;
			
            switch (invokeRequest.ToLower())
            {
                
                
                case "getgames":
                    // Game
                    result = await this.GETRequest("Games", payload);
                    break;

                    
                
                
                case "addgame":
                    // Game
                    result = await this.GETRequest("Games", payload);
                    break;

                    
                
                
                case "updategame":
                    // Game
                    result = await this.GETRequest("Games", payload);
                    break;

                    
                
                
                case "deletegame":
                    break;

                    
                
                
                case "getgamedetails":
                    // GameDetail
                    result = await this.GETRequest("GameDetails", payload);
                    break;

                    
                
                
                case "addgamedetail":
                    // GameDetail
                    result = await this.GETRequest("GameDetails", payload);
                    break;

                    
                
                
                case "updategamedetail":
                    // GameDetail
                    result = await this.GETRequest("GameDetails", payload);
                    break;

                    
                
                
                case "deletegamedetail":
                    break;

                    
                
                
                case "getcharacters":
                    // Character
                    result = await this.GETRequest("Characters", payload);
                    break;

                    
                
                
                case "addcharacter":
                    // Character
                    result = await this.GETRequest("Characters", payload);
                    break;

                    
                
                
                case "updatecharacter":
                    // Character
                    result = await this.GETRequest("Characters", payload);
                    break;

                    
                
                
                case "deletecharacter":
                    break;

                    
                
                
                case "getappusers":
                    // AppUser
                    result = await this.GETRequest("AppUsers", payload);
                    break;

                    
                
                
                case "addappuser":
                    // AppUser
                    result = await this.GETRequest("AppUsers", payload);
                    break;

                    
                
                
                case "updateappuser":
                    // AppUser
                    result = await this.GETRequest("AppUsers", payload);
                    break;

                    
                
                
                case "deleteappuser":
                    break;

                    
                default:
                    throw new Exception($"Invalid request: {invokeRequest}");
            }

            return result;
        }
        
        private async Task<string> POSTRequest(string methodName, StandardPayload payload)
        {
            var role = "Admin";
            var client = new HttpClient();
            var requestUrl = $"{this.RestEndpoint}/{role}/{methodName}";

            // Serialize the payload to JSON
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await Client.PostAsync(requestUrl, httpContent);

            return await response.Content.ReadAsStringAsync();
        }
        
        private async Task<string> GETRequest(string pluralName, StandardPayload payload)
        {
            var role = "Admin";
            var requestUrl = $"{this.RestEndpoint}/{role}/{pluralName}";
            this.AddBearerToken();
            var results = await Client.GetStringAsync(requestUrl);

            return results;
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
                    sb.AppendLine($"CRUD      - Sd");
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
                    sb.AppendLine($"CRUD      - Sd");
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
                    sb.AppendLine($"CRUD      - Sd");
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
