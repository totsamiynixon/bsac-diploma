<template>
  <v-layout>
    <v-flex>
      <v-card class="px-5 py-5">
        <v-card-media>
          <div class="media-holder">
            <iframe :src="exercise.videoUrl"
                    webkitallowfullscreen
                    mozallowfullscreen
                    allowfullscreen></iframe>

          </div>
        </v-card-media>
        <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">{{exercise.name}}</h3>
            <div>{{exercise.description}}</div>
          </div>
        </v-card-title>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  data() {
    return {
      exercise: {}
    };
  },
  methods: {
    getExercise() {
      this.$http
        .get("/api/exercises/get", { params: { id: this.$route.params.id } })
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
