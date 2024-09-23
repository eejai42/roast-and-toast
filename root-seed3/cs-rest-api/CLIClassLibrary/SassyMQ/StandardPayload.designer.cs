
using AirtableDirect.CLI.Lib.DataClasses;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace YP.SassyMQ.Lib.RabbitMQ
{
    public partial class StandardPayload
    {
        
        public GameDetail GameDetail { get; set; }
        public List<GameDetail> GameDetails { get; set; }
        
        public Character Character { get; set; }
        public List<Character> Characters { get; set; }
        
        public AppUser AppUser { get; set; }
        public List<AppUser> AppUsers { get; set; }
        
        public Game Game { get; set; }
        public List<Game> Games { get; set; }
        
        public WEapon WEapon { get; set; }
        public List<WEapon> WEapons { get; set; }
        
        public Level Level { get; set; }
        public List<Level> Levels { get; set; }
        

    }
}