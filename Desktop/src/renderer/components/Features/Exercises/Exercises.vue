<template>
  <v-flex>
    <v-text-field flat
                  solo-inverted
                  prepend-icon="search"
                  label="Search"
                  class="hidden-sm-and-down"
                  v-model="search.query"></v-text-field>
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
                   color="orange">Подробнее</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      exercises: [
        {
          id: 1,
          title: "Отжимания",
          url:
            "https://cdn.pixabay.com/photo/2017/10/20/18/10/run-2872201_960_720.png",
          preview:
            "Отжимания - главное упражнение для верхней части тела. Оно помогает развить силу и выносливость, нарастить мышцы, укрепить суставы, и, помимо тренировки мышц верхней части тела, помогает наладить их согласованную работу с мышцами средней и нижней частей тела."
        },
        {
          id: 2,
          title: "Приседания",
          url:
            "https://cdn.pixabay.com/photo/2017/10/20/18/10/run-2872201_960_720.png",
          preview:
            "Приседания — непревзойденные по эффективности упражнения для нижней части тела. Более того, слова «для нижней части тела» не так уж и важны. Развить в себе функциональную силу без приседаний практически невозможно: их выполнение задействует все мышцы ног, нагружает пресс и нижний отдел спины."
        },
        {
          id: 3,
          title: "Наклоны",
          url:
            "https://cdn.pixabay.com/photo/2017/10/20/18/10/run-2872201_960_720.png",
          preview:
            "Нakлoны тyлoвищa впepeд —  oчeнь пpocтoe yпpaжнeниe, koтopoe знakomo нam eщe c дeтcтвa. Нecmoтpя нa вcю пpocтoтy, oнo oчeнь пoлeзнo, тak kak пoзвoляeт пpивecти в тoнyc mнoжecтвo вaжных mышц, yлyчшaeт гибkocть и блaгoтвopнo влияeт нa здopoвьe."
        }
      ],
      search: {
        query: ""
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