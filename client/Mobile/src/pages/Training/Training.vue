<template>
  <f7-page>
    <f7-navbar :title="`Упражнение ${currentIndex} из ${totalCount}`"
               back-link="Назад"></f7-navbar>
    <f7-card>
      <f7-card-header>{{exercise.name}}</f7-card-header>
      <f7-card-content>
        <div class="media-holder">
          <iframe :src="`https://www.youtube.com/embed/${exercise.videoId}`"
                  webkitallowfullscreen
                  mozallowfullscreen
                  allowfullscreen></iframe>

        </div>
      </f7-card-content>
      <f7-card-footer>
        {{exercise.previewText}}
      </f7-card-footer>
    </f7-card>
    <f7-button v-if="nextExerciseId != null"
               @click="nextExercise">Далее</f7-button>
    <f7-button v-if="nextExerciseId == null"
               @click="completeTraining">Выполнено!</f7-button>
  </f7-page>
</template>

<script>
export default {
  data() {
    return {
      exercise: {},
      nextExerciseId: null,
      totalCount: null,
      currentIndex: null
    };
  },
  created() {
    this.initExercises();
  },
  methods: {
    initExercises(id) {
      let result = null;
      if (typeof id === "undefined") {
        result = this.$store.getters["userTraining/firstExercise"];
      } else {
        result = this.$store.getters["userTraining/exercise"](id);
      }
      if (!result) {
        this.$f7router.navigate("/training-result");
        return;
      }
      this.exercise = result.exercise;
      this.nextExerciseId = result.nextExerciseId;
      this.currentIndex = result.currentIndex;
      this.totalCount = result.totalCount;
    },
    nextExercise() {
      this.$store
        .dispatch("userTraining/completeExercise", this.exercise.id)
        .then(success => {
          this.initExercises(this.nextExerciseId);
        });
    },
    completeTraining() {
      this.$store.dispatch("userTraining/completeTraining").then(success => {
        this.$f7router.navigate("/training-result");
      });
    }
  }
};
</script>
