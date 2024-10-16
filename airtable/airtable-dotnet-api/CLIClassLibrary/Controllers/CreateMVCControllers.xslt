<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:param name="output-filename" select="'output.txt'" />

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/*">
        <FileSet>
            <FileSetFiles>
                <xsl:for-each select="//ObjectDefs/ObjectDef">
                <FileSetFile>
                    <RelativePath>
                        <xsl:value-of select="Name"/>
                        <xsl:text>.cs</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve">using CLIClassLibrary.ATDMQ;
using CLIClassLibrary.RoleHandlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; // changed.
using System.Text;

namespace CLIClassLibrary.Controllers
{
    public class <xsl:value-of select="PluralName"  />Controller : Controller
    {
        public IActionResult Index()
        {
            var admin = new ATDAdmin();
            var payload = admin.CreatePayload();
            return Json(admin.Get<xsl:value-of select="PluralName"  />(payload));

        }
    }
}
</xsl:element>
                </FileSetFile>
                </xsl:for-each>
                <FileSetFile>
                    <RelativePath>HomeController.cs</RelativePath>
                    <FileContents>using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLIClassLibrary.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            var actions = new List&lt;Endpoint>(new Endpoint[]
            {
                new Endpoint()
                {
                    Name = "Invoices",
                    Method = "GET",
                    Url = "/Invoices",
                    Description = "Get's a list of invoices from the data source."
                }<xsl:for-each select="//ObjectDefs/ObjectDef">,
                new Endpoint()
                {
                    Name = "<xsl:value-of select="PluralName"  />",
                    Method = "GET",
                    Url = "/<xsl:value-of select="PluralName"  />",
                    Description = "Get's a list of <xsl:value-of select="PluralName"  /> from the data source."
                }
            </xsl:for-each>
            });
            return Json(actions);
        }
    }
}</FileContents>
                </FileSetFile>
            </FileSetFiles>            
        </FileSet>
    </xsl:template>
</xsl:stylesheet>
