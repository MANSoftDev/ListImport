#region Summary
/******************************************************************************
// AUTHOR                   : Mark Nischalke 
// CREATE DATE              : 9/19/10 
// PURPOSE                  : Implementation of SharePoint Commands
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
using Microsoft.VisualStudio.SharePoint.Commands;
using Microsoft.SharePoint;
using System.Xml.Linq;
using System.Xml;

namespace SharePointCommands
{
    /// <summary>
    /// Define command names
    /// </summary>
    public static class Commands
    {
        public const string GetLists = "MANSoftDev.Commands.GetLists";
        public const string GetListData = "MANSoftDev.Commands.GetListData";
    }
    
    /// <summary>
    /// Class to implement commands
    /// </summary>
    public static class SharePointCommands
    {
        /// <summary>
        /// Get all, non-hidden, lists for the given site
        /// </summary>
        /// <param name="context"></param>
        /// <param name="siteUrl"></param>
        /// <returns>Collection of list names</returns>
        [SharePointCommand("MANSoftDev.Commands.GetLists")]
        public static List<string> GetLists(ISharePointCommandContext context, string siteUrl)
        {
            List<string> lists = new List<string>();
            using(SPSite site = new SPSite(siteUrl))
            {
                using(SPWeb web = site.OpenWeb())
                {
                    foreach(SPList item in web.Lists)
                    {
                        if(item.Hidden == false)
                        {
                            lists.Add(item.Title);
                        }
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// Get data for the give list
        /// </summary>
        /// <param name="context"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [SharePointCommand(Commands.GetListData)]
        public static ListData GetListData(ISharePointCommandContext context, GetListData info)
        {
            ListData data = new ListData();

            using(SPSite site = new SPSite(info.SiteUrl))
            {
                using(SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists.TryGetList(info.ListName);
                    if(list == null)
                    {
                        data.IsError = true;
                        data.ErrorMessage = string.Format("{0} cannot be found at {1}", info.ListName, info.SiteUrl);
                    }
                    else
                    {
                        data = Files.CreateFiles(list, info.IncludeContent);
                    }
                }
            }

            data.ListName = info.ListName;
            return data;
        }
    }
}
