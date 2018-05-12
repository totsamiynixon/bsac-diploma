<template>
  <f7-page>
    <f7-navbar>
      <f7-nav-left>
        <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" panel-open="left"></f7-link>
      </f7-nav-left>
      <f7-nav-title>Упражнения</f7-nav-title>
      <f7-nav-right>
        <f7-link icon-if-ios="f7:search" icon-if-md="material:search" @click="search.show=!search.show"></f7-link>
      </f7-nav-right>
    </f7-navbar>
         <f7-searchbar v-show="search.show"
      disable-link-text="Отменить"
      search-container="#search-list"
      placeholder="Найти упражнение"
      :clear-button="true"
      @input="search.query = $event.target.value"
      @searchbar:clear="search.query = ''"
    >
    <slot name="after-inner">
        <f7-link icon-if-ios="f7:more" icon-if-md="material:more_vert" @click="openSheet" ></f7-link>
        </slot>
    </f7-searchbar>
        <f7-card v-for="exercise in filteredExercises">
  <f7-card-header>{{exercise.title}}</f7-card-header>
  <f7-card-content>
   <iframe style="width:100%;border:0" :src="exercise.url" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
  </f7-card-content>

</f7-card>
    <search-settings></search-settings>
  </f7-page>
</template>

<script>
import SearchSettings from "./SearchSettings.vue";
export default {
    data(){
        return {
            exercises:[
                {
                    id:1,
                    title:"Отжимания",
                    url:"https://www.youtube.com/embed/Znq2dUzZN0Y"
                },
                                {
                    id:2,
                    title:"Приседания",
                    url:"https://www.youtube.com/embed/Znq2dUzZN0Y"
                },
                                                {
                    id:3,
                    title:"Наклоны",
                    url:"https://www.youtube.com/embed/Znq2dUzZN0Y"
                }
            ],
            search:{
                show:false,
                query:""
            }
        }
    },
    methods:{
        openSheet(){
         this.$f7.sheet.open("#settings-sheet", true);
        },
    },
    computed:{
        filteredExercises(){
            return this.exercises.filter(ex=>ex.title.includes(this.search.query))
        }
    },
    components:{
        SearchSettings
    }
}
</script>