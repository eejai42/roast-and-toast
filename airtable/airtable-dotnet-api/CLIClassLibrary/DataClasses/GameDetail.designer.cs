using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AirtableDirect.CLI.Lib.DataClasses
{                            
    public partial class GameDetail
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GameDetailId")]
        public String GameDetailId { get; set; }
    
        /// <summary>
        /// The Name of an attribute of the project
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        /// <summary>
        /// The Value of an attribute of the project
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Value")]
        public String Value { get; set; }
    
        /// <summary>
        /// The name of the game associated with the game detail.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GameLoadingScreen")]
        public String GameLoadingScreen { get; set; }
    
        /// <summary>
        /// The game that this attribute/detail is associated with.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Game")]
        [RemoteIsCollection]
        public String Game { get; set; }
    
        /// <summary>
        /// A flag indicating if the game is the active game.
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GameIsActive")]
        public Nullable<Boolean> GameIsActive { get; set; }
    
        /// <summary>
        /// A flag indicating if the associated game has been disabled (removed).
        /// </summary>
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GameIsDisabled")]
        public Nullable<Boolean> GameIsDisabled { get; set; }
    

        

        
        
        
    }
}
