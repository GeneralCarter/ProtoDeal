<template>
  <v-app>
    <Nav />
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>
import Nav from './components/partials/Nav.vue'

export default {
  name: 'app',
  components: {
    Nav
  },
  data () {
    return {
      signedIn: true
    }
  },
  mounted () {
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  background-color: #d3ffeb63;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

h3 {
  margin-top: 2em;
  margin-bottom: .4em;
}
</style>
