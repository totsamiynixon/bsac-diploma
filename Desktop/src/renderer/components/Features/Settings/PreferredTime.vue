<template>
  <v-layout>
    <v-list two-line
            subheader>
      <v-subheader>Предпочтительное время</v-subheader>
      <v-list-tile v-for="(item,index) in times"
                   :key="index">
        <v-list-tile-content>
          <v-list-tile-title>{{'Время ' + (index+1)}}</v-list-tile-title>
          <v-list-tile-sub-title>
            <v-dialog ref="{{'dialog'+(index+1)}}"
                      v-model="item.dialog"
                      :return-value.sync="time"
                      persistent
                      lazy
                      full-width
                      width="290px">
              <v-text-field slot="activator"
                            v-model="item.value"
                            label="Выберите время"
                            prepend-icon="access_time"
                            readonly></v-text-field>
              <v-time-picker v-model="time"
                             actions>
                <v-spacer></v-spacer>
                <v-btn flat
                       color="primary"
                       @click="item.dialog = false">Cancel</v-btn>
                <v-btn flat
                       color="primary"
                       @click="$refs['dialog'+(index+1)].save(time)">OK</v-btn>
              </v-time-picker>
            </v-dialog>
          </v-list-tile-sub-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
    <v-layout>
</template>
    
<script>
export default {
  props: ["times"],
  data() {
    return {};
  },
  methods: {
    add() {
      this.times.push({
        value: null,
        dialog: false
      });
    },
    remove(index) {
      this.times.splice(index, 1);
    }
  }
};
</script>
