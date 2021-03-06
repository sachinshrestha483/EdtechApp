const AddToCartLink = "/Student/course/AddToCart/";
const CheckCoupon ="/Student/course/CheckCouponForCourse/"
new Vue({
    el: '#vue-app',
    data: {


        //variable for this  page 

        couponcode: 0,
        couponcodeText:"LL",
        videoSelected: false,
        videoLink: '',
        response: '',
        newPrice: 0,
        discountValue:0,
        byPrice: false,
        byDiscount: false,
        couponObj: null,
        couponTrue: null,
        couponmessage: "",

        respObj: {},
        //








       
      
    }
    ,
    async  mounted() {

    //    this.loadAllcoupons();

        //  this.mainPage();


    }
    ,
    methods: {

        // This Page Functions

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

                   
                        toastr.error("Error in Adding Coupon");

                    


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