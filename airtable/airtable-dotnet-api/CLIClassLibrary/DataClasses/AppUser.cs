/*************************************************
Initially Generated by SSoT.me - 2017
    EJ Alexandra - ssotme odxml42/odxml-to-csharp-pocos
    This file WILL NOT be overwritten once changes are made.
*************************************************/
using System;
using System.ComponentModel;
                        
namespace AirtableDirect.CLI.Lib.DataClasses
{                   
    
    /// <summary>
    /// A list of users allowed to access games.
    /// </summary>
    public partial class AppUser 
    {
        public AppUser()
        {
            this.InitPoco();
        }

        public override String ToString()
        {
            return String.Format("AppUser: {0}", this.Name);
        }
                            
    }
}