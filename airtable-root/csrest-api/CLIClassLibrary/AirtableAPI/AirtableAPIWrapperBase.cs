/*************************************************
AUTO Generated by SSoT.me - 2019
    EJ Alexandra - An airtable Tool
    DO NOT MAKE CHANGES TO THIS FILE - THEY WILL BE OVERWRITTEN
*************************************************/
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using static SSoT.me.AirtableToDotNetLib.AirtableExtensions;

namespace SSoT.me.AirtableToDotNetLib
{
    public class AirtableAPIWrapperBase
    {
        public static readonly IConfiguration AppSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build(); 
        
        private string baseId;
        private string apiKey
        {
            get
            {
                if (String.IsNullOrEmpty(_apiKey))
                {
                    _apiKey = this.LoadAPIKey();
                }
                return _apiKey;
            }
            set => _apiKey = value;
        }

        private string _apiKey;
        private class AirtableKey
        {
            public string APIKey { get; set; }
            public string EmailAddress { get; set; }
        }

        private string LoadAPIKey()
        {
            var filePath = Path.Combine("/app", "default_airtable.key");
            var fi = new FileInfo(filePath);
            if (!fi.Exists) return default(string);

            // Read the file content
            var fileContent = File.ReadAllText(filePath);

            // Parse the JSON content
            var apiKeyData = JsonConvert.DeserializeObject<AirtableKey>(fileContent);

            // Return the API key
            return apiKeyData?.APIKey;
        }

        
        private string baseUrl;

        public Dictionary<String, XmlDocument> XmlDocs { get; }
        public WebClient WebClient { get; }
        public StringBuilder XmlBuilder { get; }

        /// <summary>
        ///  Creates an Airtable API Wrapper which can easily read, write, update and delete 
        ///  values from an Airtable base.
        /// </summary>
        /// <param name="apiKey">The API Key issued to you by Airtable</param>
        /// <param name="baseId">The Airtable Base ID (which can be retrieved from the API Documentation url)</param>
        /// <param name="baseUrlFormatString">URL to add the base id into. Default value: https://api.airtable.com/v0/{0}/</param>
        public AirtableAPIWrapperBase(string apiKey, string baseId, string baseUrlFormatString = "https://api.airtable.com/v0/{0}/")
        {
            this.apiKey = String.IsNullOrEmpty(apiKey) ? this.apiKey : apiKey;
            this.baseId = String.IsNullOrEmpty(baseId) ? this.baseId : baseId;

            // Console.WriteLine("Connecting to airtable db...");
            if (String.IsNullOrEmpty(this.baseId)) throw new System.Security.Authentication.AuthenticationException("baseId parameter required");
            if (String.IsNullOrEmpty(this.apiKey)) throw new System.Security.Authentication.AuthenticationException("apiKey parameter required");

            this.XmlDocs = new Dictionary<String, XmlDocument>();
            this.WebClient = new WebClient();

            this.baseUrl = String.Format(baseUrlFormatString, this.baseId);
            this.WebClient.Headers.Add("Authorization", String.Format("Bearer {0}", this.apiKey));
            this.XmlBuilder = new StringBuilder();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public DataTable GetTableAsDataTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public static object mutex = new Object();
        public XmlDocument GetTableAsXmlDocument(String singularName, String pluralName, String airtableName, String where = "True()", String view = "", int maxPages = 5, String id = "")
        {
            lock (mutex)
            {
                var doc = this.AddAirtableTable(singularName, pluralName, airtableName, where, view, maxPages, id);
                doc = doc.SimplifyAttachments();
                if (ReferenceEquals(doc, null))
                {
                    doc = new XmlDocument();
                    doc.LoadXml("<data></data>");
                }
                return doc;
             }
        }

        public T AddAirtableRow<T>(String singleName, String pluralName, String airtableName, T itemToAdd)
            where T : class, new()
        {
            var id = String.Empty;
            var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
            if (!ReferenceEquals(pi, null))
            {
                id = pi.GetValue(itemToAdd).SafeToString();
                pi.SetValue(itemToAdd, null);
            }
            
            var createdTime = String.Empty;

            var json = String.Format("{{ \"fields\":{0} }}", JsonConvert.SerializeObject(itemToAdd, new JsonSerializerSettings()
            {
                ContractResolver = new CustomContractResolver<T>(),
                NullValueHandling = NullValueHandling.Ignore
            }));
            var airtableRow = JsonConvert.DeserializeObject<AirtableRow>(json);
            var aritableRowJson = JsonConvert.SerializeObject(airtableRow);
            var tableUrl = String.Format("{0}{1}/{2}", baseUrl, airtableName, id);

            this.WebClient.Headers["Authorization"] = String.Format("Bearer {0}", this.apiKey);
            this.WebClient.Headers.Add("Content-Type", "application/json");
            try
            {
                var response = this.WebClient.UploadData(tableUrl, "POST", Encoding.UTF8.GetBytes(json));
                var responseText = Encoding.UTF8.GetString(response);
                responseText = "{ \"Airtable\": { \"AirtableRows\": [ " + responseText + " ] } }";
                var airtableTable = JsonConvert.DeserializeObject<AirtableRowContainer>(responseText);
                var row = airtableTable.Airtable.AirtableRows;
                itemToAdd = row.ConvertTo<T>().FirstOrDefault();
            }
            catch (WebException we)
            {
                throw new Exception(new StreamReader(we.Response.GetResponseStream()).ReadToEnd());
            }

            return itemToAdd;
        }

        public void DeleteAirtableRow<T>(string airtableName, T itemToDelete)
        {
            var id = String.Empty;
            var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
            if (!ReferenceEquals(pi, null))
            {
                id = pi.GetValue(itemToDelete).SafeToString();
                pi.SetValue(itemToDelete, null);
            }

            var tableUrl = String.Format("{0}{1}/{2}", baseUrl, airtableName, id);

            this.WebClient.Headers["Authorization"] = String.Format("Bearer {0}", this.apiKey);
            this.WebClient.Headers.Add("Content-Type", "application/json");
            try
            {
                var response = this.WebClient.UploadData(tableUrl, "DELETE", new byte[] { });
            }
            catch (WebException we)
            {
                throw new Exception(new StreamReader(we.Response.GetResponseStream()).ReadToEnd());
            }
        }

        public IEnumerable<AirtableRow> GetTableAsAirtableRows(string singularName, String pluralName, String airtableName, String where = "True()", String view = "", int maxPages = 5)
        {
            where = HttpUtility.UrlEncode(where.SafeToString());
            var xmlDoc = this.GetTableAsXmlDocument(singularName, pluralName, airtableName, where, view, maxPages);
            var nodeList = xmlDoc.SelectNodes("/*/*");
            var sb = new StringBuilder();
            sb.Append("<Airtable>");
            foreach (XmlElement node in nodeList)
            {
                var airtableRow = xmlDoc.CreateElement("AirtableRows");
                airtableRow.InnerXml = node.InnerXml;
                sb.Append(airtableRow.OuterXml.Replace("<AirtableRows>", "<AirtableRows json:Array=\"true\" xmlns:json=\"http://james.newtonking.com/projects/json\">"));
            }
            sb.Append("</Airtable>");
            var xml = sb.ToString();
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            var json = xdoc.XmlToJson();
            var airtableTable = JsonConvert.DeserializeObject<AirtableContainer>(json);

            if (!(airtableTable.Airtable is null)) 
            {
                return airtableTable.Airtable.AirtableRows;
            } else {
                return null;
            }
        }
        
        public void CleanAirtableRow<T>(String singularName, String pluralName, String airtableName, T itemToUpdate)
            where T : class, new()
        {
            var dupItem = this.GetTableAsAirtableRowById<T>(singularName, pluralName, airtableName, itemToUpdate);
            Type t = itemToUpdate.GetType();
            PropertyInfo[] propertyInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propInfo in propertyInfos)
            {
                var dupProperty = propInfo.GetValue(dupItem);
                bool isKey = (String.Equals(propInfo.Name, "id", StringComparison.OrdinalIgnoreCase) || propInfo.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
                if (!(dupProperty is null) && !isKey && !(propInfo.GetValue(itemToUpdate) is null) && propInfo.GetValue(itemToUpdate).Equals(dupProperty))
                {
                    propInfo.SetValue(itemToUpdate, null);
                }
            }
            // foreach property
            //    If dupItem.Property == itemToUpdate.Property
            //         itemToUpdate.Property = null;
        }
 
        private T GetTableAsAirtableRowById<T>(String singularName, String pluralName, String airtableName, T itemToUpdate)
            where T : class, new()
        {
            //SYntax for the url will be GET: appid / table / id
            var where = "True()";
            var view = ""; 
            var id = "";
            var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
            if (!ReferenceEquals(pi, null))
            {
                id = pi.GetValue(itemToUpdate).SafeToString();
            }
            if (String.IsNullOrEmpty(id)) throw new ArgumentNullException("Missing Id required by AirtableRowById<T> method()");            
            var xmlDoc = this.GetTableAsXmlDocument(singularName, pluralName, airtableName, where, view, 1, id);
            var nodeList = xmlDoc.SelectNodes("/*/*");
            var sb = new StringBuilder();
            sb.Append("<Airtable>");
            foreach (XmlElement node in nodeList)
            {
                var airtableRow = xmlDoc.CreateElement("AirtableRows");
                airtableRow.InnerXml = node.InnerXml;
                sb.Append(airtableRow.OuterXml.Replace("<AirtableRows>", "<AirtableRows json:Array=\"true\" xmlns:json=\"http://james.newtonking.com/projects/json\">"));
            }
            sb.Append("</Airtable>");
            var xml = sb.ToString();
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            var json = xdoc.XmlToJson();
            var airtableTable = JsonConvert.DeserializeObject<AirtableRowContainer>(json);
            var row = airtableTable.Airtable.AirtableRows;
            return row.ConvertTo<T>().FirstOrDefault();
        }

        public T DeepCopyItemToUpdate<T>(T itemToUpdate)
        {
            var json = JsonConvert.SerializeObject(itemToUpdate);
            return JsonConvert.DeserializeObject<T>(json);
        }
        public T UpdateAirtableRow<T>(String singularName, String pluralName, String airtableName, T itemToUpdate)
            where T : class, new()
        {
            var copyToUpdate = DeepCopyItemToUpdate(itemToUpdate);
            this.CleanAirtableRow<T>(singularName, pluralName, airtableName, copyToUpdate);
            var id = String.Empty;
            var createdTime = String.Empty;
            var pi = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                 .FirstOrDefault(wherePI => String.Equals(wherePI.Name, "id", StringComparison.OrdinalIgnoreCase) || wherePI.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
            if (!ReferenceEquals(pi, null))
            {
                id = pi.GetValue(copyToUpdate).SafeToString();
                pi.SetValue(copyToUpdate, null);
            }
            var json = String.Format("{{ \"fields\":{0} }}", JsonConvert.SerializeObject(copyToUpdate, new JsonSerializerSettings()
            {
                ContractResolver = new CustomContractResolver<T>(),
                NullValueHandling = NullValueHandling.Ignore
            }));
            var airtableRow = JsonConvert.DeserializeObject<AirtableRow>(json);
            var aritableRowJson = JsonConvert.SerializeObject(airtableRow);
            var tableUrl = String.Format("{0}{1}/{2}", baseUrl, airtableName, id);

            this.WebClient.Headers["Authorization"] = String.Format("Bearer {0}", this.apiKey);
            this.WebClient.Headers.Add("Content-Type", "application/json");
            try
            {
                var response = this.WebClient.UploadData(tableUrl, "PATCH", Encoding.UTF8.GetBytes(json));
                var responseText = Encoding.UTF8.GetString(response);
                responseText = "{ \"Airtable\": { \"AirtableRows\": [ " + responseText + " ] } }";
                var airtableTable = JsonConvert.DeserializeObject<AirtableRowContainer>(responseText);
                var row = airtableTable.Airtable.AirtableRows;
                itemToUpdate = row.ConvertTo<T>().FirstOrDefault();
            }
            catch (WebException we)
            {
                throw new Exception(new StreamReader(we.Response.GetResponseStream()).ReadToEnd());
            }
            return itemToUpdate;
        }

        private XmlDocument AddAirtableTable(string singularName, string pluralName, string airtableName, String where, String view, int maxPages, string id)
        {
            // Console.WriteLine("Getting airtable {0}", airtableName);

            {
                var finalXmlDoc = new XmlDocument();
                finalXmlDoc.AppendChild(finalXmlDoc.CreateElement(pluralName));

                var offset = String.Empty;
                var currentPage = 0;

                airtableName = Uri.EscapeUriString(airtableName);
                while (currentPage++ < maxPages)
                {
                    if (!String.IsNullOrEmpty(id)) id = String.Format("/{0}", id);

                    String tableUrl;
                    if (!String.IsNullOrEmpty(where) && String.IsNullOrEmpty(id))
                    {
                        tableUrl = $"{baseUrl}{airtableName}{id}?filterByFormula={where}&offset={offset}&view={view}";
                    }
                    else
                    {
                        tableUrl = $"{baseUrl}{airtableName}{id}?offset={offset}&view={view}";
                    }
                    var tableJson = String.Empty;
                    try
                    {
                        tableJson = WebClient.DownloadString(tableUrl);
                    }
                    catch (Exception ex)
                    {
                        var msg = String.Format("Airtable Error for {0}{1}{2}", tableUrl, Environment.NewLine, ex.Message);
                        throw new Exception(msg, ex);
                    }
                    
                    if (id != "")
                    {
                        var tempJson = tableJson;
                        tableJson = "{\"records\" : [ " + tableJson + " ]}";
                    }

                    var tableXml = String.Format("{{ \"Recordset\" : {0} }}", tableJson).JsonToXml().OuterXml;
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(tableXml);

                    var offsetNode = xmlDocument.SelectSingleNode("/*/offset");
                    foreach (var recordElement in xmlDocument.SelectNodes("/*/*").OfType<XmlElement>())
                    {
                        if (recordElement != offsetNode) 
                        {
                            var finalRecord = finalXmlDoc.CreateElement(singularName);
                            finalRecord.InnerXml = recordElement.InnerXml;
                            finalXmlDoc.DocumentElement.AppendChild(finalRecord);
                        }
                    }

                    if (offsetNode == null)
                    {
                        offset = String.Empty;
                        break;
                    }
                    else
                    {
                        offset = offsetNode.InnerText;
                        // Console.WriteLine("Offset: {0}", offset);
                    }
                }

                this.XmlBuilder.AppendLine(finalXmlDoc.DocumentElement.OuterXml);

                this.XmlDocs[singularName] = finalXmlDoc;
            }
            // Console.WriteLine("FINISHED - Getting airtable {0}", airtableName);
            return this.XmlDocs[singularName];
        }

        private string CleanXML(string airtableXml)
        {
            var xDoc = XDocument.Parse(airtableXml);
            // Console.WriteLine("Cleaning elements");
            foreach (XElement element in xDoc.Nodes())
            {
                this.CheckElement(element);
            }

            // Console.WriteLine("Cleaning entities");
            foreach (var entity in xDoc.XPathSelectElements("//Entities/Entity"))
            {
                if (entity.XPathSelectElements("Name").ToString() == "") entity.Remove();
            }

            // Console.WriteLine("Moving /fields/ up one level.");
            foreach (var fieldValue in xDoc.XPathSelectElements("//fields/*").ToList())
            {
                var parent = fieldValue.Parent.Parent;
                var immediateParent = fieldValue.Parent;
                fieldValue.Remove();
                parent.Add(fieldValue);
                if (!immediateParent.Nodes().Any()) immediateParent.Remove();
            }

            // Console.WriteLine("Cleaning text");
            foreach (XText text in xDoc.DescendantNodes().OfType<XText>())
            {
                if (text.Value.Contains("&")) text.Value = String.Format("{0}", text.Value);
            }

            // Console.WriteLine("Cleaning ids");
            var entityDict = new Dictionary<String, XElement>();
            foreach (var id in xDoc.XPathSelectElements("//id"))
            {
                var airtableName = id.Parent.Name.LocalName;
                var entityName = airtableName;
                if (!entityDict.ContainsKey(entityName))
                {
                    entityDict[entityName] = xDoc.XPathSelectElement(String.Format("//Entities/Entity/fields[Name = '{0}' or translate(DisplayName, ' ', '') = '{0}' or DisplayName = '{0}']/Name", airtableName));
                }
                if (!ReferenceEquals(entityDict[entityName], null)) entityName = entityDict[entityName].Value;
                id.Name = String.Format("{0}Id", entityName);
            }

            // Console.WriteLine("Recovering cleaned");
            var xml = xDoc.ToString();
            xml = xml.Replace("<airtable:", "<airtable_");
            xml = xml.Replace("</airtable:", "</airtable_");
            return xml;
        }

        private void CheckElement(XElement element)
        {
            var name = XmlConvert.DecodeName(element.Name.ToString());
            var cleanName = String.Join(String.Empty, name.Where(whereChar => Char.IsLetterOrDigit(whereChar)));

            if (cleanName != element.Name.ToString())
            {
                element.Name = cleanName;
            }
            
            foreach (var child in element.Nodes())
            {
                var childElement = child as XElement;
                if (!ReferenceEquals(childElement, null))
                {
                    CheckElement(childElement);
                }
            }
        }
    }

    public class AirtableRow
    {
        public String id { get; set; }
        public Dictionary<String, Object> fields { get; set; }
        public DateTime createdTime { get; set; }
    }

    public class AirtableTable
    {
        public List<AirtableRow> AirtableRows { get; set; }
    }

    public class AirtableTableRow
    {
        public AirtableRow[] AirtableRows { get; set; }
    }


    public class AirtableContainer
    {
        public AirtableTable Airtable { get; set; }
    }

    public class AirtableRowContainer
    {
        public AirtableTableRow Airtable { get; set; }
    }
}
