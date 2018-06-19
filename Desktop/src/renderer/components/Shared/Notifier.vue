<template>
  <v-dialog :value="dialog"
            @input="$emit('update:dialog', $event.target.value)"
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
  props: ["dialog"],
  data() {
    return {};
  },
  methods: {
    ok() {
      this.$router.push({ name: "training-list" });
      this.$emit("update:dialog", false);
    }
  }
};
</script>