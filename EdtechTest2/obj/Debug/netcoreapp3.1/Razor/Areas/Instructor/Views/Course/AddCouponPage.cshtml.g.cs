#pragma checksum "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\AddCouponPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95956fa2eae23db8b57bc871d7ae3f68d4ac24f1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Instructor_Views_Course_AddCouponPage), @"mvc.1.0.view", @"/Areas/Instructor/Views/Course/AddCouponPage.cshtml")]
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
#line 1 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\_ViewImports.cshtml"
using EdtechTest2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\_ViewImports.cshtml"
using EdtechTest2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95956fa2eae23db8b57bc871d7ae3f68d4ac24f1", @"/Areas/Instructor/Views/Course/AddCouponPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf257dd57df6dd081cb2ef41bf2ed2aeb470eac6", @"/Areas/Instructor/Views/_ViewImports.cshtml")]
    public class Areas_Instructor_Views_Course_AddCouponPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Course>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/addCoupon.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\AddCouponPage.cshtml"
  
    ViewData["Title"] = "AddCouponPage";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n    <div id=\"vue-app\">\r\n\r\n        <div class=\"jumbotron\">\r\n            <h1 class=\"display-4\">Add Coupoun Page </h1>\r\n            <hr class=\"my-4\">\r\n            <h2>");
#nullable restore
#line 15 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\AddCouponPage.cshtml"
           Write(Model.topicName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n            <p>Course Id {{courseId=");
#nullable restore
#line 16 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\AddCouponPage.cshtml"
                               Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"}}</p>
            <hr class=""my-4"">
            <div class=""form-check"">
                <input class=""form-check-input"" type=""radio""  v-model=""discountbyPrice"" name=""exampleRadios"" id=""exampleRadios1"" value=""option1"" checked>
                <label class=""form-check-label"" for=""exampleRadios1"">
                    Discount By Price
                </label>
            </div>
            <div class=""form-check"">
                <input class=""form-check-input"" type=""radio""  v-model=""discountbyPercent"" name=""exampleRadios"" id=""exampleRadios2"" value=""option2"">
                <label class=""form-check-label"" for=""exampleRadios2"">
                    Discount By Percentage
                </label>
            </div>

            <hr class=""my-4"">


            <div class=""form-row"">
                <div class=""col"">
                    <label> Coupon Valid Till</label>
                    <input type=""date"" class=""form-control"" v-model=""couponsValidtill"" placeholder=""ValidTill"">
            ");
            WriteLiteral(@"    </div>
                <div class=""col"">
                    <label> Numbeer of Coupon  Alloted</label>

                    <input type=""number"" class=""form-control""  v-model=""numberofCouponsAlloted""   placeholder=""Number of Coupon Alloted"">
                </div>
            </div>

            <hr class=""my-4"">

            <div class=""input-group input-group-lg m-2"">
                <div class=""input-group-prepend"">
                    <span class=""input-group-text"" id=""inputGroup-sizing-lg"">Discount Value</span>
                </div>

                <input type=""number""  v-model=""discountvlaue""   class=""form-control"" aria-label=""Large"" aria-describedby=""inputGroup-sizing-sm"">
            </div>

            <div class=""input-group input-group-lg m-2"">
                <div class=""input-group-prepend"">
                    <span class=""input-group-text"" id=""inputGroup-sizing-lg"">Coupon Code</span>
                </div>

                <input type=""text"" class=""form-control"" v-");
            WriteLiteral(@"model=""couponcode"" aria-label=""Large"" aria-describedby=""inputGroup-sizing-sm"">
            </div>

            <div class=""d-flex justify-content-center m-4"">
                <p class=""lead"">

                    <button class=""btn btn-primary btn-lg"" v-on:click=""createCoupons()"" role=""button"">Add Coupon</button>
                </p>
            </div>

        </div>

        <div>

            <h3> List Of All The Coupons For this Course</h3>

            <table class=""table table-hover"">
                <thead>
                    <tr>
                        <th scope=""col"">Coupon Id</th>
                        <th scope=""col"">Coupon Code</th>
                        <th scope=""col""> Alloted</th>
                        <th scope=""col""> Used</th>
                        <th scope=""col""> Type</th>

                        <th scope=""col""> Value</th>

                        <th scope=""col""> Valid&nbsp;till</th>

                        <th scope=""col"">Increase&nbsp;Coupons</th");
            WriteLiteral(@">
                        <th scope=""col"">Decrease&nbsp;Coupons</th>

                        <th scope=""col"">Block&nbsp;Coupon</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for=""coupon in couponsList"">
                        <th scope=""row"">{{coupon.id}}</th>
                        <td>{{coupon.couponCode}}</td>
                        <td>{{coupon.numberOfcouponAlloted}}</td>
                        <td>{{coupon.numberOfcouponAlloted-coupon.numberofcouponUnUsed}}</td>
                        <td>{{(coupon.discountMethod)==0?coupon.discountValue:coupon.discountValue+"" %""}}</td>

                        <td>{{coupon.discountValue}}</td>

                        <td>{{coupon.validTill}}</td>

                        <td>
                            <button type=""button"" class=""btn btn-success"" v-on:click=""increaseCoupon(coupon.id)"">
                                <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-p");
            WriteLiteral(@"lus-square"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                    <path fill-rule=""evenodd"" d=""M14 1H2a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"" />
                                    <path fill-rule=""evenodd"" d=""M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"" />
                                </svg>
                            </button>
                        </td>
                        <td>
                            <button type=""button"" class=""btn btn-danger"" v-on:click=""decreaseCoupon(coupon.id)"">
                                <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-dash-square"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                    <path fill-rule=""evenodd"" d=""M14 1H2a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1zM2 0a2 2 0 0 ");
            WriteLiteral(@"0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"" />
                                    <path fill-rule=""evenodd"" d=""M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8z"" />
                                </svg>
                            </button>
                        </td>
                        <td>

                            <div v-if=""coupon.isCouponBlocked"">
                                <button class=""btn btn-info"" v-on:click=""blockUnblockCoupon(coupon.id)"">
                                   UnBlock
                                </button>
                            </div>
                            <div v-else>

                                <button class=""btn btn-danger"" v-on:click=""blockUnblockCoupon(coupon.id)"">
                                    Block
                                </button>
                            </div>
                           
                        </td>

                    </tr>


                </tbody>
   ");
            WriteLiteral("         </table>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "95956fa2eae23db8b57bc871d7ae3f68d4ac24f111329", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Course> Html { get; private set; }
    }
}
#pragma warning restore 1591
