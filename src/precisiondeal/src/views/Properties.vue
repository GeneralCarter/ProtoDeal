<template>
  <div class="properties">
    <h2>Properties</h2>
    <router-view/>
    <!-- <Calculator></Calculator> -->
  </div>
</template>

<script>
import axios from 'axios'
//import Calculator from '../calculator/loanCalculator'

export default {
  name: 'Properties',
  data () {
    return {
      values: ['no data yet'],
      services: []
    }
  },
  methods: {
    async callApi () {
      try {
        const response = await axios.get('http://localhost:5000/api/values')
        this.values = response.data
      } catch (err) {
        this.values.push('Ooops!' + err)
      }
    },
    async callSecureApi () {
      try {
        axios.defaults.crossDomain = true
        axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*'
        const response = await axios.get('http://localhost:5000/WeatherForecast')
        this.services = response.data
      } catch (err) {
        console.log('secure api call failed')
      }
    }
  }
}
</script>

<style>
  .properties {
    max-width: 900px;
    margin-left: auto;
    margin-right: auto;
  }
</style>
