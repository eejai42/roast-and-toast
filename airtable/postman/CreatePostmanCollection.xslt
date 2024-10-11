<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                >
    <xsl:output method="xml" indent="yes"/>
    <xsl:include href="../CommonXsltTemplates.xslt"/>
    <xsl:param name="output-filename" select="'output.txt'" />
    <xsl:variable name="smql" select="document('SassyMQLexicon.smql')" />
    <xsl:variable name="odxml" select="document('DataSchema.odxml')" />
    <xsl:param name="api-root" select="'https://routinely-humorous-skunk.ngrok-free.app/'" />
    <xsl:param name="user-name" select="'ApiUser'" />
    <xsl:param name="email-address-name" select="'TicTacToeEmailAddress'" />
    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

	 <!-- Template to extract protocol from the URL -->
	 <xsl:template name="get-protocol">
        <xsl:param name="url"/>
        <xsl:value-of select="substring-before($url, '://')"/>
    </xsl:template>

    <!-- Template to recursively tokenize and extract hosts from the URL -->
    <xsl:template name="get-host">
        <xsl:param name="url"/>
        <xsl:choose>
            <xsl:when test="contains($url, ':')">
			<xsl:value-of select="substring-before($url, ':')"/>
            </xsl:when>
            <xsl:otherwise>
			<xsl:value-of select="$url"/>
		</xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- Template to extract port from the URL -->
    <xsl:template name="get-port">
        <xsl:param name="url"/>
        <xsl:variable name="protocol">
            <xsl:call-template name="get-protocol">
                <xsl:with-param name="url" select="$url" />
            </xsl:call-template>
        </xsl:variable>
        <xsl:value-of select="substring-after($url, ':')"/>
        <xsl:choose>
            <xsl:when test="contains($url, ':')">
                <xsl:value-of select="substring-before(substring-after($url, ':'), '/')"/>
            </xsl:when>
            <xsl:when test="$protocol='http'">80</xsl:when>
            <xsl:otherwise>443</xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- Template to recursively tokenize and extract path from the URL -->
    <xsl:template name="get-path">
        <xsl:param name="url"/>
        <xsl:choose>
            <xsl:when test="contains($url, '/')">
			<xsl:text>"</xsl:text>
                <xsl:value-of select="substring-before($url, '/')"/>
			<xsl:text>"</xsl:text>
			<xsl:text>,</xsl:text>
                <xsl:call-template name="get-path">
                    <xsl:with-param name="url" select="substring-after($url, '/')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
			<xsl:text>"</xsl:text>
			<xsl:value-of select="$url"/>
			<xsl:text>"</xsl:text>
		</xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template match="/*">
        <FileSet>
            <FileSetFiles>
				<FileSetFile>
                    <RelativePath>
                        <xsl:value-of select="$odxml/*/Name" />
                        <xsl:text>-postman_environment.json</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve">{
	"name": "<xsl:value-of select="$odxml/*/Name" />",
	"values": [
		{
			"key": "protocol",
			"value": "https",
			"type": "default",
			"enabled": true
		},
		{
			"key": "host",
			"value": "localhost",
			"type": "default",
			"enabled": true
		},
		{
			"key": "port",
			"value": "443",
			"type": "default",
			"enabled": true
		},
		{
			"key": "accessToken",
			"value": "eyJwdkfw...your-access-token-here-123",
			"type": "default",
			"enabled": true
		}
	],
	"_postman_variable_scope": "environment"
}
</xsl:element>
				</FileSetFile>					
                <FileSetFile>
                    <RelativePath>
                        <xsl:value-of select="$odxml/*/Name" />
                        <xsl:text>-postman.json</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve">{
	"info": {
		"name": "<xsl:value-of select="$odxml/*/Name" />",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
	},
	"item": [
    		
			{
				"name": "Authentication",
				"item": [
					{
				"name": "WhoAmI",
				"request": {
					"method": "GET",
					"header": [],
					"url": {
						"raw": "{{protocol}}://{{host}}:{{port}}/Account/WhoAmI",
						"protocol": "{{protocol}}",
						"host": [
							"{{host}}"
						],
						"port": "{{port}}",
						"path": [
							"Account",
							"WhoAmI"
						]
					}
				},
				"response": []
			},
            {
					"name": "Logout",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "{{protocol}}://{{host}}:{{port}}/PKCE/Logout",
							"protocol": "{{protocol}}",
							"host": [
								"{{host}}"
							],
							"port": "{{port}}",
							"path": [
								"PKCE",
								"Logout"
							]
						}
					},
					"response": []
				},
            {
					"name": "Get Login Url",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "{{protocol}}://{{host}}:{{port}}/PKCE/LoginUrl",
							"protocol": "{{protocol}}",
							"host": [
								"{{host}}"
							],
							"port": "{{port}}",
							"path": [
								"PKCE",
								"LoginUrl"
							]
						}
					},
					"response": []
				},
				{
						"name": "Exchange Token",
						"request": {
							"method": "POST",
							"header": [],
							"url": {
								"raw": "{{protocol}}://{{host}}:{{port}}/PKCE/exchange",
								"protocol": "{{protocol}}",
								"host": [
									"{{host}}"
								],
								"port": "{{port}}",
								"path": [
									"PKCE",
									"exchange"
								]
							}
						},
						"response": []
					}
			]
		},
                            <xsl:variable name="smql" select="document('SassyMQLexicon.smql')" />
    <xsl:variable name="actor-list" select="$smql//SMQActors/SMQActor" />
        <xsl:for-each select="$actor-list/Name">
            <xsl:variable name="actor" select="." /><xsl:if test="position()>1">,</xsl:if>
		{
			"name": "<xsl:value-of select="$actor"/>",
			"item": [
            {
					"name": "WhoAmI",
					"request": {
						"method": "GET",
						"header": [],
						"url": {
							"raw": "{{protocol}}://{{host}}:{{port}}/Account/WhoAmI",
							"protocol": "{{protocol}}",
							"host": [
								"{{host}}"
							],
							"port": "{{port}}",
							"path": [
								"Account",
								"WhoAmI"
							]
						}
					},
					"response": []
				}
                    <xsl:for-each select="$odxml//ObjectDefs/ObjectDef" xml:space="default">
                        <xsl:variable name="od" select="." />
                        <xsl:variable name="url">
                            <xsl:value-of select="$api-root" /><xsl:value-of select="translate($actor, $ucletters, $lcletters)"/>/<xsl:value-of select="translate($od/PluralName, $ucletters, $lcletters)" /></xsl:variable>
			<xsl:variable name="not-guest" select="position() > 1" />
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
        <!--
        <xsl:if test="$create or $read or $update or $delete">
            // <xsl:value-of select="$od/Name"/> <xsl:value-of select="$actor"/> Cleaning Extension Methods.  -<xsl:value-of select="$crud"/></xsl:if>
		-->
            
        <xsl:if test="$create">
            ,{
				"name": "Add <xsl:value-of select="$od/Name" />",
				"request": {
					"method": "POST",
					"header": [],
					"url": {
						"raw": "<xsl:value-of select="$url" />",
						"protocol": "{{protocol}}",
						"host": ["{{host}}"],
						"port": "{{port}}",
						"path": [<xsl:call-template name="get-path">
							<xsl:with-param name="url" select="concat($actor, '/', $od/Name)"/>
						</xsl:call-template>]
					}
				},
				"response": []
			}     
        </xsl:if>
        <xsl:if test="$read">
            
            ,{
				"name": "Get <xsl:value-of select="$od/PluralName" />",
				"request": {
					"method": "GET",
					"header": [],
					"url": {
						"raw": "<xsl:value-of select="$url" />",
						"protocol": "{{protocol}}",
						"host": ["{{host}}"],
						"port": "{{port}}",
						"path": [<xsl:call-template name="get-path">
							<xsl:with-param name="url" select="concat($actor, '/', $od/PluralName)"/>
						</xsl:call-template>]
					}
				},
				"response": []
			}
            ,{
				"name": "Get <xsl:value-of select="$od/Name" />",
				"request": {
					"method": "GET",
					"header": [],
					"url": {
						"raw": "<xsl:value-of select="$url" />",
						"protocol": "{{protocol}}",
						"host": ["{{host}}"],
						"port": "{{port}}",
						"path": [<xsl:call-template name="get-path">
							<xsl:with-param name="url" select="concat($actor, '/', $od/Name)"/>
						</xsl:call-template>]
					}
				},
				"response": []
			}
        </xsl:if>
        <xsl:if test="$update">
          ,{
				"name": "Update <xsl:value-of select="$od/Name" />",
				"request": {
					"method": "PUT",
					"header": [],
					"url": {
						"raw": "<xsl:value-of select="$url" />",
						"protocol": "{{protocol}}",
						"host": ["{{host}}"],
						"port": "{{port}}",
						"path": [<xsl:call-template name="get-path">
							<xsl:with-param name="url" select="concat($actor, '/', $od/Name)"/>
						</xsl:call-template>]
					}
				},
				"response": []
			}</xsl:if>
			<xsl:if test="$delete">
          ,{
				"name": "Delete <xsl:value-of select="$od/Name" />",
				"request": {
					"method": "DELETE",
					"header": [],
					"url": {
						"raw": "<xsl:value-of select="$url" />",
						"protocol": "{{protocol}}",
						"host": ["{{host}}"],
						"port": "{{port}}",
						"path": [<xsl:call-template name="get-path">
							<xsl:with-param name="url" select="concat($actor, '/', $od/Name)"/>
						</xsl:call-template>]
					}
				},
				"response": []
			}</xsl:if>
			</xsl:for-each>
		]
	}</xsl:for-each>],
	"auth": {
		"type": "bearer",
		"bearer": [
			{
				"key": "token",
				"value": "{{accessToken}}",
				"type": "string"
			}
		]
	},
	"event": [
		{
			"listen": "prerequest",
			"script": {
				"type": "text/javascript",
				"exec": [
					""
				]
			}
		},
		{
			"listen": "test",
			"script": {
				"type": "text/javascript",
				"exec": [
					""
				]
			}
		}
	],
	"variable": [
		{
			"key": "protocol",
			"value": "<xsl:call-template name="get-protocol">
			<xsl:with-param name="url" select="concat($api-root, '/')"/>
		</xsl:call-template>",
			"type": "default"
		},
		{
			"key": "host",
			"value": "<xsl:call-template name="get-host">
			<xsl:with-param name="url" select="substring-before(substring-after(concat($api-root, '/'), '://'), '/')" />
		</xsl:call-template>",
			"type": "default"
		},
		{
			"key": "port",
			"value": "<xsl:call-template name="get-port">
			<xsl:with-param name="url" select="substring-after(concat($api-root, '/'), '://')"/>
		</xsl:call-template>",
			"type": "default"
		}
	]
}
</xsl:element>
                </FileSetFile>
            </FileSetFiles>
        </FileSet>
    </xsl:template>
</xsl:stylesheet>
