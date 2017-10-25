<template>
  <div>
    <h1>{{ item.name }}</h1>
    <router-link :to="{ name: 'CircuitDetails', params: { circuitid: item.id }}">
      <img :src="thumb" />
    </router-link>
  </div>
</template>

<script>
module.exports = {
  name: 'circuitlistitem',
  props: ['item'],
  data() {
    return {
      // "thumb": null
    }
  },
  created(){
    var _this = this
    _this.thumb = `/graphics/circuit/${this.item.id}/svg?scale=2`
  }
}
</script>