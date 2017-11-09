import Vue from 'vue'
import Splash from './components/Splash'

import "vueify/lib/insert-css" // required for .vue file <style> tags

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
    el: '#splash',
    render: h => h(Splash),
    components: {
        Splash
    }
})