<template>
  <f7-page>
    <f7-navbar>
      <f7-nav-left>
        <f7-link icon-if-ios="f7:menu"
                 icon-if-md="material:menu"
                 panel-open="left"></f7-link>
      </f7-nav-left>
      <f7-nav-title>Упражнения</f7-nav-title>
      <f7-nav-right>
        <f7-link icon-if-ios="f7:search"
                 icon-if-md="material:search"
                 @click="search.show=!search.show"></f7-link>
      </f7-nav-right>
    </f7-navbar>
    <f7-searchbar v-show="search.show"
                  disable-link-text="Отменить"
                  search-container="#search-list"
                  placeholder="Найти упражнение"
                  :clear-button="true"
                  @input="search.query = $event.target.value"
                  @searchbar:clear="search.query = ''">
      <slot name="after-inner">
        <f7-link icon-if-ios="f7:more"
                 icon-if-md="material:more_vert"
                 @click="openSheet"></f7-link>
      </slot>
    </f7-searchbar>
    <f7-card v-for="exercise in filteredExercises"
             :key="exercise.title">
      <f7-card-header>{{exercise.title}}</f7-card-header>
      <f7-card-content>
        <img style="width:100%;border:0"
             :src="exercise.url" />
      </f7-card-content>
      <f7-card-actions>
        <f7-button @click="$f7router.navigate('/exercises/1')">Посмотреть</f7-button>
      </f7-card-actions>
    </f7-card>
    <search-settings></search-settings>
  </f7-page>
</template>

<script>
import SearchSettings from "./SearchSettings.vue";
export default {
  data() {
    return {
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
        show: false,
        query: ""
      }
    };
  },
  methods: {
    openSheet() {
      this.$f7.sheet.open("#settings-sheet", true);
    }
  },
  computed: {
    filteredExercises() {
      return this.exercises.filter(ex => ex.title.includes(this.search.query));
    }
  },
  components: {
    SearchSettings
  }
};
</script>
