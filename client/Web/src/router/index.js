import Vue from "vue";
import Router from "vue-router";
import Signup from "@/components/Auth/Signup";
import Signin from "@/components/Auth/Signin";
import AuthGuard from "./auth-guard";
import ProjectLoader from "./project-loader";
import DefaultLayout from "@/components/Default/Shared/Layout";
import AuthLayout from "@/components/Auth/Shared/Layout";
import AdminLayout from "@/components/Admin/Shared/Layout";
import Criterias from "@/components/Admin/Criteria/Criterias";
import Exercises from "@/components/Admin/Exercise/Exercises";
import AddOrUpdateExercise from "@/components/Admin/Exercise/AddOrUpdateExercise";
import Professions from "@/components/Admin/Profession/Professions";
import AddOrUpdateProfession from "@/components/Admin/Profession/AddOrUpdateProfession";


Vue.use(Router);

export default new Router({
  routes: [{
      path: "/",
      redirect: "/admin",
      name: "default",
      component: DefaultLayout,
      children: [
        /*{
          path: "projects",
          name: "projects",
          component: Projects,
          beforeEnter: AuthGuard
        }*/
      ],
      beforeEnter: AuthGuard
    },
    {
      path: "/admin",
      name: "admin",
      redirect: "/admin/criterias",
      component: AdminLayout,
      children: [{
          path: "criterias",
          name: "criterias",
          component: Criterias
        },
        {
          path: "exercises",
          name: "exercises",
          component: Exercises
        },
        {
          path: "exercises/create",
          name: "create-exercise",
          component: AddOrUpdateExercise
        },
        {
          path: "exercises/edit/:id",
          name: "edit-exercise",
          component: AddOrUpdateExercise
        },
        {
          path: "professions",
          name: "professions",
          component: Professions
        },
        {
          path: "professions/create",
          name: "create-profession",
          component: AddOrUpdateProfession
        },
        {
          path: "professions/edit/:id",
          name: "edit-profession",
          component: AddOrUpdateProfession
        }
      ],
      beforeEnter: AuthGuard
    },
    {
      path: "/auth",
      name: "auth",
      redirect: "auth/signin",
      component: AuthLayout,
      children: [{
          path: "signup",
          name: "Signup",
          component: Signup
        },
        {
          path: "signin",
          name: "Signin",
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
