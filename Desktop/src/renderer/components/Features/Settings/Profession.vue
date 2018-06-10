<template>
  <v-flex>
    <v-dialog v-model="dialog"
              scrollable
              max-width="300px">
      <div slot="activator">
        <v-btn color="primary"
               dark>Выбрать профессию</v-btn>
        <v-chip>{{profession.name}}</v-chip>
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
                         v-for="(group, key) in filteredProfessions"
                         :key="key"
                         :label="key.toUpperCase()">
            <v-radio v-for="item in group"
                     :label="item.name"
                     :key="item.id"
                     :value="item"></v-radio>
          </v-radio-group>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="blue darken-1"
                 flat
                 @click.native="dialog = false">Закрыть</v-btn>
          <v-btn color="blue darken-1"
                 flat
                 @click.native="dialog = false; setCurrentProfession()">Сохранить</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-flex>
</template>
<script>
export default {
  props: ["professions"],
  data: function() {
    return {
      dialog: false,
      filter: "",
      profession: {}
    };
  },
  watch: {
    value(value) {
      this.profession = this.value != null ? this.value.slice() : "";
    }
  },
  created() {
    this.profession =
      this.$store.getters["features/settings/profession"] || this.profession;
  },
  computed: {
    filteredProfessions() {
      let result = {};
      for (var key in this.professions) {
        if (this.professions.hasOwnProperty(key)) {
          let items = this.professions[key].filter(profession =>
            profession.name.includes(this.filter)
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
    setCurrentProfession() {
      this.$store.dispatch(
        "features/settings/changeProfession",
        this.profession
      );
    }
  }
};
</script>

