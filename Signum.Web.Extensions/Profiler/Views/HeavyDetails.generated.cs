﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Signum.Web.Extensions.Profiler.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using Signum.Entities;
    using Signum.Utilities;
    
    #line 1 "..\..\Profiler\Views\HeavyDetails.cshtml"
    using Signum.Utilities.ExpressionTrees;
    
    #line default
    #line hidden
    using Signum.Web;
    
    #line 2 "..\..\Profiler\Views\HeavyDetails.cshtml"
    using Signum.Web.Profiler;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.4.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Profiler/Views/HeavyDetails.cshtml")]
    public partial class HeavyDetails : System.Web.Mvc.WebViewPage<HeavyProfilerEntry>
    {
        public HeavyDetails()
        {
        }
        public override void Execute()
        {



WriteLiteral("<h2>Profiler Entry (\r\n");


            
            #line 5 "..\..\Profiler\Views\HeavyDetails.cshtml"
     foreach (var e in Model.FollowC(a => a.Parent).Skip(1).Reverse())
    {
        
            
            #line default
            #line hidden
            
            #line 7 "..\..\Profiler\Views\HeavyDetails.cshtml"
   Write(Html.ProfilerEntry(e.Index.ToString(), e.FullIndex()));

            
            #line default
            #line hidden

WriteLiteral(".\r\n");


            
            #line 8 "..\..\Profiler\Views\HeavyDetails.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    ");


            
            #line 9 "..\..\Profiler\Views\HeavyDetails.cshtml"
Write(Model.Index.ToString());

            
            #line default
            #line hidden
WriteLiteral(")\r\n</h2>\r\n");


            
            #line 11 "..\..\Profiler\Views\HeavyDetails.cshtml"
Write(Html.ActionLink("(View all)", (ProfilerController pc) => pc.Heavy(false)));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 12 "..\..\Profiler\Views\HeavyDetails.cshtml"
Write(Html.ActionLink("Download", (ProfilerController pc) => pc.DownloadFile(Model.FullIndex())));

            
            #line default
            #line hidden
WriteLiteral("\r\n<br />\r\n<h3>Breakdown</h3>\r\n<div class=\"sf-profiler-chart\" data-detail-url=\"");


            
            #line 15 "..\..\Profiler\Views\HeavyDetails.cshtml"
                                           Write(Url.Action("HeavyRoute", "Profiler"));

            
            #line default
            #line hidden
WriteLiteral("\">\r\n</div>\r\n<br />\r\n<table class=\"sf-search-results\">\r\n    <tr>\r\n        <th>Role" +
"\r\n        </th>\r\n        <td>\r\n            ");


            
            #line 23 "..\..\Profiler\Views\HeavyDetails.cshtml"
       Write(Model.Role);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <th>Time\r\n        </th>\r\n        <t" +
"d>\r\n            ");


            
            #line 30 "..\..\Profiler\Views\HeavyDetails.cshtml"
       Write(Model.Elapsed.NiceToString());

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n    </tr>\r\n</table>\r\n<br />\r\n<h3>Aditional Data</h3>\r\n<div>\r\n   " +
" <pre>\r\n    <code>\r\n        ");


            
            #line 39 "..\..\Profiler\Views\HeavyDetails.cshtml"
   Write(Model.AdditionalData);

            
            #line default
            #line hidden
WriteLiteral("\r\n    </code>\r\n    </pre>\r\n</div>\r\n<br />\r\n<h3>StackTrace</h3>\r\n");


            
            #line 45 "..\..\Profiler\Views\HeavyDetails.cshtml"
 if (Model.StackTrace == null)
{

            
            #line default
            #line hidden
WriteLiteral("    <span>No StackTrace</span>\r\n");


            
            #line 48 "..\..\Profiler\Views\HeavyDetails.cshtml"
}
else
{

            
            #line default
            #line hidden
WriteLiteral(@"    <table class=""sf-search-results"">
        <thead>
            <tr>
                <th>Type
                </th>
                <th>Method
                </th>
                <th>FileLine
                </th>
            </tr>
        </thead>
        <tbody>
");


            
            #line 63 "..\..\Profiler\Views\HeavyDetails.cshtml"
             for (int i = 0; i < Model.StackTrace.FrameCount; i++)
            {
                var frame = Model.StackTrace.GetFrame(i);
                var type = frame.GetMethod().DeclaringType;

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>\r\n");


            
            #line 69 "..\..\Profiler\Views\HeavyDetails.cshtml"
                         if (type != null)
                        {
                            var color = ColorExtensions.ToHtmlColor(type.Assembly.FullName.GetHashCode());
                    

            
            #line default
            #line hidden
WriteLiteral("                            <span style=\"color:");


            
            #line 73 "..\..\Profiler\Views\HeavyDetails.cshtml"
                                          Write(color);

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 73 "..\..\Profiler\Views\HeavyDetails.cshtml"
                                                  Write(frame.GetMethod().DeclaringType.TryCC(t => t.TypeName()));

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");


            
            #line 74 "..\..\Profiler\Views\HeavyDetails.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </td>\r\n                    <td>\r\n                        ");


            
            #line 77 "..\..\Profiler\Views\HeavyDetails.cshtml"
                   Write(frame.GetMethod().Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");


            
            #line 80 "..\..\Profiler\Views\HeavyDetails.cshtml"
                   Write(frame.GetFileLineAndNumber());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");


            
            #line 83 "..\..\Profiler\Views\HeavyDetails.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </tbody>\r\n    </table>\r\n");


            
            #line 86 "..\..\Profiler\Views\HeavyDetails.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("<br />\r\n");


            
            #line 88 "..\..\Profiler\Views\HeavyDetails.cshtml"
Write(Html.ScriptCss("~/Profiler/Content/SF_Profiler.css"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 89 "..\..\Profiler\Views\HeavyDetails.cshtml"
Write(Html.ScriptsJs("~/scripts/d3.v2.min.js",
                "~/Profiler/Scripts/SF_Profiler.js"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 91 "..\..\Profiler\Views\HeavyDetails.cshtml"
   
    var fullTree = Model.FollowC(e => e.Parent).ToList();
    fullTree.AddRange(Model.Descendants()); 


            
            #line default
            #line hidden
WriteLiteral("<script type=\"text/javascript\">\r\n    $(function() {\r\n        SF.Profiler.heavyDet" +
"ailsChart(");


            
            #line 97 "..\..\Profiler\Views\HeavyDetails.cshtml"
                                 Write(Html.Raw(fullTree.HeavyDetailsToJson()));

            
            #line default
            #line hidden
WriteLiteral(", ");


            
            #line 97 "..\..\Profiler\Views\HeavyDetails.cshtml"
                                                                           Write(Model.Depth);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    });\r\n</script>\r\n");


        }
    }
}
#pragma warning restore 1591
