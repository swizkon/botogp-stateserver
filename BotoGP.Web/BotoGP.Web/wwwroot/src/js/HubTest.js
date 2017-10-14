import $ from "jquery"
import Rx from "rxjs/Rx"
import { HubConnection } from "@aspnet/signalr-client"

import BrowserUtil from "./BrowserUtil"

import RenderEngine from "./RenderEngine"

let HubTest = {};

HubTest.DefaultWidth = 150;
HubTest.DefaultHeight = 100;

let load$ = new Rx.Subject();

load$.subscribe((x) => {
    $('h3').text(x.name);
    $('canvas.circuit-tracer').each((i, m) => {
        var points = x.map.checkPoints
        RenderEngine.drawPreview(m, points)
    })
});

var stateChange$ = Rx.Observable.fromEvent($('button.state-change'), 'click')
    .map(e => {
        return {
            'x': parseInt($(e.target).data('x')),
            'y': parseInt($(e.target).data('y'))
        }
    })
    .scan(function (acc, value, index) {
        var forceX = acc.forceX + value.x;
        var forceY = acc.forceY + value.y;
        return {
            "x": acc.x + forceX,
            "y": acc.y + forceY,
            "forceX": forceX,
            "forceY": forceY
        };
    }, { "x": 75, "y": 20, "forceX": 0, "forceY": 0 });

stateChange$.subscribe(value => {
    $('#state').text(JSON.stringify(value) + ' at ' + new Date());

    stateCircle.attr('cy', value.y)
    stateCircle.attr('cx', value.x)
});

var clickEvent$ = Rx.Observable.fromEvent($('canvas#circuit'), 'click');

// var scale = 4;

var connection;

$(document).ready(function () {

    let id = BrowserUtil.getQueryParameter("id");
    var url = `/api/circuits/${id}?only=minimal`

    if (id) {
        Rx.Observable.fromPromise($.getJSON(url)).subscribe(load$);
        $('body').attr('data-circuit-id', id);
    }

    connection = new HubConnection('/race');

    connection.on('send', data => {
        console.log("send: " + data);
    });

    connection.on('move', (racer, x, y) => {
        // $('h3').text(racer + x + y);
        // console.log(racer);

        var stateCircle = $('#' + racer)

        // var inpath = e.target.getContext("2d").isPointInStroke(x, y);

        stateCircle.attr('cy', y / 4);
        stateCircle.attr('cx', x / 4);
        // stateCircle.attr('stroke', inpath ? '#00ff00' : '#ff0000');
    });

    connection.start()
        .then(() => {
            connection.invoke('send', 'Hello');
            Rx.Observable.fromEvent($('#circuit-tracer'), 'mousemove')
                .sample(Rx.Observable.timer(0, 24))
                .subscribe(function (e) {
                    connection.invoke("move", "racer-default", e.offsetX, e.offsetY);
                });
        });
});

export default HubTest;