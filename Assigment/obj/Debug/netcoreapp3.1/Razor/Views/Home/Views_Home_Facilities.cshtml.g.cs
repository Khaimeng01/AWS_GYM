#pragma checksum "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59d5aef70ed976b106a9db062f501953b9aa7621"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Views_Home_Facilities), @"mvc.1.0.view", @"/Views/Home/Views_Home_Facilities.cshtml")]
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
#nullable restore
#line 1 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\_ViewImports.cshtml"
using Assigment;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\_ViewImports.cshtml"
using Assigment.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59d5aef70ed976b106a9db062f501953b9aa7621", @"/Views/Home/Views_Home_Facilities.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"98a2a7eb449cc6484599eea8c927c56db720cdcd", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Views_Home_Facilities : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml"
  
    ViewData["Title"] = "Views_Home_Facilities";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section class=""facilities"" id=""facilities"">

<style>
    .heading {
        margin-top: 20px; /* Adjust this value to push the heading down */
        /* Or use padding-top instead of margin-top, depending on your layout */
    }

    .heading span {
        /* Add any additional styles for the span here */
    }
</style>

<h1 class=""heading""><span>Gym Facilities</span> </h1>

<div class=""box-container"">
     <div class=""box"">
        <div class=""content"">
            <img src=https://gymfacilities.s3.amazonaws.com/Facilities+main/Facilities+main/icon-2.png");
            BeginWriteAttribute("alt", " alt=\"", 639, "\"", 645, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h3>Weightlifting</h3>\r\n                <p>Elevate your strength and fitness with our weightlifting program, led by knowledgeable trainers and designed to help you reach your goals</p>\r\n            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 866, "\"", 944, 3);
            WriteAttributeValue("", 876, "location.href=\'", 876, 15, true);
#nullable restore
#line 25 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml"
WriteAttributeValue("", 891, Url.Action("Weighlifting_Session", "Weightlifting"), 891, 52, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 943, "\'", 943, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">read more</button>\r\n        </div>\r\n    </div>\r\n    <div class=\"box\">\r\n        <div class=\"content\">\r\n            <img src=https://gymfacilities.s3.amazonaws.com/Facilities+main/Facilities+main/boxing.png");
            BeginWriteAttribute("alt", " alt=\"", 1162, "\"", 1168, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h3>Boxing Class</h3>\r\n            <p>Unleash your inner fighter and build strength, endurance, and confidence through our high-energy boxing workout</p>\r\n            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1358, "\"", 1415, 3);
            WriteAttributeValue("", 1368, "location.href=\'", 1368, 15, true);
#nullable restore
#line 33 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml"
WriteAttributeValue("", 1383, Url.Action("Boxing", "Boxing"), 1383, 31, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1414, "\'", 1414, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">read more</button>\r\n        </div>\r\n    </div>\r\n    <div class=\"box second\">\r\n        <div class=\"content\">\r\n            <img src=https://gymfacilities.s3.amazonaws.com/Facilities+main/Facilities+main/spin.png");
            BeginWriteAttribute("alt", " alt=\"", 1638, "\"", 1644, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h3>Spin Class</h3>\r\n                <p>Push your limits and experience the thrill of indoor cycling at our spin classes</p>\r\n            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1805, "\"", 1858, 3);
            WriteAttributeValue("", 1815, "location.href=\'", 1815, 15, true);
#nullable restore
#line 41 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml"
WriteAttributeValue("", 1830, Url.Action("Spin", "Spin"), 1830, 27, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1857, "\'", 1857, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">read more</button>\r\n        </div>\r\n    </div>\r\n    <div class=\"box second\">\r\n        <div class=\"content\">\r\n            <img src=https://gymfacilities.s3.amazonaws.com/Facilities+main/Facilities+main/icon-3.png");
            BeginWriteAttribute("alt", " alt=\"", 2083, "\"", 2089, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h3>Personal Trainers</h3>\r\n            <p>Get personalized training and achieve your fitness goals with our expert trainers and customized programs</p>\r\n            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2278, "\"", 2339, 3);
            WriteAttributeValue("", 2288, "location.href=\'", 2288, 15, true);
#nullable restore
#line 49 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml"
WriteAttributeValue("", 2303, Url.Action("Trainers", "Trainers"), 2303, 35, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2338, "\'", 2338, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">read more</button>\r\n        </div>\r\n    </div>\r\n    <div class=\"box\">\r\n        <div class=\"content\">\r\n            <img src=https://gymfacilities.s3.amazonaws.com/Facilities+main/Facilities+main/pilates.png");
            BeginWriteAttribute("alt", " alt=\"", 2558, "\"", 2564, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h3>Pilates</h3>\r\n            <p>Transform your body and mind at our Pilates gym, with expert instructors and a welcoming community to support your fitness journey</p>\r\n            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2768, "\"", 2827, 3);
            WriteAttributeValue("", 2778, "location.href=\'", 2778, 15, true);
#nullable restore
#line 57 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml"
WriteAttributeValue("", 2793, Url.Action("Pilates", "Pilates"), 2793, 33, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2826, "\'", 2826, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">read more</button>\r\n        </div>\r\n    </div>\r\n    <div class=\"box\">\r\n         <div class=\"content\">\r\n            <img src=https://gymfacilities.s3.amazonaws.com/Facilities+main/Facilities+main/yoga.png");
            BeginWriteAttribute("alt", " alt=\"", 3044, "\"", 3050, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h3>Yoga Class</h3>\r\n            <p>Experience the transformative power of yoga at our peaceful and inclusive gym, with classes suitable for all levels taught by certified instructors.</p>\r\n            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 3275, "\"", 3328, 3);
            WriteAttributeValue("", 3285, "location.href=\'", 3285, 15, true);
#nullable restore
#line 65 "C:\Users\laikh\Desktop\AWS_CODE\Assigment\Views\Home\Views_Home_Facilities.cshtml"
WriteAttributeValue("", 3300, Url.Action("Yoga", "Yoga"), 3300, 27, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3327, "\'", 3327, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">read more</button>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n</section>\r\n\r\n<h1>Views_Home_Facilities</h1>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
