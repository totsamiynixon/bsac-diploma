<template>
  <v-layout  row wrap>
        <v-flex md3 sm4 xs12 class="mb-4 pa-2" v-for="project in projects" :key="project.id">
           <v-card >
                <v-card-media
                  :src="project.imageUrl"
                  height="200px">
                  <v-container fill-height fluid>
                    <v-layout fill-height>
                      <v-flex xs12 align-end flexbox>
                        <span class="headline white--text" v-text="project.title"/>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card-media>
                <v-card-actions>
                  <v-btn :to="'/project/'+project.id +'/ostervalder-table'" icon>
                    <v-icon>share</v-icon>
                  </v-btn>
                  <v-spacer></v-spacer>
                  <v-btn @click="deleteProject(project.id)" icon>
                    <v-icon>delete</v-icon>
                  </v-btn>
                </v-card-actions>
              </v-card>                
              </v-flex>
            <create-project-modal :dialog.sync="dialog"></create-project-modal>
      <v-btn
        :color="'pink'"
        dark
        fab
        fixed
        bottom
        right
        @click="dialog = true"
      >      <v-icon>add</v-icon>
      </v-btn>
  </v-layout>
</template>

<script>
import CreateProjectModal from '@/components/Default/Projects/CreateProjectModal'
export default {
  data() {
    return {
      dialog: false
    };
  },
  created(){
    this.$store.dispatch("projects/loadProjects")
  },
  computed: {
    projects() {
      return this.$store.getters['projects/loadedProjects'];
    }
  },
  methods: {
    deleteProject(id) {
      this.$store.dispatch("projects/deleteProject", id);
    }
  },
  components:{
    CreateProjectModal
  }
};
</script>