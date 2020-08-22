import Vue from 'vue'
import Vuex from 'vuex'
import App from './App.vue'
import { mapState } from 'vuex'
import router from './router'
import mgr from './services/authService'
import axios from 'axios'
import VueFormulate from '@braid/vue-formulate'
import FormulateCurrencyInput from './components/formulate/FormulateCurrencyInput'
import FormulatePercentInput from './components/formulate/FormulatePercentInput'
import store from './store'

Vue.config.productionTip = false

Vue.component('FormulateCurrencyInput', FormulateCurrencyInput)
Vue.component('FormulatePercentInput', FormulatePercentInput)

Vue.use(Vuex)
Vue.use(VueFormulate, {
  library: {
    currency: {
      classification: 'text',
      component: 'FormulateCurrencyInput'
    },
    percent: {
      classification: 'text',
      component: 'FormulatePercentInput'
    }
  }
})

const globalData = {
  isAuthenticated: false,
  user: '',
  mgr: mgr
}

const globalMethods = {
  async authenticate (returnPath) {
    const user = await this.$root.getUser() //see if the user details are in local storage
    if (user) {
      this.isAuthenticated = true
      this.user = user
    } else {
      await this.$root.signIn(returnPath)
    }
  },
  async getUser () {
    try {
      let user = await this.mgr.getUser()
      return user
    } catch (err) {
      console.log(err)
    }
  },
  signIn (returnPath) {
    returnPath ? this.mgr.signinRedirect({ state: returnPath })
      : this.mgr.signinRedirect()
  },
  mapState
}

let v = new Vue({
  router,
  data: globalData,
  methods: globalMethods,
  store: store.create(),
  store,
  render: h => h(App)
}).$mount('#app')

axios.defaults.crossDomain = true
axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*'
axios.interceptors.request.use(async (config) => {
  const user = await mgr.getUser()
  if (user) {
    const authToken = user.access_token
    if (authToken) {
      config.headers.Authorization = `Bearer ${authToken}`
    }
  }
  return config
},
(err) => {
  // What do we do when we get errors?
})
