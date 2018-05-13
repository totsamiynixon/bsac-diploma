<template>
  <v-layout column justify-center>
    <v-flex>
      <img src="/static/v.png" alt="Vuetify.js" class="mb-5" />
      <blockquote class="text-xs-center">
        &#8220;First, solve the problem. Then, write the code.&#8221;
        <footer>
          <small>
            <em>&mdash;John Johnson</em>
          </small>
        </footer>
      </blockquote>
      <v-btn @click="startBackground">Start Background</v-btn>
    </v-flex>
  </v-layout>
</template>
<script>
export default {
  data() {
    return {};
  },
  methods: {
    startBackground() {
      const backgroundStartTime = +new Date();
      this.$electron.ipcRenderer.send("background-start", backgroundStartTime);
       this.$electron.ipcRenderer.on("background-response", (event, payload) => {
        alert("We are working in");
      });
    }
  },
  mounted () {
    setInterval(() => {
    this.$electron.ipcRenderer.send('ping')
  }, 10000)

  this.$electron.ipcRenderer.on('pong', (event, data) => {
    this.myDataVar = data
    console.log(data)
  })
  }
};
</script>
<style scoped>
img {
  margin-left: auto;
  margin-right: auto;
  display: block;
}
</style>