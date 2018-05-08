<template>
  <v-layout  row wrap>
        <v-flex md3 sm4 xs12 class="mb-4 pa-2" v-for="exercise in exercises" :key="exercise.id">
           <v-card >
                <v-card-text>
                  <h1>{{exercise.name}}</h1>
                </v-card-text>
                <v-card-actions>                
                    <v-btn  :to="'exercises/edit/'+exercise.id" icon>
                    <v-icon>update</v-icon>
                  </v-btn>
                  <v-spacer></v-spacer>
                  <v-btn @click="deleteExercise(exercise.id)" icon>
                    <v-icon>delete</v-icon>
                  </v-btn>
                </v-card-actions>
              </v-card>                
              </v-flex>
      <v-btn
        :color="'pink'"
        dark
        fab
        fixed
        bottom
        right
        :to="'exercises/create'"
      >      <v-icon>add</v-icon>
      </v-btn>
  </v-layout>
</template>


<script>
export default {
  data() {
    return {
      dialog: false,
      currentExercise: null
    };
  },
  created() {
    this.$store.dispatch("admin/exercise/loadExercises");
  },
  computed: {
    exercises() {
      return this.$store.getters["admin/exercise/loadedExercises"];
    }
  },
  methods: {
    deleteExercise(id) {
      this.$store.dispatch("admin/exercise/deleteExercise", id);
    }
  },
  components: {
    
  }
};
</script>
