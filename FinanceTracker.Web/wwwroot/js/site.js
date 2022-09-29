// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// FOR GOOGLE CHARTS

function drawProviderPieChart(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');
    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, obj.transactionCost]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Transaction Cost By Provider",
        backgroundColor: {
            fill: "#d7d8da",
            stroke: "#111",
            strokeWidth: 1
        },
        width: 500,
        height: 600,
        bar: { groupWidth: "20%" },
    };

    var pieChart1 = new google.visualization.PieChart(document
        .getElementById('columnchart_div1'));

    pieChart1.draw(data, ProviderPieChartOptions);
}

function drawStatusPieChart(chartData) {
    console.log(chartData);

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Status');
    data.addColumn('number', 'Amount');
    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, obj.transactionCost]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Transaction Cost By Status",
        backgroundColor: {
            fill: "#d7d8da",
            stroke: "#111",
            strokeWidth: 1
        },
        width: 500,
        height: 600,
        bar: { groupWidth: "20%" },
    };

    var pieChart1 = new google.visualization.PieChart(document
        .getElementById('columnchart_div2'));

    pieChart1.draw(data, ProviderPieChartOptions);
}