<template>
  <v-flex>
    <v-text-field flat
                  solo-inverted
                  prepend-icon="search"
                  label="Поиск"
                  class="hidden-sm-and-down"
                  @input="searchUpdate"
                  @dblclick="search.dialog = true"></v-text-field>
    <v-layout row>
      <v-flex xs12
              sm6
              md4
              px-2
              py-2
              v-for="exercise in filteredExercises"
              :key="exercise.id">
        <v-card>
          <v-card-media>
            <div class="media-holder">
              <iframe :src="exercise.videoUrl+'?controls=0&disablekb=0&fs=0&showinfo=0&end=10&rel=0'"
            
                      webkitallowfullscreen
                      mozallowfullscreen
                      allowfullscreen></iframe>
            </div>
          </v-card-media>
          <v-card-title primary-title>
            <div>
              <h3 class="headline mb-0">{{exercise.name}}</h3>
              <div>{{exercise.previewText}}</div>
            </div>
          </v-card-title>
          <v-card-actions>
            <v-btn flat
                   color="orange"
                   :to="{name:'exercise', params:{id:exercise.id}}">Подробнее</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
    <v-dialog v-model="search.dialog"
              fullscreen
              hide-overlay
              transition="dialog-bottom-transition"
              scrollable>
      <v-card tile>
        <v-toolbar card
                   dark
                   color="primary">
          <v-btn icon
                 dark
                 @click.native="search.dialog = false">
            <v-icon>close</v-icon>
          </v-btn>
          <v-toolbar-title>Настройки поиска</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn dark
                   flat
                   @click.native="search.dialog = false">Сохранить</v-btn>
          </v-toolbar-items>
        </v-toolbar>
        <v-card-text>
          <v-list three-line
                  subheader>
            <v-subheader>Основные</v-subheader>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="search.properties.byExercises"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>По упражнениям</v-list-tile-title>
                <v-list-tile-sub-title>Поиск в названии и описании упражнений</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="search.properties.byCriterias"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>По критериям</v-list-tile-title>
                <v-list-tile-sub-title>Поиск упражнений по критериям</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="search.properties.byProfessions"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>По профессиям</v-list-tile-title>
                <v-list-tile-sub-title>Поиск упражнений по названию и описанию профессии</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list>
        </v-card-text>

        <div style="flex: 1 1 auto;"></div>
      </v-card>
    </v-dialog>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      search: {
        dialog: false,
        timer: null,
        properties: {
          query: "",
          byExercises: true,
          byCriterias: false
        }
      }
    };
  },
  methods: {
    searchUpdate(value) {
      if (this.search.timer != null) {
        clearTimeout(this.search.timer);
      }
      this.search.timer = setTimeout(() => {
        this.search.properties.query = value;
      }, 1000);
    }
  },
  created() {},
  asyncComputed: {
    filteredExercises() {
      return this.$http
        .get("/api/exercises/getAll", {
          params: {
            query: this.search.properties.query,
            byCriterias: this.search.properties.byCriterias,
            byExercises: this.search.properties.byExercises
          }
        })
        .then(response => response.data);
    }
  }
};
</script>