<template>
    <v-dialog :value="dialog" @input="$emit('update:dialog',false)" max-width="500px">
                 <form @submit.prevent="onCreateExercise">
        <v-card>
          <v-card-title>
            Создать новый критерий
          </v-card-title>
          <v-card-text>
          <v-layout row>
            <v-flex xs12  >
              <v-text-field
                name="name"
                label="name"
                id="name"
                v-model="source.name"
                required></v-text-field>
            </v-flex>
          </v-layout>
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary"  @click.prevent="onCreateExercise" type="submit" flat>Создать</v-btn>
          </v-card-actions>
        </v-card>
        </form>
      </v-dialog>
</template>

<script>
export default {
  props: ["dialog"],
  data() {
    return {
      id: 0,
      name: ""
    };
  },
  computed: {
    source: {
      get() {
        return this.$store.getters["admin/exercise/currentExercise"] || this;
      },
      set(newValue) {}
    },
    formIsValid() {
      return this.source.name !== "";
    }
  },
  methods: {
    onCreateExercise() {
      if (!this.formIsValid) {
        return;
      }
      const exerciseData = {
        id: this.source.id,
        name: this.source.name
      };
      this.$store.dispatch("admin/exercise/addOrUpdateExercise", exerciseData);
      this.$emit("update:dialog", false);
    }
  }
};
</script>
