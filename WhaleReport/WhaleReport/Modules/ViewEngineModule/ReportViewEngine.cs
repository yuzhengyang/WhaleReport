using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhaleReport.Modules.ViewEngineModule
{
    public sealed class ReportViewEngine : RazorViewEngine
    {
        public ReportViewEngine()
        {
            base.AreaViewLocationFormats = new string[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml" };

            base.AreaMasterLocationFormats = new string[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml" };

            base.AreaPartialViewLocationFormats = new string[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml" };

            base.ViewLocationFormats = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"};

            base.MasterLocationFormats = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml" };

            base.PartialViewLocationFormats = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Plugins/ECharts/Views/{0}.cshtml"};

            base.FileExtensions = new string[] { "cshtml" };
        }
    }
}