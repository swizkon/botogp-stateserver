import Vue from 'vue'
import Router from 'vue-router'
import Hello from '../components/Hello'
import CircuitDetails from '../components/CircuitDetails'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/circuits/:circuitid',
      name: 'CircuitDetails',
      component: CircuitDetails
    },
    {
      path: '*',
      redirect: '/'
    }
  ]
})