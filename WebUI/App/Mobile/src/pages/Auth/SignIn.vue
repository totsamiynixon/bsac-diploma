<template>
    <f7-login-screen id="login-screen">
      <f7-view>
        <f7-page login-screen>
          <f7-login-screen-title>Login</f7-login-screen-title>
          <f7-list form>
            <f7-list-item>
              <f7-label>Username</f7-label>
              <f7-input :value="email" @input="email = $event.target.value" placeholder="Username" type="text"></f7-input>
            </f7-list-item>
            <f7-list-item>
              <f7-label>Password</f7-label>
              <f7-input :value="password" @input="password = $event.target.value" type="text" placeholder="Password"></f7-input>
            </f7-list-item>
          </f7-list>
          <f7-list>
            <f7-list-button type="submit" @click="onSignin">Sign In</f7-list-button>
            <f7-block-footer>
              <p>Click Sign In to close Login Screen</p>
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
  mounted(){
     this.$f7.loginScreen.open("#login-screen");
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
      this.$store
        .dispatch("user/signUserIn", {
          email: this.email,
          password: this.password
        });
    }
  }
};
</script>
    