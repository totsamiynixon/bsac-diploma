<template>
  <v-layout row>
    <v-flex>
      <v-card>
        <v-card-title>
          Создано:
          <strong>
            {{ userTraining.created }}
          </strong>
        </v-card-title>
        <v-list three-line>
          <template v-for="(value, key) in userTraining.exercises">
            <v-subheader :key="'s'+key">
              {{ key }}
            </v-subheader>
            <v-divider :key="'d'+key" />
            <v-list-tile v-for="exercise in value"
                         :key="exercise.title"
                         ripple
                         avatar>
              <v-list-tile-avatar>
                <img :src="getThumbnail(exercise.videoId)">
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>
                  {{ exercise.name }}
                </v-list-tile-title>
                <v-list-tile-sub-title>
                  <strong>{{ exercise.countOfRepeats }}
                    повторений
                  </strong>
                  <br>
                  <p>
                    {{ exercise.previewText }}
                  </p>
                </v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
          </template>
        </v-list>
        <v-card-actions>
          <v-btn color="primary"
                 :to="{name:'training', params:{id:getFirstId()}}">
            Начать
            тренировку
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  data() {
    return {
      userTraining: {
        exercises: {},
      },
    }
  },
  created() {
    this.$apiService.getUserTraining(true).then(groupedUserTraining => {
      this.userTraining = groupedUserTraining
      this.userTraining.created = new Date(
        this.userTraining.created,
      ).toLocaleString()
      const toCommit = Object.assign({}, groupedUserTraining)
      toCommit.exercises = []
      Object.keys(groupedUserTraining.exercises).forEach(k => {
        if (Object.prototype.hasOwnProperty.call(groupedUserTraining.exercises, k)) {
          toCommit.exercises = toCommit.exercises.concat(
            groupedUserTraining.exercises[k],
          )
        }
      })
    })
  },
  methods: {
    getThumbnail(videoId) {
      return `https://i1.ytimg.com/vi/${videoId}/default.jpg`
    },
    getFirstId() {
      Object.keys(this.userTraining.exercises).forEach(k => {
        if (
          Object.prototype.hasOwnProperty.call(this.userTraining.exercises, k)
        ) {
          return this.userTraining.exercises[k][0].id
        }
        return null
      })
    },
  },
}
</script>

