<template>
  <f7-page>
    <f7-navbar>
      <f7-nav-left>
        <f7-link icon-if-ios="f7:menu"
                 icon-if-md="material:menu"
                 panel-open="left"></f7-link>
      </f7-nav-left>
      <f7-nav-title>Тренировка</f7-nav-title>
      <f7-nav-right>
        <f7-link @click="$f7router.navigate(`/training`)">Начать</f7-link>
      </f7-nav-right>
    </f7-navbar>
    <f7-block v-for="(exercises, key) in userTraining.exercises"
              :key="key">
      <h3>{{key}}</h3>
      <ul class="list media-list"
          style="padding:0">
        <li v-for="exercise in exercises"
            :key="exercise.title">
          <a class="item-content">
            <div class="item-media"
                 style="width:30%"><img :src="getThumbnail(exercise.videoId)"
                   style="width:100%" /></div>
            <div class="item-inner">
              <div class="item-title-row">
                <div class="item-title">{{exercise.name}}</div>
              </div>
              <div class="item-subtitle">{{exercise.countOfRepeats}} повторений</div>
              <div class="item-text">{{exercise.previewText}}</div>
            </div>
          </a>
        </li>
      </ul>
    </f7-block>
  </f7-page>
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
      let toCommit = Object.assign({}, response.data);
      toCommit.exercises = [];
      for (var k in response.data.exercises) {
        if (response.data.exercises.hasOwnProperty(k)) {
          toCommit.exercises = toCommit.exercises.concat(
            response.data.exercises[k]
          );
        }
      }
      this.$store.commit("userTraining/setUserTraining", toCommit);
    });
  },
  methods: {
    getThumbnail(videoId) {
      return `https://i1.ytimg.com/vi/${videoId}/default.jpg`;
    },
    getFirstId() {
      for (var k in this.userTraining.exercises) {
        if (this.userTraining.exercises.hasOwnProperty(k)) {
          return this.userTraining.exercises[k][0].id;
        }
      }
    }
  }
};
</script>
