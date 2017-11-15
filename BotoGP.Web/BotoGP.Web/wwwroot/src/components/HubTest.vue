<template>
  <div class="hubtest">
    <h2>{{ title }}</h2>

    <div class="loading" v-if="loading">
      Loading...
    </div>
    <div v-if="error" class="error">
        Error: {{ error }}
    </div>

    <div v-if="circuitDetails" class="content">

      <svg id="state" width="150" height="100" xmlns="http://www.w3c.org.200/svg" :style="previewStyle">
          <circle id="racer-default" cx="75" cy="20" r="3" stroke="#006600" fill="#fff" stroke-width="2" style="opacity: 0.8;" />
          <circle id="racer-move" cx="75" cy="20" r="3" stroke="#003366" fill="#fff" stroke-width="2" style="opacity: 0.8;" />
      </svg>

      <canvas style="border:solid 1px gray;"
                  class="circuit-tracer" 
                  id="circuit-tracer"
                  width="600" height="400"></canvas>
    </div>


  </div>
</template>

<script>
  import { Observable, Subject } from "rxjs/Rx"
  import { HubConnection } from "@aspnet/signalr-client"
  import RenderEngine from "../js/RenderEngine"

  var connection;

  export default {
    name: 'circuit',
    data() { 
      return {
        title: 'Hub test',
        circuitDetails: null,
        loading: false,
        error: null,
        previewStyle: null
      }
    },

    created () {
      var _this = this;
      _this.error = this.circuitDetails = null
      _this.loading = true
      $.getJSON('/api/circuits/' + this.$route.params.id)
      .then((data) => {
          _this.title = data.name + " - Hub test";
          _this.circuitDetails = data
          _this.loading = false
          _this.previewStyle = `border:solid 1px white;background-image:url(/graphics/circuit/${_this.circuitDetails.id}/svg?scale=1);`
      }, (err) => {
          _this.loading = false
          _this.error = err;
      });

    },
    updated (){
      this.$nextTick(function () {

          $('canvas.circuit-tracer').each((i, m) => {
              var points = this.circuitDetails.map.checkPoints
              RenderEngine.drawPreview(m, points)
          })

          connection = new HubConnection('/race');

          connection.on('send', data => {
              console.log("send: " + data);

              this.$toasted.success('<b>Master </b> Connected to the race'); //.goAway(2000);
          });

          connection.on('move', (racer, x, y) => {
              var stateCircle = $('#' + racer)
              stateCircle.attr('cy', y / 4)
              stateCircle.attr('cx', x / 4)

              this.$toasted.show(racer + ' hello billo').goAway(2000)
          });

          connection.start()
              .then(() => {
                  
                connection.invoke('send', 'Hello from HubTest.vue');

                /*
                $.ajax({
                      type: "PUT",
                      url: "/api/racestate/default/defaultracer?key=defaultKey",
                      contentType: "application/json",
                      data: ""
                })
                */
                  
                  Observable.fromEvent($('#circuit-tracer'), 'mousemove')
                            .throttleTime(50)
                            .subscribe(function (e) {
                                connection.invoke("move", "racer-default", e.offsetX, e.offsetY);
                            });

                  Observable.fromEvent($('#circuit-tracer'), 'click')
                            .subscribe(function (e) {
                                var d = `/api/racestate/default/defaultracer?key=defaultKey&x=${e.offsetX}&y=${e.offsetY}`
                                $.ajax({
                                    type: "POST",
                                    url: d,
                                    contentType: "application/json",
                                    data: ""
                                }).then(function (d) {
                                    console.log(d);
                                });
                            });
              });


      })
    }
  }
</script>