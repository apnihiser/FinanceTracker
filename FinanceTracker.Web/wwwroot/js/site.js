'use strict';

// GOOGLE CHART DATA
function drawSummaryByProvider(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');

    var dataArray = [];
    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, Math.abs(obj.amount)]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Transaction Summary By Provider",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" },
    };

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_transaction'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function drawSummaryByStatus(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');

    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, Math.abs(obj.amount)]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Transaction Summary By Status",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" }
    };

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_status'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function drawTransactionStatusCount(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Status');

    var dataArray = [];

    $.each(chartData, function (i, obj) {
        data.addColumn('number', obj.name);
        dataArray.push(obj.count);
    });

    data.addRows([
        ["Status Summary", ...dataArray]
    ]);

    var StackedBarChartOptions = {
        height: 100,
        legend: { position: "none" },
        bar: { groupWidth: '75%' },
        isStacked: 'percent',
        backgroundColor: {
            fill: "#d7d8da"
        }
    };

    var barChart = new google.visualization.BarChart(document
        .getElementById('stackedBarChart_transaction'));

    barChart.draw(data, StackedBarChartOptions);
}

function drawSummaryByType(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Count');

    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, obj.count]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Account Summary By Type",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" }
    };

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_account__type'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function drawSummaryByAccount(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');

    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, Math.abs(obj.amount)]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Account Summary By Provider",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" }
    };

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_account__name'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function drawAccountCount(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Status');

    var dataArray = [];

    $.each(chartData, function (i, obj) {
        data.addColumn('number', obj.name);
        dataArray.push(obj.amount);
    });

    data.addRows([
        ["Type Summary", ...dataArray]
    ]);

    var accStackedBarChartOptions = {
        height: 100,
        legend: { position: "none" },
        isStacked: 'percent',
        backgroundColor: {
            fill: "#d7d8da"
        }
    };

    var barChart = new google.visualization.BarChart(document
        .getElementById('stackedBarChart_account'));

    barChart.draw(data, accStackedBarChartOptions);
}