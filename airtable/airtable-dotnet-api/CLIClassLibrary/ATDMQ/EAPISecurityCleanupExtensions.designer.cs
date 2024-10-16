using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirtableDirect.CLI.Lib.DataClasses;
using dc=AirtableDirect.CLI.Lib.DataClasses;



namespace CLIClassLibrary.ATDMQ
{
    public static partial class EAPISecurityCleanupExtensions
    {
    
        // Guest Cleaning Extension Methods.  --
        
        // Guest Cleaning Extension Methods.  --
        
        // Guest Cleaning Extension Methods.  --
        
        // Guest Cleaning Extension Methods.  --
        
        // Guest Cleaning Extension Methods.  --
        
        // Guest Cleaning Extension Methods.  --
        
        // Admin Cleaning Extension Methods.  -CRUD-
        
        // Game
        public static dc.Game AdminCleanForAdd(this dc.Game cleanGame)
        {
            var AdminGame = default(dc.Game);

            if (!ReferenceEquals(cleanGame, null))
            {
                AdminGame = new dc.Game()
                {
                     // default value: . 
                    GameId = cleanGame.GameId,
                     // default value: . 
                    Name = cleanGame.Name,
                     // default value: . 
                    LoadingScreen = cleanGame.LoadingScreen,
                     // default value: . 
                    Notes = cleanGame.Notes,
                     // default value: . 
                    IsActive = cleanGame.IsActive,
                     // default value: . 
                    GameDetails = cleanGame.GameDetails,
                     // default value: . 
                    Characters = cleanGame.Characters,
                     // default value: . 
                    IsDisabled = cleanGame.IsDisabled
                };
                
            }

            return AdminGame;
        }
        
        
        public static List<dc.Game> AdminCleanForGet(this IEnumerable<dc.Game> cleanGames)
        {
            return cleanGames.Select(Game => Game.AdminCleanForGet())
                            .ToList();
        }
        
        public static dc.Game AdminCleanForGet(this dc.Game cleanGame)
        {
            var AdminGame = default(dc.Game);

            if (!ReferenceEquals(cleanGame, null))
            {
                AdminGame = new dc.Game()
                {
                    GameId = cleanGame.GameId,
                    Name = cleanGame.Name,
                    LoadingScreen = cleanGame.LoadingScreen,
                    Notes = cleanGame.Notes,
                    IsActive = cleanGame.IsActive,
                    GameDetails = cleanGame.GameDetails,
                    Characters = cleanGame.Characters,
                    IsDisabled = cleanGame.IsDisabled
                };
            }

            return AdminGame;
        }
        
        
        public static dc.Game AdminCleanForUpdate(this dc.Game cleanGame)
        {
            var AdminGame = default(dc.Game);

            if (!ReferenceEquals(cleanGame, null))
            {
                AdminGame = new dc.Game()
                {
                    GameId = cleanGame.GameId,
                    Name = cleanGame.Name,
                    LoadingScreen = cleanGame.LoadingScreen,
                    Notes = cleanGame.Notes,
                    IsActive = cleanGame.IsActive,
                    GameDetails = cleanGame.GameDetails,
                    Characters = cleanGame.Characters,
                    IsDisabled = cleanGame.IsDisabled
                };
            }

            return AdminGame;
        }

        // Admin Cleaning Extension Methods.  -CRUD-
        
        // GameDetail
        public static dc.GameDetail AdminCleanForAdd(this dc.GameDetail cleanGameDetail)
        {
            var AdminGameDetail = default(dc.GameDetail);

            if (!ReferenceEquals(cleanGameDetail, null))
            {
                AdminGameDetail = new dc.GameDetail()
                {
                     // default value: . 
                    GameDetailId = cleanGameDetail.GameDetailId,
                     // default value: . 
                    Name = cleanGameDetail.Name,
                     // default value: . 
                    Value = cleanGameDetail.Value,
                     // default value: . 
                    GameLoadingScreen = cleanGameDetail.GameLoadingScreen,
                     // default value: . 
                    Game = cleanGameDetail.Game,
                     // default value: . 
                    GameIsActive = cleanGameDetail.GameIsActive,
                     // default value: . 
                    GameIsDisabled = cleanGameDetail.GameIsDisabled
                };
                
            }

            return AdminGameDetail;
        }
        
        
        public static List<dc.GameDetail> AdminCleanForGet(this IEnumerable<dc.GameDetail> cleanGameDetails)
        {
            return cleanGameDetails.Select(GameDetail => GameDetail.AdminCleanForGet())
                            .ToList();
        }
        
        public static dc.GameDetail AdminCleanForGet(this dc.GameDetail cleanGameDetail)
        {
            var AdminGameDetail = default(dc.GameDetail);

            if (!ReferenceEquals(cleanGameDetail, null))
            {
                AdminGameDetail = new dc.GameDetail()
                {
                    GameDetailId = cleanGameDetail.GameDetailId,
                    Name = cleanGameDetail.Name,
                    Value = cleanGameDetail.Value,
                    GameLoadingScreen = cleanGameDetail.GameLoadingScreen,
                    Game = cleanGameDetail.Game,
                    GameIsActive = cleanGameDetail.GameIsActive,
                    GameIsDisabled = cleanGameDetail.GameIsDisabled
                };
            }

            return AdminGameDetail;
        }
        
        
        public static dc.GameDetail AdminCleanForUpdate(this dc.GameDetail cleanGameDetail)
        {
            var AdminGameDetail = default(dc.GameDetail);

            if (!ReferenceEquals(cleanGameDetail, null))
            {
                AdminGameDetail = new dc.GameDetail()
                {
                    GameDetailId = cleanGameDetail.GameDetailId,
                    Name = cleanGameDetail.Name,
                    Value = cleanGameDetail.Value,
                    GameLoadingScreen = cleanGameDetail.GameLoadingScreen,
                    Game = cleanGameDetail.Game,
                    GameIsActive = cleanGameDetail.GameIsActive,
                    GameIsDisabled = cleanGameDetail.GameIsDisabled
                };
            }

            return AdminGameDetail;
        }

        // Admin Cleaning Extension Methods.  -CRUD-
        
        // Character
        public static dc.Character AdminCleanForAdd(this dc.Character cleanCharacter)
        {
            var AdminCharacter = default(dc.Character);

            if (!ReferenceEquals(cleanCharacter, null))
            {
                AdminCharacter = new dc.Character()
                {
                     // default value: . 
                    CharacterId = cleanCharacter.CharacterId,
                     // default value: . 
                    Name = cleanCharacter.Name,
                     // default value: . 
                    Avatar = cleanCharacter.Avatar,
                     // default value: . 
                    PrimaryWeapon = cleanCharacter.PrimaryWeapon,
                     // default value: . 
                    PrimaryWeaponName = cleanCharacter.PrimaryWeaponName,
                     // default value: . 
                    PrimaryWeaponDescription = cleanCharacter.PrimaryWeaponDescription,
                     // default value: . 
                    PrimaryWeaponImageURL = cleanCharacter.PrimaryWeaponImageURL,
                     // default value: . 
                    Type = cleanCharacter.Type,
                     // default value: . 
                    IsMissing = cleanCharacter.IsMissing,
                     // default value: . 
                    IntroducedAtLevel = cleanCharacter.IntroducedAtLevel,
                     // default value: . 
                    Description = cleanCharacter.Description,
                     // default value: . 
                    Skills = cleanCharacter.Skills,
                     // default value: . 
                    Game = cleanCharacter.Game,
                     // default value: . 
                    GameIsActive = cleanCharacter.GameIsActive
                };
                
            }

            return AdminCharacter;
        }
        
        
        public static List<dc.Character> AdminCleanForGet(this IEnumerable<dc.Character> cleanCharacters)
        {
            return cleanCharacters.Select(Character => Character.AdminCleanForGet())
                            .ToList();
        }
        
        public static dc.Character AdminCleanForGet(this dc.Character cleanCharacter)
        {
            var AdminCharacter = default(dc.Character);

            if (!ReferenceEquals(cleanCharacter, null))
            {
                AdminCharacter = new dc.Character()
                {
                    CharacterId = cleanCharacter.CharacterId,
                    Name = cleanCharacter.Name,
                    Avatar = cleanCharacter.Avatar,
                    PrimaryWeapon = cleanCharacter.PrimaryWeapon,
                    PrimaryWeaponName = cleanCharacter.PrimaryWeaponName,
                    PrimaryWeaponDescription = cleanCharacter.PrimaryWeaponDescription,
                    PrimaryWeaponImageURL = cleanCharacter.PrimaryWeaponImageURL,
                    Type = cleanCharacter.Type,
                    IsMissing = cleanCharacter.IsMissing,
                    IntroducedAtLevel = cleanCharacter.IntroducedAtLevel,
                    Description = cleanCharacter.Description,
                    Skills = cleanCharacter.Skills,
                    Game = cleanCharacter.Game,
                    GameIsActive = cleanCharacter.GameIsActive
                };
            }

            return AdminCharacter;
        }
        
        
        public static dc.Character AdminCleanForUpdate(this dc.Character cleanCharacter)
        {
            var AdminCharacter = default(dc.Character);

            if (!ReferenceEquals(cleanCharacter, null))
            {
                AdminCharacter = new dc.Character()
                {
                    CharacterId = cleanCharacter.CharacterId,
                    Name = cleanCharacter.Name,
                    Avatar = cleanCharacter.Avatar,
                    PrimaryWeapon = cleanCharacter.PrimaryWeapon,
                    PrimaryWeaponName = cleanCharacter.PrimaryWeaponName,
                    PrimaryWeaponDescription = cleanCharacter.PrimaryWeaponDescription,
                    PrimaryWeaponImageURL = cleanCharacter.PrimaryWeaponImageURL,
                    Type = cleanCharacter.Type,
                    IsMissing = cleanCharacter.IsMissing,
                    IntroducedAtLevel = cleanCharacter.IntroducedAtLevel,
                    Description = cleanCharacter.Description,
                    Skills = cleanCharacter.Skills,
                    Game = cleanCharacter.Game,
                    GameIsActive = cleanCharacter.GameIsActive
                };
            }

            return AdminCharacter;
        }

        // Admin Cleaning Extension Methods.  -CRUD-
        
        // AppUser
        public static dc.AppUser AdminCleanForAdd(this dc.AppUser cleanAppUser)
        {
            var AdminAppUser = default(dc.AppUser);

            if (!ReferenceEquals(cleanAppUser, null))
            {
                AdminAppUser = new dc.AppUser()
                {
                     // default value: . 
                    AppUserId = cleanAppUser.AppUserId,
                     // default value: . 
                    Name = cleanAppUser.Name,
                     // default value: . 
                    EmailAddress = cleanAppUser.EmailAddress,
                     // default value: . 
                    Roles = cleanAppUser.Roles,
                     // default value: . 
                    Notes = cleanAppUser.Notes
                };
                
            }

            return AdminAppUser;
        }
        
        
        public static List<dc.AppUser> AdminCleanForGet(this IEnumerable<dc.AppUser> cleanAppUsers)
        {
            return cleanAppUsers.Select(AppUser => AppUser.AdminCleanForGet())
                            .ToList();
        }
        
        public static dc.AppUser AdminCleanForGet(this dc.AppUser cleanAppUser)
        {
            var AdminAppUser = default(dc.AppUser);

            if (!ReferenceEquals(cleanAppUser, null))
            {
                AdminAppUser = new dc.AppUser()
                {
                    AppUserId = cleanAppUser.AppUserId,
                    Name = cleanAppUser.Name,
                    EmailAddress = cleanAppUser.EmailAddress,
                    Roles = cleanAppUser.Roles,
                    Notes = cleanAppUser.Notes
                };
            }

            return AdminAppUser;
        }
        
        
        public static dc.AppUser AdminCleanForUpdate(this dc.AppUser cleanAppUser)
        {
            var AdminAppUser = default(dc.AppUser);

            if (!ReferenceEquals(cleanAppUser, null))
            {
                AdminAppUser = new dc.AppUser()
                {
                    AppUserId = cleanAppUser.AppUserId,
                    Name = cleanAppUser.Name,
                    EmailAddress = cleanAppUser.EmailAddress,
                    Roles = cleanAppUser.Roles,
                    Notes = cleanAppUser.Notes
                };
            }

            return AdminAppUser;
        }

        // Admin Cleaning Extension Methods.  -CRUD-
        
        // Weapon
        public static dc.Weapon AdminCleanForAdd(this dc.Weapon cleanWeapon)
        {
            var AdminWeapon = default(dc.Weapon);

            if (!ReferenceEquals(cleanWeapon, null))
            {
                AdminWeapon = new dc.Weapon()
                {
                     // default value: . 
                    WeaponId = cleanWeapon.WeaponId,
                     // default value: . 
                    Name = cleanWeapon.Name,
                     // default value: . 
                    Description = cleanWeapon.Description,
                     // default value: . 
                    ImageURL = cleanWeapon.ImageURL,
                     // default value: . 
                    Characters = cleanWeapon.Characters
                };
                
            }

            return AdminWeapon;
        }
        
        
        public static List<dc.Weapon> AdminCleanForGet(this IEnumerable<dc.Weapon> cleanWeapons)
        {
            return cleanWeapons.Select(Weapon => Weapon.AdminCleanForGet())
                            .ToList();
        }
        
        public static dc.Weapon AdminCleanForGet(this dc.Weapon cleanWeapon)
        {
            var AdminWeapon = default(dc.Weapon);

            if (!ReferenceEquals(cleanWeapon, null))
            {
                AdminWeapon = new dc.Weapon()
                {
                    WeaponId = cleanWeapon.WeaponId,
                    Name = cleanWeapon.Name,
                    Description = cleanWeapon.Description,
                    ImageURL = cleanWeapon.ImageURL,
                    Characters = cleanWeapon.Characters
                };
            }

            return AdminWeapon;
        }
        
        
        public static dc.Weapon AdminCleanForUpdate(this dc.Weapon cleanWeapon)
        {
            var AdminWeapon = default(dc.Weapon);

            if (!ReferenceEquals(cleanWeapon, null))
            {
                AdminWeapon = new dc.Weapon()
                {
                    WeaponId = cleanWeapon.WeaponId,
                    Name = cleanWeapon.Name,
                    Description = cleanWeapon.Description,
                    ImageURL = cleanWeapon.ImageURL,
                    Characters = cleanWeapon.Characters
                };
            }

            return AdminWeapon;
        }

        // Admin Cleaning Extension Methods.  -CRUD-
        
        // Level
        public static dc.Level AdminCleanForAdd(this dc.Level cleanLevel)
        {
            var AdminLevel = default(dc.Level);

            if (!ReferenceEquals(cleanLevel, null))
            {
                AdminLevel = new dc.Level()
                {
                     // default value: . 
                    LevelId = cleanLevel.LevelId,
                     // default value: . 
                    Name = cleanLevel.Name,
                     // default value: . 
                    Description = cleanLevel.Description,
                     // default value: . 
                    LevelNumber = cleanLevel.LevelNumber,
                     // default value: . 
                    Location = cleanLevel.Location,
                     // default value: . 
                    ImageURL = cleanLevel.ImageURL,
                     // default value: . 
                    GoogleKey = cleanLevel.GoogleKey
                };
                
            }

            return AdminLevel;
        }
        
        
        public static List<dc.Level> AdminCleanForGet(this IEnumerable<dc.Level> cleanLevels)
        {
            return cleanLevels.Select(Level => Level.AdminCleanForGet())
                            .ToList();
        }
        
        public static dc.Level AdminCleanForGet(this dc.Level cleanLevel)
        {
            var AdminLevel = default(dc.Level);

            if (!ReferenceEquals(cleanLevel, null))
            {
                AdminLevel = new dc.Level()
                {
                    LevelId = cleanLevel.LevelId,
                    Name = cleanLevel.Name,
                    Description = cleanLevel.Description,
                    LevelNumber = cleanLevel.LevelNumber,
                    Location = cleanLevel.Location,
                    ImageURL = cleanLevel.ImageURL,
                    GoogleKey = cleanLevel.GoogleKey
                };
            }

            return AdminLevel;
        }
        
        
        public static dc.Level AdminCleanForUpdate(this dc.Level cleanLevel)
        {
            var AdminLevel = default(dc.Level);

            if (!ReferenceEquals(cleanLevel, null))
            {
                AdminLevel = new dc.Level()
                {
                    LevelId = cleanLevel.LevelId,
                    Name = cleanLevel.Name,
                    Description = cleanLevel.Description,
                    LevelNumber = cleanLevel.LevelNumber,
                    Location = cleanLevel.Location,
                    ImageURL = cleanLevel.ImageURL,
                    GoogleKey = cleanLevel.GoogleKey
                };
            }

            return AdminLevel;
        }

        // Player Cleaning Extension Methods.  --
        
        // Player Cleaning Extension Methods.  --
        
        // Player Cleaning Extension Methods.  --
        
        // Player Cleaning Extension Methods.  --
        
        // Player Cleaning Extension Methods.  --
        
        // Player Cleaning Extension Methods.  --
        
        // NPC Cleaning Extension Methods.  --
        
        // NPC Cleaning Extension Methods.  --
        
        // NPC Cleaning Extension Methods.  --
        
        // NPC Cleaning Extension Methods.  --
        
        // NPC Cleaning Extension Methods.  --
        
        // NPC Cleaning Extension Methods.  --
        
    }
}
