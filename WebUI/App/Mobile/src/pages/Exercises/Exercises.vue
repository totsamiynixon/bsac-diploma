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
                 @click="search.dialog=!search.dialog"></f7-link>
      </f7-nav-right>
    </f7-navbar>
    <f7-searchbar v-show="search.dialog == true"
                  disable-link-text="Отменить"
                  search-container="#search-list"
                  placeholder="Найти упражнение"
                  :clear-button="true"
                  @input="searchUpdate"
                  @searchbar:clear="search.properties.query = ''">
      <slot name="after-inner">
        <f7-link icon-if-ios="f7:more"
                 icon-if-md="material:more_vert"
                 @click="openSheet"></f7-link>
      </slot>
    </f7-searchbar>
    <f7-card v-for="exercise in filteredExercises"
             :key="exercise.name">
      <f7-card-header>{{exercise.name}}</f7-card-header>
      <f7-card-content>
        <img :src="`https://i.ytimg.com/vi/${exercise.videoId}/mqdefault.jpg`" />
      </f7-card-content>
      <f7-card-footer>
        <f7-button @click="$f7router.navigate(`/exercises/${exercise.id}`)">Посмотреть</f7-button>
      </f7-card-footer>
    </f7-card>
    <f7-sheet id="settings-sheet">
      <f7-block-title>Настройки поиска</f7-block-title>
      <f7-list>
        <f7-list-item checkbox
                      title="По упражнениям"
                      name="demo-checkbox"
                      @input="search.properties.byExercises = $event.target.value"
                      checked></f7-list-item>
        <f7-list-item checkbox
                      title="По критериям"
                      name="demo-checkbox"
                      @input="search.properties.byCriterias = $event.target.value"></f7-list-item>
      </f7-list>
    </f7-sheet>
  </f7-page>
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
      },
      filteredExercises: []
    };
  },
  methods: {
    searchUpdate($event) {
      if (this.search.timer != null) {
        clearTimeout(this.search.timer);
      }
      this.search.timer = setTimeout(() => {
        this.search.properties.query = $event.target.value;
      }, 1000);
    },
    openSheet() {
      this.$f7.sheet.open("#settings-sheet", true);
    }
  },
  created() {
    this.$http
      .get("/api/exercises/getAll", {
        params: {
          query: this.search.properties.query,
          byCriterias: this.search.properties.byCriterias,
          byExercises: this.search.properties.byExercises
        }
      })
      .then(response => (this.filteredExercises = response.data));
  },
  watch: {
    "search.properties": {
      handler(value) {
        this.$http
          .get("/api/exercises/getAll", {
            params: {
              query: this.search.properties.query,
              byCriterias: this.search.properties.byCriterias,
              byExercises: this.search.properties.byExercises
            }
          })
          .then(response => (this.filteredExercises = response.data));
      },
      deep: true
    }
  }
};
</script>
