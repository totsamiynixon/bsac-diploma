<template>
  <v-layout>
    <v-flex>
      <v-card class="px-5 py-5">
        <v-card-media>
          <div class="media-holder">
            <iframe :src="`https://www.youtube.com/embed/${currentExercise.videoId}`"
                    webkitallowfullscreen
                    mozallowfullscreen
                    allowfullscreen></iframe>

          </div>
        </v-card-media>
        <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">{{currentExercise.name}}</h3>
            <div>{{currentExercise.previewText}}</div>
          </div>
        </v-card-title>
        <v-card-actions>
          <h3 class="headline mb-0"
              v-show="currentIndex > 0">Упражнение {{currentIndex}} из
            {{userTraining.exercises.length}}</h3>
          <v-spacer></v-spacer>
          <v-btn flat
                 v-if="hasNext"
                 @click="markAsFinished(currentExercise.id)">
            Далее!</v-btn>
          <v-btn flat
                 v-if="!hasNext"
                 @click="completeTraining"
                 color="orange">Выполнено!</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>


<script>
export default {
  data() {
    return {
      userTraining: {
        id: null,
        exercises: [],
        isPassed: false,
        created: false,
      },
    }
  },

  computed: {
    currentExercise() {
      if (this.userTraining.id == null) {
        return {
          videoId: null,
          name: null,
          id: null,
        }
      }
      return (
        this.userTraining.exercises.find(f => !f.isPassed) ||
        this.userTraining.exercises.slice(-1).pop()
      )
    },
    currentIndex() {
      if (!this.userTraining) {
        return null
      }
      return this.userTraining.exercises.findIndex(f => !f.isPassed) + 1
    },
    hasNext() {
      if (!this.userTraining) {
        return false
      }
      return this.userTraining.exercises.filter(f => !f.isPassed).length > 1
    },
  },
  created() {
    this.$apiService.getUserTraining().then(userTraining => {
      this.userTraining = {
        ...userTraining,
      }
      this.userTraining.exercises = this.userTraining.exercises.map(f => {
        f.isPassed = false
        return f
      })
    })
  },
  methods: {
    markAsFinished(id) {
      this.userTraining.exercises.find(f => f.id === id).isPassed = true
      this.userTraining = {
        ...this.userTraining,
      }
    },
    completeTraining() {
      this.$apiService
        .completeUserTraining(this.userTraining.id)
        .then(success => {
          this.$router.push({
            name: 'training-result',
            params: { userTrainingId: this.userTraining.id },
          })
        })
    },
  },
}
</script>
