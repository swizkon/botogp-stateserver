<template>

<div class="container body-content">
    <botogp-nav></botogp-nav>
    <div id="app">
      <div class="hello">
        <h1>{{ msg }}</h1>
      </div>
      <router-view></router-view>

    </div>
    <botogp-footer></botogp-footer>
  </div>
</template>

<script>

import Nav from './components/Nav'
import Footer from './components/Footer'

  export default {
    name: 'app',
    data() {
      return {
        msg: 'Bundled with Brunch!'
      }
    },
    components: {
      'botogp-nav': Nav,
      'botogp-footer': Footer
    }
  } 
</script>

<style>
  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    /*
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
    */
  }
</style>