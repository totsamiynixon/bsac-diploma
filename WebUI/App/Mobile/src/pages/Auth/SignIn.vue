<template>
  <f7-login-screen id="login-screen">
    <f7-view>
      <f7-page login-screen>
        <f7-login-screen-title>Вход</f7-login-screen-title>
        <f7-list form>
          <f7-list-item>
            <f7-label>Имя пользователя</f7-label>
            <f7-input :value="email"
                      @input="email = $event.target.value"
                      placeholder="Имя пользователя"
                      type="text"></f7-input>
          </f7-list-item>
          <f7-list-item>
            <f7-label>Пароль</f7-label>
            <f7-input :value="password"
                      @input="password = $event.target.value"
                      type="text"
                      placeholder="Пароль"></f7-input>
          </f7-list-item>
        </f7-list>
        <f7-list>
          <f7-list-button type="submit"
                          @click="onSignin">Войти</f7-list-button>
          <f7-list-button link="/auth/sign-up">Зарегистрироваться</f7-list-button>
          <f7-block-footer>
            <p>@BSACTeam</p>
          </f7-block-footer>
        </f7-list>
      </f7-page>
    </f7-view>
  </f7-login-screen>
</template>
    
 <script>
export default {
  data() {
    return {
      email: "",
      password: ""
    };
  },
  computed: {
    user() {
      return this.$store.getters["user/user"];
    }
  },
  mounted() {
    this.$f7.loginScreen.open("#login-screen");
  },
  created() {
    this.$f7.loginScreen.close("#sign-up-screen");
  },
  watch: {
    user(value) {
      if (value !== null && value !== undefined) {
        this.$f7router.navigate("/");
        this.$f7.loginScreen.close("#login-screen");
      }
    }
  },
  methods: {
    onSignin() {
      this.$store.dispatch("user/signUserIn", {
        email: this.email,
        password: this.password
      });
    }
  }
};
</script>
    