import Vue from "vue";
import Router from "vue-router";

// AUTH
import Signup from "@/components/Auth/Signup";
import Signin from "@/components/Auth/Signin";
import TheAuthLayout from "@/components/Auth/Shared/TheLayout";
import AuthGuard from "./auth-guard";

// FEATURES
import TheFeaturesLayout from "@/components/Features/Shared/TheLayout"

// EXERCISES
import Exercises from "@/components/Features/Exercises/Exercises.vue"
import SingleExercise from "@/components/Features/Exercises/Exercise.vue"

// TRAINING
import TrainingList from "@/components/Features/Training/TrainingList.vue"
import Training from "@/components/Features/Training/Training.vue"
import TrainingResult from "@/components/Features/Training/TrainingResult.vue";

// SETTINGS
import Settings from "@/components/Features/Settings/Settings.vue"

Vue.use(Router);

export default new Router({
  routes: [{
      path: "/",
      name: "default",
    },
    {
      path: "/features",
      name: "features",
      redirect: {
        name: "exercises"
      },
      component: TheFeaturesLayout,
      children: [{
          path: "exercises",
          name: "exercises",
          component: Exercises,
          beforeEnter:AuthGuard
        },
        {
          path: "exercises/:id",
          name: "exercise",
          component: SingleExercise,
          beforeEnter:AuthGuard
        },
        {
          path: "settings",
          name: "settings",
          component: Settings,
          beforeEnter:AuthGuard
        },
        {
          path: "training/:id",
          name: "training",
          component: Training,
          beforeEnter:AuthGuard
        },
        {
          path: "training-list",
          name: "training-list",
          component: TrainingList,
          beforeEnter:AuthGuard
        },
        {
          path: "training-result",
          name: "training-result",
          component: TrainingResult,
          beforeEnter:AuthGuard
        },
      ],
      beforeEnter: AuthGuard
    },
    {
      path: "/auth",
      name: "auth",
      component: TheAuthLayout,
      redirect: {
        name: "signin"
      },
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
  mode: "hash",
  linkActiveClass: "active-route"
});
