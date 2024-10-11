<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                >
    <xsl:output method="xml" indent="yes"/>
    <xsl:include href="../CommonXsltTemplates.xslt"/>
    <xsl:param name="output-filename" select="'output.txt'" />
    <xsl:variable name="smql" select="document('SassyMQLexicon.smql')" />
    <xsl:variable name="odxml" select="document('DataSchema.odxml')" />
    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/*">
        <FileSet>
            <FileSetFiles>
                <xsl:for-each select="$smql//SMQActors/SMQActor">
                    <xsl:variable name="actor" select="." />
                    <xsl:for-each select="ActorCanSay/SMQMessageKey">
                        <xsl:variable name="msg" select="$smql//SMQMessages/SMQMessage[SMQMessageKey=current()]" />
                        <xsl:if test="translate($msg/Category, $ucletters, $lcletters) = 'custom'">
                        <FileSetFile>
                            <RelativePath>
                                <xsl:value-of select="$actor/Name"/>
                                <xsl:text>/</xsl:text>
                                <xsl:value-of select="$msg/Name"/>
                                <xsl:text>Controller.cs</xsl:text>
                            </RelativePath>
                            <OverwriteMode>Never</OverwriteMode>
                            <xsl:element name="FileContents" xml:space="preserve">using Auth0.AuthenticationApi.Models;
using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Mvc;

using CLIClassLibrary.ATDMQ;
using AirtableDirect.CLI.Lib.DataClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using Swan.Formatters;
using YP.SassyMQ.Lib.RabbitMQ;

namespace ASPNet_REST_API.Controllers.Admin
{
    [ApiController]
    [Route("/<xsl:value-of select="$actor/Name"/>")]
    public class <xsl:value-of select="$msg/Name"/>Controller : Controller
    {
<xsl:if test="translate(normalize-space($actor/Name), $ucletters, $lcletters) != 'guest'">
        //[Authorize(Roles = "<xsl:value-of select="$actor/Name"/>", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]</xsl:if>
        [HttpPost("<xsl:value-of select="$msg/Name"/>")]
        public IActionResult <xsl:value-of select="$msg/Name"/>(StandardPayload payload)
        {
            try
            {
                ATD<xsl:value-of select="$actor/Name"/> atd<xsl:value-of select="$actor/Name"/> = new ATD<xsl:value-of select="$actor/Name"/>();
                atd<xsl:value-of select="$actor/Name"/>.EmailAddress = this.User.Identity.Name;
                atd<xsl:value-of select="$actor/Name"/>.UserIdentity = this.User.Identities.FirstOrDefault();
                payload.AccessToken = atd<xsl:value-of select="$actor/Name"/>.AccessToken;
                atd<xsl:value-of select="$actor/Name"/>.<xsl:value-of select="$msg/Name"/>(payload);
            }
            catch (Exception ex)
            {
                payload.ErrorMessage = ex.Message;                
            }
            payload.AccessToken = null;
            var json = JsonConvert.SerializeObject(payload, Formatting.Indented, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            return Content(json, "application/json");

        }
    }
}
                    </xsl:element>
                        </FileSetFile>
                        </xsl:if>
                        </xsl:for-each>
                    <xsl:for-each select="$odxml//ObjectDefs/ObjectDef">
                        <xsl:variable name="od" select="." />
                        <xsl:if test="count($smql//SMQMessage[ActorFrom=$actor/Name and (RAWValues/Payload = $od/Name or RAWValues/Response = $od/Name)])>0">
                <FileSetFile>
                    <RelativePath>
                        <xsl:value-of select="$actor/Name"/>
                        <xsl:text>/</xsl:text>
                        <xsl:value-of select="$actor/Name"/>
                        <xsl:value-of select="$od/PluralName"/>
                        <xsl:text>Controller.cs</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve">using Auth0.AuthenticationApi.Models;
using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Mvc;

using CLIClassLibrary.ATDMQ;
using AirtableDirect.CLI.Lib.DataClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using Swan.Formatters;
using YP.SassyMQ.Lib.RabbitMQ;

namespace ASPNet_REST_API.Controllers.Admin
{
    [ApiController]
    [Route("/<xsl:value-of select="$actor/Name"/>")]
    public class <xsl:value-of select="$actor/Name"/><xsl:value-of select="$od/PluralName"/>Controller : Controller
    {<xsl:if test="$actor/ActorCanSay/SMQMessageKey[string-length(substring-after(text(), concat($actor/LowerName,'.get',translate($od/PluralName, $ucletters, $lcletters))))>0]">
        
<xsl:if test="normalize-space($actor/Name) != 'Guest'">
        //[Authorize(Roles = "<xsl:value-of select="$actor/Name"/>", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]</xsl:if>
        [HttpGet("<xsl:value-of select="$od/PluralName"/>")]
        public IActionResult Index(string airtableWhere = null, string view = "Grid%20view", int maxPages = 5)
        {
            var results = new List&lt;<xsl:value-of select="$od/Name"/>>();
            try
            {
                ATD<xsl:value-of select="$actor/Name"/> atd<xsl:value-of select="$actor/Name"/> = new ATD<xsl:value-of select="$actor/Name"/>();
                atd<xsl:value-of select="$actor/Name"/>.EmailAddress = this.User.Identity.Name;
                atd<xsl:value-of select="$actor/Name"/>.UserIdentity = this.User.Identities.FirstOrDefault();
                var payload = atd<xsl:value-of select="$actor/Name"/>.CreatePayload("{}", airtableWhere, view, maxPages);
                results = atd<xsl:value-of select="$actor/Name"/>.Get<xsl:value-of select="$od/PluralName"/>(payload)?.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding <xsl:value-of select="$od/PluralName"/>: {ex.Message}", ex);
            }
            var json = JsonConvert.SerializeObject(results, Formatting.Indented);

            return Content(json, "application/json");

        }</xsl:if>
                        
<xsl:if test="$actor/ActorCanSay/SMQMessageKey[string-length(substring-after(text(), concat($actor/LowerName,'.add',translate($od/Name, $ucletters, $lcletters))))>0]">
    <xsl:if test="normalize-space($actor/Name) != 'Guest'">
        //[Authorize(Roles = "<xsl:value-of select="$actor/Name"/>", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]</xsl:if>
        [HttpPost("<xsl:value-of select="$od/PluralName"/>")]
        [HttpPost("<xsl:value-of select="$od/Name"/>")]
        public IActionResult Add(string airtableWhere = null, string view  = "Grid%20view", int maxPages = 5)
        {
            var result = default(<xsl:value-of select="$od/Name"/>);
            try
            {
                ATD<xsl:value-of select="$actor/Name"/> atd<xsl:value-of select="$actor/Name"/> = new ATD<xsl:value-of select="$actor/Name"/>();
                atd<xsl:value-of select="$actor/Name"/>.EmailAddress = this.User.Identity.Name;
                atd<xsl:value-of select="$actor/Name"/>.UserIdentity = this.User.Identities.FirstOrDefault();
                using (var reader = new StreamReader(this.Request.Body))
                {
                    var body = reader.ReadToEndAsync().GetAwaiter().GetResult();
                    var payload = atd<xsl:value-of select="$actor/Name"/>.CreatePayload("{}", airtableWhere, view, maxPages);
                    var bodyAsPayload = JsonConvert.DeserializeObject&lt;StandardPayload>(body);
                    if (bodyAsPayload != null) payload.<xsl:value-of select="$od/Name"/> = bodyAsPayload.<xsl:value-of select="$od/Name"/>.<xsl:value-of select="$actor/Name"/>CleanForAdd();
                    if (payload.<xsl:value-of select="$od/Name"/> is null) payload.<xsl:value-of select="$od/Name"/> = JsonConvert.DeserializeObject&lt;<xsl:value-of select="$od/Name"/>>(body).<xsl:value-of select="$actor/Name"/>CleanForAdd();
                    result = atd<xsl:value-of select="$actor/Name"/>.Add<xsl:value-of select="$od/Name"/>(payload).<xsl:value-of select="$actor/Name"/>CleanForGet();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding <xsl:value-of select="$od/PluralName"/>: {ex.Message}", ex);
            }
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);

            return Content(json, "application/json");

        }</xsl:if>
                        
<xsl:if test="$actor/ActorCanSay/SMQMessageKey[string-length(substring-after(text(), concat($actor/LowerName,'.update',translate($od/Name, $ucletters, $lcletters))))>0]">
    <xsl:if test="normalize-space($actor/Name) != 'Guest'">
        //[Authorize(Roles = "<xsl:value-of select="$actor/Name"/>", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]</xsl:if>
        [HttpPut("<xsl:value-of select="$od/Name"/>")]
        public IActionResult Update(string airtableWhere = null, string view  = "Grid%20view", int maxPages = 5)
        {
            var result = new <xsl:value-of select="$od/Name"/>();
            try
            {
                using (var reader = new StreamReader(this.Request.Body))
                {
                    var body = reader.ReadToEndAsync().GetAwaiter().GetResult();
                    ATD<xsl:value-of select="$actor/Name"/> atd<xsl:value-of select="$actor/Name"/> = new ATD<xsl:value-of select="$actor/Name"/>();
                    atd<xsl:value-of select="$actor/Name"/>.EmailAddress = this.User.Identity.Name;
                    var payload = atd<xsl:value-of select="$actor/Name"/>.CreatePayload("{}", airtableWhere, view, maxPages);
                    var bodyAsPayload = JsonConvert.DeserializeObject&lt;StandardPayload>(body);
                    if (bodyAsPayload != null) payload.<xsl:value-of select="$od/Name"/> = bodyAsPayload.<xsl:value-of select="$od/Name"/>.<xsl:value-of select="$actor/Name"/>CleanForAdd();
                    if (payload.<xsl:value-of select="$od/Name"/> is null) payload.<xsl:value-of select="$od/Name"/> = JsonConvert.DeserializeObject&lt;<xsl:value-of select="$od/Name"/>>(body).<xsl:value-of select="$actor/Name"/>CleanForAdd();
                    result = atd<xsl:value-of select="$actor/Name"/>.Update<xsl:value-of select="$od/Name"/>(payload)?.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating <xsl:value-of select="$od/PluralName"/>: {ex.Message}", ex);
            }
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);

            return Content(json, "application/json");

        }</xsl:if>
                        
<xsl:if test="$actor/ActorCanSay/SMQMessageKey[string-length(substring-after(text(), concat($actor/LowerName,'.delete',translate($od/Name, $ucletters, $lcletters))))>0]">
    <xsl:if test="normalize-space($actor/Name) != 'Guest'">
        //[Authorize(Roles = "<xsl:value-of select="$actor/Name"/>", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]</xsl:if>
        [HttpDelete("<xsl:value-of select="$od/Name"/>")]
        public IActionResult Delete(string id)
        {
            try {
                ATD<xsl:value-of select="$actor/Name"/> atd<xsl:value-of select="$actor/Name"/> = new ATD<xsl:value-of select="$actor/Name"/>();
                atd<xsl:value-of select="$actor/Name"/>.EmailAddress = this.User.Identity.Name;
                var payload = atd<xsl:value-of select="$actor/Name"/>.CreatePayload();
                payload.<xsl:value-of select="$od/Name"/> = new <xsl:value-of select="$od/Name"/>() { <xsl:value-of select="$od/PropertyDefs/PropertyDef[IsPrimaryKey]/Name"/> = id };
                atd<xsl:value-of select="$actor/Name"/>.Delete<xsl:value-of select="$od/Name"/>(payload);
                var json = JsonConvert.SerializeObject(true, Formatting.Indented);
                return Content(json, "application/json");    
            } 
            catch (Exception ex) {
                throw new Exception($"Error updating <xsl:value-of select="$od/PluralName"/>: {ex.Message}", ex);
            }
        }</xsl:if>                        
    }
}
                    </xsl:element>
                </FileSetFile>
                        </xsl:if>
                    </xsl:for-each>
                </xsl:for-each>
            </FileSetFiles>
        </FileSet>
    </xsl:template>
</xsl:stylesheet>
