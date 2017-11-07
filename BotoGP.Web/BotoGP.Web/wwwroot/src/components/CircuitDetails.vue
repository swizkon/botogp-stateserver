<template>
  <div class="hello">
    <h2>{{ title }}</h2>

    <div class="loading" v-if="loading">
      Loading...
    </div>
    <div v-if="error" class="error">
        Error: {{ error }}
    </div>

    <div v-if="circuitDetails" class="content">
      <h2>{{ circuitDetails.name }}</h2>
      <p>{{ circuitDetails.id }}</p>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'circuit',
    data() { 
      return {
        title: 'Circuit details...',
        circuitDetails: null,
        loading: false,
        error: null
      }
    },

    created () {
      var _this = this;
      
      _this.error = this.circuitDetails = null
      _this.loading = true
      $.getJSON('/api/circuits/' + this.$route.params.circuitid)
      .then((data) => {
          _this.circuitDetails = data
          _this.loading = false
      }, (err) => {
          _this.loading = false
          _this.error = err;
      });
    }
  }
</script>