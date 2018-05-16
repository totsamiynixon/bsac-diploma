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
      <f7-list-item title="Nothing found"></f7-list-item>
    </f7-list>
    <f7-list contacts-list>
      <f7-list-group v-for="(group, key) in filtered"
                     :key="key">
        <f7-list-item :title="key"
                      group-title></f7-list-item>
        <f7-list-item v-for="name in group"
                      :title="name"
                      :key="name"
                      :value="name"
                      :checked="currentProfession.name == name"
                      @change="setCurrentProfession(id)"
                      radio></f7-list-item>
      </f7-list-group>
    </f7-list>

  </f7-page>
</template>
<script>
export default {
  data: function() {
    return {
      professions: professions,
      filter: "",
      profession: {
        id: 0,
        name: ""
      }
    };
  },
  created() {
    axios.get("/api/professions/").then(response => {
      this.professions = professions;
    }, null);
  },
  computed: {
    filtered() {
      let result = {};
      for (var key in this.professions) {
        if (this.professions.hasOwnProperty(key)) {
          let items = this.professions[key].filter(profession =>
            profession.includes(this.filter)
          );
          if (items.length > 0) {
            result[key] = items;
          }
        }
      }
      return result;
    },
    currentProfession() {
      let result = this.$store.getters["settings/profession"];
      return result != null ? result : this.profession;
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

