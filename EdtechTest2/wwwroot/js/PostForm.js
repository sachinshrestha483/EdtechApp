//var file;
//let filesData = new FormData();


new Vue({
    el: '#vue-app',
    data: {
        name: '',
        email: '',
        selectedFile: '',
        myFile: '',
        uploading: false,
        progress:0,
       
            selectedFile: null,
       

    },
    // our methods
    methods: {

        fileUp: function (e) {
            //file = event.target.files[0];
            //filesData.append('file', this.file);
            //this.myFile = 's';
            this.selectedFile = event.target.files[0]
        },


        processForm: function (e) {
            //console.log({ name: this.name, email: this.email });
           
            //if (this.myFile == '') {
            //    axios.post('https://jsonplaceholder.typicode.com/users', {
            //        name: this.name,
            //        email: this.email,
            //        file: {
            //            headers: {
            //                'Content-Type': 'multipart/form-data'
            //            }
            //        },
            //    }).then((response) => {
            //        const data = response.data;
            //        console.log(data);
            //    });
            //}
            //else {
            const formData = new FormData();
            formData.append('fd', this.selectedFile, this.selectedFile.name);
            formData.append('name', this.name);
            formData.append('email', this.email);

            console.log(formData);
            var my_object = {
                name: this.name,
               email: this.email,
                fd: formData,


            };
            axios.post('/student/demo/Data', formData, {
                onUploadProgress: progressEvent => {
                    this.uploading = true;
                    this.progress = Math.round(progressEvent.loaded / progressEvent.total * 100);

                    if (Math.round(progressEvent.loaded / progressEvent.total * 100) == 100) {
                        this.uploading = false;
                    }
                    
                    //this.uploading = true;
                    //console.log(Math.round(progressEvent.loaded / progressEvent.total * 100) + "%");
                    //this.uploading = false;


                }
            }).then((response) => {
                console.log(my_object);
                const data = response.data;
                console.log(response);
            });


            //axios.post('/student/demo/Data',
            //    filesData,
            //    {
            //        headers: {
            //            'Content-Type': 'multipart/form-data'
            //        }
            //    }
            //).then((response) => {
            //        const data = response.data;
            //        console.log(data);
            //        console.log("PostedWithFile");
            //    });
           // }


            // alert('Processing!'+this.name+" "+this.email);

        }
    }

})