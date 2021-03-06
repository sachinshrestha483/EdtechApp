var vl;

new Vue({
    el: '#vue-app',

    data: {
        courseId:'',
        length: '',
        uploading: false,
        showlengthbox:false,
        progress: 0,

        selectedFile: null,

        

    },

    methods: {

        SetValue: function (x) {
            console.log("set value is:"+x);
         },

        FileUp: function (e) {
            console.log("File Up Called");
            this.selectedFile = event.target.files[0]
            const data = URL.createObjectURL(event.target.files[0]);
            var video = document.createElement("VIDEO");
            video.src = data;
            video.preload = 'metadata';
            video.onloadedmetadata = function () {
                window.URL.revokeObjectURL(video.src);
                var duration = video.duration;

                document.getElementById("id1").value = Math.round(duration / 60);


            
            }

            //  console.log(this.length);
          //  console.log(video.duration);
           
        },

      

        processForm: function (a) {
            //IFormFile CourseIntroductionVideo,int CourseId, int Duration
           // this.length = document.getElementById("id1").value
            this.length = document.getElementById("id1").value

            console.log(this.length);
            console.log("the value of  the a is :" + this.length);
            this.courseId = a;
            const formData = new FormData();
            formData.append('CourseIntroductionVideo', this.selectedFile, this.selectedFile.name);
            formData.append('CourseId', this.courseId);
            formData.append('Duration', this.length);

            console.log("From Our Side" +"Course ID:"+ this.courseId +"Course Length:" +this.length);
            //var my_object = {
            //    name: this.name,
            //    email: this.email,
            //    fd: formData,


            //};
            axios.post('/instructor/course/UCourseIntroVideo', formData, {
                onUploadProgress: progressEvent => {
                    this.uploading = true;
                    this.progress = Math.round(progressEvent.loaded / progressEvent.total * 100);

                    if (Math.round(progressEvent.loaded / progressEvent.total * 100) == 100) {
                        this.uploading = false;
                    }

                  

                }
            }).then((response) => {
                //console.log(my_object);
                const data = response.data;
                console.log(response);
            });


            

        }


        
    }



});