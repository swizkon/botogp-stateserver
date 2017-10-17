<template>
  <div class="hello">
    <h2>{{ msg }}</h2>



    <h3 v-for="item in items">{{item.name}}
    
    <router-link :to="{ name: 'CircuitDetails', params: { circuitid: item.id }}">{{item.name}}</router-link>
    </h3>
  </div>
</template>

<script>
  export default {
    name: 'hello',
    data() { 
      return {
        msg: 'Welcome to 5001 Vue.js App, bundled with Brunch!',
        items: []
      }
    },

    created () {
      // this.fetchData()
      var _this = this;
        $.getJSON('/api/circuits', function (json) {
            _this.items = json;
        });
    }

  
  }
</script>