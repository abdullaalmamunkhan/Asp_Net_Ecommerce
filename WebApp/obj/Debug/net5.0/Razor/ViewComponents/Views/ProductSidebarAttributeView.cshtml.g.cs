#pragma checksum "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3914d25d26c19692d31ef1a4fb06800b7fcb88da"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.ViewComponents_Views_ProductSidebarAttributeView), @"mvc.1.0.view", @"/ViewComponents/Views/ProductSidebarAttributeView.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3914d25d26c19692d31ef1a4fb06800b7fcb88da", @"/ViewComponents/Views/ProductSidebarAttributeView.cshtml")]
    public class ViewComponents_Views_ProductSidebarAttributeView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DbModelsCore.Models.Ecommerce.Catalog.DBProductAttribute>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml"
 if (Model != null && Model.Count() > 0)
{

    foreach (var thisAttribute in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"cp-sideNav-info-box\">\r\n            <div class=\"cp-info-title\"> ");
#nullable restore
#line 9 "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml"
                                   Write(thisAttribute.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n            <div class=\"cp-info-content\">\r\n");
#nullable restore
#line 11 "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml"
                 if (thisAttribute.AttributeItems != null && thisAttribute.AttributeItems.Count > 0)
                {
                    foreach (var item in thisAttribute.AttributeItems)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <label class=\"checkbox__wrapper\">\r\n                            <div class=\"checkbox--subtle checkbox\">\r\n                                <input type=\"checkbox\"");
            BeginWriteAttribute("value", " value=\"", 728, "\"", 744, 1);
#nullable restore
#line 17 "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml"
WriteAttributeValue("", 736, item.ID, 736, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"dummy_item_checker\">\r\n                                <span class=\"checkbox__symbol\"></span>\r\n                            </div>\r\n                            <span class=\"checkbox__texts\">");
#nullable restore
#line 20 "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml"
                                                     Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        </label>\r\n");
#nullable restore
#line 22 "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml"
                    }

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n");
#nullable restore
#line 27 "F:\My_Work\SocialWebApiSolution\asp.net-core-ecommerce\SocialWebApiSolutions\WebApp\ViewComponents\Views\ProductSidebarAttributeView.cshtml"
    }
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DbModelsCore.Models.Ecommerce.Catalog.DBProductAttribute>> Html { get; private set; }
    }
}
#pragma warning restore 1591