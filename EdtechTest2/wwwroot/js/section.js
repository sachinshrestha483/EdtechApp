const getLecturesurl = "/instructor/course/LecturesList/";
const getlectureWithContent = "/Instructor/Course/LectureDetailsContent/"
const DeleteDownlodableContentUrl = "/Instructor/Course/DeleteDownlodableContent/"
const EditAddDownlodableContent = "/Instructor/Course/AddDownlodableContent/"
const SaveEditedLectureUrl = "/Instructor/Course/SaveEditedLecture"
const DeleteLectureUrl ="/Instructor/Course/DeleteLecture/"
//DeleteDownlodableContent




new Vue({
    el: '#vue-app',

    data: {

        AddlectureName: "",
        sectionId: "",
        AddLectureVideoDuration: 0,
        AddlectureArticle: "",
        AddlectureDescription: "",
        selectedAddLectureVideoFile: null,
        DownlodableAddContentFile: null,
        progress: 0,
        lecturesList: [],
        ToEditLectureId: 0,
        EditLectureobj: null,
        EditLectureArticle: "",
        EditLectureDescription: "",
        EditAddDownlodableContentFile: null,
        temp: 0,
        EditLectureVideo: null,
        EditedLectureVideoLength:0,
      

        
      


        // show or hide box
        showUploading: false,
        showAddLecture: true,
        showLectureVideoUploadDetails: false,
        showLectureArticle: false,
        showLectureDescription: false,
        showArticleUploading: false,
        showEditAddVideoInsteadofArticle: false,
        showEditAddArticleInsteadofVideo: false,
        showLoaderEditAddingDownlodableContent: false,
        showSaveEditedLectureSpinner: false,
        showEditedVideoDetails: false,
        showSpinnerEditLecture:false,
        //

    },
    methods: {
        //AddLectureDescriptionText AddLectureArticleText


        DeleteLecture: function () {

            axios.delete(DeleteLectureUrl + this.ToEditLectureId).then((response) => {

                if (response.status == 200) {


                    toastr.success("Lecture Deleted Sucessfully");



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

                    if (response.status == 200) {
                        // this.getLectures(this.sectionId)
                        // var x = this.ToEditLectureId;
                        tinymce.remove("#EditlectureDescriptionId");
                        tinymce.remove("#EditlectureArticleId");
                        this.ToEditLectureId = 0;
                        this.getLectures(this.sectionId)
                       
                        // this.ToEditLectureId = x;
                    }

                }

                else {
                   
                    toastr.error(" Downlodable Content Not Deleted  Sucessfully");
                }



            });
            
        },

        EditLectureVideoFileUp: async function (e) {
            

           // this.showLectureVideoUploadDetails = true;

            //file = event.target.files[0];
            //filesData.append('file', this.file);
            //this.myFile = 's';
           
            console.log("Adding File");
            this.EditLectureVideo = event.target.files[0];
            this.showEditedVideoDetails = true;
            console.log(this.EditLectureVideo);
            const data = URL.createObjectURL(event.target.files[0]);
            var video = document.createElement("VIDEO");
            video.src = data;
            video.preload = 'metadata';
            video.onloadedmetadata = function () {
                window.URL.revokeObjectURL(video.src);
                var duration = video.duration;

                document.getElementById("EditvideoLengthId").value = Math.round(duration / 60);
                                                                


            }
           // console.log("Video length:" + this.EditedLectureVideoLength);
        },


        SaveEditLectureData: async function () {


            // nullify it later so not get error at later......
           //we send all the things article description and the video ....

            try {
                var editlectureArticle = tinymce.get("EditlectureArticleId").getContent();


            }
            catch{
                var editlectureArticle = null;
            }

            try {
                var editlectureDescription = tinymce.get("EditlectureDescriptionId").getContent();
               



                console.log("-------------" + editlectureDescription + "-------------");
                console.log("-------------" + editlectureArticle + "-------------");


                this.EditLectureArticle = editlectureArticle;
                this.EditLectureDescription = editlectureDescription;
                
                console.log("in try");

            } catch (e) {
               var  editlectureDescription=null
               // this.EditLectureArticle = "";
                //this.EditLectureDescription = "";
                console.log("in Catch");
            }


           
              

            
          

            console.log("Edit Lecture Submit");
            console.log(this.EditLectureArticle);
            console.log(this.EditLectureDescription);
            try {
                this.EditedLectureVideoLength = document.getElementById("EditvideoLengthId").value
            }
            catch{

            }
            console.log("Video length:" + this.EditedLectureVideoLength);

            const formData = new FormData();
            formData.append('Id', this.ToEditLectureId);
            formData.append('EditLectureArticle', this.EditLectureArticle);
            formData.append('EditLectureDescription', this.EditLectureDescription);
            formData.append('EditLectureVideo', this.EditLectureVideo);
            formData.append('Length', this.EditedLectureVideoLength);
            this.showSpinnerEditLecture = true;
            this.showSaveEditedLectureSpinner = true;

            axios.post(SaveEditedLectureUrl, formData).then((response) => {


               


                if (response.status == 200) {
                    toastr.success("Edited Lecture Sucessfully");
                    this.showSaveEditedLectureSpinner = false;
                    this.showSpinnerEditLecture = false;

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
                    //  this.sectionName = this.getSection(this.sectionId);
                }
                else {
                    toastr.error("Error in Editing lectuture");
                    this.showSaveEditedLectureSpinner = false;
                    this.showSpinnerEditLecture = false;

                    //  this.sectionName = this.getSection(this.sectionId);


                }
                tinymce.remove("#EditlectureDescriptionId");
                tinymce.remove("#EditlectureArticleId");
                this.showEditedVideoDetails = false;
                this.EditLectureVideo = null;
                this.temp = this.ToEditLectureId;
                this.EditLecture(0);
                this.EditLecture(this.temp);
                


                //console.log(my_object);
                //const data = response.data;
                //console.log(data);
            });




        },




        AddDownlodableContentFileUp: async function (e) {
            console.log("Adding File");
            this.EditAddDownlodableContentFile = event.target.files[0];
            console.log(this.EditAddDownlodableContentFile);


        },

        AddDownlodableContent: async function (lectureId) {

            if (this.EditAddDownlodableContentFile != null) {

                const formData = new FormData();
                formData.append('id', lectureId);
                formData.append('downlodableContent', this.EditAddDownlodableContentFile);
                this.showLoaderEditAddingDownlodableContent = true;
                axios.post(EditAddDownlodableContent, formData).then((response) => {
                    //  console.log(my_object);
                   // const data = response.data;
                    if (response.status == 200) {
                        toastr.success("Added Content Sucessfully");
                        this.showLoaderEditAddingDownlodableContent = false;
                        //this.showUploading = false;
                        //this.showArticleUploading = false;


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
                        //  this.sectionName = this.getSection(this.sectionId);

                        console.log("dwd");

                        this.temp = this.ToEditLectureId;
                        this.EditLecture(0);
                        this.EditLecture(this.temp);
                    }
                    else {
                        toastr.error("Error in Adding Content");
                        this.showLoaderEditAddingDownlodableContent = false;

                        //  this.sectionName = this.getSection(this.sectionId);


                    }

                    console.log(response);
                });

            }
            else {
                toastr.error("Not Able To Upload The File");
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

        },


        showEditAddArticleInsteadofVideofun: async function () {

            if (this.showEditAddArticleInsteadofVideo == true) {
                tinymce.remove("#EditlectureArticleId");
                this.showEditAddArticleInsteadofVideo = false;
                return;

            }

            this.showEditAddArticleInsteadofVideo = true;
            await tinymce.init({
                selector: 'textarea',
                plugins: 'advlist autolink lists link  charmap print preview hr anchor pagebreak',
                toolbar_mode: 'floating',
            });
            await tinymce.init({
                selector: 'textarea',
                plugins: 'advlist autolink lists link  charmap print preview hr anchor pagebreak',
                toolbar_mode: 'floating',
            });
        },

        EditLecture: async function (id) {


            if (this.ToEditLectureId == id) {
                tinymce.remove("#EditlectureDescriptionId");
                tinymce.remove("#EditlectureArticleId");


               
                this.ToEditLectureId = 0;
               


               
                return;

            }



            this.ToEditLectureId = id;



           await  axios.get(getlectureWithContent + this.ToEditLectureId).then(response => {
                console.log("Edit");
               this.EditLectureobj = response.data
               this.EditLectureArticle = response.data.article;
               this.EditLectureDescription = response.data.description;

               console.log(this.EditLectureArticle);
               console.log(this.EditLectureDescription);

               //tinymce.get("#EditlectureDescriptionId").setContent("<p>Hello world!</p>");
               //tinymce.get("#EditlectureArticleId").setContent("<p>Hello world!</p>");

               // console.log( this.EditLectureobj);


              

                // this.sectionName = response.data.name


            })


            await tinymce.init({
                selector: 'textarea',
                plugins: 'advlist autolink lists link  charmap print preview hr anchor pagebreak',
                toolbar_mode: 'floating',
            });


            await tinymce.init({
                selector: 'textarea',
                plugins: 'advlist autolink lists link  charmap print preview hr anchor pagebreak',
                toolbar_mode: 'floating',
            });


           


           
            console.log("Function Working");
            console.log(this.ToEditLectureId);
           

            
        },


        deleteDownlodableContent: async function (contentId) {
            var id;
            console.log("Delete File");
            axios.delete(DeleteDownlodableContentUrl + contentId).then((response) => {

                if (response.status == 200) {


                    toastr.success("Deleted Downlodable Content Sucessfully");



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

                    if (response.status == 200) {
                        // this.getLectures(this.sectionId)
                       // var x = this.ToEditLectureId;
                        this.temp = this.ToEditLectureId;
                        this.EditLecture(0);
                        this.EditLecture(this.temp);
                       // this.ToEditLectureId = x;
                    }

                }

                else {

                    toastr.error(" Downlodable Content Not Deleted  Sucessfully");
                }



            });






        },


        SelectedLectureVideoFileUp: function (e) {

            this.showLectureVideoUploadDetails = true;

            //file = event.target.files[0];
            //filesData.append('file', this.file);
            //this.myFile = 's';
            this.selectedAddLectureVideoFile = event.target.files[0];
            const data = URL.createObjectURL(event.target.files[0]);
            var video = document.createElement("VIDEO");
            video.src = data;
            video.preload = 'metadata';
            video.onloadedmetadata = function () {
                window.URL.revokeObjectURL(video.src);
                var duration = video.duration;

                document.getElementById("videoLength").value = Math.round(duration / 60);



            }
        },

        DownlodableContentFileUp: function (e) {
            console.log("Downlodable file");
            this.DownlodableAddContentFile = event.target.files[0];
            console.log("Downlodable file added");

        },

        processAddLectureForm: function (a) {
            var lectureDescription = tinymce.get("lectureDescriptionId").getContent();
            var lectureArticle = tinymce.get("lectureArticleId").getContent();

            this.AddlectureDescription = lectureDescription;
            console.log("LectureDescription:" + lectureDescription);
            this.AddlectureArticle = lectureArticle

            if (this.AddlectureName == '') {
                toastr.error("Lecture Name Required");
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
                return;
            }

            if (this.AddlectureArticle == '' && this.selectedAddLectureVideoFile == null) {
                toastr.error("One of Either Lecture Article or Lecture Video Should Be There");
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
                return;
            }





            




            if (this.selectedAddLectureVideoFile != null) {
                this.AddLectureVideoDuration = document.getElementById("videoLength").value

                console.log(this.AddLectureVideoDuration);



                const formData = new FormData();
                formData.append('LectureName', this.AddlectureName);
                formData.append('LectureVideo', this.selectedAddLectureVideoFile, this.selectedAddLectureVideoFile.name);
                formData.append('SectionId', this.sectionId);
              //  formData.append('LectureArticle', this.AddlectureArticle);
                formData.append('LectureDescription', this.AddlectureDescription);

                formData.append('LectureVideoLength', this.AddLectureVideoDuration);
                formData.append('DownlodableContent', this.DownlodableAddContentFile);

                //console.log("From Our Side" + "Course ID:" + this.courseId + "Course Length:" + this.length);
                ////var my_object = {
                //    name: this.name,
                //    email: this.email,
                //    fd: formData,


                //};
                axios.post('/instructor/course/AddLecture', formData, {
                    onUploadProgress: progressEvent => {
                        this.showUploading = true;
                        this.showArticleUploading = true;

                        this.progress = Math.round(progressEvent.loaded / progressEvent.total * 100);

                        if (Math.round(progressEvent.loaded / progressEvent.total * 100) == 100) {
                        }



                    }
                }).then((response) => {
                    //  console.log(my_object);
                    const data = response.data;
                    if (response.status == 200) {
                        toastr.success("Added Lecture Sucessfully");
                        this.showUploading = false;
                        this.showArticleUploading = false;
                        this.getLectures(this.sectionId)
                        


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
                      //  this.sectionName = this.getSection(this.sectionId);
                    }
                    else {
                        toastr.error("Error in Adding lectuture");
                      //  this.sectionName = this.getSection(this.sectionId);


                    }

                    console.log(response);

                });
            }


            else if (this.AdLectureArticle!='') {
                const formData = new FormData();
                formData.append('LectureName', this.AddlectureName);
               // formData.append('LectureVideo', this.selectedAddLectureVideoFile, this.selectedAddLectureVideoFile.name);
                formData.append('SectionId', this.sectionId);
                formData.append('LectureArticle', this.AddlectureArticle);
                formData.append('LectureDescription', this.AddlectureDescription);

              //  formData.append('LectureVideoLength', this.AddLectureVideoDuration);
                formData.append('DownlodableContent', this.DownlodableAddContentFile);

                //console.log("From Our Side" + "Course ID:" + this.courseId + "Course Length:" + this.length);
                ////var my_object = {
                //    name: this.name,
                //    email: this.email,
                //    fd: formData,


                //};
                axios.post('/instructor/course/AddLecture', formData, {
                    onUploadProgress: progressEvent => {
                        this.showArticleUploading = true;
                        this.progress = Math.round(progressEvent.loaded / progressEvent.total * 100);

                        if (Math.round(progressEvent.loaded / progressEvent.total * 100) == 100) {
                        }



                    }
                }).then((response) => {
                    //  console.log(my_object);
                    const data = response.data;
                    if (response.status == 200) {

                        toastr.success("Added Lecture Sucessfully");
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
                        this.showArticleUploading = false;

                        this.getLectures(this.sectionId);

                       // this.sectionName = this.getSection(this.sectionId);
                    }
                    else {
                        toastr.error("Error in Adding lectuture");
                       // this.sectionName = this.getSection(this.sectionId);


                    }

                    console.log(response);
                });

               
            }

           
            console.log("Clearing the klecture name");
            this.AddlectureName = "";
            console.log("Updating list")
            this.getLectures(this.sectionId);

        },

        getLectures: function (sectionId) {


            axios.get(getLecturesurl + sectionId).then(response => {

                this.lecturesList = response.data
                console.log(this.lecturesList);
               // this.sectionName = response.data.name


            })

        }

         
    


    },
    mounted() {
        this.getLectures(this.sectionId);
     
    }












});
