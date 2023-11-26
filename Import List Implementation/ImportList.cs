#region Summary
/******************************************************************************
// AUTHOR                   : Mark Nischalke 
// CREATE DATE              : 9/19/10 
// PURPOSE                  : Import SharePoint list into SIte Definition project
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
using Microsoft.VisualStudio.SharePoint;
using Microsoft.SharePoint;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Import_List_Implementation
{
    public class ImportList
    {
        // Constants for project item types
        private const string LIST = "Microsoft.VisualStudio.SharePoint.ListDefinition";
        private const string LIST_INSTANCE = "Microsoft.VisualStudio.SharePoint.ListInstance";
        private const string CONTENT_TYPE = "Microsoft.VisualStudio.SharePoint.ContentType";
        private const string FIELD = "Microsoft.VisualStudio.SharePoint.Field";
        
        /// <summary>
        /// Begin process of importing list
        /// </summary>
        /// <param name="project"></param>
        public void Import(ISharePointProject project)
        {
            SelectList dlg = new SelectList(project);

            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Attempt to get the list information
                // Display message if it can't be found
                // Shouldn't happen, but just in case.
                SharePointCommands.ListData listData = GetListData(project, dlg.SiteURL, dlg.ListName, dlg.IncludeContent);
                if(listData.IsError == null)
                {
                    MessageBox.Show(listData.ErrorMessage);
                }
                else
                {
                    listData.ListName = dlg.ListName;

                    // Last parameter prevents item from being added to feature by default
                    ISharePointProjectItem item = project.ProjectItems.Add(listData.ListName, LIST, true);
                    
                    string folder = Path.GetDirectoryName(item.FullPath);

                    // Now to add the files for list definition and schema
                    string fileName = Path.Combine(folder, "Schema.xml");
                    WriteFile(fileName, listData.Schema);
                    ISharePointProjectItemFile file = item.Files.AddFromFile(fileName);
                    // Must set type to update spdata file
                    file.DeploymentType = DeploymentType.ElementFile; 

                    fileName = Path.Combine(folder, "Elements.xml");
                    WriteFile(fileName, listData.ListTemplate);
                    file = item.Files.AddFromFile(fileName);
                    file.DeploymentType = DeploymentType.ElementManifest;

                    // Add list instance
                    item = project.ProjectItems.Add(listData.ListName, listData.ListName + " Instance", LIST_INSTANCE, true);
                    folder = Path.GetDirectoryName(item.FullPath);

                    fileName = Path.Combine(folder, "Elements.xml");
                    WriteFile(fileName, listData.ListInstance);
                    file = item.Files.AddFromFile(fileName);
                    file.DeploymentType = DeploymentType.ElementManifest;
                }
            }
        }

        #region Private methods

        /// <summary>
        /// Get the SPList matching the given name from the specified site
        /// </summary>
        /// <param name="site">URL of site</param>
        /// <param name="listName">Name of list</param>
        /// <param name="includeContent">Should content be included in list instance</param>
        /// <returns>string containing schema file xml</returns>
        private SharePointCommands.ListData GetListData(ISharePointProject project, string siteURL, string listName, bool includeContent)
        {
            SharePointCommands.GetListData data = new SharePointCommands.GetListData();
            data.SiteUrl = siteURL;
            data.ListName = listName;
            data.IncludeContent = includeContent;

            return project.SharePointConnection.ExecuteCommand<SharePointCommands.GetListData, SharePointCommands.ListData>
                (SharePointCommands.Commands.GetListData, data);
        }

        /// <summary>
        /// Write given XElement to file
        /// </summary>
        /// <param name="fileName">Full path of file to write</param>
        /// <param name="element">XElement to write to file</param>
        private void WriteFile(string fileName, XElement element)
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.Indent = true;

            using(XmlWriter writer = XmlWriter.Create(fileName, xws))
            {
                element.WriteTo(writer);  
            }
        }

        #endregion
    }
}
