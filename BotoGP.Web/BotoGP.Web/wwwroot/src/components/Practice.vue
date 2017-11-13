<template>
  <div class="practice">
    <h2>{{ title }}</h2>

    <div class="loading" v-if="loading">
      Loading...
    </div>
    <div v-if="error" class="error">
        Error: {{ error }}
    </div>

    <div v-if="circuitDetails" class="content">
      <p>{{ circuitDetails.id }}</p>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'circuit',
    data() { 
      return {
        title: 'Practice',
        circuitDetails: null,
        loading: false,
        error: null
      }
    },

    created () {
      var _this = this;
      
      _this.error = this.circuitDetails = null
      _this.loading = true
      $.getJSON('/api/circuits/' + this.$route.params.id)
      .then((data) => {
          _this.title = "Practice on " + data.name
          _this.circuitDetails = data
          _this.loading = false
      }, (err) => {
          _this.loading = false
          _this.error = err;
      });
    }
  }
</script>