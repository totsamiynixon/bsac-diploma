<template>
  <f7-page>
    <f7-navbar back-link="Back"
               title="Профессия"
               sliding></f7-navbar>
    <f7-searchbar disable-link-text="Cancel"
                  search-container="#search-list"
                  placeholder="Найти профессию"
                  :clear-button="true"
                  @input="onSearch($event.target.value)"
                  :enable="true"
                  @searchbar:clear="onClear"></f7-searchbar>
    <f7-list class="searchbar-not-found">
      <f7-list-item title="Ничего не найдено"></f7-list-item>
    </f7-list>
    <f7-list contacts-list>
      <f7-list-group v-for="(value, key) in filteredProfessions"
                     :key="key">
        <f7-list-item :title="key"
                      group-title></f7-list-item>
        <f7-list-item v-for="item in value"
                      :title="item.name"
                      :key="item.id"
                      :value="item.id"
                      :checked="profession.name == item.name"
                      @change="setCurrentProfession(item)"
                      radio></f7-list-item>
      </f7-list-group>
    </f7-list>

  </f7-page>
</template>


<script>
export default {
  data: function() {
    return {
      filter: "",
      professions: []
    };
  },
  created() {
    this.$http.get("/api/settings/initData").then(response => {
      this.professions = response.data.professions;
    });
  },
  computed: {
    profession() {
      return this.$store.getters["settings/profession"] || {};
    },
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
    setCurrentProfession(profession) {
      this.$store.dispatch("settings/changeProfession", profession);
    }
  }
};
</script>

