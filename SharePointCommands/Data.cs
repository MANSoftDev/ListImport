#region Summary
/******************************************************************************
// AUTHOR                   : Mark Nischalke 
// CREATE DATE              : 9/19/10 
// PURPOSE                  : Data structures for List Import
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

namespace SharePointCommands
{
    /// <summary>
    /// Structure to pass info to command 
    /// </summary>
    [Serializable]
    public struct GetListData
    {
        public string SiteUrl { get; set; }
        public string ListName { get; set; }
        public bool IncludeContent { get; set; }
    }

    /// <summary>
    /// Structure to contain info about list
    /// since SPList object is not serializable
    /// </summary>
    [Serializable]
    public struct ListData
    {
        public string ListName { get; set; }
        public XElement Schema { get; set; }
        public XElement ListInstance { get; set; }
        public XElement ListTemplate { get; set; }

        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
