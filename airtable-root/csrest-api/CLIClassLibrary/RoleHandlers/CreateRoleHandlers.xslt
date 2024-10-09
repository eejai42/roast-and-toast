<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:param name="dish-root" select="'not-set'"/>
    <xsl:variable name="lexicon" select="concat($dish-root, '\SassyMQLexicon.smql')" />
    <xsl:variable name="smq" select="document($lexicon)" />
    <xsl:output method="xml" indent="yes"/>
    <xsl:variable name="odxml" select="document('DataSchema.odxml')" />
    <xsl:param name="baseId" select="''"/>
    <xsl:param name="crud-coordinator" select="'CRUDCoordinator'" />
    <xsl:param name="user-name" select="'ApiUser'" />
    <xsl:param name="email-address-name" select="'EmailAddress'" />
    <xsl:param name="role-name" select="'Role'" />

    <xsl:include href="../CommonXsltTemplates.xslt"/>
    <xsl:param name="output-filename" select="'output.txt'" />

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/*">
        <FileSet>
			<FileSetFiles>
			    <xsl:for-each select="$smq//SMQActors/SMQActor">
			        <xsl:variable name="role-name" select="Name" />
					<FileSetFile>
						<RelativePath>
							<xsl:text>../ATDMQ/ATD</xsl:text>
                            <xsl:value-of select="$role-name"/>
                            <xsl:text>/</xsl:text>
                            <xsl:value-of select="$role-name"/>
                            <xsl:text>.designer.cs</xsl:text>
				        </RelativePath>
					    <xsl:element name="FileContents" xml:space="preserve">using AirtableDirect.CLI.Lib.DataClasses;
using AirtableToDotNet.APIWrapper;
using System;
using System.Linq;
using System.Collections.Generic;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Security.Claims;

namespace CLIClassLibrary.ATDMQ
{
	public partial class ATD<xsl:value-of select="Name"/> : ATDActorBase
    {
       public ClaimsIdentity UserIdentity { get; set; }<xsl:for-each select="$smq//SMQMessages/SMQMessage[ActorFrom = $role-name]"><xsl:variable name="is-update" select="substring(Name,1,6)='Update'"  /> // IsUpdate: <xsl:value-of select="$is-update" /> - <xsl:value-of select="RAWValues/Response" />.
       public <xsl:choose><xsl:when test="translate(normalize-space(Category), $ucletters, $lcletters)!='crud'">virtual <xsl:choose>
			<xsl:when test="normalize-space(RAWValues/Response)=''">void</xsl:when>
			<xsl:when test="translate(normalize-space(RAWValues/IsResponseCollection), $ucletters, $lcletters)='true' or RAWValues/IsResponseCollection = 1 or $is-update">IEnumerable&lt;<xsl:value-of select="RAWValues/Response"/>&gt;</xsl:when>
			<xsl:otherwise><xsl:value-of select="RAWValues/Response"/></xsl:otherwise>
			</xsl:choose> <xsl:value-of select="Name"/>(StandardPayload Payload) => <xsl:choose>
                <xsl:when test="RAWValues/Category='Custom'">this.Custom<xsl:value-of select="Name"/>(Payload);</xsl:when>
                <xsl:otherwise>throw new NotImplementedException();</xsl:otherwise>
            </xsl:choose></xsl:when><xsl:otherwise><xsl:choose>
			<xsl:when test="normalize-space(RAWValues/Response)=''">void</xsl:when>
			<xsl:when test="translate(normalize-space(RAWValues/IsResponseCollection), $ucletters, $lcletters)='true' or RAWValues/IsResponseCollection = 1 or $is-update">IEnumerable&lt;<xsl:value-of select="RAWValues/Response"/>&gt;</xsl:when>
			<xsl:otherwise><xsl:value-of select="RAWValues/Response"/></xsl:otherwise>
			</xsl:choose> <xsl:value-of select="Name"/>(StandardPayload Payload) {
			<xsl:choose><xsl:when test="Category='crud'">
				AirtableDirectCLIAirtableAPIWrapper api = new AirtableDirectCLIAirtableAPIWrapper(Payload.ApiKey, Payload.BaseId);
				<xsl:choose>
				<xsl:when test="substring(Name,1,3)='Add'">return api.Insert(Payload.<xsl:value-of select="RAWValues/Payload"/>.<xsl:value-of select="$role-name" />CleanForAdd());</xsl:when>
				<xsl:when test="substring(Name,1,3)='Get'">return api.<xsl:value-of select="Name"/>(Wrap<xsl:value-of select="$role-name" /><xsl:value-of select="Name"/>Where(Payload.AirtableWhere), Payload.View, Payload.MaxPages).<xsl:value-of select="$role-name" />CleanForGet();</xsl:when>
				<xsl:when test="$is-update">
          if (Payload.<xsl:apply-templates select="RAWValues/Payload" mode="pluralize"/> is null)
          {
              return this.Update<xsl:apply-templates select="RAWValues/Payload"  mode="pluralize"/>(api, Payload);
          }
          else
          {
              var updated<xsl:value-of select="RAWValues/Payload"/> = api.Update(Payload.<xsl:value-of select="RAWValues/Payload"/>.<xsl:value-of select="$role-name" />CleanForUpdate());
              return new List&lt;<xsl:value-of select="RAWValues/Payload"/>>(new <xsl:value-of select="RAWValues/Payload"/>[] { updated<xsl:value-of select="RAWValues/Payload"/> });
          }
        </xsl:when>
				<xsl:when test="substring(Name,1,6)='Delete'">api.Delete(Payload.<xsl:value-of select="RAWValues/Payload"/>);</xsl:when>
				</xsl:choose></xsl:when>
			    <xsl:otherwise>
		        </xsl:otherwise>
		    </xsl:choose>
			}</xsl:otherwise></xsl:choose>        
        private string Wrap<xsl:value-of select="$role-name" /><xsl:value-of select="Name"/>Where(string airtableWhere)
        {<xsl:variable name="payload" select="normalize-space(RAWValues/Response)" />
                <xsl:variable name="od" select="$odxml//ObjectDefs/ObjectDef[normalize-space(Name)=$payload]" />
        <xsl:variable name="airtable-where-needs-escaping" select="normalize-space($od/*[local-name() = concat($role-name,'AirtableWhere')])" />
        <xsl:variable name="airtable-where" xml:space="default">
            <xsl:call-template name="escape-curly-braces">
                <xsl:with-param name="input-string" select="$airtable-where-needs-escaping"></xsl:with-param>
            </xsl:call-template>
        </xsl:variable>
            // AirtableWhere: <xsl:value-of select="$airtable-where" /><xsl:choose>
            <xsl:when test="$airtable-where = ''">
            return airtableWhere;</xsl:when>
            <xsl:otherwise>
            airtableWhere = String.IsNullOrEmpty(airtableWhere) ? "True()" : airtableWhere;
            return $"AND(<xsl:value-of select="$airtable-where"/>,{airtableWhere})".Replace("$<xsl:value-of select="$user-name"/>.<xsl:value-of select="$email-address-name "/>$", this.EmailAddress);</xsl:otherwise></xsl:choose> 
        }
      <xsl:if test="substring(Name, 1, 6) = 'Update'"><xsl:variable name="odName" select="$odxml//ObjectDefs/ObjectDef[normalize-space(Name)=normalize-space(current()/RAWValues/Payload)]/Name" /><xsl:variable name="odPluralName" select="$odxml//ObjectDefs/ObjectDef[normalize-space(Name)=normalize-space(current()/RAWValues/Payload)]/PluralName" />
        private List&lt;<xsl:value-of select="$odName"  />> Update<xsl:value-of select="$odPluralName"  />(AirtableDirectCLIAirtableAPIWrapper api, StandardPayload payload)
        {
            if ((payload.<xsl:value-of select="$odName" /> is null) &amp;&amp; !(payload.<xsl:value-of select="$odPluralName"  /> is null)) payload.<xsl:value-of select="$odPluralName"  /> = new List&lt;<xsl:value-of select="$odName"  />>() { payload.<xsl:value-of select="$odName"  />};
            if (!(payload.<xsl:value-of select="$odPluralName"  /> is null) &amp;&amp; payload.<xsl:value-of select="$odPluralName"  />.Any())
            {
                var updatedItems = new List&lt;<xsl:value-of select="$odName"  />>();
                payload.<xsl:value-of select="$odPluralName"  />.ForEach(item =>
                {
                    var updated<xsl:value-of select="$odPluralName"  /> = api.Update(item);
                    updatedItems.Add(updated<xsl:value-of select="$odPluralName"  />);
                });
                return updatedItems;
            }
            else throw new Exception("Updating <xsl:value-of select="$odPluralName"  /> requeres either payload.<xsl:value-of select="$odName"  /> or payload.<xsl:value-of select="$odPluralName"  /> to be non-null values.");
        }
    </xsl:if>
</xsl:for-each>
    }
}
					    </xsl:element>
			        </FileSetFile>
                    <xsl:for-each select="$smq//SMQMessages/SMQMessage[ActorFrom = $role-name and translate(Category, $ucletters, $lcletters) = 'custom']">
                        <xsl:variable name="is-update" select="substring(Name,1,6)='Update'"  /> // IsUpdate: <xsl:value-of select="Category" />
                        <FileSetFile>
				        <RelativePath>
							<xsl:text>../ATDMQ/ATD</xsl:text>
							<xsl:value-of select="$role-name"/>
                            <xsl:text>/</xsl:text>
                            <xsl:value-of select="$role-name"/>
                            <xsl:value-of select="Name"/>
							<xsl:text>Handler.cs</xsl:text>
                        </RelativePath>
                        <OverwriteMode>Never</OverwriteMode>
                        <xsl:element name="FileContents" xml:space="preserve">using System;
using YP.SassyMQ.Lib.RabbitMQ;

namespace CLIClassLibrary.ATDMQ
{                   
    public partial class ATD<xsl:value-of select="$role-name"/>
    {
       public StandardPayload Custom<xsl:value-of select="Name"/>(StandardPayload Payload)
       {
            // Default behavior would be
            // return Payload;
            
            throw new NotImplementedException();
	   }
    }
}
					</xsl:element>
                    </FileSetFile>
                </xsl:for-each>
            </xsl:for-each>
                <xsl:for-each select="$smq//SMQActors/SMQActor">
                    <xsl:variable name="role-name" select="Name" />
                    <FileSetFile>
                        <RelativePath>
                            <xsl:text>/ATDHandlers/</xsl:text>
                            <xsl:value-of select="Name"/>
                            <xsl:text>CLIHandler.cs</xsl:text>
                        </RelativePath>
                        <OverwriteMode>Never</OverwriteMode>
                        <xsl:element name="FileContents" xml:space="preserve">using Newtonsoft.Json;
using AirtableDirect.CLI.Lib.DataClasses;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Text;
using System;
using AirtableToDotNet.APIWrapper;
using System.Collections.Generic;
using System.Threading;
using CLIClassLibrary.ATDMQ;
using CLIClassLibrary.AirtableAPI;

namespace CLIClassLibrary.RoleHandlers.ATDHandlers
{
    public partial class <xsl:value-of select="Name"/>CLIHandler : ATDRoleHandlerBase&lt;ATD<xsl:value-of select="Name"/>>
    {

        public <xsl:value-of select="Name"/>CLIHandler(ATDKey atdKey)
            : base(atdKey)
        {
        }

        public override string Handle(string invoke, string data, string where, Int32 maxPages, String view)
        {
            if (string.IsNullOrEmpty(data)) data = "{}";
            string result = HandlerFactory(invoke, data, where, maxPages);
            return result;
        }
    }
}</xsl:element>
                    </FileSetFile>

                    <FileSetFile>
                            <RelativePath>
                                <xsl:text>/ATDHandlers/</xsl:text>
                                <xsl:value-of select="Name"/>
                                <xsl:text>CLIHandler.designer.cs</xsl:text>
                            </RelativePath>
                            <xsl:element name="FileContents" xml:space="preserve">using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using AirtableDirect.CLI.Lib.DataClasses;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using CLIClassLibrary.AirtableAPI;
using static SSoT.me.AirtableToDotNetLib.AirtableExtensions;

namespace CLIClassLibrary.RoleHandlers.ATDHandlers
{
    public partial class <xsl:value-of select="Name"/>CLIHandler
    {
        public override void AddHelp(StringBuilder sb, string helpTerm)
        {
            sb.AppendLine($"Help for <xsl:value-of select="Name"/>.");
            
            helpTerm = helpTerm.ToLower();
            var found = helpTerm == "general";
            
            if (helpTerm == "general")
            {
                sb.AppendLine();
                <xsl:for-each select="$smq/*/SMQMessages/SMQMessage[ActorFrom = $role-name]">
                sb.AppendLine($"<xsl:choose>
                    <xsl:when test="normalize-space(RAWValues/Response) != ''"><xsl:value-of select="RAWValues/Response"/></xsl:when>
                <xsl:otherwise>void</xsl:otherwise>
                </xsl:choose>: <xsl:value-of select="Name"  />");</xsl:for-each>                                            
            }
            
            sb.AppendLine($"{Environment.NewLine}Available Actions Matching: {helpTerm}");
            <xsl:for-each select="$smq//SMQMessages/SMQMessage[ActorFrom = $role-name]">
            if ("<xsl:value-of select="translate(Name, $ucletters, $lcletters)"/>".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - <xsl:value-of select="Name"/>");
                if ("<xsl:value-of select="translate(Name, $ucletters, $lcletters)"/>".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.Print<xsl:value-of select="Name"/>Help(sb);
                }
                found = true;
            }</xsl:for-each>
                       
            if (!found)
            {
                sb.AppendLine();
                sb.AppendLine($"{Environment.NewLine}UNABLE TO FIND COMMAND: {helpTerm} not found.");
            }
        }

        private string HandlerFactory(string invokeRequest, string payloadString, string where, Int32 maxPages)
        {
            var result = "";
            var payload = JsonConvert.DeserializeObject&lt;StandardPayload>(payloadString, new JsonSerializerSettings() { 
                    FloatParseHandling = FloatParseHandling.Decimal, 
                    ContractResolver = new CustomContractResolver&lt;StandardPayload>()
            });
            payload.SetActor(this.ATDActor);
            payload.AccessToken = this.ATDActor.AccessToken;
            payload.ApiKey = AirtableKey.CurrentKey.APIKey;
            payload.BaseId = StageInfo.CurrentStage.BaseID;
            payload.AirtableWhere = where;
            payload.MaxPages = maxPages &lt; 1 ? 5 : maxPages;
            Object reply = new Object();
			
            switch (invokeRequest.ToLower())
            {<xsl:for-each select="$smq//SMQMessages/SMQMessage[ActorFrom = $role-name]">
                case "<xsl:value-of select="translate(Name, $ucletters, $lcletters)"/>":<xsl:choose><xsl:when test="RAWValues/Response != ''">
				    reply = this.ATDActor.<xsl:value-of select="Name"/>(payload<xsl:if test="translate(normalize-space(IsDirectMessage), $ucletters, $lcletters) = 'true'">null, </xsl:if>);
                    result = JsonConvert.SerializeObject(reply, Formatting.Indented);</xsl:when><xsl:otherwise>
				    this.ATDActor.<xsl:value-of select="Name"/>(payload);
				    result = JsonConvert.SerializeObject(payload, Formatting.Indented);</xsl:otherwise></xsl:choose>
                    break;                   
</xsl:for-each>
                default:
                    throw new Exception($"Invalid request: {invokeRequest}");
            }

            return result;
        }
        
        <xsl:for-each select="$smq/*/SMQMessages/SMQMessage[ActorFrom = $role-name]"><xsl:variable name="msg" select="." />
        public void Print<xsl:value-of select="Name"/>Help(StringBuilder sb)
        {
            <xsl:for-each select="$odxml//ObjectDefs/ObjectDef[Name = $msg/RAWValues/Response]"><xsl:variable name="crud" select="normalize-space(*[name() = concat($role-name,'CRUD')])" />
                <xsl:if test="$crud != 'None' and $crud != ''">
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: <xsl:value-of select="Name" />     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                <xsl:for-each select="PropertyDefs/PropertyDef"><xsl:variable name="pd-crud" select="*[name() = concat($role-name,'CRUD')]" /><xsl:if test="string-length(normalize-space($pd-crud)) > 0">
                    sb.AppendLine($"<xsl:value-of select="$pd-crud"/>      - <xsl:value-of select="Name"/>");</xsl:if></xsl:for-each>
                </xsl:if>
            </xsl:for-each>
        }
        </xsl:for-each>

    }
}
</xsl:element>
                        </FileSetFile>
                    <FileSetFile>
                        <RelativePath>
                            <xsl:text>/RESTHandlers/</xsl:text>
                            <xsl:value-of select="Name"/>
                            <xsl:text>RESTCLIHandler.cs</xsl:text>
                        </RelativePath>
                        <OverwriteMode>Never</OverwriteMode>
                        <xsl:element name="FileContents" xml:space="preserve">using Newtonsoft.Json;
using AirtableDirect.CLI.Lib.DataClasses;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Text;
using System;
using AirtableToDotNet.APIWrapper;
using System.Collections.Generic;
using System.Threading;
using CLIClassLibrary.ATDMQ;
using CLIClassLibrary.AirtableAPI;

namespace CLIClassLibrary.RoleHandlers.RESTHandlers
{
    public partial class <xsl:value-of select="Name"/>RESTCLIHandler : RESTRoleHandlerBase
    {
        public <xsl:value-of select="Name"/>RESTCLIHandler(string restEndpoint)
            : base(restEndpoint)
        {
        
        }

        public override string Handle(string invoke, string data, string where, Int32 maxPages, String view)
        {
            if (string.IsNullOrEmpty(data)) data = "{}";
            string result = HandlerFactory(invoke, data, where, maxPages).GetAwaiter().GetResult();
            return result;
        }
    }
}</xsl:element>
                    </FileSetFile>

                    <FileSetFile>
                        <RelativePath>
                            <xsl:text>/RESTHandlers/</xsl:text>
                            <xsl:value-of select="Name"/>
                            <xsl:text>RESTCLIHandler.designer.cs</xsl:text>
                        </RelativePath>
                        <xsl:element name="FileContents" xml:space="preserve">using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using AirtableDirect.CLI.Lib.DataClasses;
using YP.SassyMQ.Lib.RabbitMQ;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using CLIClassLibrary.AirtableAPI;
using static SSoT.me.AirtableToDotNetLib.AirtableExtensions;
using System.Net.Http;

namespace CLIClassLibrary.RoleHandlers.RESTHandlers
{
    public partial class <xsl:value-of select="Name"/>RESTCLIHandler
    {
        public override void AddHelp(StringBuilder sb, string helpTerm)
        {
            sb.AppendLine($"Help for <xsl:value-of select="Name"/>.");
            
            helpTerm = helpTerm.ToLower();
            var found = helpTerm == "general";
            
            if (helpTerm == "general")
            {
                sb.AppendLine();
                <xsl:for-each select="$smq/*/SMQMessages/SMQMessage[ActorFrom = $role-name]">
                sb.AppendLine($"<xsl:choose>
                    <xsl:when test="normalize-space(RAWValues/Response) != ''"><xsl:value-of select="RAWValues/Response"/></xsl:when>
                <xsl:otherwise>void</xsl:otherwise>
                </xsl:choose>: <xsl:value-of select="Name"  />");</xsl:for-each>                                            
            }
            
            sb.AppendLine($"{Environment.NewLine}Available Actions Matching: {helpTerm}");
            <xsl:for-each select="$smq//SMQMessages/SMQMessage[ActorFrom = $role-name]">
            if ("<xsl:value-of select="translate(Name, $ucletters, $lcletters)"/>".Contains(helpTerm, StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($" - <xsl:value-of select="Name"/>");
                if ("<xsl:value-of select="translate(Name, $ucletters, $lcletters)"/>".Equals(helpTerm, StringComparison.OrdinalIgnoreCase)) 
                {
                    this.Print<xsl:value-of select="Name"/>Help(sb);
                }
                found = true;
            }</xsl:for-each>
                       
            if (!found)
            {
                sb.AppendLine();
                sb.AppendLine($"{Environment.NewLine}UNABLE TO FIND COMMAND: {helpTerm} not found.");
            }
        }

        private async Task&lt;string> HandlerFactory(string invokeRequest, string payloadString, string where, Int32 maxPages)
        {
            string result = null;
            var payload = JsonConvert.DeserializeObject&lt;StandardPayload>(payloadString, new JsonSerializerSettings() { 
                    FloatParseHandling = FloatParseHandling.Decimal, 
                    ContractResolver = new CustomContractResolver&lt;StandardPayload>()
            });
            payload.AirtableWhere = where;
            payload.MaxPages = maxPages &lt; 1 ? 5 : maxPages;
			
            switch (invokeRequest.ToLower())
            {<xsl:for-each select="$smq//SMQMessages/SMQMessage[ActorFrom = $role-name]">
                <xsl:variable name="payload" select="RAWValues/Response" />
                <xsl:variable name="od" select="$odxml//ObjectDefs/ObjectDef[Name=$payload]" />
                case "<xsl:value-of select="translate(Name, $ucletters, $lcletters)"/>":<xsl:choose><xsl:when test="RAWValues/Response != ''">
                    // <xsl:value-of select="$payload" />
                    result = await this.GETRequest("<xsl:value-of select="$od/PluralName"/>", payload);</xsl:when>
                    <xsl:when test="translate(Category, $ucletters, $lcletters) = 'custom'">
                    result = await this.POSTRequest("<xsl:value-of select="Name"/>", payload);</xsl:when>
                    </xsl:choose>
                    break;

                    </xsl:for-each>
                default:
                    throw new Exception($"Invalid request: {invokeRequest}");
            }

            return result;
        }
        
        private async Task&lt;string> POSTRequest(string methodName, StandardPayload payload)
        {
            var role = "<xsl:value-of select="$role-name" />";
            var client = new HttpClient();
            var requestUrl = $"{this.RestEndpoint}/{role}/{methodName}";

            // Serialize the payload to JSON
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await Client.PostAsync(requestUrl, httpContent);

            return await response.Content.ReadAsStringAsync();
        }
        
        private async Task&lt;string> GETRequest(string pluralName, StandardPayload payload)
        {
            var role = "<xsl:value-of select="$role-name" />";
            var requestUrl = $"{this.RestEndpoint}/{role}/{pluralName}";
            this.AddBearerToken();
            var results = await Client.GetStringAsync(requestUrl);

            return results;
        }
        
        <xsl:for-each select="$smq/*/SMQMessages/SMQMessage[ActorFrom = $role-name]"><xsl:variable name="msg" select="." />
        public void Print<xsl:value-of select="Name"/>Help(StringBuilder sb)
        {
            <xsl:for-each select="$odxml//ObjectDefs/ObjectDef[Name = $msg/RAWValues/Response]"><xsl:variable name="crud" select="normalize-space(*[name() = concat($role-name,'CRUD')])" />
                <xsl:if test="$crud != 'None' and $crud != ''">
                sb.AppendLine();
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine($"* *  OBJECT DEF: <xsl:value-of select="Name" />     *");
                sb.AppendLine($"* * * * * * * * * * * * * * * * * * * * * * * * * * *");
                sb.AppendLine();
                <xsl:for-each select="PropertyDefs/PropertyDef"><xsl:variable name="pd-crud" select="*[name() = concat($role-name,'CRUD')]" /><xsl:if test="string-length(normalize-space($pd-crud)) > 0">
                    sb.AppendLine($"<xsl:value-of select="$pd-crud"/>      - <xsl:value-of select="Name"/>");</xsl:if></xsl:for-each>
                </xsl:if>
            </xsl:for-each>
        }
        </xsl:for-each>

    }
}
</xsl:element>
                    </FileSetFile>
                </xsl:for-each>
                <FileSetFile>
                    <RelativePath>
                        <xsl:text>RoleHandlerFactory.cs</xsl:text>
                    </RelativePath>
                    <OverwriteMode>Never</OverwriteMode>
                    <xsl:element name="FileContents" xml:space="preserve">using CLIClassLibrary.AirtableAPI;
using SSoTme.Default.Lib.CLIHandler;
using System;
using System.Collections.Generic;
using System.Text;
using CLIClassLibrary.RoleHandlers.ATDHandlers;
using CLIClassLibrary.RoleHandlers.RESTHandlers;

namespace CLIClassLibrary.RoleHandlers
{
    public static partial class RoleHandlerFactory
    {
        // you can put hand code for the role handler factor here - but ...
        // MOST of the important work here is done in the designer file that 
        // gets regenerated any time the 'rules' change.
    }
}
</xsl:element>
                </FileSetFile>
                <FileSetFile>
                    <RelativePath>
                        <xsl:text>RoleHandlerFactory.designer.cs</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve">using CLIClassLibrary.AirtableAPI;
using SSoTme.Default.Lib.CLIHandler;
using System;
using System.Collections.Generic;
using System.Text;
using CLIClassLibrary.RoleHandlers.ATDHandlers;
using CLIClassLibrary.RoleHandlers.RESTHandlers;

namespace CLIClassLibrary.RoleHandlers
{
    public static partial class RoleHandlerFactory
    {
        public static RoleHandlerBase CreateHandler(string runAs, string restEndpoint)
        {        
            if (String.IsNullOrEmpty(runAs)) runAs = EAPICLIHandler.GetCurrentUserRoleName();
            if (String.IsNullOrEmpty(restEndpoint)) return RoleHandlerFactory.CallATD(runAs);
            else return RoleHandlerFactory.InvokeRest(runAs, restEndpoint);
        }
        
        public static RoleHandlerBase InvokeRest(string runAs, string restEndpoint)
        {
            switch (runAs.ToLower())
            {
                <xsl:for-each select="$smq//SMQActors/SMQActor">
                    <xsl:variable name="role-name" select="Name" />
                case "<xsl:value-of select="translate(Name, $ucletters, $lcletters)" />":
                    return new <xsl:value-of select="$role-name" />RESTCLIHandler(restEndpoint);
</xsl:for-each>

                default:
                    throw new Exception($"Can't find CLIHandler for {runAs} actor.");
            }
        }
        
        public static RoleHandlerBase CallATD(string runAs) {
            ATDKey.ProjectName = "<xsl:value-of select="$baseId"/>";
            var atdKey = EAPICLIHandler.GetCurrentKey(runAs);
            switch (runAs.ToLower())
            {
                <xsl:for-each select="$smq//SMQActors/SMQActor">
                    <xsl:variable name="role-name" select="Name" />
                case "<xsl:value-of select="translate(Name, $ucletters, $lcletters)" />":
                    return new <xsl:value-of select="$role-name" />CLIHandler(atdKey);
</xsl:for-each>

                default:
                    throw new Exception($"Can't find CLIHandler for {runAs} actor.");
            }
        }

        public static void ListRoles(StringBuilder sbHelpBuilder)
        {
           <xsl:for-each select="$smq//SMQActors/SMQActor"><xsl:variable name="role-name" select="Name" />
            sbHelpBuilder.AppendLine(" - <xsl:value-of select="$role-name"/>");</xsl:for-each>
        }
    }
}
</xsl:element>
                </FileSetFile>
                <FileSetFile>

                    <RelativePath>../SassyMQ/StandardPayload.designer.cs</RelativePath>
                    <FileContents xml:space="preserve">
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
        <xsl:for-each select="$odxml//ObjectDefs/ObjectDef">
        public <xsl:value-of select="Name"/> <xsl:value-of select="Name"/> { get; set; }
        public List&lt;<xsl:value-of select="Name"/>> <xsl:value-of select="PluralName"/> { get; set; }
        </xsl:for-each>

    }
}</FileContents>
                </FileSetFile>
                <FileSetFile>
                    <RelativePath>../ATDMQ/EAPISecurityCleanupExtensions.designer.cs</RelativePath>
                    <FileContents>using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirtableDirect.CLI.Lib.DataClasses;
using dc=AirtableDirect.CLI.Lib.DataClasses;



namespace CLIClassLibrary.ATDMQ
{
    public static partial class EAPISecurityCleanupExtensions
    {
    <xsl:variable name="smql" select="document('SassyMQLexicon.smql')" />
    <xsl:variable name="actor-list" select="$smql//SMQActors/SMQActor" />
        <xsl:for-each select="$actor-list/Name"><xsl:variable name="actor" select="." /><xsl:variable name="not-guest" select="position() > 1" />
        <xsl:for-each select="$odxml//ObjectDefs/ObjectDef" xml:space="default">
            <xsl:variable name="od" select="." />
            <xsl:variable name="crud1" select="translate($od/*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
            <xsl:variable name="crud">
                <xsl:choose>
                    <xsl:when test="normalize-space($crud1) != ''">
                        <xsl:value-of select="$crud1" />
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:value-of select="translate($od/MatchingMetaData/*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:variable>
            <xsl:variable name="create" select="$crud = 'C' or string-length(substring-before($crud, 'C')) > 0 or string-length(substring-after($crud, 'C')) > 0" />
            <xsl:variable name="read" select="$crud = 'R' or string-length(substring-before($crud, 'R')) > 0 or string-length(substring-after($crud, 'R')) > 0" />
            <xsl:variable name="update" select="$crud = 'U' or string-length(substring-before($crud, 'U')) > 0 or string-length(substring-after($crud, 'U')) > 0" />
            <xsl:variable name="delete" select="$crud = 'D' or string-length(substring-before($crud, 'D')) > 0 or string-length(substring-after($crud, 'D')) > 0" />
            <xsl:variable name="emailToken" select="concat('$', $user-name, '.', $email-address-name, '$')" />
            <xsl:variable name="where1" select="normalize-space($od/MatchingMetaData/*[name() = concat($actor, 'Where')])" />
            <xsl:variable name="where">
            <xsl:call-template name="string-replace-all">
                <xsl:with-param name="text" select="$where1" />
                <xsl:with-param name="replace" select="$emailToken" />
                <xsl:with-param name="by" select="'@0'" />
            </xsl:call-template>
                </xsl:variable>
        // <xsl:value-of select="$actor"/> Cleaning Extension Methods.  -<xsl:value-of select="$crud"/>-
        <xsl:if test="$create or $read or $update or $delete">
        // <xsl:value-of select="$od/Name"/>
        </xsl:if>
        <xsl:variable name="pds" select="PropertyDefs/PropertyDef" />
        <xsl:variable name="create-pds1">
            <AllPds>
            <xsl:for-each select="$pds">
                <xsl:variable name="pd-crud1" select="translate(*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud2" select="translate(MatchingMetaData/*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud">
                    <xsl:choose>
                        <xsl:when test="translate(normalize-space(IsReadonly), $ucletters, $lcletters) ='1'"></xsl:when>
                        <xsl:when test="normalize-space($pd-crud1) != ''">
                            <xsl:value-of select="$pd-crud1"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="$pd-crud2"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:variable>
                <xsl:if test="$pd-crud = 'C' or string-length(substring-before($pd-crud, 'C')) > 0 or string-length(substring-after($pd-crud, 'C')) > 0">
                    <xsl:copy-of select="." />
                </xsl:if>
            </xsl:for-each>
        </AllPds>
        </xsl:variable> 
        <xsl:variable name="create-pds" select="msxsl:node-set($create-pds1)/*" />
            
        <xsl:variable name="read-pds1">

            <AllPds>
            <xsl:for-each select="$pds">
                <xsl:variable name="pd-crud1" select="translate(*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud2" select="translate(MatchingMetaData/*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud">
                    <xsl:choose>
                        <xsl:when test="normalize-space($pd-crud1) != ''">
                            <xsl:value-of select="$pd-crud1"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="$pd-crud2"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:variable>
                <xsl:if test="$pd-crud = 'R' or string-length(substring-before($pd-crud, 'R')) > 0 or string-length(substring-after($pd-crud, 'R')) > 0">
                    <xsl:copy-of select="." />
                </xsl:if>
            </xsl:for-each>
             </AllPds>
       </xsl:variable>
        <xsl:variable name="read-pds" select="msxsl:node-set($read-pds1)/*" />
            
        <xsl:variable name="update-pds1">
            <AllPds>
            <xsl:for-each select="$pds">
                <xsl:variable name="pd-crud1" select="translate(*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud2" select="translate(MatchingMetaData/*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud">
                    <xsl:choose>
                        <xsl:when test="translate(normalize-space(IsReadonly), $ucletters, $lcletters) ='1'"></xsl:when>
                        <xsl:when test="normalize-space($pd-crud1) != ''">
                            <xsl:value-of select="$pd-crud1"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="$pd-crud2"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:variable>
                <xsl:if test="$pd-crud = 'U' or string-length(substring-before($pd-crud, 'U')) > 0 or string-length(substring-after($pd-crud, 'U')) > 0">
                    <xsl:copy-of select="." />
                </xsl:if>
            </xsl:for-each>
                </AllPds>
        </xsl:variable>
        <xsl:variable name="update-pds" select="msxsl:node-set($update-pds1)/*" />
            
        <xsl:variable name="delete-pds1">
            <AllPds>
            <xsl:for-each select="$pds">
                <xsl:variable name="pd-crud1" select="translate(*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud2" select="translate(MatchingMetaData/*[name() = $actor or name() = concat($actor, 'CRUD')], $lcletters, $ucletters)" />
                <xsl:variable name="pd-crud">
                    <xsl:choose>
                        <xsl:when test="normalize-space($pd-crud1) != ''">
                            <xsl:value-of select="$pd-crud1"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="$pd-crud2"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:variable>
                <xsl:if test="$pd-crud = 'D' or string-length(substring-before($pd-crud, 'D')) > 0 or string-length(substring-after($pd-crud, 'D')) > 0">
                    <xsl:copy-of select="." />
                </xsl:if>
            </xsl:for-each>
                </AllPds>
        </xsl:variable>
        <xsl:variable name="delete-pds" select="msxsl:node-set($delete-pds1)/*" />
            
        <xsl:if test="$create">
        public static dc.<xsl:value-of select="$od/Name"/>&#32;<xsl:value-of select="$actor"/>CleanForAdd(this dc.<xsl:value-of select="$od/Name"/> clean<xsl:value-of select="$od/Name"/>)
        {
            var <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/> = default(dc.<xsl:value-of select="$od/Name"/>);

            if (!ReferenceEquals(clean<xsl:value-of select="$od/Name"/>, null))
            {
                <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/> = new dc.<xsl:value-of select="$od/Name"/>()
                {
                    <xsl:for-each select="$create-pds/*"> // default value: <xsl:value-of select="MatchingMetaData/DefaultValue"/>. 
                    <xsl:value-of select="Name" /> = clean<xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name" /><xsl:if test="position() &lt; count($create-pds/*)">,
                    </xsl:if></xsl:for-each>
                };
                <xsl:for-each select="$pds[normalize-space(MatchingMetaData/DefaultValue) != '']">
                    <xsl:choose>
                        <xsl:when test="(translate(normalize-space(DataType),$lcletters, $ucletters)='TEXT' or translate(normalize-space(DataType),$lcletters, $ucletters)='STRING') and IsCollection=0">
                if (String.IsNullOrEmpty(<xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/>))
                {
                    <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/> = "<xsl:value-of select="MatchingMetaData/DefaultValue"/>";
                }</xsl:when>
                        <xsl:when test="(translate(normalize-space(DataType),$lcletters, $ucletters)='TEXT' or translate(normalize-space(DataType),$lcletters, $ucletters)='STRING') and IsCollection!=0">
                if (<xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/> is null)
                {
                    <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/> = "<xsl:value-of select="MatchingMetaData/DefaultValue"/>".Split(",");
                }</xsl:when>
                        <xsl:when test="translate(normalize-space(DataType),$lcletters, $ucletters)='INT32' or 
                                            translate(normalize-space(DataType),$lcletters, $ucletters)='INT64' or
                                            translate(normalize-space(DataType),$lcletters, $ucletters)='DECIMAL'">
                if (<xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/> == 0)
                {
                    <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/> = <xsl:value-of select="MatchingMetaData/DefaultValue"/>;
                }</xsl:when>
                        <xsl:when test="translate(normalize-space(DataType),$lcletters, $ucletters)='DATE' or 
                                            translate(normalize-space(DataType),$lcletters, $ucletters)='DATETIME'">
                if (<xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/> == DateTime.MinValue)
                {
                    <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name"/> = DateTime.UtcNow;
                }           
                        </xsl:when>
                    </xsl:choose>
                </xsl:for-each>
            }

            return <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>;
        }
        
        </xsl:if>
        <xsl:if test="$read">
        public static List&lt;dc.<xsl:value-of select="$od/Name"/>>&#32;<xsl:value-of select="$actor"/>CleanForGet(this IEnumerable&lt;dc.<xsl:value-of select="$od/Name"/>> clean<xsl:value-of select="$od/PluralName"/>)
        {
            return clean<xsl:value-of select="$od/PluralName"/>.Select(<xsl:value-of select="$od/Name"/> => <xsl:value-of select="$od/Name"/>.<xsl:value-of select="$actor"/>CleanForGet())
                            .ToList();
        }
        
        public static dc.<xsl:value-of select="$od/Name"/>&#32;<xsl:value-of select="$actor"/>CleanForGet(this dc.<xsl:value-of select="$od/Name"/> clean<xsl:value-of select="$od/Name"/>)
        {
            var <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/> = default(dc.<xsl:value-of select="$od/Name"/>);

            if (!ReferenceEquals(clean<xsl:value-of select="$od/Name"/>, null))
            {
                <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/> = new dc.<xsl:value-of select="$od/Name"/>()
                {
                    <xsl:for-each select="$read-pds/*"> 
                    <xsl:value-of select="Name" /> = clean<xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name" /><xsl:if test="position() &lt; count($read-pds/*)">,
                    </xsl:if></xsl:for-each>
                };
            }

            return <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>;
        }
        
        </xsl:if>
        <xsl:if test="$update">
        public static dc.<xsl:value-of select="$od/Name"/>&#32;<xsl:value-of select="$actor"/>CleanForUpdate(this dc.<xsl:value-of select="$od/Name"/> clean<xsl:value-of select="$od/Name"/>)
        {
            var <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/> = default(dc.<xsl:value-of select="$od/Name"/>);

            if (!ReferenceEquals(clean<xsl:value-of select="$od/Name"/>, null))
            {
                <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/> = new dc.<xsl:value-of select="$od/Name"/>()
                {
                    <xsl:for-each select="$update-pds/*"> 
                    <xsl:value-of select="Name" /> = clean<xsl:value-of select="$od/Name"/>.<xsl:value-of select="Name" /><xsl:if test="position() &lt; count($update-pds/*)">,
                    </xsl:if></xsl:for-each>
                };
            }

            return <xsl:value-of select="$actor"/><xsl:value-of select="$od/Name"/>;
        }
</xsl:if></xsl:for-each></xsl:for-each>
    }
}
</FileContents>
                </FileSetFile>
            </FileSetFiles>
        </FileSet>
    </xsl:template>

    <xsl:template name="get-actors">
        <xsl:param name="list" />
        <xsl:variable name="actor" select="substring-before(concat($list, ','), ',')" />
        <SMQActor>
            <Name>
                <xsl:value-of select="$actor"/>
            </Name>
            <ParentActor>
                <xsl:value-of select="$crud-coordinator"/>
            </ParentActor>
        </SMQActor>
        <xsl:variable name="after" select="substring-after($list, ',')" />
        <xsl:if test="string-length($after) > 0">
            <xsl:call-template name="get-actors">
                <xsl:with-param name="list" select="$after" />
            </xsl:call-template>
        </xsl:if>
    </xsl:template>


    <xsl:template match="RAWValues/Payload" mode="pluralize">
        <xsl:value-of select="$odxml//ObjectDefs/ObjectDef[Name=current()]/PluralName"/>
    </xsl:template>

    <xsl:template name="escape-curly-braces">
        <xsl:param name="input-string"/>
        <xsl:choose>
            <!-- Look for '{' and replace it with '{{' -->
            <xsl:when test="contains($input-string, '{')">
                <xsl:value-of select="substring-before($input-string, '{')"/>
                <xsl:text>{{</xsl:text>
                <xsl:call-template name="escape-curly-braces">
                    <xsl:with-param name="input-string" select="substring-after($input-string, '{')"/>
                </xsl:call-template>
            </xsl:when>
            <!-- Look for '}' and replace it with '}}' -->
            <xsl:when test="contains($input-string, '}')">
                <xsl:value-of select="substring-before($input-string, '}')"/>
                <xsl:text>}}</xsl:text>
                <xsl:call-template name="escape-curly-braces">
                    <xsl:with-param name="input-string" select="substring-after($input-string, '}')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$input-string"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

</xsl:stylesheet>
