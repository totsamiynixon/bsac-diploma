<template>
  <v-container>
    <v-layout row>
      <v-flex xs12
              sm6
              offset-sm3>
        <v-card>
          <v-card-text>
            <v-container>
              <v-form @submit.prevent="onSignup"
                      ref="signUpForm">
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field name="email"
                                  label="E-mail"
                                  id="email"
                                  v-model="email"
                                  type="email"
                                  :rules="validation.email"></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field name="password"
                                  label="Пароль"
                                  id="password"
                                  v-model="password"
                                  type="password"
                                  :rules="validation.password"></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-text-field name="confirmPassword"
                                  label="Подтвердите пароль"
                                  id="confirmPassword"
                                  v-model="confirmPassword"
                                  type="password"
                                  :rules="comparePasswords"></v-text-field>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex xs12>
                    <v-btn type="submit">
                      Зарегестрироваться
                    </v-btn>
                    <v-btn flat
                           :to="'/auth/signin'">
                      Вход
                    </v-btn>
                  </v-flex>
                </v-layout>
              </v-form>
            </v-container>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      validation: {
        email: [
          v => !!v || "Email является обязательным",
          v =>
            /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
            "E-mail является обязательным"
        ],
        password: [v => !!v || "Пароль является обязательным"]
      },
      email: "",
      password: "",
      confirmPassword: ""
    };
  },
  computed: {
    comparePasswords() {
      return [
        this.password !== this.confirmPassword ? "Пароли не совпадают" : "",
        !!this.password || "Введите подтверждение пароля"
      ];
    },
    user() {
      return this.$store.getters["user/user"];
    }
  },
  watch: {
    user(value) {
      if (value !== null && value !== undefined) {
        this.$router.push("/");
      }
    }
  },
  methods: {
    onSignup() {
      if (this.$refs.signUpForm.validate()) {
        this.$store.dispatch("user/signUserUp", {
          email: this.email,
          password: this.password,
          confirmPassword: this.confirmPassword
        });
      }
    },
    onDismissed() {
      this.$store.dispatch("shared/clearErrors");
    }
  }
};
</script>
