
const url = "/Instructor/course/CurrentCourseStatusApi/";

const approvalLink = "/Instructor/course/SendCourseForApproval/";


const publishCourseLink = "/Instructor/course/PublishCourse/";

const unPublishCourseLink ="/Instructor/course/UnPublishCourse/"

new Vue({
    // el means element which it going to control
    // now after giving it the id of the dom  vue make a connection between the this insrtance and  that given name..
    //this instance goinfg to dcontrol  every thing  to do with the dom element  every thing inside it 


    el: '#vue-app',
    data: {
        courseId: 0,
        lectureDescription: "",
        lectureName: "",

        loading: false,

        lectureArticle: "",



        currentLectureId: 0,

        lectureDownlodableContents: [],
        showLecture: false,
        results: {},
        videoLink: "",
        isChecked: true,
        isNotChecked: false,
    }
    ,
    async  mounted() {


        this.mainPage();


    }
    ,
    methods: {
        // can acess the yhe values of the data 
        // this means the instance it self


        mainPage: async  function () {

            console.log("Function Running");

            this.loading = true;
            await axios.get(url + this.courseId).then(response => {
                this.results = response.data
                console.log("Response:" + response.data);
                console.log("Course Id:" + this.courseId);


                // console.log(this.results.course.courseStatus);
                console.log("function Ended");



            })

            this.loading = false;
        },


        unpublishCourse: async function (id) {

            await axios.post(unPublishCourseLink + id).then((response) => {


                if (response.status == 200) {
                    toastr.success("Course UnPublished Sucessfully ");
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }


                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Course Not Able To Be UnPublished ");
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }

                    //   console.log(error.response.status);
                }
                console.log("Preview function");

            });

            this.mainPage();

        },


        publishCourse: async function (id) {

            await axios.post(publishCourseLink + id).then((response) => {


                if (response.status == 200) {
                    toastr.success("Course Published Sucessfully ");
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }


                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Course Not Able To Be Published SucessFully");
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }

                    //   console.log(error.response.status);
                }
                console.log("Preview function");

            });

            this.mainPage();


        },

        sendForApproval:async  function (id) {

          
         await   axios.post(approvalLink+id).then((response) => {


                if (response.status == 200) {
                    toastr.success("Course Submitted For Approval Sucessfully ");
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }


                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Course Not Able To Be Submitted  for Approval Some Error Occured");
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }

                    //   console.log(error.response.status);
                }
                console.log("Preview function");

            });

            this.mainPage();

        }
        ,


        showLectureFunction: function (lecture) {

            this.showLecture = true;

            this.lectureDescription = lecture.lectureDescription;

            this.lectureName = lecture.name;


            this.videoLink = lecture.lectureContentLink;

            console.log(this.videoLink);

            this.lectureArticle = lecture.lectureArticle;

            this.lectureDownlodableContents = lecture.downlodableContents;

            this.currentLectureId = lecture.lecId;

            console.log(this.currentLectureId);











        },




    }
    //dosent control anything outside the el 


});