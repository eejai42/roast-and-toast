using AirtableDirect.CLI.Lib.DataClasses;
using AirtableToDotNet.APIWrapper;
using System;
using System.Linq;
using System.Collections.Generic;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Security.Claims;

namespace CLIClassLibrary.ATDMQ
{
	public partial class ATDAdmin : ATDActorBase
    {
       public ClaimsIdentity UserIdentity { get; set; } // IsUpdate: false - GameDetail.
       public IEnumerable<GameDetail> GetGameDetails(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetGameDetails(WrapAdminGetGameDetailsWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).AdminCleanForGet();
			}        
        private string WrapAdminGetGameDetailsWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - GameDetail.
       public GameDetail AddGameDetail(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.Insert(Payload.GameDetail.AdminCleanForAdd());
			}        
        private string WrapAdminAddGameDetailWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: true - GameDetail.
       public IEnumerable<GameDetail> UpdateGameDetail(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
          if (Payload.GameDetails is null)
          {
              return this.UpdateGameDetails(api, Payload);
          }
          else
          {
              var updatedGameDetail = api.Update(Payload.GameDetail.AdminCleanForUpdate());
              return new List<GameDetail>(new GameDetail[] { updatedGameDetail });
          }
        
			}        
        private string WrapAdminUpdateGameDetailWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

              
        private List<GameDetail> UpdateGameDetails(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if (!(payload.GameDetails is null) && payload.GameDetails.Any())
            {
                var updatedItems = new List<GameDetail>();
                payload.GameDetails.ForEach(item =>
                {
                    var updatedGameDetails = api.Update(item);
                    updatedItems.Add(updatedGameDetails);
                });
                return updatedItems;
            }
            else throw new Exception("Payload.Product or Payload.Products required to update products");
        }
      
 // IsUpdate: false - .
       public void DeleteGameDetail(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				api.Delete(Payload.GameDetail);
			}        
        private string WrapAdminDeleteGameDetailWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Character.
       public IEnumerable<Character> GetCharacters(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetCharacters(WrapAdminGetCharactersWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).AdminCleanForGet();
			}        
        private string WrapAdminGetCharactersWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Character.
       public Character AddCharacter(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.Insert(Payload.Character.AdminCleanForAdd());
			}        
        private string WrapAdminAddCharacterWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: true - Character.
       public IEnumerable<Character> UpdateCharacter(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
          if (Payload.Characters is null)
          {
              return this.UpdateCharacters(api, Payload);
          }
          else
          {
              var updatedCharacter = api.Update(Payload.Character.AdminCleanForUpdate());
              return new List<Character>(new Character[] { updatedCharacter });
          }
        
			}        
        private string WrapAdminUpdateCharacterWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

              
        private List<Character> UpdateCharacters(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if (!(payload.Characters is null) && payload.Characters.Any())
            {
                var updatedItems = new List<Character>();
                payload.Characters.ForEach(item =>
                {
                    var updatedCharacters = api.Update(item);
                    updatedItems.Add(updatedCharacters);
                });
                return updatedItems;
            }
            else throw new Exception("Payload.Product or Payload.Products required to update products");
        }
      
 // IsUpdate: false - .
       public void DeleteCharacter(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				api.Delete(Payload.Character);
			}        
        private string WrapAdminDeleteCharacterWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - AppUser.
       public IEnumerable<AppUser> GetAppUsers(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetAppUsers(WrapAdminGetAppUsersWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).AdminCleanForGet();
			}        
        private string WrapAdminGetAppUsersWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - AppUser.
       public AppUser AddAppUser(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.Insert(Payload.AppUser.AdminCleanForAdd());
			}        
        private string WrapAdminAddAppUserWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: true - AppUser.
       public IEnumerable<AppUser> UpdateAppUser(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
          if (Payload.AppUsers is null)
          {
              return this.UpdateAppUsers(api, Payload);
          }
          else
          {
              var updatedAppUser = api.Update(Payload.AppUser.AdminCleanForUpdate());
              return new List<AppUser>(new AppUser[] { updatedAppUser });
          }
        
			}        
        private string WrapAdminUpdateAppUserWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

              
        private List<AppUser> UpdateAppUsers(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if (!(payload.AppUsers is null) && payload.AppUsers.Any())
            {
                var updatedItems = new List<AppUser>();
                payload.AppUsers.ForEach(item =>
                {
                    var updatedAppUsers = api.Update(item);
                    updatedItems.Add(updatedAppUsers);
                });
                return updatedItems;
            }
            else throw new Exception("Payload.Product or Payload.Products required to update products");
        }
      
 // IsUpdate: false - .
       public void DeleteAppUser(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				api.Delete(Payload.AppUser);
			}        
        private string WrapAdminDeleteAppUserWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Game.
       public IEnumerable<Game> GetGames(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetGames(WrapAdminGetGamesWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).AdminCleanForGet();
			}        
        private string WrapAdminGetGamesWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Game.
       public Game AddGame(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.Insert(Payload.Game.AdminCleanForAdd());
			}        
        private string WrapAdminAddGameWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: true - Game.
       public IEnumerable<Game> UpdateGame(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
          if (Payload.Games is null)
          {
              return this.UpdateGames(api, Payload);
          }
          else
          {
              var updatedGame = api.Update(Payload.Game.AdminCleanForUpdate());
              return new List<Game>(new Game[] { updatedGame });
          }
        
			}        
        private string WrapAdminUpdateGameWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

              
        private List<Game> UpdateGames(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if (!(payload.Games is null) && payload.Games.Any())
            {
                var updatedItems = new List<Game>();
                payload.Games.ForEach(item =>
                {
                    var updatedGames = api.Update(item);
                    updatedItems.Add(updatedGames);
                });
                return updatedItems;
            }
            else throw new Exception("Payload.Product or Payload.Products required to update products");
        }
      
 // IsUpdate: false - .
       public void DeleteGame(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				api.Delete(Payload.Game);
			}        
        private string WrapAdminDeleteGameWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - WEapon.
       public IEnumerable<WEapon> GetWEapons(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetWEapons(WrapAdminGetWEaponsWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).AdminCleanForGet();
			}        
        private string WrapAdminGetWEaponsWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - WEapon.
       public WEapon AddWEapon(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.Insert(Payload.WEapon.AdminCleanForAdd());
			}        
        private string WrapAdminAddWEaponWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: true - WEapon.
       public IEnumerable<WEapon> UpdateWEapon(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
          if (Payload.WEapons is null)
          {
              return this.UpdateWEapons(api, Payload);
          }
          else
          {
              var updatedWEapon = api.Update(Payload.WEapon.AdminCleanForUpdate());
              return new List<WEapon>(new WEapon[] { updatedWEapon });
          }
        
			}        
        private string WrapAdminUpdateWEaponWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

              
        private List<WEapon> UpdateWEapons(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if (!(payload.WEapons is null) && payload.WEapons.Any())
            {
                var updatedItems = new List<WEapon>();
                payload.WEapons.ForEach(item =>
                {
                    var updatedWEapons = api.Update(item);
                    updatedItems.Add(updatedWEapons);
                });
                return updatedItems;
            }
            else throw new Exception("Payload.Product or Payload.Products required to update products");
        }
      
 // IsUpdate: false - .
       public void DeleteWEapon(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				api.Delete(Payload.WEapon);
			}        
        private string WrapAdminDeleteWEaponWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Level.
       public IEnumerable<Level> GetLevels(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetLevels(WrapAdminGetLevelsWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).AdminCleanForGet();
			}        
        private string WrapAdminGetLevelsWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: false - Level.
       public Level AddLevel(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.Insert(Payload.Level.AdminCleanForAdd());
			}        
        private string WrapAdminAddLevelWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      
 // IsUpdate: true - Level.
       public IEnumerable<Level> UpdateLevel(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
          if (Payload.Levels is null)
          {
              return this.UpdateLevels(api, Payload);
          }
          else
          {
              var updatedLevel = api.Update(Payload.Level.AdminCleanForUpdate());
              return new List<Level>(new Level[] { updatedLevel });
          }
        
			}        
        private string WrapAdminUpdateLevelWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

              
        private List<Level> UpdateLevels(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if (!(payload.Levels is null) && payload.Levels.Any())
            {
                var updatedItems = new List<Level>();
                payload.Levels.ForEach(item =>
                {
                    var updatedLevels = api.Update(item);
                    updatedItems.Add(updatedLevels);
                });
                return updatedItems;
            }
            else throw new Exception("Payload.Product or Payload.Products required to update products");
        }
      
 // IsUpdate: false - .
       public void DeleteLevel(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				api.Delete(Payload.Level);
			}        
        private string WrapAdminDeleteLevelWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }

      

    }
}
					    