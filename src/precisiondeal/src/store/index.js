import Vue from 'vue'
import Vuex from 'vuex'
import types from '../types'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    selectedProperty: null
  },
  mutations: {
    setSelectedProperty (state, property) {
      state.selectedProperty = property
    }
  },
  actions: {
    setSelectedProperty ({ commit }, property) {
      commit(types.SET_CURRENT_PROPERTY, property)
    }
  }
})
