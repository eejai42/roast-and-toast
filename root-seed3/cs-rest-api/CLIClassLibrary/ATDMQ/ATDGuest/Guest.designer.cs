using AirtableDirect.CLI.Lib.DataClasses;
using AirtableToDotNet.APIWrapper;
using System;
using System.Linq;
using System.Collections.Generic;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Security.Claims;

namespace CLIClassLibrary.ATDMQ
{
	public partial class ATDGuest : ATDActorBase
    {
       public ClaimsIdentity UserIdentity { get; set; } // IsUpdate: false - .
       public void RequestToken(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
			}        
        private string WrapGuestRequestTokenWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - .
       public void ValidateTemporaryAccessToken(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
			}        
        private string WrapGuestValidateTemporaryAccessTokenWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - .
       public void WhoAmI(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
			}        
        private string WrapGuestWhoAmIWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - .
       public void WhoAreYou(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
			}        
        private string WrapGuestWhoAreYouWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - GameDetail.
       public IEnumerable<GameDetail> GetGameDetails(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetGameDetails(WrapGuestGetGameDetailsWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).GuestCleanForGet();
			}        
        private string WrapGuestGetGameDetailsWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Character.
       public IEnumerable<Character> GetCharacters(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetCharacters(WrapGuestGetCharactersWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).GuestCleanForGet();
			}        
        private string WrapGuestGetCharactersWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - AppUser.
       public IEnumerable<AppUser> GetAppUsers(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetAppUsers(WrapGuestGetAppUsersWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).GuestCleanForGet();
			}        
        private string WrapGuestGetAppUsersWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Game.
       public IEnumerable<Game> GetGames(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetGames(WrapGuestGetGamesWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).GuestCleanForGet();
			}        
        private string WrapGuestGetGamesWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - WEapon.
       public IEnumerable<WEapon> GetWEapons(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetWEapons(WrapGuestGetWEaponsWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).GuestCleanForGet();
			}        
        private string WrapGuestGetWEaponsWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Level.
       public IEnumerable<Level> GetLevels(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetLevels(WrapGuestGetLevelsWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).GuestCleanForGet();
			}        
        private string WrapGuestGetLevelsWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      

    }
}
					    