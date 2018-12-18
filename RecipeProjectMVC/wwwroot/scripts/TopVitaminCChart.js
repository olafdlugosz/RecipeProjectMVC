createLabelsVit = async () => {

    let labelList = [];
    let response = await fetch(`api/TopVitaminC`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        labelList.push(data[i].label);
    }
    return labelList;
};
createDataVit = async () => {
    let dataList = [];
    let response = await fetch(`api/TopVitaminC`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        dataList.push(data[i].total);
    }
    return dataList;
};
let labelListVit = [];
let dataListVit = [];
createLabelsVit().then(res => {
    labelListVit = res
    createDataVit().then(baz => {
        dataListVit = baz;
        displayChartVit();
    });
});
displayChartVit = () => {
    new Chart(document.getElementById("topVitaminCChart"), {
        "type": "horizontalBar",
        "data": {
            "labels": labelListVit,
            "datasets": [{
                "label": "Highest in Vitamin C",
                "data": dataListVit,
                "fill": false,
                "backgroundColor": ["rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)",
                    "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(54, 162, 235, 0.2)",
                    "rgba(153, 102, 255, 0.2)", "rgba(201, 203, 207, 0.2)"
                ],
                "borderColor": ["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)",
                    "rgb(75, 192, 192)", "rgb(54, 162, 235)", "rgb(153, 102, 255)", "rgb(201, 203, 207)"
                ],
                "borderWidth": 1
            }]
        },
        "options": {
            "scales": {
                "xAxes": [{
                    "ticks": {
                        "beginAtZero": true
                    }
                }]

            }
        }
    });
};
