import Vue from "vue";
import Router from "vue-router";

//AUTH
import Signup from "@/components/Auth/Signup";
import Signin from "@/components/Auth/Signin";
import AuthLayout from "@/components/Auth/Shared/Layout";
import AuthGuard from "./auth-guard";

//FEATURES
import FeaturesLayout from "@/components/Features/Shared/Layout"

//EXERCISES
import Exercises from "@/components/Features/Exercises/Exercises.vue"


Vue.use(Router);

export default new Router({
  routes: [{
      path: "/",
      name: "default",
      redirect: "/features"
    },
    {
      path: "/features",
      name: "features",
      component: FeaturesLayout,
      children: [{
        path: "",
        name: "exercises",
        component: Exercises,
      }],
      beforeEnter: AuthGuard
    },
    {
      path: "/auth",
      name: "auth",
      component: AuthLayout,
      children: [{
          path: "signup",
          name: "signup",
          component: Signup
        },
        {
          path: "signin",
          name: "signin",
          component: Signin
        }
      ]
    }
  ],
  mode: "history",
  linkActiveClass: "active-route"
});