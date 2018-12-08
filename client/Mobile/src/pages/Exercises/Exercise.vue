<template>
  <f7-page>
    <f7-navbar title="Упражнения"
               back-link="Back"></f7-navbar>
    <f7-card>
      <f7-card-header>{{exercise.name}}</f7-card-header>
      <f7-card-content>
        <iframe style="width:100%;border:0"
                :src="`https://www.youtube.com/embed/${exercise.videoId}`"
                webkitallowfullscreen
                mozallowfullscreen
                allowfullscreen></iframe>
      </f7-card-content>
      <f7-card-footer>
        {{exercise.description}}
      </f7-card-footer>
    </f7-card>
  </f7-page>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      exercise: {}
    };
  },
  methods: {
    getExercise() {
      console.log(this.$f7route);
      axios
        .get("/api/exercises/get", {
          params: { id: this.$f7route.params.id }
        })
        .then(response => {
          this.exercise = response.data;
        });
    }
  },
  created() {
    this.getExercise();
  }
};
</script>
