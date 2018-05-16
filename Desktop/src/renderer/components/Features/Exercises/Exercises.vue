<template>
  <v-flex>
    <v-text-field flat
                  solo-inverted
                  prepend-icon="search"
                  label="Поиск"
                  class="hidden-sm-and-down"
                  v-model="search.query"
                  @dblclick="settingsDialog = true"></v-text-field>
    <v-layout row>
      <v-flex xs12
              sm6
              md4
              px-2
              py-2
              v-for="exercise in filteredExercises"
              :key="exercise.id">
        <v-card>
          <v-card-media :src="exercise.url"
                        height="200px">
          </v-card-media>
          <v-card-title primary-title>
            <div>
              <h3 class="headline mb-0">{{exercise.title}}</h3>
              <div>{{exercise.preview}}</div>
            </div>
          </v-card-title>
          <v-card-actions>
            <v-btn flat
                   color="orange"
                   :to="{name:'exercise', params:{id:0}}">Подробнее</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
    <v-dialog v-model="settingsDialog"
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
                 @click.native="settingsDialog = false">
            <v-icon>close</v-icon>
          </v-btn>
          <v-toolbar-title>Настройки поиска</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn dark
                   flat
                   @click.native="settingsDialog = false">Сохранить</v-btn>
          </v-toolbar-items>
        </v-toolbar>
        <v-card-text>
          <v-list three-line
                  subheader>
            <v-subheader>Основные</v-subheader>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="search.settings.byExercises"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>По упражнениям</v-list-tile-title>
                <v-list-tile-sub-title>Поиск в названии и описании упражнений</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="search.settings.byCriterias"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>По критериям</v-list-tile-title>
                <v-list-tile-sub-title>Поиск упражнений по критериям</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="search.settings.byProfessions"></v-checkbox>
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
      settingsDialog: false,
      exercises: [
        {
          id: 1,
          title: "Отжимания",
          url: "http://sportwiki.to/images/0/0e/Atlas_fitnesa9.jpg",
          preview:
            "Отжимания - главное упражнение для верхней части тела. Оно помогает развить силу и выносливость, нарастить мышцы, укрепить суставы, и, помимо тренировки мышц верхней части тела, помогает наладить их согласованную работу с мышцами средней и нижней частей тела."
        },
        {
          id: 2,
          title: "Приседания",
          url: "http://sportwiki.to/images/3/3f/Sil_men_prised.jpg",
          preview:
            "Приседания — непревзойденные по эффективности упражнения для нижней части тела. Более того, слова «для нижней части тела» не так уж и важны. Развить в себе функциональную силу без приседаний практически невозможно: их выполнение задействует все мышцы ног, нагружает пресс и нижний отдел спины."
        },
        {
          id: 3,
          title: "Наклоны",
          url: "http://www.fitnessera.ru/wp-content/uploads/2017/06/17.jpg",
          preview:
            "Нakлoны тyлoвищa впepeд —  oчeнь пpocтoe yпpaжнeниe, koтopoe знakomo нam eщe c дeтcтвa. Нecmoтpя нa вcю пpocтoтy, oнo oчeнь пoлeзнo, тak kak пoзвoляeт пpивecти в тoнyc mнoжecтвo вaжных mышц, yлyчшaeт гибkocть и блaгoтвopнo влияeт нa здopoвьe."
        }
      ],
      search: {
        query: "",
        settings: {
          byExercises: true,
          byCriterias: false,
          byProfessions: false
        }
      }
    };
  },
  computed: {
    filteredExercises() {
      return this.exercises.filter(ex => ex.title.includes(this.search.query));
    }
  }
};
</script>