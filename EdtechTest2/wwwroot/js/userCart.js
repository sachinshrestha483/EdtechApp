const cartCoursesLink = "/student/Cart/CartData/"

const deletecourse = "/student/Cart/DeleteCartItems/"


const totalCart = "/student/Cart/CartTotalData/"

  


new Vue({
    el: '#vue-app',
    data: {


        //variable for this  page 
        results: {},
        totalCartObject: {},
        //










    },
    async  mounted() {

        //    this.loadAllcoupons();

        this.ListCartCourses();
        this.TotalCartValue();
        //  this.mainPage();


    }
    ,
    methods: {

        // This Page Functions

        TotalCartValue: async function () {
            await axios.get(totalCart).then(response => {
                this.totalCartObject = response.data
                console.log("Total Cart:"+this.totalCartObject);



            })
           
            //this.TotalCartValue();
        },


        ListCartCourses: async function () {
            await axios.get(cartCoursesLink).then(response => {
                this.results = response.data
                console.log(this.results);
              


            })
            
        },

        deleteCourse: async function (id) {

            await axios.post(deletecourse+id).then((response) => {

                console.log(response);




                if (response.status == 200) {

                    toastr.success("Deleted Course SucessFully From Cart");



                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {

                    toastr.error("Not Able to Delete Course");


                    //   console.log(error.response.status);
                }


            });
            this.ListCartCourses();

            this.TotalCartValue();
        },



        addToCart: async function (id) {

            console.log("Calling the Functin");
            const formData = new FormData();
            formData.append('id', id);
            formData.append('couponId', this.couponcode);

            await axios.post(AddToCartLink + id, formData).then((response) => {

                console.log(response);




                if (response.status == 200) {

                    toastr.success("Added To Cart SucessFully");



                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {

                    toastr.error("Error in Adding Course to Cart");


                    //   console.log(error.response.status);
                }


            });



        },

        checkCoupon: async function (id) {

            //couponObj.data.newPrice


            const formData = new FormData();
            formData.append('courseId', id);
            formData.append('couponCode', this.couponcodeText);

            await axios.post(CheckCoupon, formData).then((response) => {
                if (response.status == 200) {

                    //   console.log("Response.newPrice:" + response.newPrice)
                    this.couponObj = response;

                    // console.log("response :"+response);

                    // console.log("new Price :" + response.newPrice);






                    toastr.success("  Coupon Code  Applied Sucessfully");



                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {


                    toastr.error(response.data.error);




                    //   console.log(error.response.status);
                }
                console.log("Coupon Added");

            });


            if (this.couponObj == null) {

                this.couponTrue = false;
                this.couponmessage = "Coupon Not Correct Or Expired";



            }

            else {



                this.couponTrue = true;
                this.couponcode = this.couponObj.data.id;
                this.couponmessage = "Coupon Added Sucessfully";



            }


        },

        selectedVideo: function (link) {
            // this.selectedVideo = true;

            if (this.videoSelected == true) {
                this.videoSelected = false;
                this.videoLink = link;
                this.videoSelected = true;
                console.log("this.VideoLink:" + this.videoLink);
                document.getElementById("myVideo").load();

                return;

            }

            else {


                this.videoSelected = true;





                this.videoLink = link;

                document.getElementById("myVideo").load();
                console.log("this.VideoLink:" + this.videoLink);


                // this.videoSelected = true;
                //   Vue.forceUpdate();


            }

            // console.log("its False Now")
            //this.videoSelected = true;

        },




        //



        // can acess the yhe values of the data
        // this means the instance it self










    }
    //dosent control anything outside the el 


});