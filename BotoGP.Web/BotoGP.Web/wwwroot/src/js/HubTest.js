import $ from "jquery"
import { Observable, Subject } from "rxjs/Rx"
import { HubConnection } from "@aspnet/signalr-client"
import BrowserUtil from "./BrowserUtil"
import RenderEngine from "./RenderEngine"

let HubTest = {};

/*
let load$ = new Subject();

load$.subscribe((x) => {
    $('h3').text(x.name);
    $('canvas.circuit-tracer').each((i, m) => {
        var points = x.map.checkPoints
        RenderEngine.drawPreview(m, points)
    })
});
*/

// var connection;

$(document).ready(function () {
    /*
    let id = BrowserUtil.getQueryParameter("id");
    var url = `/api/circuits/${id}?only=minimal`

    if (id) {
        Observable.fromPromise($.getJSON(url)).subscribe(load$);
        $('body').attr('data-circuit-id', id);
    }
    */

    /*
    connection = new HubConnection('/race');

    connection.on('send', data => {
        console.log("send: " + data);
    });

    connection.on('move', (racer, x, y) => {
        var stateCircle = $('#' + racer)
        stateCircle.attr('cy', y / 4)
        stateCircle.attr('cx', x / 4)
    });

    connection.start()
        .then(() => {
            
            connection.invoke('send', 'Hello from HubTest.js');

            $.ajax({
                type: "PUT",
                url: "/api/racestate/default/defaultracer?key=defaultKey",
                contentType: "application/json",
                data: ""
            })

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
        */


});

// export default HubTest; 