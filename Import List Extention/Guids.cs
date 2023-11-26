// Guids.cs
// MUST match guids.h
using System;

namespace ManSoftDevLLC.Import_List_Extention
{
    static class GuidList
    {
        public const string guidImport_List_ExtentionPkgString = "036b0b5c-5501-4e07-b5b9-668a52e6db26";
        public const string guidImport_List_ExtentionCmdSetString = "299b562b-0f70-486f-a26e-1d91316ba03a";

        public static readonly Guid guidImport_List_ExtentionCmdSet = new Guid(guidImport_List_ExtentionCmdSetString);
    };
}