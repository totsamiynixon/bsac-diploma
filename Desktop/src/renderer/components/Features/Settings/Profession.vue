<template>
  <v-flex>
    <v-dialog v-model="dialog"
              scrollable
              max-width="300px">
      <div slot="activator">
        <v-btn color="primary"
               dark>Выбрать профессию</v-btn>
        <v-chip>{{value}}</v-chip>
      </div>
      <v-card>
        <v-card-title>Выберите профессию</v-card-title>
        <v-text-field flat
                      solo-inverted
                      prepend-icon="search"
                      label="Search"
                      class="hidden-sm-and-down"
                      v-model="filter"></v-text-field>
        <v-divider></v-divider>
        <v-card-text style="height: 300px;">
          <v-radio-group column
                         v-model="profession"
                         v-for="(group, key) in filtered"
                         :key="key"
                         :label="key">
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
                 @click.native="dialog = false; update();">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-flex>
</template>
<script>
export default {
  props: ["value"],
  data: function() {
    return {
      profession: null,
      professions: professions,
      dialog: false,
      filter: ""
    };
  },
  watch: {
    value(value) {
      this.profession = this.value != null ? this.value.slice() : "";
    }
  },
  created() {
    this.profession = this.value != null ? this.value.slice() : "";
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
    },
    update() {
      this.$emit("update:value", this.profession);
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

