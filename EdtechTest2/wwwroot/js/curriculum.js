const geturl = "/instructor/course/GetAllSections/";



new Vue({
    el: '#vue-app',

    data: {
        show:false,
        courseId: '',
       sectionName:'',
        resultObj: {},
      


    },

    methods: {

        getSections: function (a) {
            axios.get(geturl + a).then(response => {
                this.resultObj = response.data

                
            })
        },



        processForm:async function (a) {

            this.courseId = a;

            console.log("course Id:" + this.courseId + "Content Name" + this.sectionName);
            const formData = new FormData();

            formData.append('sectionName', this.sectionName);
            formData.append('courseId', this.courseId);

          

            //};
          await   axios.post('/instructor/course/AddSection', formData).then((response) => {
                //console.log(my_object);
              const data = response.data;
              if (response.status==200) {
                  toastr.success("Added it Sucessfully");

              }


                //console.log(response);
          }).catch(function (error) {
              if (error.response) {
                  toastr.error("Error in Adding It");

                  console.log(error.response.status);
              }
              
          });

            this.getSections(this.courseId);

           // console.log(this.resultObj);



        }



    },


    mounted() {
        this.getSections(this.courseId);
    }
});