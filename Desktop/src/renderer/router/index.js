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

//TRAINING
import TrainingList from "@/components/Features/Training/TrainingList.vue"
import Training from "@/components/Features/Training/Training.vue"
import TrainingResult from "@/components/Features/Training/TrainingResult.vue";

//SETTINGS
import Settings from "@/components/Features/Settings/Settings.vue"


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
      redirect: {
        name: "exercises"
      },
      component: FeaturesLayout,
      children: [{
          path: "exercises",
          name: "exercises",
          component: Exercises,
        },
        {
          path: "settings",
          name: "settings",
          component: Settings,
        },
        {
          path: "training/:id",
          name: "training",
          component: Training,
        },
        {
          path: "training-list",
          name: "training-list",
          component: TrainingList,
        },
        {
          path: "training-result",
          name: "training-result",
          component: TrainingResult,
        },
      ],
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
    },
    {
      path: '*',
      redirect: '/'
    }
  ],
  mode: "history",
  linkActiveClass: "active-route"
});