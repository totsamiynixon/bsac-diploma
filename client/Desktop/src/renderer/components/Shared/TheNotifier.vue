<template>
  <v-dialog :value="open"
            max-width="290">
    <v-card>
      <v-card-title class="headline">Пришло время немного позаниматься!</v-card-title>
      <v-card-text></v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="green darken-1"
               flat="flat"
               @click.native="ok();">Хорошо, поехали!</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  data() {
    return {
      open: false
    };
  },
  created() {
    this.$eventBus.on(
      "notifier:notify-user-about-training",
      (event, payload) => {
        this.open = true;
      }
    );
  },
  methods: {
    ok() {
      this.open = false;
      this.$router.push({ name: "training-list" });
    }
  }
};
</script>
