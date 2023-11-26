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
// Copy right notice must remain in place.
//
******************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.SharePoint;
using System.ComponentModel.Composition;

namespace Import_List_Implementation
{
    [Export(typeof(ISharePointProjectExtension))]
    public class ImportListExtention : ISharePointProjectExtension
    {
        public void Initialize(ISharePointProjectService projectService)
        {
            projectService.ProjectMenuItemsRequested += new EventHandler<SharePointProjectMenuItemsRequestedEventArgs>(OnProjectMenuItemsRequested);   
        }

        void OnProjectMenuItemsRequested(object sender, SharePointProjectMenuItemsRequestedEventArgs e)
        {
            IMenuItem menu = e.AddMenuItems.Add("Import List");
            menu.Click += new EventHandler<MenuItemEventArgs>(OnMenuClick);
        }

        void OnMenuClick(object sender, MenuItemEventArgs e)
        {
            ImportList import = new ImportList();
            import.Import(e.Owner as ISharePointProject);
        }
    }
}
