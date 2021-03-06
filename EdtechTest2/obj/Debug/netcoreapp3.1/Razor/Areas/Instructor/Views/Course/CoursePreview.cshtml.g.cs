#pragma checksum "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CoursePreview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "082461e6fb8ad708c1c9203a49f949c26e678fd4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Instructor_Views_Course_CoursePreview), @"mvc.1.0.view", @"/Areas/Instructor/Views/Course/CoursePreview.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"082461e6fb8ad708c1c9203a49f949c26e678fd4", @"/Areas/Instructor/Views/Course/CoursePreview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf257dd57df6dd081cb2ef41bf2ed2aeb470eac6", @"/Areas/Instructor/Views/_ViewImports.cshtml")]
    public class Areas_Instructor_Views_Course_CoursePreview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<EdtechTest2.Areas.Instructor.ViewModels.CoursePreviewViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/coursePreview.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CoursePreview.cshtml"
  
    ViewData["Title"] = "CoursePreview";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>CoursePreview</h1>\r\n\r\n\r\n\r\n\r\n");
            WriteLiteral("\r\n\r\n\r\n\r\n<div id=\"vue-app\" class=\"container\">\r\n    {{courseId=");
#nullable restore
#line 27 "C:\Users\sachi\source\repos\EdtechTest2\EdtechTest2\Areas\Instructor\Views\Course\CoursePreview.cshtml"
          Write(ViewBag.courseId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"}}

    <div v-if=""loading"">
      
        <div class=""container h-100 d-flex justify-content-center"" style=""height: 100%"">
           
                <div class=""spinner-border text-info"" role=""status"">
                    <span class=""sr-only"">Loading...</span>
                </div>
          
        </div>
       
        
      
        
    </div>

    <div v-else>
        <div>
            <br>
            <br>
            <div class=""row flex-column-reverse flex-lg-row"">

                <div class=""col-md-4 col-sm-12"">

                    <div class=""overflow-auto  bg-light"" style=""max-width: 400px; max-height: 750px;"">



                        <div v-for=""(section,index) in results.contents"" class=""card"" style=""width: 23rem;"">
                            <div class=""card-header btn btn-secondary text-dark text-left bg-light list-group-item "" data-toggle=""collapse"" v-bind:href=""'#collapseExample'+index"" role=""button"" aria-expanded=""false"" aria-controls=""collapseExa");
            WriteLiteral(@"mple"">
                                {{section.sectionName}}
                            </div>
                            <ul class="" collapse list-group list-group-flush  "" v-bind:id=""'collapseExample'+index"">



                                <div v-for=""lecture in section.lecturesData"">

                                    <div v-bind:class=""(currentLectureId==lecture.lecId)?'bg-primary':''"">
                                        <li v-bind:class=""(currentLectureId==lecture.lecId)?'btn btn-secondary border border-dark border-bottom border-left border-right bg-primary  text-dark text-left list-group-item':'btn btn-secondary border border-dark border-bottom border-left border-right  text-dark text-left list-group-item'""
                                            v-on:click=""showLectureFunction(lecture)"">


                                            <div class=""column"">
                                                <div class=""row"">
                                                  ");
            WriteLiteral(@"  <p> &nbsp; &nbsp;  {{lecture.name}}</p>
                                                </div>

                                                <div class="" row "">
                                                    &nbsp; &nbsp;
                                                    <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-play-fill text-right"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                                        <path d=""M11.596 8.697l-6.363 3.692c-.54.313-1.233-.066-1.233-.697V4.308c0-.63.692-1.01 1.233-.696l6.363 3.692a.802.802 0 0 1 0 1.393z"" />
                                                    </svg>
                                                    <span class=""badge badge-secondary"">{{lecture.length}} Minutes</span>
                                                </div>




                                            </div>



                                        </li>

                                    </div>");
            WriteLiteral("\n\r\n                                </div>\r\n\r\n\r\n\r\n\r\n");
            WriteLiteral(@"
                            </ul>
                        </div>













                    </div>




                </div>
                <div class=""col-md-8 col-sm-12 container-fluid"">



                    <div v-if=""showLecture"">

                        <div v-if=""videoLink"">
                            <div class=""embed-responsive embed-responsive-16by9 "">
                                <iframe class=""embed-responsive-item"" v-bind:src='videoLink' allowfullscreen></iframe>
                            </div>

                        </div>
                        <div v-else>
                            <h3> Lecture Article </h3>
                            <br />

                            <div class=""p-3 mb-2 bg-light text-dark"" v-html=""lectureArticle""></div>
                        </div>




                        <br>
                        <p class=""text-justify"">Downlodable Content Below of Leture Description Section </p>

            ");
            WriteLiteral(@"            <div class=""jumbotron jumbotron-fluid"">
                            <div class=""container"">

                                <h3>Lecture Description</h3>
                                <br />
                                <br />

                                <p v-html=""lectureDescription"">

                                </p>



                                <hr class=""my-4"">

                                <h3>Downlodable Content</h3>
                                <br />
                                <br />


                                <div class=""row container"">

                                    <div v-for=""item in lectureDownlodableContents"">
                                        <div class=""column"">

                                            <a class=""btn btn-info"" v-bind:href=""item.link"" download rel=""noopener noreferrer"" target=""_blank"">


                                                {{item.name}}
                                     ");
            WriteLiteral(@"           <svg width=""1em"" height=""1em"" viewBox=""0 0 16 16"" class=""bi bi-download"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                                                    <path fill-rule=""evenodd"" d=""M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z"" />
                                                    <path fill-rule=""evenodd"" d=""M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3z"" />
                                                </svg>

                                            </a>
                                        </div>
                                        &nbsp;
                                        &nbsp;
                                        &nbsp;

                                    </div>




                                </div>



                            </div>
         ");
            WriteLiteral("               </div>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "082461e6fb8ad708c1c9203a49f949c26e678fd411181", async() => {
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
                WriteLiteral("\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<EdtechTest2.Areas.Instructor.ViewModels.CoursePreviewViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
