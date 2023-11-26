using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.SharePoint.Commands;
using Microsoft.SharePoint;

namespace Import_List_Implementation
{
    public static class Commands
    {
        public const string GetLists = "MANSoftDev.Commands.GetLists";
        public const string GetList = "MANSoftDev.Commands.GetList";
        public const string Test = "MANSoftDev.Commands.Test";
    }

    public static class SharePointCommands
    {
        [SharePointCommand(Commands.Test)]
        public static void Test(ISharePointCommandContext context, string siteUrl)
        {
            using(SPSite site = new SPSite(siteUrl))
            {

            }
        }

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
                        lists.Add(item.Title);
                    }
                }
            }

            return lists;
        }

        [SharePointCommand(Commands.GetList)]
        public static SPList GetList(ISharePointCommandContext context, Tuple<string, string> info)
        {
            using(SPSite site = new SPSite(info.Item1))
            {
                using(SPWeb web = site.OpenWeb())
                {
                    return web.Lists.TryGetList(info.Item2);
                }
            }
        }
    }
}
