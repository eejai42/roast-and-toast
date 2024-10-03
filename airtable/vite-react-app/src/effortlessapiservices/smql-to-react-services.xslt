<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                >
    <xsl:output method="xml" indent="yes"/>
    <xsl:include href="../CommonXsltTemplates.xslt"/>
    <xsl:param name="output-filename" select="'output.txt'" />
    <xsl:variable name="odxml" select="document('DataSchema.odxml')"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/*">
        <FileSet>
            <FileSetFiles>
                <xsl:for-each select="/*/SMQActors/SMQActor"><xsl:variable name="actor" select="."/>
                    <FileSetFile>
                        <RelativePath>
                            <xsl:value-of select="concat(Name, 'Service.tsx')"/>
                        </RelativePath>
                        <xsl:element name="FileContents" xml:space="preserve">
import BaseService from './BaseService';
<xsl:for-each select="$odxml//ObjectDefs/ObjectDef">
import { <xsl:value-of select="Name"/> } from '../models/<xsl:value-of select="Name"/>';
</xsl:for-each>

class <xsl:value-of select="concat(Name, 'Service')"/> extends BaseService {
    <xsl:for-each select="ActorCanSay/SMQMessageKey"><xsl:for-each select="/*/SMQMessages/SMQMessage[SMQMessageKey=current()]">
        <xsl:variable name="msg" select="."/><xsl:variable name="od" select="$odxml//ObjectDefs/ObjectDef[Name = $msg/RAWValues/Response or Name = $msg/RAWValues/Payload]" />
    <xsl:choose>
    <xsl:when test="$msg/RAWValues/Category='Custom'">
    async <xsl:value-of select="$msg/Name" />(payload:Object|null) : Promise&lt;<xsl:value-of select="$od/Name"/>&gt; { 
        return this.apiCall("POST", "<xsl:value-of select="$actor/Name"/>", "<xsl:value-of select="$msg/PluralName" />", "", payload, null); // <xsl:value-of select="$od/Name"/></xsl:when>
    <xsl:when test="$msg/RAWValues/Category='CRUD' and substring($msg/Name, 1, 3) = 'Get'">
    async Get<xsl:value-of select="$od/PluralName" />(view:string = 'Grid%20view', airtableWhere :string = '') : Promise&lt;<xsl:value-of select="$od/Name"/>[]&gt;  { 
        return this.apiCall("GET", "<xsl:value-of select="$actor/Name"/>", "<xsl:value-of select="$od/PluralName" />", view, null, airtableWhere, null); // <xsl:value-of select="$od/Name"/>
    }        
    async Get<xsl:value-of select="$od/Name" />(id:string)  : Promise&lt;<xsl:value-of select="$od/Name"/>|null&gt; { 
        var db<xsl:value-of select="$od/PluralName" /> : any = await this.apiCall("GET", "<xsl:value-of select="$actor/Name"/>", "<xsl:value-of select="$od/PluralName" />", null, null, "RECORD_ID()='" + id + "'", null); // <xsl:value-of select="$od/Name"/>
        if (!db<xsl:value-of select="$od/PluralName" /> || (db<xsl:value-of select="$od/PluralName" />.length == 0)) return null;
        return db<xsl:value-of select="$od/PluralName" />[0];
        </xsl:when>
    <xsl:when test="$msg/RAWValues/Category='CRUD' and substring($msg/Name, 1, 3) = 'Add'">
    async <xsl:value-of select="$msg/Name" />(<xsl:value-of select="$od/Name"/>:<xsl:value-of select="$od/Name"/>) : Promise&lt;<xsl:value-of select="$od/Name"/>&gt;  { 
        return this.apiCall("POST", "<xsl:value-of select="$actor/Name"/>", "<xsl:value-of select="$od/PluralName" />", null, <xsl:value-of select="$od/Name"/>, null, null); // <xsl:value-of select="$od/Name"/> 1</xsl:when>
    <xsl:when test="$msg/RAWValues/Category='CRUD' and substring($msg/Name, 1, 6) = 'Update'">
    async <xsl:value-of select="$msg/Name" />(<xsl:value-of select="$od/Name"/>:<xsl:value-of select="$od/Name"/>)  : Promise&lt;<xsl:value-of select="$od/Name"/>&gt; {
        return this.apiCall("PUT", "<xsl:value-of select="$actor/Name"/>", "<xsl:value-of select="$od/Name" />", null, <xsl:value-of select="$od/Name"/>, null, null); // <xsl:value-of select="$od/Name"/> 2</xsl:when>
    <xsl:when test="$msg/RAWValues/Category='CRUD' and substring($msg/Name, 1, 6) = 'Delete'">
    async <xsl:value-of select="$msg/Name" />(id:string)  : Promise&lt;<xsl:value-of select="$od/Name"/>&gt; { 
        return this.apiCall("DELETE", "<xsl:value-of select="$actor/Name"/>", "<xsl:value-of select="$od/Name" />", "", id, null, null); // <xsl:value-of select="$od/Name"/></xsl:when>
</xsl:choose>
   }
    
    </xsl:for-each></xsl:for-each>
}

export default new <xsl:value-of select="concat(Name, 'Service')"/>();</xsl:element>
                    </FileSetFile>                    
                </xsl:for-each>
                <FileSetFile>
                    <RelativePath>
                        <xsl:text>BaseService.tsx</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve">// effortlessapiservices/BaseService.tsx
class BaseService {
    async apiCall(method :string = "GET", controller:string = "User", endpoint :string = "AppUsers", view :string|null = "Grid%20view", payload :Object|null, airtableWhere :string|null, params :string|null) {
    try {
        const token = localStorage.getItem("access_token");
  
        airtableWhere = airtableWhere ? `&amp;airtableWhere=${airtableWhere}` : '';
        var url;
        view = view ? `&amp;view=${view}` : '';
        if (params) {
            url = `https://localhost:42123/${controller}/${endpoint}?${params}${airtableWhere}`;
        } else {
            url = `https://localhost:42123/${controller}/${endpoint}?${view}${airtableWhere}`;
            if (method == "DELETE") {
                url = `https://localhost:42123/${controller}/${endpoint}?id=${payload}`;
            } 
        }

        const response = await fetch(url, {
          method: method,
          headers: { "Content-Type": "application/json", Authorization: `Bearer ${token}` },
          body: payload ? JSON.stringify(payload) : null,
        });
        if (!response.ok) {
          throw new Error(`Failed to ${method} ${endpoint} ${JSON.stringify(response)}`);
        }
  
        return await response.json();
      } catch (ex) {
        console.error('Error: ', ex);
      }
    }
  }
  
  export default BaseService;
  

</xsl:element>
                </FileSetFile>
            </FileSetFiles>
        </FileSet>
    </xsl:template>
</xsl:stylesheet>
