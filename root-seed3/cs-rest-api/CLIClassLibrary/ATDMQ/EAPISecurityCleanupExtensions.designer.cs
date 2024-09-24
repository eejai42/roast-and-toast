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
    
        // Guest Cleaning Extension Methods.  -R-
        
        // GameDetail
        public static List<dc.GameDetail> GuestCleanForGet(this IEnumerable<dc.GameDetail> cleanGameDetails)
        {
            return cleanGameDetails.Select(GameDetail => GameDetail.GuestCleanForGet())
                            .ToList();
        }
        
        public static dc.GameDetail GuestCleanForGet(this dc.GameDetail cleanGameDetail)
        {
            var GuestGameDetail = default(dc.GameDetail);

            if (!ReferenceEquals(cleanGameDetail, null))
            {
                GuestGameDetail = new dc.GameDetail()
                {
                    GameDetailId = cleanGameDetail.GameDetailId,
                    Name = cleanGameDetail.Name,
                    GameLoadingScreen = cleanGameDetail.GameLoadingScreen,
                    Value = cleanGameDetail.Value,
                    Game = cleanGameDetail.Game,
                    GameIsActive = cleanGameDetail.GameIsActive,
                    GameIsDisabled = cleanGameDetail.GameIsDisabled
                };
            }

            return GuestGameDetail;
        }
        
        
        // Guest Cleaning Extension Methods.  -R-
        
        // Character
        public static List<dc.Character> GuestCleanForGet(this IEnumerable<dc.Character> cleanCharacters)
        {
            return cleanCharacters.Select(Character => Character.GuestCleanForGet())
                            .ToList();
        }
        
        public static dc.Character GuestCleanForGet(this dc.Character cleanCharacter)
        {
            var GuestCharacter = default(dc.Character);

            if (!ReferenceEquals(cleanCharacter, null))
            {
                GuestCharacter = new dc.Character()
                {
                    CharacterId = cleanCharacter.CharacterId,
                    Name = cleanCharacter.Name,
                    Avatar = cleanCharacter.Avatar,
                    Type = cleanCharacter.Type,
                    IsMissing = cleanCharacter.IsMissing,
                    BackgroundColor = cleanCharacter.BackgroundColor,
                    Description = cleanCharacter.Description,
                    IntroducedAtLevel = cleanCharacter.IntroducedAtLevel,
                    Game = cleanCharacter.Game,
                    GameIsActive = cleanCharacter.GameIsActive
                };
            }

            return GuestCharacter;
        }
        
        
        // Guest Cleaning Extension Methods.  -R-
        
        // AppUser
        public static List<dc.AppUser> GuestCleanForGet(this IEnumerable<dc.AppUser> cleanAppUsers)
        {
            return cleanAppUsers.Select(AppUser => AppUser.GuestCleanForGet())
                            .ToList();
        }
        
        public static dc.AppUser GuestCleanForGet(this dc.AppUser cleanAppUser)
        {
            var GuestAppUser = default(dc.AppUser);

            if (!ReferenceEquals(cleanAppUser, null))
            {
                GuestAppUser = new dc.AppUser()
                {
                    AppUserId = cleanAppUser.AppUserId,
                    Name = cleanAppUser.Name,
                    Notes = cleanAppUser.Notes,
                    EmailAddress = cleanAppUser.EmailAddress,
                    Roles = cleanAppUser.Roles
                };
            }

            return GuestAppUser;
        }
        
        
        // Guest Cleaning Extension Methods.  -R-
        
        // Game
        public static List<dc.Game> GuestCleanForGet(this IEnumerable<dc.Game> cleanGames)
        {
            return cleanGames.Select(Game => Game.GuestCleanForGet())
                            .ToList();
        }
        
        public static dc.Game GuestCleanForGet(this dc.Game cleanGame)
        {
            var GuestGame = default(dc.Game);

            if (!ReferenceEquals(cleanGame, null))
            {
                GuestGame = new dc.Game()
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

            return GuestGame;
        }
        
        
        // Guest Cleaning Extension Methods.  -R-
        
        // WEapon
        public static List<dc.WEapon> GuestCleanForGet(this IEnumerable<dc.WEapon> cleanWEapons)
        {
            return cleanWEapons.Select(WEapon => WEapon.GuestCleanForGet())
                            .ToList();
        }
        
        public static dc.WEapon GuestCleanForGet(this dc.WEapon cleanWEapon)
        {
            var GuestWEapon = default(dc.WEapon);

            if (!ReferenceEquals(cleanWEapon, null))
            {
                GuestWEapon = new dc.WEapon()
                {
                    WEaponId = cleanWEapon.WEaponId,
                    Name = cleanWEapon.Name,
                    Description = cleanWEapon.Description
                };
            }

            return GuestWEapon;
        }
        
        
        // Guest Cleaning Extension Methods.  -R-
        
        // Level
        public static List<dc.Level> GuestCleanForGet(this IEnumerable<dc.Level> cleanLevels)
        {
            return cleanLevels.Select(Level => Level.GuestCleanForGet())
                            .ToList();
        }
        
        public static dc.Level GuestCleanForGet(this dc.Level cleanLevel)
        {
            var GuestLevel = default(dc.Level);

            if (!ReferenceEquals(cleanLevel, null))
            {
                GuestLevel = new dc.Level()
                {
                    LevelId = cleanLevel.LevelId,
                    Name = cleanLevel.Name,
                    Description = cleanLevel.Description
                };
            }

            return GuestLevel;
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
                    
                };
            }

            return AdminAppUser;
        }

        // Admin Cleaning Extension Methods.  -CRUD-
        
        // Game
        public static dc.Game AdminCleanForAdd(this dc.Game cleanGame)
        {
            var AdminGame = default(dc.Game);

            if (!ReferenceEquals(cleanGame, null))
            {
                AdminGame = new dc.Game()
                {
                    
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
                    
                };
            }

            return AdminGame;
        }

        // Admin Cleaning Extension Methods.  -CRUD-
        
        // WEapon
        public static dc.WEapon AdminCleanForAdd(this dc.WEapon cleanWEapon)
        {
            var AdminWEapon = default(dc.WEapon);

            if (!ReferenceEquals(cleanWEapon, null))
            {
                AdminWEapon = new dc.WEapon()
                {
                    
                };
                
            }

            return AdminWEapon;
        }
        
        
        public static List<dc.WEapon> AdminCleanForGet(this IEnumerable<dc.WEapon> cleanWEapons)
        {
            return cleanWEapons.Select(WEapon => WEapon.AdminCleanForGet())
                            .ToList();
        }
        
        public static dc.WEapon AdminCleanForGet(this dc.WEapon cleanWEapon)
        {
            var AdminWEapon = default(dc.WEapon);

            if (!ReferenceEquals(cleanWEapon, null))
            {
                AdminWEapon = new dc.WEapon()
                {
                    
                };
            }

            return AdminWEapon;
        }
        
        
        public static dc.WEapon AdminCleanForUpdate(this dc.WEapon cleanWEapon)
        {
            var AdminWEapon = default(dc.WEapon);

            if (!ReferenceEquals(cleanWEapon, null))
            {
                AdminWEapon = new dc.WEapon()
                {
                    
                };
            }

            return AdminWEapon;
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