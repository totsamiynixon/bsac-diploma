<template>
  <v-layout>
    <v-flex>
      <v-card class="px-5 py-5">
        <v-card-media>
          <div class="media-holder">
            <iframe :src="`https://www.youtube.com/embed/${exercise.videoId}`"
                    webkitallowfullscreen
                    mozallowfullscreen
                    allowfullscreen></iframe>

          </div>
        </v-card-media>
        <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">{{exercise.name}}</h3>
            <div>{{exercise.previewText}}</div>
          </div>
        </v-card-title>
        <v-card-actions>
          <h3 class="headline mb-0">Упражнение {{currentIndex}} из {{totalCount}}</h3>
          <v-spacer></v-spacer>
           <v-btn flat v-if="nextExerciseId != null"
                  @click="nextExercise">
                 Далее!</v-btn>
          <v-btn flat v-if="nextExerciseId == null"
                 @click="completeTraining"
                 color="orange">Выполнено!</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>


<script>
export default {
  data(){
    return {
      exercise:{},
      nextExerciseId: null,
      totalCount:null,
      currentIndex: null
    }
  },
  created(){
    this.initExercise(this.$route.params.id);
  },
  methods:{
    initExercise(id){
       var result =  this.$store.getters["features/userTraining/exercise"](id);
  if(!result){
    this.$router.push({name:'training-result'});
    return;
  }
  this.exercise = result.exercise;
  this.nextExerciseId = result.nextExerciseId;
  this.currentIndex = result.currentIndex;
  this.totalCount = result.totalCount;
    },
    nextExercise(){
      this.$store.dispatch("features/userTraining/completeExercise", this.exercise.id).then(success=>{
      this.initExercise(this.nextExerciseId);
      });
    },
    completeTraining(){
      this.$store.dispatch("features/userTraining/completeTraining").then((success)=>{
        this.$router.push({name:'training-result'});
      });
    }
  }
}
</script>
