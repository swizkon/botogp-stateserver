<template>
  <div class="jumbotron circuit-preview">
    <h1>{{ item.name }}</h1>

    <img :src="thumb" />
    <router-link :to="{ name: 'CircuitDetails', params: { id: item.id }}">
      Stats
    </router-link>
    <router-link :to="{ name: 'Race', params: { id: item.id }}" class="btn btn-primary btn-lg">
      Race this circuit
    </router-link>
    <router-link :to="{ name: 'Practice', params: { id: item.id }}" class="btn btn-default">
      Practice
    </router-link>
    <router-link :to="{ name: 'HubTest', params: { id: item.id }}" class="btn btn-default">
      HubTest
    </router-link>
    <router-link :to="{ name: 'Edit', params: { id: item.id }}" class="btn btn-default">
      Edit
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
  created() {
    this.thumb = `/graphics/circuit/${this.item.id}/svg?scale=3`
  }
}
</script>