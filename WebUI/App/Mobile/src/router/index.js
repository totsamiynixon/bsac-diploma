import HomePage from "../pages/home.vue";
import AboutPage from "../pages/about.vue";
import FormPage from "../pages/form.vue";
import DynamicRoutePage from "../pages/dynamic-route.vue";
import NotFoundPage from "../pages/not-found.vue";
import Tabs from "../pages/tabs.vue"


import PanelLeftPage from "../pages/panel-left.vue";
import PanelRightPage from "../pages/panel-right.vue";

/*AUTH*/
import Signin from "../pages/Auth/SignIn.vue";
/*Guards*/
import AuthGuard from "./auth-guard";
import authGuard from "./auth-guard";

export default [
  {
    path: "/auth/sign-in",
    name: "Signin",
    component: Signin
  },
  {
    path: "/",
    component: HomePage,
    on: {
      pageBeforeIn: authGuard
    }
  },
  {
    path: "/panel-left/",
    component: PanelLeftPage
  },
  {
    path: "/tabs/",
    component: Tabs
  },
  {
    path: "/panel-right/",
    component: PanelRightPage
  },
  {
    path: "/about/",
    component: AboutPage
  },
  {
    path: "/form/",
    component: FormPage
  },
  {
    path: "/dynamic-route/blog/:blogId/post/:postId/",
    component: DynamicRoutePage
  },
  {
    path: "(.*)",
    component: NotFoundPage
  }
];
