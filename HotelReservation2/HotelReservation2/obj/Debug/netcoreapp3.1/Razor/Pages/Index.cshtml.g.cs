#pragma checksum "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "380718e3426ab5b6e7f86cc920ecdc2bbcc26140"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(HotelReservation2.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace HotelReservation2.Pages
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
#line 1 "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\_ViewImports.cshtml"
using HotelReservation2;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"380718e3426ab5b6e7f86cc920ecdc2bbcc26140", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3cd623672ad848a337262971e25acc20cff4bf31", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\Index.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n");
#nullable restore
#line 8 "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\Index.cshtml"
     if (Model.RoomTypes.Count() == default(decimal))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p class=\"text-center\">No Data To Display</p>\r\n");
#nullable restore
#line 11 "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\Index.cshtml"
     if (Model.RoomTypes.Count() != default(decimal))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p class=\"text-center\"> Data Count ");
#nullable restore
#line 14 "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\Index.cshtml"
                                      Write(Model.RoomTypes.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 15 "C:\Users\ttgkp\Source\Repos\DevMKhalil\BedayaRepo\HotelReservation2\HotelReservation2\Pages\Index.cshtml"
    }
    

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
