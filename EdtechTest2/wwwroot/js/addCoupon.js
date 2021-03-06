//final 
const url = "/Instructor/course/CurrentCourseStatusApi/";

const approvalLink = "/Instructor/course/SendCourseForApproval/";


const publishCourseLink = "/Instructor/course/PublishCourse/";

const unPublishCourseLink = "/Instructor/course/UnPublishCourse/"

const blockUnblockCouponLink = "/Instructor/course/BlockUnblockCoupon/"

//





//This Page  Constants

const createCoupon = "/Instructor/course/AddCoupon/";
const couponListUrl = "/Instructor/course/CouponsList/";
const increseCouponCount = "/Instructor/course/IncreaseCoupon/";
const decreaseCouponCount = "/Instructor/course/DecreaseCoupon/";




//



new Vue({
    // el means element which it going to control
    // now after giving it the id of the dom  vue make a connection between the this insrtance and  that given name..
    //this instance goinfg to dcontrol  every thing  to do with the dom element  every thing inside it 


    el: '#vue-app',
    data: {


        //variable for this  page 


        couponcode: "",
        numberofCouponsAlloted: 0,
        discountbyPrice: false,
        discountbyPercent: false,
        discountvlaue: 0,
        couponsValidtill:"",
        couponsList:[],



           



        //


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

        this.loadAllcoupons();

      //  this.mainPage();


    }
    ,
    methods: {
        // can acess the yhe values of the data 
        // this means the instance it self




        // function for this page

        blockUnblockCoupon: async function (id) {
            await axios.post(blockUnblockCouponLink + id).then((response) => {
                if (response.status == 200) {
                    toastr.success("Operation On Coupon Done ");



                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Operation On Coupon Not Done");


                    //   console.log(error.response.status);
                }
                console.log("Coupon Added");

            });



            this.loadAllcoupons();

        },


        loadAllcoupons: async function () {
            await axios.get(couponListUrl + this.courseId).then(response => {
                this.couponsList = response.data
                console.log("Response:" + response.data);


                // console.log(this.results.course.courseStatus);
                console.log("function Ended");



            })
        },


        createCoupons:async  function () {
          

         if (this.isChecked == false && this.isChecked == false) {
        toastr.error("Select Either of The By Discount or Price ");
        return;
            }
            if (this.couponcode == "") {
                toastr.error("Set Coupun Code ");
                return;
            }

            if (this.discountvlaue ==0 ) {
                toastr.error("Set  The Discount Value Correct ");
                return;
            }

            if (this.couponsValidtill == "") {
                toastr.error("Set The Coupun Valid Date");
                return;
            }





        

            const formData = new FormData();


            formData.append('courseId', this.courseId);
            formData.append('couponCode', this.couponcode);
            formData.append('validtill', this.couponsValidtill);

            if (this.discountbyPercent == true) {
                formData.append('discountType',1);
            }
            else {
                formData.append('discountType', 0);
            }

           
            formData.append('discountValue', this.discountvlaue);
            formData.append('couponAlloted', this.numberofCouponsAlloted);
           

          


           await  axios.post(createCoupon, formData).then((response) => {
                if (response.status == 200) {
                    toastr.success("Coupon  Added ");
                    


                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Course Not Able To Be UnPublished ");
                    

                    //   console.log(error.response.status);
                }
                console.log("Coupon Added");
               
            });



            this.loadAllcoupons();











        },


        increaseCoupon: async function (id) {
            await axios.post(increseCouponCount+id).then((response) => {
                if (response.status == 200) {
                    toastr.success("Coupon  Added ");



                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Coupon Count Not  Added");


                    //   console.log(error.response.status);
                }
                console.log("Coupon Added");

            });



            this.loadAllcoupons();

        },

        decreaseCoupon: async function (id) {
            await axios.post(decreaseCouponCount+id).then((response) => {
                if (response.status == 200) {
                    toastr.success("Coupon  Decreased ");



                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Coupon  Not Decreased");


                    //   console.log(error.response.status);
                }
                console.log("Coupon Added");

            });



            this.loadAllcoupons();

        },

        







        // this page






        mainPage: async function () {

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

        sendForApproval: async function (id) {


            await axios.post(approvalLink + id).then((response) => {


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