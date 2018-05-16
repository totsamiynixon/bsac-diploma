<template>
  <div v-show="loading"
       id="loading"
       class="lds-css">
    <div style="width:100%;height:100%"
         class="lds-eclipse">
      <div></div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  computed: {
    loading() {
      var loading = this.$store.getters["shared/loading"];
      return loading;
    }
  }
};
</script>

<style scoped>
#loading {
  position: fixed;
  z-index: 999999;
  height: 100%;
  width: 100%;
  background: #fff;
}
@keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
@-webkit-keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
.lds-eclipse {
  position: relative;
}
.lds-eclipse div {
  position: absolute;
  -webkit-animation: lds-eclipse 1s linear infinite;
  animation: lds-eclipse 1s linear infinite;
  width: 160px;
  height: 160px;
  top: 35%;
  left: 28%;
  border-radius: 50%;
  box-shadow: 0 4px 0 0 #1875e5;
  -webkit-transform-origin: 80px 82px;
  transform-origin: 80px 82px;
}
.lds-eclipse {
  width: 100% !important;
  height: 100% !important;
  position: absolute;
  -webkit-transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
  transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
}
</style>
