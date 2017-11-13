import Vue from 'vue'
import Router from 'vue-router'

import Hello from '../components/Hello'
import CircuitDetails from '../components/CircuitDetails'
import HubTest from '../components/HubTest'
import Practice from '../components/Practice'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/circuits/:id/overview',
      name: 'CircuitDetails',
      component: CircuitDetails
    },
    {
      path: '/circuits/:id/hubtest',
      name: 'HubTest',
      component: HubTest
    },
    {
      path: '/circuits/:id/practice',
      name: 'Practice',
      component: Practice
    },
    {
      path: '*',
      redirect: '/'
    }
  ]
})