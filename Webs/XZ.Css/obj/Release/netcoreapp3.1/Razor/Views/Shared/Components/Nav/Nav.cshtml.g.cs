#pragma checksum "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "51266b0da307f976565e1b417cced45bf5ed789c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Nav_Nav), @"mvc.1.0.view", @"/Views/Shared/Components/Nav/Nav.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"51266b0da307f976565e1b417cced45bf5ed789c", @"/Views/Shared/Components/Nav/Nav.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"894802d5289a21f81c5e5e0b8c55a0ba8ad3993a", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Nav_Nav : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SysUserPermissionModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
  
    string selectedNav = ViewBag.Nav;

#line default
#line hidden
            WriteLiteral("<div class=\"nav-scroll\">\r\n    <nav>\r\n        <a");
            BeginWriteAttribute("class", " class=\"", 124, "\"", 197, 2);
            WriteAttributeValue("", 132, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 7 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                   if (selectedNav == "home") {

#line default
#line hidden
                WriteLiteral("selected");
#line 7 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                    }

#line default
#line hidden
                PopWriter();
            }
            ), 132, 51, false);
            WriteAttributeValue(" ", 183, "gq-singleline", 184, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/\"><span class=\"gq-icon\">&#xe910;</span><span class=\"text\">主页</span></a>\r\n");
#line 8 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
         if (this.Model != null)
        {
            

#line default
#line hidden
#line 10 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("25db625b-d142-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 440, "\"", 518, 2);
            WriteAttributeValue("", 448, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 12 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "contracts") {

#line default
#line hidden
                WriteLiteral("selected");
#line 12 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                                 }

#line default
#line hidden
                PopWriter();
            }
            ), 448, 56, false);
            WriteAttributeValue(" ", 504, "gq-singleline", 505, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/contracts/index\"><span class=\"gq-icon\">&#xe667;</span><span class=\"text\">合同</span></a>\r\n");
#line 13 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 14 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("c3e1bece-d143-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 746, "\"", 821, 2);
            WriteAttributeValue("", 754, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 16 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "agents") {

#line default
#line hidden
                WriteLiteral("selected");
#line 16 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                              }

#line default
#line hidden
                PopWriter();
            }
            ), 754, 53, false);
            WriteAttributeValue(" ", 807, "gq-singleline", 808, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/agents/index\"><span class=\"gq-icon\">&#xe60e;</span><span class=\"text\">代理商</span></a>\r\n");
#line 17 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 18 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("d0f09441-d143-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 1047, "\"", 1125, 2);
            WriteAttributeValue("", 1055, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 20 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "merchants") {

#line default
#line hidden
                WriteLiteral("selected");
#line 20 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                                 }

#line default
#line hidden
                PopWriter();
            }
            ), 1055, 56, false);
            WriteAttributeValue(" ", 1111, "gq-singleline", 1112, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/merchants/index\"><span class=\"gq-icon\">&#xe617;</span><span class=\"text\">商户</span></a>\r\n");
#line 21 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 22 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("db5514c7-d143-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 1353, "\"", 1426, 2);
            WriteAttributeValue("", 1361, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 24 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "apps") {

#line default
#line hidden
                WriteLiteral("selected");
#line 24 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                            }

#line default
#line hidden
                PopWriter();
            }
            ), 1361, 51, false);
            WriteAttributeValue(" ", 1412, "gq-singleline", 1413, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/apps/index\"><span class=\"gq-icon\">&#xe6f0;</span><span class=\"text\">应用</span></a>\r\n");
#line 25 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 26 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("e9c707da-d143-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 1649, "\"", 1724, 2);
            WriteAttributeValue("", 1657, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 28 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "orders") {

#line default
#line hidden
                WriteLiteral("selected");
#line 28 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                              }

#line default
#line hidden
                PopWriter();
            }
            ), 1657, 53, false);
            WriteAttributeValue(" ", 1710, "gq-singleline", 1711, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/orders/index\"><span class=\"gq-icon\">&#xe60b;</span><span class=\"text\">订单</span></a>\r\n");
#line 29 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 30 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("f56e31fa-d143-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 1949, "\"", 2026, 2);
            WriteAttributeValue("", 1957, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 32 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "waybills") {

#line default
#line hidden
                WriteLiteral("selected");
#line 32 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                                }

#line default
#line hidden
                PopWriter();
            }
            ), 1957, 55, false);
            WriteAttributeValue(" ", 2012, "gq-singleline", 2013, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/waybills/index\"><span class=\"gq-icon\">&#xe692;</span><span class=\"text\">运单</span></a>\r\n");
#line 33 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 34 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("0341b306-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 2253, "\"", 2329, 2);
            WriteAttributeValue("", 2261, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 36 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "refunds") {

#line default
#line hidden
                WriteLiteral("selected");
#line 36 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                               }

#line default
#line hidden
                PopWriter();
            }
            ), 2261, 54, false);
            WriteAttributeValue(" ", 2315, "gq-singleline", 2316, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/refunds/index\"><span class=\"gq-icon\">&#xe630;</span><span class=\"text\">退款</span></a>\r\n");
#line 37 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 38 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("0ecc7dac-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 2555, "\"", 2632, 2);
            WriteAttributeValue("", 2563, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 40 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "disputes") {

#line default
#line hidden
                WriteLiteral("selected");
#line 40 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                                }

#line default
#line hidden
                PopWriter();
            }
            ), 2563, 55, false);
            WriteAttributeValue(" ", 2618, "gq-singleline", 2619, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/disputes/index\"><span class=\"gq-icon\">&#xe6f1;</span><span class=\"text\">争议</span></a>\r\n");
#line 41 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 42 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("1e81a56a-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 2859, "\"", 2936, 2);
            WriteAttributeValue("", 2867, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 44 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "supports") {

#line default
#line hidden
                WriteLiteral("selected");
#line 44 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                                }

#line default
#line hidden
                PopWriter();
            }
            ), 2867, 55, false);
            WriteAttributeValue(" ", 2922, "gq-singleline", 2923, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/supports/index\"><span class=\"gq-icon\">&#xe66f;</span><span class=\"text\">客户服务</span></a>\r\n");
#line 45 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 46 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("29735e88-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 3165, "\"", 3243, 2);
            WriteAttributeValue("", 3173, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 48 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "drawcashs") {

#line default
#line hidden
                WriteLiteral("selected");
#line 48 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                                 }

#line default
#line hidden
                PopWriter();
            }
            ), 3173, 56, false);
            WriteAttributeValue(" ", 3229, "gq-singleline", 3230, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/drawcashs/index\"><span class=\"gq-icon\">&#xe602;</span><span class=\"text\">提现</span></a>\r\n");
#line 49 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 50 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("331c2b5a-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 3471, "\"", 3547, 2);
            WriteAttributeValue("", 3479, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 52 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "reports") {

#line default
#line hidden
                WriteLiteral("selected");
#line 52 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                               }

#line default
#line hidden
                PopWriter();
            }
            ), 3479, 54, false);
            WriteAttributeValue(" ", 3533, "gq-singleline", 3534, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/reports/index\"><span class=\"gq-icon\">&#xe6bf;</span><span class=\"text\">报表</span></a>\r\n");
#line 53 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 54 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("3a95092a-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 3773, "\"", 3847, 2);
            WriteAttributeValue("", 3781, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 56 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "rates") {

#line default
#line hidden
                WriteLiteral("selected");
#line 56 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                             }

#line default
#line hidden
                PopWriter();
            }
            ), 3781, 52, false);
            WriteAttributeValue(" ", 3833, "gq-singleline", 3834, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/rates/index\"><span class=\"gq-icon\">&#xe61c;</span><span class=\"text\">汇率</span></a>\r\n");
#line 57 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }
            

#line default
#line hidden
#line 62 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("4c90ef50-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 4381, "\"", 4458, 2);
            WriteAttributeValue("", 4389, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 64 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "gateways") {

#line default
#line hidden
                WriteLiteral("selected");
#line 64 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                                }

#line default
#line hidden
                PopWriter();
            }
            ), 4389, 55, false);
            WriteAttributeValue(" ", 4444, "gq-singleline", 4445, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/gateways/index\"><span class=\"gq-icon\">&#xe6b7;</span><span class=\"text\">网关</span></a>\r\n");
#line 65 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 66 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("55f593f8-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 4685, "\"", 4760, 2);
            WriteAttributeValue("", 4693, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 68 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "emails") {

#line default
#line hidden
                WriteLiteral("selected");
#line 68 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                              }

#line default
#line hidden
                PopWriter();
            }
            ), 4693, 53, false);
            WriteAttributeValue(" ", 4746, "gq-singleline", 4747, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/emails/index\"><span class=\"gq-icon\">&#xe651;</span><span class=\"text\">邮件模版</span></a>\r\n");
#line 69 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }

#line default
#line hidden
#line 70 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
             if (this.Model.IsPermission("5da7c4a4-d144-11ea-a061-de15bc72dcfe"))
            {

#line default
#line hidden
            WriteLiteral("                <a");
            BeginWriteAttribute("class", " class=\"", 4987, "\"", 5063, 2);
            WriteAttributeValue("", 4995, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#line 72 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                           if (selectedNav == "notices") {

#line default
#line hidden
                WriteLiteral("selected");
#line 72 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
                                                                               }

#line default
#line hidden
                PopWriter();
            }
            ), 4995, 54, false);
            WriteAttributeValue(" ", 5049, "gq-singleline", 5050, 14, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"/notices/index\"><span class=\"gq-icon\">&#xe604;</span><span class=\"text\">公告</span></a>\r\n");
#line 73 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
            }
            

#line default
#line hidden
#line 77 "D:\XZ.Core\XZ.Solution2\XZ.Solution\Webs\XZ.Css\Views\Shared\Components\Nav\Nav.cshtml"
               
        }

#line default
#line hidden
            WriteLiteral("    </nav>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SysUserPermissionModel> Html { get; private set; }
    }
}
#pragma warning restore 1591