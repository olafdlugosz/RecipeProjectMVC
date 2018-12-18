
createLabels = async () => {

    let labelList = [];
    let response = await fetch(`api/TopProtein`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        labelList.push(data[i].label);
    }
    return labelList;
}
createData = async () => {
    let dataList = [];
    let response = await fetch(`api/TopProtein`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        dataList.push(data[i].total);
    }
    return dataList;
}
let labelList = [];
let dataList = [];
createLabels().then(res => {
    labelList = res
    createData().then(baz => {
        dataList = baz;
        displayChart();
    });
});
displayChart = () => {
    new Chart(document.getElementById("topProtein"), {
        "type": "horizontalBar",
        "data": {
            "labels": labelList,
            "datasets": [{
                "label": "Highest Protein",
                "data": dataList,
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
