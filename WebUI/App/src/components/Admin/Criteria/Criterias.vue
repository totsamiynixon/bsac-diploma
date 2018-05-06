<template>
  <v-layout  row wrap>
        <v-flex md3 sm4 xs12 class="mb-4 pa-2" v-for="criteria in criterias" :key="criteria.id">
           <v-card >
                <v-card-text>
                  <h1>{{criteria.name}}</h1>
                </v-card-text>
                <v-card-actions>                
                    <v-btn  @click="updateCriteria(criteria)" target="_blank" icon>
                    <v-icon>update</v-icon>
                  </v-btn>
                  <v-spacer></v-spacer>
                  <v-btn @click="deleteCriteria(criteria.id)" icon>
                    <v-icon>delete</v-icon>
                  </v-btn>
                </v-card-actions>
              </v-card>                
              </v-flex>
              <create-or-update-criteria-modal :dialog.sync="dialog" :criteria.sync="currentCriteria"></create-or-update-criteria-modal>
      <v-btn
        :color="'pink'"
        dark
        fab
        fixed
        bottom
        right
        @click="createCriteria"
      >      <v-icon>add</v-icon>
      </v-btn>
  </v-layout>
</template>


<script>
import CreateOrUpdateCriteriaModal from "@/components/Admin/Criteria/CreateOrUpdateCriteriaModal";
export default {
  data() {
    return {
      dialog: false,
      currentCriteria: null
    };
  },
  created() {
    this.$store.dispatch("admin/criteria/loadCriterias");
  },
  computed: {
    criterias() {
      return this.$store.getters["admin/criteria/loadedCriterias"];
    }
  },
  methods: {
    createCriteria() {
      this.$store.dispatch("admin/criteria/setCurrentCriteria", null);
      this.dialog = true;
    },
    updateCriteria(criteria) {
      this.$store.dispatch("admin/criteria/setCurrentCriteria", criteria);
      this.dialog = true;
    },
    deleteCriteria(id) {
      this.$store.dispatch("admin/criteria/deleteCriteria", id);
    }
  },
  components: {
    CreateOrUpdateCriteriaModal
  }
};
</script>