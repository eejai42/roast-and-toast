using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AirtableDirect.CLI.Lib.DataClasses
{                            
    public partial class Game
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GameId")]
        public String GameId { get; set; }
    
        /// <summary>
        /// Name of the character.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        /// <summary>
        /// Loading screen image.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LoadingScreen")]
        public String LoadingScreen { get; set; }
    
        /// <summary>
        /// Notes about the game
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }
    
        /// <summary>
        /// A flag indicating if this game is the active game.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsActive")]
        public Nullable<Boolean> IsActive { get; set; }
    
        /// <summary>
        /// Links to the details about the game
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GameDetails")]
        public String GameDetails { get; set; }
    
        /// <summary>
        /// A list of characters in the game.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Characters")]
        public String Characters { get; set; }
    
        /// <summary>
        /// Flag indicating if the game is disabled.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsDisabled")]
        public Nullable<Boolean> IsDisabled { get; set; }
    

        

        
        
        
    }
}
