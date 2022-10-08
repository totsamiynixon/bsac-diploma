import HomePage from "../pages/home.vue";
import AboutPage from "../pages/about.vue";
import FormPage from "../pages/form.vue";
import DynamicRoutePage from "../pages/dynamic-route.vue";
import NotFoundPage from "../pages/not-found.vue";
import Tabs from "../pages/tabs.vue"



import PanelLeftPage from "../pages/panel-left.vue";
import PanelRightPage from "../pages/panel-right.vue";

/*SETTINS*/
import Settings from "../pages/Settings/Settings.vue"
import ProfessionSettings from "../pages/Settings/Profession.vue"
import PreferredTimeSettings from "../pages/Settings/PreferredTime.vue"

/*EXERCISES*/
import Exercises from "../pages/Exercises/Exercises.vue"
import Exercise from "../pages/Exercises/Exercise.vue"
/*TRAINING*/
import Training from "../pages/Training/Training.vue"
import TrainingList from "../pages/Training/TrainingList.vue"
import TrainingResult from "../pages/Training/TrainingResult.vue"
/*AUTH*/
import Signin from "../pages/Auth/SignIn.vue";
import SignUp from "../pages/Auth/SignUp.vue";
/*Guards*/
import AuthGuard from "./auth-guard";
import authGuard from "./auth-guard";

export default [{
    path: "/auth/sign-in",
    name: "Signin",
    component: Signin
  },
  {
    path: "/auth/sign-up",
    name: "SignUp",
    component: SignUp
  },
  {
    path: "/",
    component: HomePage,
    on: {
      pageBeforeIn: authGuard
    }
  },
  {
    path: "/exercises",
    component: Exercises
  },
  {
    path: "/exercises/:id",
    component: Exercise
  },
  {
    path: "/training-list",
    component: TrainingList
  },
  {
    path: "/training",
    component: Training
  },
  {
    path: "/training-result",
    component: TrainingResult
  },
  {
    path: "/settings",
    component: Settings,
    routes: [{
        path: "profession",
        name: "settings-profession",
        component: ProfessionSettings,
      },
      {
        path: "preferred-time",
        name: "settings-preferred-time",
        component: PreferredTimeSettings,
      }
    ],
    on: {
      pageBeforeIn: authGuard
    }
  },
  {
    path: "/panel-left/",
    component: PanelLeftPage
  }
];
