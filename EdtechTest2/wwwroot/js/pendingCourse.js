
const url = "/Admin/Course/PendingCoursesList";

const courseReviewAction = "/Admin/Course/PendingCourseOperation/"

new Vue({
    // el means element which it going to control
    // now after giving it the id of the dom  vue make a connection between the this insrtance and  that given name..
    //this instance goinfg to dcontrol  every thing  to do with the dom element  every thing inside it 


    el: '#vue-app',
    data: {
        courseId: 0,

        selectedrejectCourseId:0,
        couseRejectionReason:"",


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


        this.loadCourse();
        
    }
    ,
    methods: {
        // can acess the yhe values of the data 
        // this means the instance it self

        




        loadCourse: async  function ()  {

            console.log("Function Running");

            this.loading = true;
            await axios.get(url).then(response => {
                this.results = response.data
                console.log("Response:" + response.data);


                // console.log(this.results.course.courseStatus);
                console.log("function Ended");



            })

            this.loading = false;
        },



        rejectTheCourse: async function (id) {


            const formData = new FormData();
            formData.append('coursesId', id);
            formData.append('approved', 0);
            formData.append('rejected', 1);
            formData.append('reasonsForRejection', this.couseRejectionReason);
            await axios.post(courseReviewAction, formData).then((response) => {


                if (response.status == 200) {
                    toastr.success("Course Rejected ");
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
                    toastr.error("Course Not Rejected");
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

            });
            console.log("Operation  function");
            this.loadCourse();
        },



        approveThecourse:async function (id) {
            const formData = new FormData();
            formData.append('coursesId', id);
            formData.append('approved', 1);
            formData.append('rejected', 0);
            formData.append('reasonsForRejection', "");

         await    axios.post(courseReviewAction, formData).then((response) => {


                if (response.status == 200) {
                    toastr.success("Course Approved ");
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
                    toastr.error("Course Not Approved");
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
                
            });
            console.log("Operation  function");
            this.loadCourse();
        },



        showRejectLectureSection: function(id){

            if (this.selectedrejectCourseId == id) {
                this.selectedrejectCourseId = 0;
                return;
            }

            this.selectedrejectCourseId = id;


        },










        sendForApproval: function (id) {


            axios.post(approvalLink + id).then((response) => {


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