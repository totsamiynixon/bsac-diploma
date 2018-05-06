<template>
<div v-show="loading" id="app-loading">
       <v-progress-circular indeterminate :size="50" color="primary"></v-progress-circular>
</div>
</template>

<script>
export default {
    data(){
        return {};
    },
    computed: {
        loading(){
            var loading = this.$store.getters["shared/loading"];
            return loading;
        }
    }
}
</script>


<style scoped>
#app-loading {
    position: fixed;
    width:100%;
    height: 100%;
    z-index: 99999;
    background-color:rgba(255,255,255, 0.6);
}
#app-loading .progress-circular {
    position: absolute;
    top:50%;
    left:50%;
    margin: auto;
}
</style>