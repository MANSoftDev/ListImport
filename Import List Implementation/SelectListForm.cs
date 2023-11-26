#region Summary
/******************************************************************************
// AUTHOR                   : Mark Nischalke 
// CREATE DATE              : 9/19/10 
// PURPOSE                  : Select lists from given SharePoint Site
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;
using Microsoft.VisualStudio.SharePoint;

namespace Import_List_Implementation
{
    public partial class SelectList : Form
    {
        public SelectList(ISharePointProject project)
        {
            InitializeComponent();
            Project = project;
            SiteURL = project.SiteUrl.AbsoluteUri;
        }
        
        /// <summary>
        /// Handle OnLeave event of SiteUrl textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if(!string.IsNullOrWhiteSpace(SiteURL))
            {
                List<string> lists = Project.SharePointConnection.ExecuteCommand<string, List<string>>(SharePointCommands.Commands.GetLists, SiteURL);
                
                Lists.Items.Clear();

                foreach(string list in lists)
                {
                    Lists.Items.Add(list);
                }

                Lists.Enabled = Lists.Items.Count != 0;
            }

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handle List combobox selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectList(object sender, EventArgs e)
        {
            ImportBtn.Enabled = Lists.SelectedIndex != -1;    
        }

        #region Properties
        
        public string SiteURL
        {
            get { return siteURL.Text; }
            private set { siteURL.Text = value; }
        }

        public string ListName
        {
            get { return Lists.SelectedItem.ToString(); }
        }

        public bool IncludeContent
        {
            get { return chkContent.Checked; }
        }

        private ISharePointProject Project { get; set; }

        #endregion



    }
}
