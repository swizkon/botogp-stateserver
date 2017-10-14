import $ from "jquery"

let defaultWidth = 150;

let RenderEngine = {

    renderPreviews: function () {
        $('canvas.circuit-preview').each((i, m) => {
            var points = $(m).data('checkpoints');
            RenderEngine.drawPreview(m, points);
        });
    },
    drawPreview: function (canvas, points) {
        var scale = canvas.width / defaultWidth;
        var scaledPoints = $.map(points, (o, i) => {
            return {
                x: o.x * scale,
                y: o.y * scale
            }
        });
        
        var context = canvas.getContext("2d");
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.translate(0.0, 0.0);

        RenderEngine.drawPath(context, 15 * scale, "#ddd", scaledPoints);
        RenderEngine.drawPath(context, 20 * scale, "rgba(200,200,200,0.5)", scaledPoints);
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

export default RenderEngine;