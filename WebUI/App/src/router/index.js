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
import AddOrUpdateexercise from "@/components/Admin/Exercise/AddOrUpdateExercise";

Vue.use(Router);

export default new Router({
  routes: [
    {
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
      children: [
        {
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
          component: AddOrUpdateexercise
        },
        {
          path: "exercises/edit/:id",
          name: "edit-exercise",
          component: AddOrUpdateexercise
        }
      ],
      beforeEnter: AuthGuard
    },
    {
      path: "/auth",
      name: "auth",
      redirect: "auth/signin",
      component: AuthLayout,
      children: [
        {
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
    }
  ],
  mode: "history",
  linkActiveClass: "active-route"
});
