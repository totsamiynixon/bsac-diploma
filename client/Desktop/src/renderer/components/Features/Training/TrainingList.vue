<template>
  <v-layout row>
    <v-flex>
      <v-card>
        <v-card-title>Создано:
          <strong>{{userTraining.created}}</strong>
        </v-card-title>
        <v-list three-line>
          <template v-for="(value, key) in userTraining.exercises">
            <v-subheader :key="key">{{ key }}</v-subheader>
            <v-divider :key="key"></v-divider>
            <v-list-tile :key="exercise.title"
                         v-for="exercise in value"
                         ripple
                         avatar
                         >
              <v-list-tile-avatar>
                <img :src="getThumbnail(exercise.videoId)">
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>{{exercise.name}}</v-list-tile-title>
                <v-list-tile-sub-title>
                  <strong>{{exercise.countOfRepeats}} повторений</strong>
                  <br>
                  <p>{{exercise.previewText}}</p>
                </v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
          </template>
        </v-list>
        <v-card-actions>
          <v-btn color="primary" :to="{name:'training', params:{id:getFirstId()}}">Начать тренировку</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  data() {
    return {
      userTraining: {}
    };
  },
  created() {
    this.$http.get("/api/userTrainings/get").then(response => {
      this.userTraining = response.data;
      this.userTraining.created = new Date(this.userTraining.created).toLocaleString();
      let toCommit = Object.assign({},response.data);
      toCommit.exercises = [];
      for (var k in response.data.exercises){
    if (response.data.exercises.hasOwnProperty(k)) {
         toCommit.exercises = toCommit.exercises.concat(response.data.exercises[k]);
    }
}
      this.$store.commit("features/userTraining/setUserTraining",toCommit);
    });
  },
  methods:{
    getThumbnail(videoId){
      return `https://i1.ytimg.com/vi/${videoId}/default.jpg`
    },
    getFirstId(){
     for (var k in this.userTraining.exercises){
     if (this.userTraining.exercises.hasOwnProperty(k)) {
         return this.userTraining.exercises[k][0].id;
    }
    
    }
    }
  }
};

/*
 items: [
        {
          header: "Низкая сложность",
          exercises: [
            {
              avatar: "https://www.wikireading.ru/img/143620_73_i_042.png",
              title: "Наклоны",
              countOfRepeats: 10,
              subtitle:
                "oчeнь пpocтoe yпpaжнeниe, koтopoe знakomo нam eщe c дeтcтвa"
            }
          ]
        },
        {
          exercises: [
            {
              avatar:
                "https://i.ytimg.com/vi/vJmNWi2EMew/hqdefault.jpg?sqp=-oaymwEYCNIBEHZIVfKriqkDCwgBFQAAiEIYAXAB&rs=AOn4CLBEpvl3mFwNzV-JpynmS5WcO-tSbQ",
              title: "Отжиманя",
              countOfRepeats: 10,
              subtitle:
                "Постарайтесь соблюдать технику выполнения. Вдох на во время опускания туловища, выдох на подъеме"
            },
            {
              avatar: "http://sportwiki.to/images/3/3f/Sil_men_prised.jpg",
              title: "Приседания",
              countOfRepeats: 10,
              subtitle:
                "Одно из базовых силовых упражнений (в том числе в пауэрлифтинге и культуризме)"
            }
          ],
          header: "Средняя сложность"
        }
      ]
      */
</script>

     