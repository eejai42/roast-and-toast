using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AirtableDirect.CLI.Lib.DataClasses
{                            
    public partial class Character
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CharacterId")]
        public String CharacterId { get; set; }
    
        /// <summary>
        /// Name of the character.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        /// <summary>
        /// A url to an image for this character in the game
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Avatar")]
        public String Avatar { get; set; }
    
        /// <summary>
        /// The type of character (Marshmallow, Ninja, NPC, etc.)
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Type")]
        public String Type { get; set; }
    
        /// <summary>
        /// A flag indicating if this character is 'missing' - i.e. they have not been found yet.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsMissing")]
        public Nullable<Boolean> IsMissing { get; set; }
    
        /// <summary>
        /// The number of the level which the character shows up. For some games, all characters will be introduced right out of the gate. This field is agnostic of the specific values - and it can be left blank.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IntroducedAtLevel")]
        public Nullable<Int32> IntroducedAtLevel { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "sd")]
        public String Sd { get; set; }
    
        /// <summary>
        /// Short description of the character for the game.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Skills")]
        public String Skills { get; set; }
    
        /// <summary>
        /// The game associated with this character.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Game")]
        [RemoteIsCollection]
        public String Game { get; set; }
    
        /// <summary>
        /// A flag for whether the game associated with this character is active.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GameIsActive")]
        public Nullable<Boolean> GameIsActive { get; set; }
    

        

        
        
        
    }
}
