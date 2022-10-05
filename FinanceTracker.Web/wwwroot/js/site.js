// FOR GOOGLE CHARTS

function drawTransactionStackedBarChart(chartData) {
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
        .getElementById('pieChart_transaction'));

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

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_status'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function drawAccountStackedBarChart(chartData) {
    var data = new google.visualization.DataTable();
    var dataArray = [];

    data.addColumn('string', 'Type');

    $.each(chartData, function (i, obj) {
        data.addColumn('number', obj.name);
        dataArray.push(obj.count);
    });

    data.addRows([
        ["Type Summary", ...dataArray]
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
        .getElementById('stackedBarChart_account'));

    barChart.draw(data, StackedBarChartOptions);
}

function drawAccountProviderPieChart(chartData) {
    console.log(chartData);
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');
    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, obj.amount]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Balance By Account",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" },
    };

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_account__name'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function drawAccountTypePieChart(chartData) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Amount');
    var dataArray = [];

    $.each(chartData, function (i, obj) {
        dataArray.push([obj.name, obj.amount]);
    });
    data.addRows(dataArray);

    var ProviderPieChartOptions = {
        title: "Balance By Type",
        backgroundColor: {
            fill: "#d7d8da"
        },
        bar: { groupWidth: "20%" },
    };

    var pieChart = new google.visualization.PieChart(document
        .getElementById('pieChart_account__type'));

    pieChart.draw(data, ProviderPieChartOptions);
}

function callGoogleChart(response, chartType) {
    if (response.status === 1) {
        google.charts.load("current", {
            packages: ["corechart"],
        });
        google.charts.setOnLoadCallback(function () {
            switch (chartType) {
                case "provider":
                    drawProviderPieChart(response.dataEnum);
                    break;
                case "status":
                    drawStatusPieChart(response.dataEnum);
                    break;
                case "statusCount":
                    drawTransactionStackedBarChart(response.dataEnum);
                    break;
                case "accountCount":
                    drawAccountStackedBarChart(response.dataEnum);
                    break;
                case "accountProviderCost":
                    drawAccountProviderPieChart(response.dataEnum);
                    break;
                case "accountTypeCost":
                    drawAccountTypePieChart(response.dataEnum);
                    break;
            }
        });
    } else {
        $.notify(response.message, "error");
    }
}