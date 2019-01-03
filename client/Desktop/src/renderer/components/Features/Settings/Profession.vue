<template>
  <v-flex>
    <v-dialog v-model="dialog"
              scrollable
              max-width="300px">
      <div slot="activator">
        <v-btn color="primary"
               dark>Выбрать профессию</v-btn>
        <v-chip>{{profession.name}}</v-chip>
      </div>
      <v-card>
        <v-card-title class="px-2">Выберите профессию
          <v-text-field flat
                        prepend-icon="search"
                        label="Search"
                        class="hidden-sm-and-down"
                        v-model="filter"></v-text-field>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text style="height: 300px;">
          <v-radio-group column
                         v-for="group in filteredProfessions"
                         :key="group.group"
                         :label="group.group"
                         v-model="professionModel.id">
            <v-radio v-for="item in group.children"
                     :label="item.name"
                     :key="item.id"
                     :value="item.id"></v-radio>
          </v-radio-group>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="blue darken-1"
                 flat
                 @click.native="dialog = false">Закрыть</v-btn>
          <v-btn color="blue darken-1"
                 flat
                 @click.native="dialog = false; setCurrentProfession()">Сохранить</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-flex>
</template>
<script>
export default {
  data() {
    return {
      dialog: false,
      filter: '',
      professionModel: {
        id: null,
        name: null,
      },
      professions: [],
    }
  },

  computed: {
    filteredProfessions() {
      const filteredPrfessions = this.professions.filter(f =>
        f.name.includes(this.filter),
      )
      const data = filteredPrfessions.reduce((r, e) => {
        const group = e.name[0]
        if (!r[group]) r[group] = { group, children: [e] }
        else r[group].children.push(e)
        return r
      }, {})

      return Object.values(data)
    },
    profession() {
      return this.$store.getters['user/profession'] || {}
    },
  },
  created() {
    this.$apiService.getAllProfessions().then(professions => {
      this.professions = professions
    })
    this.professionModel = {
      ...this.$store.getters['user/profession'],
    }
  },
  methods: {
    onSearch(query) {
      this.filter = query
    },
    onClear() {
      this.filter = ''
    },
    setCurrentProfession() {
      this.$store.dispatch(
        'user/changeProfession',
        this.professions.find(f => f.id === this.professionModel.id),
      )
    },
  },
}
</script>

