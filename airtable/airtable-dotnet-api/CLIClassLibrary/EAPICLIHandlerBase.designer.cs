using Plossum.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoTme.Default.Lib.CLIHandler
{

    [CommandLineManager(ApplicationName = "carr-rest-api Command Line Interface",
                        Copyright = "Copyright 2023, EJ Alexandra",
                        Description = @"")]
    public partial class EAPICLIHandlerBase
    {
        
        [CommandLineOption(Description = "Lists the project's available stages", MinOccurs = 0, Aliases = "")]
        public bool listStageInfo { get; set; }
        
        [CommandLineOption(Description = "Authenticate as a user with Auth0 integration", MinOccurs = 0, Aliases = "")]
        public bool authenticate { get; set; }
        
        [CommandLineOption(Description = "Check who you are currently operating as", MinOccurs = 0, Aliases = "")]
        public bool whoami { get; set; }
        
        [CommandLineOption(Description = "the user's password", MinOccurs = 0, Aliases = "")]
        public string demoPassword { get; set; }
        
        [CommandLineOption(Description = "Raw data provided", MinOccurs = 0, Aliases = "")]
        public string bodyData { get; set; }
        
        [CommandLineOption(Description = "Path to file to use", MinOccurs = 0, Aliases = "")]
        public string bodyFile { get; set; }
        
        [CommandLineOption(Description = "Who to run as", MinOccurs = 0, Aliases = "as")]
        public string runas { get; set; }
        
        [CommandLineOption(Description = "AMQPS Connection String", MinOccurs = 0, Aliases = "")]
        public string amqps { get; set; }
        
        [CommandLineOption(Description = "Limit the where clause", MinOccurs = 0, Aliases = "")]
        public string where { get; set; }
        
        [CommandLineOption(Description = "Output file name", MinOccurs = 0, Aliases = "")]
        public string output { get; set; }
        
        [CommandLineOption(Description = "Get help on a given topic", MinOccurs = 0, Aliases = "")]
        public bool help { get; set; }
        
        [CommandLineOption(Description = "The specific command to execute.", MinOccurs = 0, Aliases = "")]
        public string action { get; set; }
        
        [CommandLineOption(Description = "Maximum number of pages to return in the results", MinOccurs = 0, Aliases = "")]
        public int maxpages { get; set; }
        
        [CommandLineOption(Description = "Which view in the data source to pull data from.", MinOccurs = 0, Aliases = "")]
        public string view { get; set; }
        
        [CommandLineOption(Description = "Airtable API key to use for requests.", MinOccurs = 0, Aliases = "")]
        public string apikey { get; set; }
        
        [CommandLineOption(Description = "ID of Airtable base you're querying.", MinOccurs = 0, Aliases = "")]
        public string baseid { get; set; }
        
        [CommandLineOption(Description = "Name of project that is being used will be saved with the Airtable credentials.", MinOccurs = 0, Aliases = "")]
        public string projectname { get; set; }
        
        [CommandLineOption(Description = "Flag to set the email+apikey as default", MinOccurs = 0, Aliases = "")]
        public bool makeDefault { get; set; }
        
        [CommandLineOption(Description = "User's email address", MinOccurs = 0, Aliases = "")]
        public string emailAddress { get; set; }
        
        [CommandLineOption(Description = "A stage for the project", MinOccurs = 0, Aliases = "")]
        public string stage { get; set; }
        
        [CommandLineOption(Description = "Start http://rest-api.ssot.me:4242 server to respond to services", MinOccurs = 0, Aliases = "")]
        public bool startServer { get; set; }
        
        [CommandLineOption(Description = "The http REST endpoint to connect to in order to get data", MinOccurs = 0, Aliases = "")]
        public string restEndpoint { get; set; }
        
    }
}
