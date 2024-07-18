window.renderChart = (canvasId, config) => {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas?.getContext('2d');

    if (!ctx) {
        console.error(`Canvas element with id '${canvasId}' not found.`);
        return;
    }

    // Check if a chart already exists for this canvas and destroy it
    if (canvas.chartInstance) {
        canvas.chartInstance.destroy();
    }

    // Create the new chart and save the instance to the canvas element
    canvas.chartInstance = new Chart(ctx, JSON.parse(config));
};
