<template>
  <div class="hello">
    <h2>{{ msg }}</h2>
    <div v-for="item in items">
        <h1>{{ item.name }}</h1>
        <router-link :to="{ name: 'CircuitDetails', params: { circuitid: item.id }}">
          <img :src="item.thumb" />
        </router-link>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'hello',
    data() { 
      var a = "DA stuffxcxc"
      return {
        msg: `Nice ${a}`,
        items: []
      }
    },

    created () {
      var _this = this;
        $.getJSON('/api/circuits', function (json) {
            _this.items = $.map(json, (o, i) => {
                        return {
                            "name": o.name,
                            "id": o.id,
                            "thumb": `/graphics/circuit/${o.id}/svg?scale=2`
                        }
                    })
        })
    }
  }
</script>