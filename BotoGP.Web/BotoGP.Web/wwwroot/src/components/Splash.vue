<template>
  <div class="container body-content">
    <botogp-nav></botogp-nav>

      <div class="hello">
        <h1>{{ msg }}</h1>
      </div>
    
<div id="circuit-carousel" class="carousel slide" data-ride="carousel" data-interval="6000">
    <ol class="carousel-indicators">
        <li v-if="first" data-target="#myCarousel" data-slide-to="0" class="active"></li>
        <li data-target="#myCarousel" v-for="(item, index) in items" :data-slide-to="index+1"></li>
    </ol>
    <div class="carousel-inner" role="listbox">
        
          <div v-if="first" class="item active">
            <img class="circuit" :alt="first.name" :src="first.thumb" />
            <div class="carousel-caption" role="option">
               <h1>{{first.name}}</h1>
               <p>
                <a class="btn btn-default" :href="first.url">
                  Practice
                </a>
              </p>
             </div>
          </div>
        

          <div v-for="(item, index) in items" class="item">
            <img class="circuit" :alt="first.name" :src="item.thumb" /> 
                <div class="carousel-caption" role="option">
                    <h1>{{item.name}}</h1>
                    <p>
                <a class="btn btn-default" :href="item.url">
                  Practice
                </a>
                    </p>
                </div>
          </div>
        
    </div>
    
    <a class="left carousel-control" href="#circuit-carousel" role="button" data-slide="prev">
        <span class="icon-prev" aria-hidden="true"></span>
        <span class="sr-only">Previous</span>
    </a>
    <a class="right carousel-control" href="#circuit-carousel" role="button" data-slide="next">
        <span class="icon-next" aria-hidden="true"></span>
        <span class="sr-only">Next</span>
    </a>
</div>


<div class="row">
    <div class="col-sm-6">
        <h2>Application uses</h2>
        <p class="lead">Some lead text...</p>
        <p>... and then some more...</p>
    </div>
    <div class="col-sm-6">
        <h2>How to</h2>
        <p class="lead">Some lead text...</p>
        <p>... and then some more...</p>
    </div>
</div>

    <botogp-footer></botogp-footer>
  </div>
</template>

<script>

import Nav from '../components/Nav'
import Footer from '../components/Footer'

  export default {
    name: 'splash',
    data() {
      return {
        msg: 'Loading circuits...',
        head: null,
        items: null
      }
    },
    components: {
      'botogp-nav': Nav,
      'botogp-footer': Footer
    },

    created () {
      var _this = this;
        $.getJSON('/api/circuits/references', function (json) {
            _this.items = $.map(json, (o, i) => {
                        return {
                            "name": o.name,
                            "id": o.id,
                            "thumb": `/graphics/circuit/${o.id}/svg`,
                            "url": `/circuits.html#/circuit/${o.id}/practice`
                        }
                    })
            _this.first = _this.items.shift();
            _this.msg = "Splash.vue";
        })
    }
  } 
</script>