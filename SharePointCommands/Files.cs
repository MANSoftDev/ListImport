#region Summary
/******************************************************************************
// AUTHOR                   : Mark Nischalke 
// CREATE DATE              : 9/19/10 
// PURPOSE                  : Create files required for list definition
// 
// EXTERNAL DEPENDENCIES    : 
// SPECIAL CHARACTERISTICS 
// OR LIMITATIONS           : 
//
// Copyright © 2010, MANSOftDev, LLC all rights reserved
// ===========================================================================
// Copyright notice must remain in place.
//
******************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.SharePoint;

namespace SharePointCommands
{
    /// <summary>
    /// Class to create XML files for SPList
    /// </summary>
    internal class Files
    {
        /// <summary>
        /// Create schema and element files for import list
        /// </summary>
        /// <param name="list">SPList to create files from</param>
        /// <param name="includeContent">Should include contents in list instance</param>
        /// <returns>ListData containing necessary info for list import</returns>
        public static ListData CreateFiles(SPList list, bool includeContent)
        {
            ListData data = new ListData();

            if(list != null)
            {
                string views = list.Views.SchemaXml;
                string fields = list.Fields.SchemaXml;

                string contentTypes = "<ContentTypes>";

                foreach(SPContentType item in list.ContentTypes)
                {
                    contentTypes += item.Parent.SchemaXmlWithResourceTokens;
                }
                contentTypes += "</ContentTypes>";

                string title = list.Title;
                string url = list.RootFolder.Name;
                int baseType = (int)list.BaseType;
                int templateType = (int)list.BaseTemplate;


                data.IsError = false;
                data.Schema = CreateSchema(title, url, baseType, templateType, contentTypes, fields, views);
                data.ListTemplate = CreateListTemplate(title, templateType, baseType, title, list.Description);
                data.ListInstance = CreateListInstance(title, templateType, url, list.Description);
            }
            else
            {
                // Shouldn't happen, but just in case.
                data.IsError = true;
                data.ErrorMessage = "SPList object is null";
            }

            return data;
        }

        /// <summary>
        /// Create schema file
        /// </summary>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="baseType"></param>
        /// <param name="templateType"></param>
        /// <param name="contentTypes"></param>
        /// <param name="fields"></param>
        /// <param name="views"></param>
        /// <returns>Schema xml</returns>
        private static XElement CreateSchema(string title, string url, int baseType, int templateType,
                                                string contentTypes, string fields, string views)
        {
            XNamespace ns = "http://schemas.microsoft.com/sharepoint/";

            XElement contentTypesXML = XElement.Parse(contentTypes);
            XElement fieldsXML = XElement.Parse(fields);
            XElement viewsXML = XElement.Parse(views);

            XElement schema = new XElement(ns + "List",
                new XAttribute("Title", title),
                new XAttribute("Direction", "none"),
                new XAttribute("Url", "Lists/" + url),
                new XAttribute("BaseType", baseType),
                new XAttribute("Type", templateType),
                new XAttribute("BrowserFileHandling", "Permissive"),
                new XAttribute("FolderCreation", "FALSE"),
                new XAttribute("Catalog", "FALSE"),
                new XAttribute("SendToLocation", "|"),
                new XAttribute("ImageUrl", "/_layouts/images/itgen.png"),
                new XAttribute(XNamespace.Xmlns + "ows", "Microsoft SharePoint"),
                new XAttribute(XNamespace.Xmlns + "spctf", "http://schemas.microsoft.com/sharepoint/v3/contenttype/forms"),

                new XElement("MetaData",
                    contentTypesXML,
                    fieldsXML,
                    CreateFormsElement(),
                    CleanViews(viewsXML)
                    )
                );

            // .NET wants to add an empty xmlns element
            // to the child elements when a namespace is 
            // added to the parent. They only way to remove it
            // is from the string representation of the element
            return XElement.Parse(schema.ToString().Replace("xmlns=\"\"", ""));
        }

        /// <summary>
        /// Clean unnecessary elements and attributes from view xml
        /// </summary>
        /// <param name="views"></param>
        /// <returns>xml fragment for views elements</returns>
        private static XElement CleanViews(XElement views)
        {
            views.Elements("View").Attributes("Name").Remove();

            foreach(XElement view in views.Elements("View"))
            {
                // Need to remove pathing info from this attribute
                view.SetAttributeValue("Url", "AllItems.aspx");
                // Although these attributes are optional, they must be
                // set for Visual Studio to deploy
                view.SetAttributeValue("SetupPath", @"pages\viewpage.aspx");
                view.SetAttributeValue("WebPartZoneID", "Main");
            }

            return views;
        }

        /// <summary>
        /// Create forms element
        /// </summary>
        /// <returns>xml fragment for froms element</returns>
        private static XElement CreateFormsElement()
        {
            XElement forms = new XElement("Forms",
                new XElement("Form",
                    new XAttribute("Type", "DisplayForm"),
                    new XAttribute("Url", "DispForm.aspx"),
                    new XAttribute("SetupPath", @"pages\form.aspx"),
                    new XAttribute("WebPartZoneID", "Main")),
                new XElement("Form",
                    new XAttribute("Type", "EditForm"),
                    new XAttribute("Url", "EditForm.aspx"),
                    new XAttribute("SetupPath", @"pages\form.aspx"),
                    new XAttribute("WebPartZoneID", "Main")),
                new XElement("Form",
                    new XAttribute("Type", "NewForm"),
                    new XAttribute("Url", "NewForm.aspx"),
                    new XAttribute("SetupPath", @"pages\form.aspx"),
                    new XAttribute("WebPartZoneID", "Main"))
                    );

            return forms;
        }

        /// <summary>
        /// Create list template 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="templateType"></param>
        /// <param name="baseType"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <returns>xml fragment for template</returns>
        private static XElement CreateListTemplate(string name, int templateType, int baseType, string displayName, string description)
        {
            XNamespace ns = "http://schemas.microsoft.com/sharepoint/";

            XElement xml = new XElement(ns + "Elements",
                new XElement("ListTemplate",
                    new XAttribute("Name", name), // Name of project item also
                    new XAttribute("Type", templateType),  // Matches TemplateType in ListInstance
                    new XAttribute("BaseType", baseType),
                    new XAttribute("OnQuickLaunch", "TRUE"),
                    new XAttribute("SecurityBits", "11"),
                    new XAttribute("Sequence", "410"),
                    new XAttribute("DisplayName", displayName),
                    new XAttribute("Description", description),
                    new XAttribute("Image", "/_layouts/images/itgen.png"))
                );

            // .NET wants to add an empty xmlns element
            // to the child elements when a namespace is 
            // added to the parent. They only way to remove it
            // is from the string representation of the element
            return XElement.Parse(xml.ToString().Replace("xmlns=\"\"", ""));
        }

        /// <summary>
        /// Create list instance
        /// </summary>
        /// <param name="title"></param>
        /// <param name="templateType"></param>
        /// <param name="url"></param>
        /// <param name="description"></param>
        /// <returns>xml fragment for list instance</returns>
        private static XElement CreateListInstance(string title, int templateType, string url, string description)
        {
            XNamespace ns = "http://schemas.microsoft.com/sharepoint/";

            XElement xml = new XElement(ns + "Elements",
                new XElement("ListInstance",
                    new XAttribute("Title", title + " Instance"),
                    new XAttribute("OnQuickLaunch", "TRUE"),
                    new XAttribute("TemplateType", templateType), // Matches type in ListTemplate
                    new XAttribute("Url", "Lists/" + url),
                    new XAttribute("Description", description))
                );

            // .NET wants to add an empty xmlns element
            // to the child elements when a namespace is 
            // added to the parent. They only way to remove it
            // is from the string representation of the element
            return XElement.Parse(xml.ToString().Replace("xmlns=\"\"", ""));
        }
    }
}
