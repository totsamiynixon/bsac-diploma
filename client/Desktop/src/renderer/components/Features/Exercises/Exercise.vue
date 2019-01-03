<template>
  <v-layout>
    <v-flex>
      <v-card class="px-5 py-5">
        <v-card-media>
          <div class="media-holder">
            <iframe :src="`https://youtube.com/embed/${exercise.videoId}?controls=0&disablekb=0&fs=0&showinfo=0&end=10&rel=0`"
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
  asyncComputed:{
    exercise(){
      return this.$apiService.getExercise(this.$route.params.id)
    }
  }
}
</script>
