<template>
    <v-dialog :value="dialog" @input="$emit('update:dialog',false)" max-width="500px">
                 <form @submit.prevent="onCreateCriteria">
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
            <v-btn color="primary"  @click.prevent="onCreateCriteria" type="submit" flat>Создать</v-btn>
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
        return this.$store.getters["admin/criteria/currentCriteria"] || this;
      },
      set(newValue) {}
    },
    formIsValid() {
      return this.source.name !== "";
    }
  },
  methods: {
    onCreateCriteria() {
      if (!this.formIsValid) {
        return;
      }
      const criteriaData = {
        id: this.source.id,
        name: this.source.name
      };
      this.$store.dispatch("admin/criteria/addOrUpdateCriteria", criteriaData);
      this.$emit("update:dialog", false);
    }
  }
};
</script>
