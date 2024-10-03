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
            if ((payload.GameDetail is null) && !(payload.GameDetails is null)) payload.GameDetails = new List<GameDetail>() { payload.GameDetail};
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
            else throw new Exception("Updating GameDetails requeres either payload.GameDetail or payload.GameDetails to be non-null values.");
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
            if ((payload.Character is null) && !(payload.Characters is null)) payload.Characters = new List<Character>() { payload.Character};
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
            else throw new Exception("Updating Characters requeres either payload.Character or payload.Characters to be non-null values.");
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
            if ((payload.AppUser is null) && !(payload.AppUsers is null)) payload.AppUsers = new List<AppUser>() { payload.AppUser};
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
            else throw new Exception("Updating AppUsers requeres either payload.AppUser or payload.AppUsers to be non-null values.");
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
            if ((payload.Game is null) && !(payload.Games is null)) payload.Games = new List<Game>() { payload.Game};
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
            else throw new Exception("Updating Games requeres either payload.Game or payload.Games to be non-null values.");
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
      
 // IsUpdate: false - Weapon.
       public IEnumerable<Weapon> GetWeapons(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.GetWeapons(WrapAdminGetWeaponsWhere(Payload.AirtableWhere), Payload.View, Payload.MaxPages).AdminCleanForGet();
			}        
        private string WrapAdminGetWeaponsWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }
      
 // IsUpdate: false - Weapon.
       public Weapon AddWeapon(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				return api.Insert(Payload.Weapon.AdminCleanForAdd());
			}        
        private string WrapAdminAddWeaponWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }
      
 // IsUpdate: true - Weapon.
       public IEnumerable<Weapon> UpdateWeapon(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				
          if (Payload.Weapons is null)
          {
              return this.UpdateWeapons(api, Payload);
          }
          else
          {
              var updatedWeapon = api.Update(Payload.Weapon.AdminCleanForUpdate());
              return new List<Weapon>(new Weapon[] { updatedWeapon });
          }
        
			}        
        private string WrapAdminUpdateWeaponWhere(string airtableWhere)
        {
                
        
        
            // AirtableWhere: 
            return airtableWhere; 
        }
      
        private List<Weapon> UpdateWeapons(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if ((payload.Weapon is null) && !(payload.Weapons is null)) payload.Weapons = new List<Weapon>() { payload.Weapon};
            if (!(payload.Weapons is null) && payload.Weapons.Any())
            {
                var updatedItems = new List<Weapon>();
                payload.Weapons.ForEach(item =>
                {
                    var updatedWeapons = api.Update(item);
                    updatedItems.Add(updatedWeapons);
                });
                return updatedItems;
            }
            else throw new Exception("Updating Weapons requeres either payload.Weapon or payload.Weapons to be non-null values.");
        }
    
 // IsUpdate: false - .
       public void DeleteWeapon(StandardPayload Payload) {
			
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				api.Delete(Payload.Weapon);
			}        
        private string WrapAdminDeleteWeaponWhere(string airtableWhere)
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
            if ((payload.Level is null) && !(payload.Levels is null)) payload.Levels = new List<Level>() { payload.Level};
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
            else throw new Exception("Updating Levels requeres either payload.Level or payload.Levels to be non-null values.");
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
					    