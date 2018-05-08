
<template>
  <v-container>
    <v-form ref="form" lazy-validation>
      <v-text-field v-model="currentProfession.name"
                    :rules="validation.profession.name"
                    label="Name"></v-text-field>
      <v-text-field v-model="currentProfession.description"
                    :rules="validation.profession.description"
                    label="Description"
                    multi-line
                    required></v-text-field>
      <v-layout row v-for="(currentProfessionCriteria,index) in currentProfession.criterias">
        <v-flex px-5 md5>
          <v-select :items="selectOptions"
                    :value="currentProfession.criterias[index]"
                    @change="setCriteria($event, index)"
                    label="Select Criteria"
                    item-text="text"
                    item-value="value"></v-select>
        </v-flex>
        <v-flex px-5 md5>
          <v-text-field v-model="currentProfession.criterias[index].weight"
                        :rules="validation.profession.criteria.weight"
                        label="Weight"></v-text-field>
        </v-flex>
        <v-flex md2>
          <v-btn @click="removeCriteria(index)"><v-icon>remove</v-icon></v-btn>
        </v-flex>
      </v-layout>
      <v-btn v-show="currentProfession.criterias.length != criterias.length" @click="addCriteria">Add Criteria</v-btn>
      <v-btn @click="submit">submit</v-btn>
    </v-form>
  </v-container>
</template>

<script>
  export default {
    data() {
      return {
        validation: {
          profession: {
            name: [
              v => !!v || 'Name is required',
              v => (v && v.length <= 15) || 'Name must be less than 15 characters'
            ],
            description: [
              v => !!v || 'Description is required',
              v => (v && v.length <= 256) || 'Description must be less than 256 characters'
            ],
            criteria: {
              weight: [
                v => !!v || 'Weight is required',
              ]
            }
          }
        },
        profession: {
          id: 0,
          name: "",
          description: "",
          criterias: []
        },
        criterias: [],
      };
    },
    methods: {
      setCriteria(criteria, index) {
        this.currentProfession.criterias[index] = criteria;
        this.currentProfession.criterias = this.currentProfession.criterias.slice();
      },
      addCriteria() {
        this.currentProfession.criterias.push({
          id: 0,
          criteriaId: 0,
          weight: 10
        });
      },
      removeCriteria(index) {
        this.currentProfession.criterias.splice(index, 1);
      },
      submit() {
        if (this.$refs.form.validate()) {
          var payload = Object.assign({}, this.currentProfession);
          console.log(payload);
          this.$store.dispatch("admin/profession/addOrUpdateProfession", payload);
        }
      },
    },
    created() {
      var id = this.$route.params.id || null;
      this.$store.dispatch("admin/profession/loadProfession", id != null ? { id: id } : null);
    },
    computed: {
      enabledCriterias() {
        var disabled = this.criterias.filter(c => this.currentProfession.criterias.filter(z => z.criteriaId == c.id).length > 0);
        disabled.forEach(v => {
          v.disabled = true;
        })
        var enabled = this.criterias.filter(c => disabled.filter(z => z.id == c.id).length == 0);
        enabled.forEach(v => {
          v.disabled = false;
        });
        return this.criterias;
      },
      currentProfession: {
        get() {
          var data = this.$store.getters["admin/profession/currentProfessionData"];
          if (data != null) {
            this.profession = data.profession;
            this.criterias = data.criterias;
          }
          return this.profession;
        }
      },
      selectOptions: {
        get() {
          var options = this.criterias.map(c => {
            var item = this.currentProfession.criterias.filter(z => z.criteriaId == c.id)[0];
            return {
              text: c.name,
              value: item || {
                id: 0,
                criteriaId: c.id,
                weight: 10
              },
              disabled: item != null
            }
          });
          return options;
        }
      }
    }
  };
</script>
