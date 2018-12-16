<template>
  <v-flex>
    <v-list two-line
            subheader>
      <v-subheader>Предпочтительное время</v-subheader>
      <v-list-tile v-for="(item,index) in model.times"
                   :key="index">
        <v-list-tile-action>
          <v-btn dark
                 fab
                 small
                 @click="remove(index)">
            <v-icon style="height:initial;">remove</v-icon>
          </v-btn>
        </v-list-tile-action>
        <v-list-tile-content>
          <v-list-tile-sub-title>
            <v-dialog ref="dialog"
                      v-model="item.dialog"
                      :return-value.sync="item.value"
                      persistent
                      lazy
                      full-width
                      width="290px">
              <v-text-field slot="activator"
                            v-model="item.value"
                            :label="'Выберите время ' + (index+1)"
                            prepend-icon="access_time"
                            readonly></v-text-field>
              <v-time-picker v-model="item.value"
                             actions>
                <v-spacer></v-spacer>
                <v-btn flat
                       color="primary"
                       @click="item.dialog = false">Закрыть</v-btn>
                <v-btn flat
                       color="primary"
                       @click="$refs.dialog[index].save(item.value)">Сохранить</v-btn>
              </v-time-picker>
            </v-dialog>
          </v-list-tile-sub-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
    <v-btn flat
           color="primary"
           @click="add">Добавить</v-btn>
    <v-btn flat
           color="dark"
           @click="save">Сохранить</v-btn>
  </v-flex>
</template>
    
<script>
export default {
  data() {
    return {
      model: {
        times: []
      }
    };
  },
  created() {
    this.model.times = this.$store.getters["user/trainingTime"].map(time => {
      return {
        value: time,
        dialog: false
      };
    });
    this.$store.watch(
      () => this.$store.getters["user/trainingTime"],
      value => {
        this.model.times = value.map(time => {
          return {
            value: time,
            dialog: false
          };
        });
      }
    );
  },
  methods: {
    add() {
      this.model.times.push({
        value: null,
        dialog: false
      });
    },
    remove(index) {
      this.model.times.splice(index, 1);
    },
    save() {
      var payload = this.model.times.map(s => {
        return s.value;
      });
      this.$store.dispatch("user/changePreferredTime", payload);
    }
  }
};
</script>
