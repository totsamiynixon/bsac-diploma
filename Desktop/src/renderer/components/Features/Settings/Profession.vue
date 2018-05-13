<template>
  <v-layout>
    <v-dialog v-model="dialog"
              scrollable
              max-width="300px">
      <v-btn slot="activator"
             color="primary"
             dark>Open Dialog</v-btn>
      <v-card>
        <v-card-title>Select Country</v-card-title>
        <v-text-field flat
                      solo-inverted
                      prepend-icon="search"
                      label="Search"
                      class="hidden-sm-and-down"
                      v-model="filter"></v-text-field>
        <v-divider></v-divider>
        <v-card-text style="height: 300px;">
          <v-radio-group v-model="profession"
                         column
                         v-for="(group, key) in filtered"
                         :key="key">
            <v-radio v-for="name in group"
                     :label="name"
                     :key="name"
                     :value="name"></v-radio>
          </v-radio-group>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="blue darken-1"
                 flat
                 @click.native="dialog = false">Close</v-btn>
          <v-btn color="blue darken-1"
                 flat
                 @click.native="dialog = false">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
</template>
<script>
export default {
  props: ["profession"],
  data: function() {
    return {
      professions: professions,
      filter: ""
    };
  },
  computed: {
    filtered() {
      let result = {};
      for (var key in professions) {
        if (professions.hasOwnProperty(key)) {
          let items = professions[key].filter(profession =>
            profession.includes(this.filter)
          );
          if (items.length > 0) {
            result[key] = items;
          }
        }
      }
      return result;
    }
  },
  methods: {
    onSearch(query) {
      this.filter = query;
    },
    onClear() {
      this.filter = "";
    },
    setCurrentProfession(value) {
      this.profession = value;
    }
  }
};

var professions = {
  A: ["Автогонщик", "Адвокат", "Архитектор"],
  Б: ["Блогер", "Ботаник", "Бухгалтер"],
  В: ["Вахтер", "Водител", "Военнослужащий", "Врач"],
  V: ["Vladimir"]
};
</script>

