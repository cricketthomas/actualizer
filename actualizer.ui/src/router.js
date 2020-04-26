import Vue from 'vue';
import Router from 'vue-router';
import Auth from '@okta/okta-vue';

import Home from '@/views/Home.vue';
import Dashboard from '@/views/Dashboard.vue';

import Settings from '@/views/DashboardViews/Settings.vue';
import About from '@/views/DashboardViews/About.vue';
import Search from '@/views/DashboardViews/Search.vue';
import Analytics from '@/views/DashboardViews/Analytics.vue';

// TODO: Make these lazy load.
Vue.use(Router);

Vue.use(Auth, {
  issuer: process.env.VUE_APP_ISSUER_URL,
  clientId: `${process.env.VUE_APP_CLIENTID}`,
  redirectUri: `${process.env.VUE_APP_OKTA_BASEURL}implicit/callback`,
  scopes: ['openid', 'profile', 'email'],
  pkce: true
});

const router = new Router({
  mode: 'history',
  linkActiveClass: 'is-active',
  routes: [
    {
      path: '/dashboard',
      name: 'Dashboard',
      component: Dashboard,
      children: [
        {
          path: '/dashboard/about',
          name: 'about',
          component: About,
          meta: {
            requiresAuth: false,
            title: 'about'
          }
        },
        {
          path: '/dashboard/search',
          name: 'search',
          component: Search,
          meta: {
            requiresAuth: false,
            title: 'search'
          }
        },
          {
            path: '/dashboard/settings',
            name: 'settings',
            component: Settings,
            meta: {
              requiresAuth: false,
              title: 'Settings'
            }
          },


        {
          path: '/dashboard/analytics',
          name: 'analytics',
          component: Analytics,
          meta: {
            requiresAuth: false,
            title: 'Analytics'
          }
        }
      ]
    },
    
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false,
        title: `${process.env.VUE_APP_TITLE}`
      }
    },
    {
      path: '/implicit/callback',
      component: Auth.handleCallback()
    }
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    // component: () =>
    //   import(/* webpackChunkName: "about" */ "./views/About.vue")
  ]
});

router.beforeEach(Vue.prototype.$auth.authRedirectGuard());
router.afterEach(to => {
  document.title = `${process.env.VUE_APP_TITLE}/${to.meta.title}`;
});

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
