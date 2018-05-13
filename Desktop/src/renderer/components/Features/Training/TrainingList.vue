<template>
  <v-layout row>
    <v-flex>
      <v-card>
        <v-list three-line>
          <template v-for="(item, index) in items">
            <v-subheader :key="item.header">{{ item.header }}</v-subheader>
            <v-divider :key="index"></v-divider>
            <v-list-tile :key="exercise.title" v-for="exercise in item.exercises"
                         ripple
                         avatar
                         :to="{name:'training', params:{id:0}}">
              <v-list-tile-avatar>
                <img :src="exercise.avatar">
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title v-html="exercise.title"></v-list-tile-title>
                <v-list-tile-sub-title>
                  <strong>{{exercise.countOfRepeats}} повторений</strong>
                  <br>
                  <p>{{exercise.subtitle}}</p>
                </v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
          </template>
        </v-list>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  data() {
    return {
      items: [
        {
          exercises: [
            {
              avatar:
                "https://i.ytimg.com/vi/vJmNWi2EMew/hqdefault.jpg?sqp=-oaymwEYCNIBEHZIVfKriqkDCwgBFQAAiEIYAXAB&rs=AOn4CLBEpvl3mFwNzV-JpynmS5WcO-tSbQ",
              title: "Отжиманя",
              countOfRepeats: 10,
              subtitle:
                "Постарайтесь соблюдать технику выполнения. Вдох на во время опускания туловища, выдох на подъеме"
            }
          ],
          header: "Средняя сложность"
        }
      ]
    };
  }
};
</script>