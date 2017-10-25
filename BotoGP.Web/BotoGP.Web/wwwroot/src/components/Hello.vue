<template>
  <div class="hello">
    <h2>{{ msg }}</h2>
    <div v-for="item in items">
        <circuit-list-item :item="item"></circuit-list-item>
    </div>
  </div>
</template>

<script>

import CircuitListItem from './CircuitListItem'

  export default {
    name: 'hello',
    data() { 
      var a = "Da stuffxcxc"
      return {
        msg: `Nice ${a}`,
        items: []
      }
    },
    
    components: {
      CircuitListItem
    },

    created () {
      var _this = this;
        $.getJSON('/api/circuits', function (json) {
            _this.items = $.map(json, (o, i) => {
                        return {
                            "name": o.name,
                            "id": o.id
                        }
                    })
        })
    }
  }
</script>