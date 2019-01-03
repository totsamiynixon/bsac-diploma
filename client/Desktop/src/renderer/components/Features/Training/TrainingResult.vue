<template>
  <v-layout justify-center
            wrap
            column
            class="px-5 py-5"
            style="text-align:center;">
    <h1>Как прошла ваша тренировка??</h1>
    <div>
      <v-rating v-model="rating"
                background-color="indigo lighten-3"
                color="indigo"
                :size="size"></v-rating>
    </div>
    <div>
      <v-btn @click="saveRating()">
        Оценить и выйти
      </v-btn>
    </div>
  </v-layout>
</template>

<script>
export default {
  data() {
    return {
      rating: 5,
    }
  },
  computed: {
    size() {
      return this.$vuetify.breakpoint.xs || this.$vuetify.breakpoint.sm
        ? 32
        : 64
    },
  },
  methods: {
    saveRating() {
      this.$apiService
        .updateUserTrainingRating(
          this.$route.params.userTratiningId,
          this.rating,
        )
        .then(response => {
          this.$router.push({ name: 'exercises' })
        })
    },
  },
}
</script>
