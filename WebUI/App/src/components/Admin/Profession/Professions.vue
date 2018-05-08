<template>
  <v-layout  row wrap>
        <v-flex md3 sm4 xs12 class="mb-4 pa-2" v-for="profession in professions" :key="profession.id">
           <v-card >
                <v-card-text>
                  <h1>{{profession.name}}</h1>
                </v-card-text>
                <v-card-actions>                
                    <v-btn  :to="'professions/edit/'+profession.id" icon>
                    <v-icon>update</v-icon>
                  </v-btn>
                  <v-spacer></v-spacer>
                  <v-btn @click="deleteProfession(profession.id)" icon>
                    <v-icon>delete</v-icon>
                  </v-btn>
                </v-card-actions>
              </v-card>                
              </v-flex>
      <v-btn
        :color="'pink'"
        dark
        fab
        fixed
        bottom
        right
        :to="'professions/create'"
      >      <v-icon>add</v-icon>
      </v-btn>
  </v-layout>
</template>


<script>
export default {
  data() {
    return {
      dialog: false,
      currentProfession: null
    };
  },
  created() {
    this.$store.dispatch("admin/profession/loadProfessions");
  },
  computed: {
    professions() {
      return this.$store.getters["admin/profession/loadedProfessions"];
    }
  },
  methods: {
    deleteProfession(id) {
      this.$store.dispatch("admin/profession/deleteProfession", id);
    }
  },
  components: {
    
  }
};
</script>
