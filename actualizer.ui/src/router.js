import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import About from "./views/About.vue";
import Auth from "@okta/okta-vue";
//import { isAbsolute } from "path";

Vue.use(Router);

Vue.use(Auth, {
  issuer: "https://dev-839928.okta.com/oauth2/default",
  clientId: "0oa1gm432n01jOOSX357",
  redirectUri: "http://localhost:8080/implicit/callback",
  scopes: ["openid", "profile", "email"],
  pkce: true
});


const router =  new Router({
  mode: 'history',

  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    { path: '/implicit/callback', component: Auth.handleCallback() },

    {
      path: "/about",
      name: "about",
      component: About,
      meta:{
        requiresAuth: true
      }
     
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      // component: () =>
      //   import(/* webpackChunkName: "about" */ "./views/About.vue")
    }
  ]
  
});
router.beforeEach(Vue.prototype.$auth.authRedirectGuard());

export default router;
// Router.beforeEach((to, from, next) => {
//   const requiresAuth = to.matched.some(x => x.meta.requiresAuth)
//   const currentUser = firebase.auth().currentUser

//   if (requiresAuth && !currentUser) {
//     next('/login')
//   } else if (requiresAuth && currentUser) {
//     next()
//   } else {
//     next()
//   }
// })
