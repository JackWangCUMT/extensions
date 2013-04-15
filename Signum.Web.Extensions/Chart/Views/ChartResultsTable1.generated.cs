﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Signum.Web.Extensions.Chart.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    
    #line 5 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 4 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using Signum.Engine;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using Signum.Entities;
    
    #line default
    #line hidden
    
    #line 8 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using Signum.Entities.Chart;
    
    #line default
    #line hidden
    
    #line 1 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using Signum.Entities.DynamicQuery;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using Signum.Entities.Reflection;
    
    #line default
    #line hidden
    using Signum.Utilities;
    
    #line 7 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using Signum.Web;
    
    #line default
    #line hidden
    
    #line 9 "..\..\Chart\Views\ChartResultsTable.cshtml"
    using Signum.Web.Chart;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Chart\Views\ChartResultsTable.cshtml"
        
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Chart/Views/ChartResultsTable.cshtml")]
    public class ChartResultsTable : System.Web.Mvc.WebViewPage<TypeContext<ChartRequest>>
    {
        public ChartResultsTable()
        {
        }
        public override void Execute()
        {











            
            #line 11 "..\..\Chart\Views\ChartResultsTable.cshtml"
   
    ResultTable queryResult = (ResultTable)ViewData[ViewDataKeys.Results];
    bool navigate = (bool)ViewData[ViewDataKeys.Navigate];
    var formatters = (Dictionary<int, CellFormatter>)ViewData[ViewDataKeys.Formatters];


            
            #line default
            #line hidden

            
            #line 16 "..\..\Chart\Views\ChartResultsTable.cshtml"
 if (queryResult == null)
{ 
    
            
            #line default
            #line hidden
            
            #line 18 "..\..\Chart\Views\ChartResultsTable.cshtml"
Write(JavascriptMessage.Signum_noResults.NiceToString());

            
            #line default
            #line hidden
            
            #line 18 "..\..\Chart\Views\ChartResultsTable.cshtml"
                                                      
}
else
{

            
            #line default
            #line hidden
WriteLiteral("    <table id=\"");


            
            #line 22 "..\..\Chart\Views\ChartResultsTable.cshtml"
          Write(Model.Compose("tblResults"));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"sf-search-results\">\r\n        <thead class=\"ui-widget-header ui-corner-to" +
"p\">\r\n            <tr>\r\n");


            
            #line 25 "..\..\Chart\Views\ChartResultsTable.cshtml"
                 if (!Model.Value.GroupResults && navigate)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <th class=\"ui-state-default sf-th-entity\">\r\n                 " +
"   </th>\r\n");


            
            #line 29 "..\..\Chart\Views\ChartResultsTable.cshtml"
                }

            
            #line default
            #line hidden

            
            #line 30 "..\..\Chart\Views\ChartResultsTable.cshtml"
                 if (queryResult.Columns.Any())
                {
                    foreach (ResultColumn col in queryResult.Columns)
                    {
                        var order = Model.Value.Orders.FirstOrDefault(oo => oo.Token.FullKey() == col.Column.Name);
                        OrderType? orderType = null;
                        if (order != null)
                        {
                            orderType = order.OrderType;
                        }

            
            #line default
            #line hidden
WriteLiteral("                    <th class=\"ui-state-default ");


            
            #line 40 "..\..\Chart\Views\ChartResultsTable.cshtml"
                                            Write((orderType == null) ? "" : (orderType == OrderType.Ascending ? "sf-header-sort-down" : "sf-header-sort-up"));

            
            #line default
            #line hidden
WriteLiteral("\">\r\n                        <input type=\"hidden\" value=\"");


            
            #line 41 "..\..\Chart\Views\ChartResultsTable.cshtml"
                                               Write(col.Column.Name);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n                        ");


            
            #line 42 "..\..\Chart\Views\ChartResultsTable.cshtml"
                   Write(col.Column.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </th>\r\n");


            
            #line 44 "..\..\Chart\Views\ChartResultsTable.cshtml"
                    }
                }

            
            #line default
            #line hidden
WriteLiteral("            </tr>\r\n        </thead>\r\n        <tbody class=\"ui-widget-content\">\r\n");


            
            #line 49 "..\..\Chart\Views\ChartResultsTable.cshtml"
             if (!queryResult.Rows.Any())
            {

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td colspan=\"");


            
            #line 52 "..\..\Chart\Views\ChartResultsTable.cshtml"
                             Write(queryResult.Columns.Count() + (navigate ? 1 : 0));

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 52 "..\..\Chart\Views\ChartResultsTable.cshtml"
                                                                                 Write(JavascriptMessage.Signum_noResults.NiceToString());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");


            
            #line 55 "..\..\Chart\Views\ChartResultsTable.cshtml"
            }
            else
            {
                foreach (var row in queryResult.Rows)
                {
                    if (Model.Value.GroupResults)
                    {

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n");


            
            #line 63 "..\..\Chart\Views\ChartResultsTable.cshtml"
                     foreach (var col in queryResult.Columns)
                    {
                        CellFormatter ft = formatters[col.Index];
                        var value = row[col];

            
            #line default
            #line hidden
WriteLiteral("                            <td ");


            
            #line 67 "..\..\Chart\Views\ChartResultsTable.cshtml"
                           Write(ft.WriteDataAttribute(value));

            
            #line default
            #line hidden
WriteLiteral(">\r\n                            ");


            
            #line 68 "..\..\Chart\Views\ChartResultsTable.cshtml"
                       Write(ft.Formatter(Html, value));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </td>\r\n");


            
            #line 70 "..\..\Chart\Views\ChartResultsTable.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </tr>\r\n");


            
            #line 72 "..\..\Chart\Views\ChartResultsTable.cshtml"
                    }
                    else
                    {
                        Lite<IdentifiableEntity> entityField = row.Entity;

            
            #line default
            #line hidden
WriteLiteral("                <tr data-entity=\"");


            
            #line 76 "..\..\Chart\Views\ChartResultsTable.cshtml"
                            Write(entityField.Key());

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");


            
            #line 77 "..\..\Chart\Views\ChartResultsTable.cshtml"
                     if (entityField != null && navigate)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <td>\r\n                            ");


            
            #line 80 "..\..\Chart\Views\ChartResultsTable.cshtml"
                       Write(QuerySettings.EntityFormatRules.Last(fr => fr.IsApplyable(entityField)).Formatter(Html, entityField));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </td>\r\n");


            
            #line 82 "..\..\Chart\Views\ChartResultsTable.cshtml"
                    }

            
            #line default
            #line hidden

            
            #line 83 "..\..\Chart\Views\ChartResultsTable.cshtml"
                     foreach (var col in queryResult.Columns)
                    {
                        var value = row[col];
                        var ft = formatters[col.Index];

            
            #line default
            #line hidden
WriteLiteral("                        <td ");


            
            #line 87 "..\..\Chart\Views\ChartResultsTable.cshtml"
                       Write(ft.WriteDataAttribute(value));

            
            #line default
            #line hidden
WriteLiteral(">\r\n                            ");


            
            #line 88 "..\..\Chart\Views\ChartResultsTable.cshtml"
                       Write(ft.Formatter(Html, value));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </td>\r\n");


            
            #line 90 "..\..\Chart\Views\ChartResultsTable.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </tr>\r\n");


            
            #line 92 "..\..\Chart\Views\ChartResultsTable.cshtml"
                    }
                }
            }

            
            #line default
            #line hidden
WriteLiteral("        </tbody>\r\n        <tfoot>\r\n        </tfoot>\r\n    </table>\r\n");


            
            #line 99 "..\..\Chart\Views\ChartResultsTable.cshtml"
}

            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
