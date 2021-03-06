#pragma checksum "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c61973194dd5956717e3aa71c660ec0812e6cd68"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Instructor_Views_Course_CurrentCourseStatus), @"mvc.1.0.view", @"/Areas/Instructor/Views/Course/CurrentCourseStatus.cshtml")]
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
#nullable restore
#line 2 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
using EdtechTest2.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c61973194dd5956717e3aa71c660ec0812e6cd68", @"/Areas/Instructor/Views/Course/CurrentCourseStatus.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf257dd57df6dd081cb2ef41bf2ed2aeb470eac6", @"/Areas/Instructor/Views/_ViewImports.cshtml")]
    public class Areas_Instructor_Views_Course_CurrentCourseStatus : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EdtechTest2.Areas.Instructor.ViewModels.CurrentCourseViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/currentCourseStatus.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
  
    ViewData["Title"] = "CurrentCourseStatus";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n    <div class=\"container\" id=\"vue-app\">\r\n\r\n\r\n\r\n        <div class=\"alert alert-dark\" role=\"alert\">\r\n\r\n            <p>\r\n                Course Id-  {{courseId= ");
#nullable restore
#line 16 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                   Write(Model.course.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("}}\r\n            </p>\r\n            <p>\r\n                Course Status - {{results.course.courseStatus}}\r\n            </p>\r\n            <hr class=\"my-4\">\r\n\r\n\r\n\r\n\r\n\r\n            <div");
            BeginWriteAttribute("v-if", " v-if=\"", 515, "\"", 580, 3);
            WriteAttributeValue("", 522, "results.course.courseStatus==\'", 522, 30, true);
#nullable restore
#line 27 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 552, SD.CourseStatusDevelopment, 552, 27, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 579, "\'", 579, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n                <button v-on:click=\"sendForApproval(results.course.id)\" class=\"btn btn-primary\">Send For Approval </button>\r\n\r\n\r\n            </div>\r\n\r\n            <div");
            BeginWriteAttribute("v-if", " v-if=\"", 753, "\"", 815, 3);
            WriteAttributeValue("", 760, "results.course.courseStatus==\'", 760, 30, true);
#nullable restore
#line 34 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 790, SD.CourseStatusApproved, 790, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 814, "\'", 814, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <button class=\"btn btn-primary\" v-on:click=\"publishCourse(results.course.id)\">Published The Course</button>\r\n\r\n\r\n\r\n            </div>\r\n\r\n            <div");
            BeginWriteAttribute("v-if", " v-if=\"", 988, "\"", 1050, 3);
            WriteAttributeValue("", 995, "results.course.courseStatus==\'", 995, 30, true);
#nullable restore
#line 41 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 1025, SD.CourseStatusRejected, 1025, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1049, "\'", 1049, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n                <button class=\"btn btn-primary\" v-on:click=\"sendForApproval(results.course.id)\">Resend Send For Approval </button>\r\n\r\n\r\n\r\n            </div>\r\n\r\n            <div");
            BeginWriteAttribute("v-if", " v-if=\"", 1232, "\"", 1295, 3);
            WriteAttributeValue("", 1239, "results.course.courseStatus==\'", 1239, 30, true);
#nullable restore
#line 49 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 1269, SD.CourseStatusPublished, 1269, 25, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1294, "\'", 1294, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n                <button class=\"btn btn-info\" v-on:click=\"unpublishCourse(results.course.id)\">UnPublish The Course </button>\r\n\r\n\r\n            </div>\r\n\r\n\r\n            <div");
            BeginWriteAttribute("v-if", " v-if=\"", 1470, "\"", 1530, 3);
            WriteAttributeValue("", 1477, "results.course.courseStatus==\'", 1477, 30, true);
#nullable restore
#line 57 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 1507, SD.CourseStatusReview, 1507, 22, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1529, "\'", 1529, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n                <p>\r\n                    Course Has been Send For the Review it is Under The Review Period it Can Take 1 or 2 hours to get the course Reviewed\r\n\r\n                </p>\r\n\r\n\r\n            </div>\r\n\r\n\r\n            <div");
            BeginWriteAttribute("v-if", " v-if=\"", 1764, "\"", 1829, 3);
            WriteAttributeValue("", 1771, "results.course.courseStatus==\'", 1771, 30, true);
#nullable restore
#line 68 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 1801, SD.CourseStatusUnPublished, 1801, 27, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1828, "\'", 1828, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n                <button class=\"btn btn-primary\" v-on:click=\"publishCourse(results.course.id)\">List The Course Again </button>\r\n\r\n\r\n\r\n\r\n\r\n            </div>\r\n\r\n\r\n\r\n\r\n");
            WriteLiteral("\r\n\r\n        </div>\r\n\r\n        <br />\r\n        <br />\r\n        <br />\r\n        <br />\r\n\r\n\r\n        <div");
            BeginWriteAttribute("v-if", " v-if=\"", 3011, "\"", 3106, 6);
            WriteAttributeValue("", 3018, "results.course.courseStatus==\'", 3018, 30, true);
#nullable restore
#line 115 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 3048, SD.CourseStatusPublished, 3048, 25, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3073, "\'||", 3073, 3, true);
            WriteAttributeValue(" ", 3076, "\'", 3077, 2, true);
#nullable restore
#line 115 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
WriteAttributeValue("", 3078, SD.CourseStatusUnPublished, 3078, 27, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3105, "\'", 3105, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n\r\n            <div class=\"d-flex justify-content-between \">\r\n                <div class=\"p-4 border border-dark bg-light\">\r\n\r\n                    <h3 class=\"text-center\"> Number of Students</h3>\r\n                    <h2 class=\"text-center\"> ");
#nullable restore
#line 122 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                        Write(Model.numberofStudentEnrolled);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n                </div>\r\n                <div class=\"p-4 border border-dark bg-light\">\r\n\r\n                    <h3 class=\"text-center\"> Revenue This Month</h3>\r\n                    <h2 class=\"text-center\"> ");
#nullable restore
#line 128 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                        Write(Model.revenueThisMonth);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n\r\n\r\n                </div>\r\n                <div class=\"p-4 border border-dark bg-light\">\r\n\r\n                    <h3 class=\"text-center\"> Revenue Today</h3>\r\n                    <h2 class=\"text-center\"> ");
#nullable restore
#line 136 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                        Write(Model.totalrevenueToday);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n\r\n\r\n                </div>\r\n\r\n                <div class=\"p-4 border border-dark bg-light\">\r\n\r\n                    <h3 class=\"text-center\">  Total Revenue </h3>\r\n                    <h2 class=\"text-center\">");
#nullable restore
#line 145 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                       Write(Model.totalrevenue);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </h2>


                </div>

            </div>

            <br />
            <br />
            <br />
            <br />

            <h2> Comments on Course</h2>
            <hr class=""my-4"">


            <div class=""border p-3"">
                <h3 class=""text-left"">Student Feedback</h3>
");
#nullable restore
#line 163 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                  
                    double sum = 0;
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 165 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                     foreach (var item in Model.CourseRatings)
                    {
                        sum = sum + item.fullstar;
                        if (item.halfstar == 1)
                        {
                            sum = sum + 0.5;

                        }
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 173 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                     


                    var average = sum / Model.CourseRatings.Count();
                

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                <h1 class=\"display-4\">");
#nullable restore
#line 180 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                 Write(sum);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
                <ul class=""list-group"">
                    <li class=""list-group-item"">

                        <button type=""button"" class=""btn btn-outline-secondary btn-lg btn-block"">
                            <div class=""row"">


                                <div class=""col-md-5"">
                                    <div class=""progress"">
                                        <div class=""progress-bar"" role=""progressbar"" style=""width: 25%"" aria-valuenow=""25"" aria-valuemin=""0"" aria-valuemax=""100""></div>
                                    </div>

                                </div>

                                <div class=""col-md-4"">

                                    <h1 class=""lead"">


");
#nullable restore
#line 200 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                          

                                            if (average % 1 == 0)
                                            {
                                                for (int y = 0; y < average; y++)
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                    <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-star-fill m-0"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                                        <path d=""M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.283.95l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"" />
                                                    </svg>
");
#nullable restore
#line 209 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                                }
                                            }
                                            else
                                            {
                                                for (int y = 0; y < average; y++)
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                    <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-star-fill m-0"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                                        <path d=""M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.283.95l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"" />
                                                    </svg>
");
#nullable restore
#line 218 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                                }



#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-star-half m-0"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                                    <path fill-rule=""evenodd"" d=""M5.354 5.119L7.538.792A.516.516 0 0 1 8 .5c.183 0 .366.097.465.292l2.184 4.327 4.898.696A.537.537 0 0 1 16 6.32a.55.55 0 0 1-.17.445l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256a.519.519 0 0 1-.146.05c-.341.06-.668-.254-.6-.642l.83-4.73L.173 6.765a.55.55 0 0 1-.171-.403.59.59 0 0 1 .084-.302.513.513 0 0 1 .37-.245l4.898-.696zM8 12.027c.08 0 .16.018.232.056l3.686 1.894-.694-3.957a.564.564 0 0 1 .163-.505l2.906-2.77-4.052-.576a.525.525 0 0 1-.393-.288L8.002 2.223 8 2.226v9.8z"" />
                                                </svg>
");
#nullable restore
#line 224 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"

                                            }
                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n                                    </h1>\r\n                                </div>\r\n                                <div class=\"col-md-2\">\r\n                                    (");
#nullable restore
#line 234 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                Write(Model.CourseRatings.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(")\r\n                                </div>\r\n                            </div>\r\n\r\n\r\n\r\n                        </button>\r\n\r\n\r\n\r\n                    </li>\r\n\r\n                </ul>\r\n\r\n");
#nullable restore
#line 248 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                 foreach (var item in Model.CourseRatings)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"container\">\r\n                        <h5 class=\"lead\">\r\n\r\n\r\n");
#nullable restore
#line 254 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                             for (int x = 0; x < item.fullstar; x++)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-star-fill m-0"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                    <path d=""M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.283.95l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"" />
                                </svg>
");
#nullable restore
#line 259 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 262 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                             if (item.halfstar == 1)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-star-half m-0"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                    <path fill-rule=""evenodd"" d=""M5.354 5.119L7.538.792A.516.516 0 0 1 8 .5c.183 0 .366.097.465.292l2.184 4.327 4.898.696A.537.537 0 0 1 16 6.32a.55.55 0 0 1-.17.445l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256a.519.519 0 0 1-.146.05c-.341.06-.668-.254-.6-.642l.83-4.73L.173 6.765a.55.55 0 0 1-.171-.403.59.59 0 0 1 .084-.302.513.513 0 0 1 .37-.245l4.898-.696zM8 12.027c.08 0 .16.018.232.056l3.686 1.894-.694-3.957a.564.564 0 0 1 .163-.505l2.906-2.77-4.052-.576a.525.525 0 0 1-.393-.288L8.002 2.223 8 2.226v9.8z"" />
                                </svg>
");
#nullable restore
#line 267 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"

                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n\r\n                        </h5>\r\n                        <p class=\"lead\">\r\n                            ");
#nullable restore
#line 277 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                       Write(item.RatingComment);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </p>\r\n                        <br />\r\n                        <p class=\"font-weight-bold\">");
#nullable restore
#line 280 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                                               Write(item.ApplicationUser.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n                        <hr class=\"my-4\">\r\n\r\n                    </div>\r\n");
#nullable restore
#line 285 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CurrentCourseStatus.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n            </div>\r\n\r\n\r\n            <br />\r\n            <br />\r\n            <br />\r\n            <br />\r\n        </div>\r\n\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c61973194dd5956717e3aa71c660ec0812e6cd6823952", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EdtechTest2.Areas.Instructor.ViewModels.CurrentCourseViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591