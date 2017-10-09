import $ from "jquery"
import Rx from "rxjs/Rx"

let BotoGP = window['BotoGP'] || {};

BotoGP.DefaultWidth = 150;
BotoGP.DefaultHeight = 100;

BotoGP.printer = { 

    renderPreviews: function () {
        $('canvas.circuit-preview').each((i, m) => {
            var points = $(m).data('checkpoints');
            BotoGP.printer.drawPreview(m, points);
        });
    },
    drawPreview: function (canvas, points) {
        var scale = canvas.width / BotoGP.DefaultWidth;
        var scaledPoints = $.map(points, (o, i) => {
            return {
                x: o[0] * scale,
                y: o[1] * scale
            }
        });
        var context = canvas.getContext("2d");
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.translate(0.0, 0.0);

        BotoGP.printer.drawPath(context, 15 * scale, "#ddd", scaledPoints);
        BotoGP.printer.drawPath(context, 20 * scale, "rgba(200,200,200,0.5)", scaledPoints);
    },

    drawPath: function (context, lineWidth, color, points) {
        context.lineWidth = lineWidth;
        context.strokeStyle = color;
        context.beginPath();
        context.moveTo(points[0].x, points[0].y);
        var pointIndex;
        for (pointIndex = 0; pointIndex < points.length; pointIndex++) {
            context.lineTo(points[pointIndex].x, points[pointIndex].y);
        }
        context.closePath();
        context.lineJoin = 'round';
        context.stroke();
    }
};

BotoGP.designer = {
    isPointOfInterest: function (context, x, y) {
        var inpath = context.isPointInStroke(x, y);

        return inpath != context.isPointInStroke(x - 1, y);
         //   || inpath != context.isPointInStroke(x + 1, y);
    },
    pointsOfInterest: function (canvas) {
        if(!canvas) return {};
        
        var context = canvas.getContext("2d");
        var x, y, pointsOfInterest = { "on": [], "off": [], "heat": {}};
        for (y = 0; y < canvas.height; y += 1) {
            for (x = 0; x < canvas.width; x += 1) {

                var inpath = context.isPointInStroke(x, y);

                var type = inpath ? "on" : "off";

                var isOfInterest = BotoGP.designer.isPointOfInterest(context, x, y);
                if (isOfInterest) {
                    pointsOfInterest[type][pointsOfInterest[type].length] = {
                        'x': x,
                        'y': y
                    };
                    var h = pointsOfInterest["heat"][y.toString()] || {};
                    h[x.toString()] = type == "on" ? 1 : 0;
                    pointsOfInterest["heat"][y.toString()] = h;
                }
            }
        }
        
        return pointsOfInterest;
    }
};

BotoGP.repo = {
    changeName: function (id, name) {
        BotoGP.repo.change(id, { "name": name });
    },
    changeDataMap: function (id, dataMap) {
        var checkPoints = $.map(dataMap["checkpoints"], (o, i) => {
            return {
                x: o[0] * scale,
                y: o[1] * scale
            }
        });
        var dto = {
            "checkPoints": checkPoints
        };
        BotoGP.repo.change(id, dto);
    },
    change: function (id, changes) {
        $.ajax({
            type: "POST",
            url: "/api/circuits/" + id,
            contentType: "application/json",
            data: JSON.stringify(changes)
        }).then(function (d) {
            $('h1.circuit-checkpoints').text(JSON.stringify(d.dataMap.checkpoints));
        });
    }
};

var circuitModel = {
    "name": "Default track",
    "width": 150,
    "height": 100,
    "scale": scale,
    "checkpoints": [],
    "pointsOfInterest": {}
};

// var points = [];

var clickEvent$ = Rx.Observable.fromEvent($('canvas#circuit'), 'click');

var pointClick$ = clickEvent$.map(function (e) {
    return {
        'x': e.offsetX,
        'y': e.offsetY
    }
}).startWith({ 'x': 150 / 2, 'y': 20 });

var pointsChange$ = pointClick$.scan(function (acc, value, index) {
    acc[index] = [
        value.x, value.y
    ];
    return acc;
}, []);

pointsChange$.subscribe(function (value) {
    $('canvas.circuit-preview, canvas#preview').each((i, m) => {
        BotoGP.printer.drawPreview(m, value);
    });

    var previewCanvas = document.querySelector("canvas#preview");
    if(!previewCanvas) return;

    var pointsOfInterest = BotoGP.designer.pointsOfInterest(previewCanvas);
    var circuitId = $(previewCanvas).data("circuit-id");

    var checkPoints = $.map(value, (o, i) => {
        return { x: o[0], y: o[1] }
    });

    BotoGP.repo.change(circuitId,
        {
            "name": $('h1.circuit-name').text(),
            "checkPoints": checkPoints,
            "onTrack": pointsOfInterest["on"],
            "offTrack": pointsOfInterest["off"]
        });
});

pointsChange$.subscribe(function (value) {
    var point = value[value.length - 1];
    var canvas = document.querySelector("canvas#circuit");
    if(!canvas)
        return;
    var canvasContext = canvas.getContext("2d");
    canvasContext.lineTo(point[0], point[1]);
    canvasContext.lineWidth = 9 / scale;
    canvasContext.strokeStyle = "#ccc";
    canvasContext.stroke();

    canvasContext.moveTo(point[0], point[1]);

    canvasContext.beginPath()
    canvasContext.arc(point[0], point[1], 9 / scale, 0, 2 * Math.PI, false);
    canvasContext.fillStyle = '#ccc';
    canvasContext.fill();
    canvasContext.strokeStyle = "#ccc";
    canvasContext.stroke();

    canvasContext.moveTo(point[0], point[1]);
});


var nameChange$ = Rx.Observable.fromEvent($('h2 input.circuit-name'), 'keyup')
    .debounceTime(500)
    .distinctUntilChanged()
    .map(function (e) {
        return {
            "id": $(e.target).data("circuit-id"),
            "name": e.target.value
        }
    });

nameChange$.subscribe(function (d) {
    BotoGP.repo.changeName(d.id, d.name);
});



var scale = 4;

$(document).ready(function () {

    BotoGP.printer.renderPreviews();

    pointClick$.subscribe(function (p) {

        var canvas = document.querySelector("canvas#plotter");
        if(!canvas) return;
        var canvasContext = canvas.getContext("2d");

        var previewCanvas = document.querySelector("canvas#preview");
        if(!previewCanvas) return;

        canvasContext.clearRect(0, 0, canvas.width * scale, canvas.height * scale);
        canvasContext.translate(0.0, 0.0);

        var radius = 2;
        var pointsOfInterest = BotoGP.designer.pointsOfInterest(previewCanvas);
        canvasContext.fillStyle = '#cc0000';
        $.each(pointsOfInterest["off"], function (index, point) {
            canvasContext.beginPath();
            canvasContext.arc(point.x * scale, point.y * scale, radius, 0, 2 * Math.PI, false);
            canvasContext.fill();
        });

        canvasContext.fillStyle = '#00ff00';
        $.each(pointsOfInterest["on"], function (index, point) {
            canvasContext.beginPath();
            canvasContext.arc(point.x * scale, point.y * scale, radius, 0, 2 * Math.PI, false);
            canvasContext.fill();
        });

        $('#serialized').text(JSON.stringify(circuitModel));
    });

    Rx.Observable.fromEvent($('canvas.circuit-preview'), 'mousemove').subscribe(function (e) {

        var scale = e.target.width / BotoGP.DefaultWidth;
        
        var x = e.offsetX, y = e.offsetY;
        $('#circle1').attr('cy', y / scale);
        $('#circle1').attr('cx', x / scale);

        var inpath = e.target.getContext("2d").isPointInStroke(x, y);
        $('#circle1').attr('stroke', inpath ? '#00ff00' : '#ff0000');
    });
    
    Rx.Observable.fromEvent($('canvas.circuit-preview'), 'click').subscribe(function (e) {
        var scale = e.target.width / BotoGP.DefaultWidth;
        var x = Math.round( e.offsetX / scale), y = Math.round(e.offsetY / scale);
        $.get("/api/heatmaps/" + $(e.target).data("circuit-id") + "/tileinfo?x=" + x + "&y=" + y, function (d) {
            $("#hits").text(d);
        }
        );
    });
});

export default BotoGP;