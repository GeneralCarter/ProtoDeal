import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Callback from '../views/Callback.vue'
import NewProperty from '../views/properties/New.vue'
import PropertyList from '../views/properties/List.vue'
import EditProperty from '../views/properties/Edit'
import Unauthorized from '../views/Unauthorized.vue'
import { AuthService } from '../services/authService'

Vue.use(VueRouter)
let authService = new AuthService()

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/properties',
    name: 'properties',
    meta: {
      requiresAuth: true
    },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "properties" */ '../views/Properties.vue'),
    children: [
      {
        path: 'new',
        name: 'newProperty',
        component: NewProperty
      },
      {
        path: ':id',
        name: 'editProperty',
        component: EditProperty
      },
      {
        path: '',
        component: PropertyList
      }
    ]
  },
  {
    path: '/callback',
    name: 'callback',
    component: Callback
  },
  {
    path: '/unauthorized',
    name: 'Unauthorized',
    component: Unauthorized
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router

router.beforeEach(async (to, from, next) => {
  let app = router.app.$data || { isAuthenticated: false }
  if (app.isAuthenticated) {
    // already signed in, we can navigate anywhere
    next()
  } else if (to.matched.some(record => record.meta.requiresAuth)) {
    authService.getRole().then(
      sucess => {
        if (to.meta.role == sucess) {
          next()
        } else {
          next('/unauthorized')
        }
      },
      err => {
        console.log(err)
      })
  } else {
    // No auth required. We can navigate
    next()
  }
})
