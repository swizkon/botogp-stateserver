<template>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a href="/index.html" class="navbar-brand">Boto GP <small>(Home)</small></a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li><a href="/circuits.html">Circuits</a></li>
                    <li><a href="/About">About</a></li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
  module.exports = {
    name: 'botogp-nav',
    data() {
      return {
        copyright: '2017 - swizkon'
      }
    }
  } 
</script>