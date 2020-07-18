<template>
<nav class="navbar container" role="navigation" aria-label="main navigation">
  <!-- <div class="navbar-brand">
    <a class="navbar-item" href="/">
      <strong class="is-size-4">Precision Deal</strong>
    </a>
  </div> -->
  <div id="navbar" class="navbar-menu">
    <div class="navbar-start">
      <router-link to="/" class="navbar-item">Home</router-link>
      <router-link to="/properties" class="navbar-item">Properties</router-link>
    </div>
    <div class="navbar-end">
      <div v-if="isSignedIn">
        <a class="nav-link navbar-item" @click="authService.signOut()">Sign out</a>
      </div>
    </div>
  </div>
</nav>
</template>
<script>

import { AuthService } from '../../services/authService';
import UserManager from '../../services/authService';


export default {
    name: 'Nav',
    data () {
      return {
        authService: new AuthService(),
        isSignedIn: false
      }
    },
    mounted () {
      let nav = this;
      nav.authService.getSignedIn().then(function(isSignedIn) {
        nav.isSignedIn = isSignedIn;
      });

      UserManager.events.addUserLoaded(function() {
        nav.isSignedIn = true;
      });

      UserManager.events.addUserUnloaded(function() {
        nav.isSignedIn = false;
      });

  }
}
</script>
<style lang="less" scoped>
nav {
  display: flex;
  background-color: #41b8838c;

  #navbar {
    display: flex;
    flex: auto;
    justify-content: space-between;
    margin-top: 25px;
    margin-bottom: 30px;
    max-width: 900px;
    margin-right: auto;
    margin-left: auto;
  }
  a {
    padding: 0 10px;
    font-weight: bold;
    color: #2c3e50;
    &.router-link-exact-active {
      color: #d88d00;
    }
    cursor: pointer;
    text-decoration: underline;
  }
}
</style>