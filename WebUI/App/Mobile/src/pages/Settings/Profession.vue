<template>
  <f7-page>
    <f7-navbar title="Профессия" back-link="Back"></f7-navbar>
    <f7-row no-gap>
        <f7-col width="50" v-for="profession in professions" :key="profession.id">
      <f7-card >
  <f7-card-header>{{profession.name}}</f7-card-header>
  <f7-card-content>{{profession.description}}</f7-card-content>
</f7-card>
</f7-col>
</f7-row>
  </f7-page>
</template>
<script>
export default {
  data() {
    return {
      professions: [
        {
          id: 0,
          name: "Бухгалтер",
          description: "Описания"
        },
        {
          id: 1,
          name: "Бухгалтер 1",
          description: "Описания 1"
        },
        {
          id: 2,
          name: "Бухгалтер 2",
          description: "Описания 2"
        }
      ]
    };
  }
};
</script>
