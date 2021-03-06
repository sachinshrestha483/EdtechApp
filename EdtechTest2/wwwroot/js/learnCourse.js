
const url = "/Student/Home/learnApi/";
const getRating = "/Student/Home/HasRating/";

const setRating = "/Student/Home/SetRating/";

const UpdateRating = "/Student/Home/UpdateRating/";

new Vue({
    // el means element which it going to control
    // now after giving it the id of the dom  vue make a connection between the this insrtance and  that given name..
    //this instance goinfg to dcontrol  every thing  to do with the dom element  every thing inside it 


    el: '#vue-app',
    data: {











        Rfullstar: 0,
        Rhalfstar: 0,

        fullstar: 0,
        halfstar: 0,


        half: "M5.354 5.119L7.538.792A.516.516 0 0 1 8 .5c.183 0 .366.097.465.292l2.184 4.327 4.898.696A.537.537 0 0 1 16 6.32a.55.55 0 0 1-.17.445l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256a.519.519 0 0 1-.146.05c-.341.06-.668-.254-.6-.642l.83-4.73L.173 6.765a.55.55 0 0 1-.171-.403.59.59 0 0 1 .084-.302.513.513 0 0 1 .37-.245l4.898-.696zM8 12.027c.08 0 .16.018.232.056l3.686 1.894-.694-3.957a.564.564 0 0 1 .163-.505l2.906-2.77-4.052-.576a.525.525 0 0 1-.393-.288L8.002 2.223 8 2.226v9.8z",
        full: "M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.283.95l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z",
        empty: "M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.523-3.356c.329-.314.158-.888-.283-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767l-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288l1.847-3.658 1.846 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.564.564 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z",



        o: {
            star: "M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.523-3.356c.329-.314.158-.888-.283-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767l-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288l1.847-3.658 1.846 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.564.564 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z",
        },
        s: {
            star: "M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.523-3.356c.329-.314.158-.888-.283-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767l-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288l1.847-3.658 1.846 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.564.564 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z",
        },
        t: {
            star: "M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.523-3.356c.329-.314.158-.888-.283-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767l-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288l1.847-3.658 1.846 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.564.564 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z",
        },
        f: {
            star: "M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.523-3.356c.329-.314.158-.888-.283-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767l-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288l1.847-3.658 1.846 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.564.564 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z",
        },
        fi: {
            star: "M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.523-3.356c.329-.314.158-.888-.283-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767l-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288l1.847-3.658 1.846 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.564.564 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z",
        },














        rating: "",
        ratingText:"",
        ratingObject: {},
        showRating: false,
        oldRating: false,
        NewRating:false,


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
        this.loading = true;
        await axios.get(url + this.courseId).then(response => {
            this.results = response.data
            console.log("Response:" + response.data);
            console.log("Course Id:" + this.courseId);


        })

        this.loading = false;

        this.getRatingfun();
    }
    ,
    methods: {
        // can acess the yhe values of the data 
        // this means the instance it self


        changeRating: function () {
            this.countStar();
            const formData = new FormData();

            formData.append('courseId', this.courseId);
            formData.append('halfStar', this.Rhalfstar);
            formData.append('fullstar', this.Rfullstar);
            formData.append('ratingText', this.ratingText);


            axios.post(UpdateRating, formData).then((response) => {

                if (response.status == 200) {
                    toastr.success("Review Updateed Sucessfully");



                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Review Not Updated ");

                    //   console.log(error.response.status);
                }
                console.log("Preview function");

            });


            this.showRating = false;
            this.oldRating = false;
            this.NewRating = false;


            this.getRatingfun();
        },



        countStar: function () {


            if (this.o.star == this.full) {
                this.fullstar++;
            }
            else if (this.o.star == this.half) {
                this.halfstar++;
            }


            if (this.s.star == this.full) {
                this.fullstar++;
            }
            else if (this.s.star == this.half) {
                this.halfstar++;
            }




            if (this.t.star == this.full) {
                this.fullstar++;
            }
            else if (this.t.star == this.half) {
                this.halfstar++;
            }




            if (this.f.star == this.full) {
                this.fullstar++;
            }
            else if (this.f.star == this.half) {
                this.halfstar++;
            }



            if (this.fi.star == this.full) {
                this.fullstar++;
            }
            else if (this.fi.star == this.half) {
                this.halfstar++;
            }

            this.Rfullstar = this.fullstar;
            this.Rhalfstar = this.halfstar;

            this.fullstar = 0;
            this.halfstar = 0;


        },




        firstStar: function () {
            if (this.o.star == this.empty) {
                this.o.star = this.half



                return;
            }


            if (this.o.star == this.half) {
                this.o.star = this.full


                return;
            }


            if (this.o.star == this.full) {
                this.o.star = this.empty


                return;
            }

        },


        SecondStar: function () {

            if (this.o.star == this.empty || this.o.star == this.half) {
                return
            }

            if (this.s.star == this.empty) {
                this.s.star = this.half


                return;
            }


            if (this.s.star == this.half) {
                this.s.star = this.full



                return;
            }


            if (this.s.star == this.full) {
                this.s.star = this.empty


                return;
            }
        },


        ThirdStar: function () {

            if (this.s.star == this.empty || this.s.star == this.half) {
                return
            }
            if (this.t.star == this.empty) {
                this.t.star = this.half


                return;
            }


            if (this.t.star == this.half) {
                this.t.star = this.full


                return;
            }


            if (this.t.star == this.full) {
                this.t.star = this.empty


                return;
            }
        },


        fourthStar: function () {

            if (this.t.star == this.empty || this.t.star == this.half) {
                return
            }


            if (this.f.star == this.empty) {
                this.f.star = this.half



                return;
            }


            if (this.f.star == this.half) {
                this.f.star = this.full

                return;
            }


            if (this.f.star == this.full) {
                this.f.star = this.empty

                return;
            }
        },



        fifthStar: function () {
            if (this.f.star == this.empty || this.f.star == this.half) {
                return
            }


            if (this.fi.star == this.empty) {
                this.fi.star = this.half

                return;
            }


            if (this.fi.star == this.half) {
                this.fi.star = this.full
                return;
            }


            if (this.fi.star == this.full) {
                this.fi.star = this.empty
                return;
            }
        },













        submitRating: function () {


            this.countStar();
            const formData = new FormData();

            formData.append('courseId', this.courseId);
            formData.append('halfStar', this.Rhalfstar);
            formData.append('fullstar', this.Rfullstar);
            formData.append('ratingComment', this.ratingText);


            axios.post(setRating, formData).then((response) => {

                if (response.status == 200) {
                    toastr.success("Review Added Sucessfully");
                   


                }

                const data = response.data;
                console.log(data);
            }).catch(function (error) {
                if (error.response) {
                    toastr.error("Review Not Added ");
                  
                    //   console.log(error.response.status);
                }
                console.log("Preview function");

            });


            this.showRating = false;
            this.oldRating = false;
            this.NewRating = false;


            this.getRatingfun();

        },



        showRatingFun: async function () {

            //use of await function  is very important

           await  this.getRatingfun();
           

            this.showRating = !this.showRating;
        },


        getRatingfun: async function(){

            await axios.get(getRating + this.courseId).then(response => {

                this.rating = response;

                console.log("Response:" + response.data);

                if (response.data.rating == 0) {



                    this.NewRating = true;
                }
                else if (response.data.rating == 1) {
                    ratingObject = response.data.ratingObject;




                    var h = response.data.ratingObject.halfStar;
                    var f = response.data.ratingObject.fullStar;

                    console.log("Half Star :" + h);
                    console.log("Full Star :" + f);



                    var stars = [this.o, this.s, this.t, this.f, this.fi];




                    var current = 0;


                    for (i = 0; i < f; i++) {
                        stars[i].star = this.full;
                        current++;
                    }




                    if (h == 1) {

                        console.log("Printing Half Star at pos :" + current)
                        stars[current].star = this.half;
                    }

                    console.log("Rating Text:" + response.data.ratingObject.ratingComment);
                    this.ratingText = response.data.ratingObject.ratingComment;


                    this.oldRating = true;








                }
               


            })
        },



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