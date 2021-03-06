
const url = "/Instructor/course/CoursePreviewAPI/";



new Vue({
    // el means element which it going to control
    // now after giving it the id of the dom  vue make a connection between the this insrtance and  that given name..
    //this instance goinfg to dcontrol  every thing  to do with the dom element  every thing inside it 


    el: '#vue-app',
    data: {
        courseId: 0,
        lectureDescription: "",
        lectureName: "",

        loading:false,

        lectureArticle:"",



        currentLectureId: 0,

        lectureDownlodableContents: [],
        showLecture: false,
        results: {},
        videoLink:"",
        isChecked: true,
        isNotChecked: false,
    }
    ,
    async  mounted() {
        this.loading = true;
      await   axios.get(url + this.courseId).then(response => {
            this.results = response.data
            console.log("Response:"+response.data);
            console.log("Course Id:" + this.courseId);
            

        })

        this.loading = false;
    }
    ,
    methods: {
        // can acess the yhe values of the data 
        // this means the instance it self






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