<template>
  <f7-page>
    <f7-navbar>
      <f7-nav-left>
        <f7-link icon-if-ios="f7:menu"
                 icon-if-md="material:menu"
                 panel-open="left"></f7-link>
      </f7-nav-left>
      <f7-nav-title>Настройки</f7-nav-title>
    </f7-navbar>
    <f7-list>
      <f7-list-item link="/settings/profession/"
                    title="Профессия">
        <slot name="after-list">
          {{profession}}
        </slot>
      </f7-list-item>
      <f7-list-item link="/settings/preferred-time/"
                    title="Предпочтительное время"></f7-list-item>
    </f7-list>
  </f7-page>
</template>
<script>
export default {
  data: function() {
    return {
      professions: [],
      settings: {}
    };
  },
  methods: {},
  created() {
    this.$http.get("/api/settings/initData").then(response => {
      this.professions = response.data.professions;
      this.$store.dispatch("settings/installSettings", response.data.settings);
    });
  },
  computed: {
    profession() {
      return this.$store.getters["settings/profession"].name || "";
    }
  }
};
</script>
