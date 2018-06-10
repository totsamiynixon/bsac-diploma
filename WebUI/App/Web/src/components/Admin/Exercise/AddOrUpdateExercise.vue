
<template>
  <v-container>
    <v-form ref="form"
            lazy-validation>
      <v-text-field v-model="currentExercise.name"
                    :rules="validation.exercise.name"
                    label="Название"></v-text-field>
      <v-text-field v-model="currentExercise.videoUrl"
                    :rules="validation.exercise.videoUrl"
                    label="Видео"></v-text-field>
      <v-flex>
        <iframe :src="currentExercise.videoUrl"></iframe>
      </v-flex>
      <v-text-field v-model="currentExercise.description"
                    :rules="validation.exercise.description"
                    label="Описание"
                    multi-line
                    required></v-text-field>
      <v-text-field v-model="currentExercise.previewText"
                    :rules="validation.exercise.previewText"
                    label="Текст на превью"
                    multi-line
                    required></v-text-field>
      <v-select :items="getDifficultyLevelOptions()"
                :value="currentExercise.difficultyLevel"
                @change="setCriteria($event, index)"
                label="Выберите критерий"
                item-text="text"
                item-value="id"></v-select>
      <v-layout row
                v-for="(currentExerciseCriteria,index) in currentExercise.criterias"
                :key="index">
        <v-flex px-5
                md5>
          <v-select :items="selectOptions"
                    :value="currentExercise.criterias[index]"
                    @change="setCriteria($event, index)"
                    label="Выберите критерий"
                    item-text="text"
                    item-value="value"></v-select>
        </v-flex>
        <v-flex px-5
                md5>
          <v-text-field v-model="currentExercise.criterias[index].weight"
                        :rules="validation.exercise.criteria.weight"
                        label="Вес"></v-text-field>
        </v-flex>
        <v-flex md2>
          <v-btn @click="removeCriteria(index)">
            <v-icon>remove</v-icon>
          </v-btn>
        </v-flex>
      </v-layout>
      <v-btn v-show="currentExercise.criterias.length != criterias.length"
             @click="addCriteria">Добавить критерий</v-btn>
      <v-btn @click="submit">Сохранить</v-btn>
    </v-form>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      validation: {
        exercise: {
          name: [
            v => !!v || "Название является обязательным",
            v =>
              (v && v.length <= 25) ||
              "Название не должно содержать больше 25 символов"
          ],
          description: [
            v => !!v || "Описание является обязательным",
            v =>
              (v && v.length <= 512) ||
              "Описание должно быть длиной не более 512 символов"
          ],
          previewText: [
            v => !!v || "Текст для превью является обязательным",
            v =>
              (v && v.length <= 200) ||
              "Текст для превью должен быть длиной не более 200 символов"
          ],
          criteria: {
            weight: [v => !!v || "Вес является обязательным"]
          },
          videoUrl: [
            v => !!v || "Видео является обязятельным полем",
            v =>
              v.match(new RegExp("youtube.com/((v|embed)/)?[a-zA-Z0-9]+")) ||
              "Видео должно быть с YouTube"
          ]
        }
      },
      exercise: {
        id: 0,
        name: "",
        description: "",
        previewText: "",
        difficultyLevel: "",
        criterias: []
      },
      criterias: []
    };
  },
  methods: {
    getDifficultyLevelOptions() {
      return [
        {
          id: 0,
          text: "Легко"
        },
        {
          id: 1,
          text: "Средне"
        },
        {
          id: 2,
          text: "Тяжело"
        }
      ];
    },
    setCriteria(criteria, index) {
      this.currentExercise.criterias[index] = criteria;
      this.currentExercise.criterias = this.currentExercise.criterias.slice();
    },
    addCriteria() {
      this.currentExercise.criterias.push({
        id: 0,
        criteriaId: 0,
        weight: 10
      });
    },
    removeCriteria(index) {
      this.currentExercise.criterias.splice(index, 1);
    },
    submit() {
      if (this.$refs.form.validate()) {
        var payload = Object.assign({}, this.currentExercise);
        console.log(payload);
        this.$store.dispatch("admin/exercise/addOrUpdateExercise", payload);
      }
    }
  },
  created() {
    var id = this.$route.params.id || null;
    this.$store.dispatch(
      "admin/exercise/loadExercise",
      id != null ? { id: id } : null
    );
  },
  computed: {
    enabledCriterias() {
      var disabled = this.criterias.filter(
        c =>
          this.currentExercise.criterias.filter(z => z.criteriaId == c.id)
            .length > 0
      );
      disabled.forEach(v => {
        v.disabled = true;
      });
      var enabled = this.criterias.filter(
        c => disabled.filter(z => z.id == c.id).length == 0
      );
      enabled.forEach(v => {
        v.disabled = false;
      });
      return this.criterias;
    },
    currentExercise: {
      get() {
        var data = this.$store.getters["admin/exercise/currentExerciseData"];
        if (data != null) {
          this.exercise = data.exercise;
          this.criterias = data.criterias;
        }
        return this.exercise;
      }
    },
    selectOptions: {
      get() {
        var options = this.criterias.map(c => {
          var item = this.currentExercise.criterias.filter(
            z => z.criteriaId == c.id
          )[0];
          return {
            text: c.name,
            value: item || {
              id: 0,
              criteriaId: c.id,
              weight: 10
            },
            disabled: item != null
          };
        });
        return options;
      }
    }
  }
};
</script>
