new Vue({
    // el means element which it going to control
    // now after giving it the id of the dom  vue make a connection between the this insrtance and  that given name..
    //this instance goinfg to dcontrol  every thing  to do with the dom element  every thing inside it 
    el: '#vue-app',
    data: {
        checkedNames: [],
        courseId: 0,
        isChecked: true,
        isNotChecked:false,
    }
    ,
    methods: {
        // can acess the yhe values of the data 
        // this means the instance it self


        setPreviewLecture: function (lectureId) {

            console.log("Preview function");

            const formData = new FormData();
            formData.append('lectureId', lectureId);
            axios.post('/Instructor/course/SetUnsetPreview', formData).then((response) => {


                if (response.status == 200) {
                    toastr.success("Operation on Lecture Done SecessFully");
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
                    toastr.error("Error in Doing Operation on the Lecture ");
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
        },


        showorHideLecture: function (lectureId) {

            console.log("function Get called");

            const formData = new FormData();
            formData.append('lectureId', lectureId);



            axios.post('/Instructor/course/ShowHideLecture', formData).then((response) => {


                if (response.status == 200) {
                    toastr.success("Operation on Lecture Done SecessFully");
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
                    toastr.error("Error in Doing Operation on the Lecture ");
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

        },


        showOrHideSection: function (sectionId) {


            console.log("Function Get Called");
            const formData = new FormData();
            formData.append('sectionId', sectionId);



            axios.post('/Instructor/course/ShowHideSection', formData).then((response) => {


                if (response.status == 200) {
                    toastr.success("Operation on Section Done SecessFully");
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
                    toastr.error("Error in Doing Operation on the Section");
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
            console.log("Function done");

        },


        processForm: function (e) {
            const formData = new FormData();
            formData.append('id', this.checkedNames);


            for (var i = 0; i < this.checkedNames.length; i++) {

                formData.append('lectureIds', this.checkedNames[i]);

            }


       //     formData.append('lectureIds', this.name);


            axios.post('/Instructor/course/SetFreeLectures', formData).then((response) => {


                if (response.status == 200) {
                    toastr.success("Lectures Added As Free Lecture..");
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
                    toastr.error("Lectures Cant be  Added As Free Lecture.");
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




        },


    }
    //dosent control anything outside the el 


});