#pragma checksum "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Error\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7f2929204718396b153cc8d1384cee393d32b4da"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Error_Index), @"mvc.1.0.view", @"/Views/Error/Index.cshtml")]
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
#line 1 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\_ViewImports.cshtml"
using XZ.Css;

#line default
#line hidden
#line 2 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\_ViewImports.cshtml"
using XZ.Css.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f2929204718396b153cc8d1384cee393d32b4da", @"/Views/Error/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"894802d5289a21f81c5e5e0b8c55a0ba8ad3993a", @"/Views/_ViewImports.cshtml")]
    public class Views_Error_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<XZ.Core.Exceptions.KnownException>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Error\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            WriteLiteral("\r\n<h1>错误信息</h1>\r\n<div>Message:<label>");
#line 7 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Error\Index.cshtml"
               Write(Model.Message);

#line default
#line hidden
            WriteLiteral("</label></div>\r\n<div>ErrorCode:<label>");
#line 8 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Error\Index.cshtml"
                 Write(Model.ErrorCode);

#line default
#line hidden
            WriteLiteral("</label></div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<XZ.Core.Exceptions.KnownException> Html { get; private set; }
    }
}
#pragma warning restore 1591