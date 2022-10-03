﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// FOR GOOGLE CHARTS

function drawProviderPieChart(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');
    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, obj.amount]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Transaction Cost By Provider",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" },
    };

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_Transaction'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function drawStatusPieChart(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');
    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, obj.amount]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Transaction Cost By Status",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" }
    };

    var pieChart1 = new google.visualization.PieChart(document
        .getElementById('pieChart_Status'));

    pieChart1.draw(data, ProviderPieChartOptions);
}

function drawStackedBarChart(chartData) {
    var data = new google.visualization.DataTable();
    var dataArray = [];

    data.addColumn('string', 'Status');

    $.each(chartData, function (i, obj) {
        data.addColumn('number', obj.name);
        dataArray.push(obj.count);
    });

    data.addRows([
        ["Status Summary", ...dataArray]
    ]);

    var StackedBarChartOptions = {
        height: 100,
        legend: {position: "none"},
        bar: { groupWidth: '75%' },
        isStacked: 'percent',
        backgroundColor: {
            fill: "#d7d8da"
        }
    };

    var pieChart1 = new google.visualization.BarChart(document
        .getElementById('stackedBarChart_Transaction'));

    pieChart1.draw(data, StackedBarChartOptions);
}