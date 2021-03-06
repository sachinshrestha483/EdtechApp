

new Vue({
    el: '#vue-app',
    data: {
      
        videoSelected: false,
        videoLink:'',


    },
   
    methods: {

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

        



        

     
    }

})