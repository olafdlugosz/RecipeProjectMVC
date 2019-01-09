createLabelsForLCHFLineChart = async () => {

    let labelList = [];
    let response = await fetch(`GetLCHF`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        labelList.push(data[i].label);
    }
    console.log("this is LabelsData", labelList);
    return labelList;
};
createFatData = async () => {

    let fatList = [];
    let response = await fetch(`GetLCHF`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        fatList.push(data[i].fat);
    }
    console.log("this is fatData", fatList);
    return fatList;
};
createCarbData = async () => {
    let dataList = [];
    let response = await fetch(`GetLCHF`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        dataList.push(data[i].carb);
    }
    console.log("this is carbData", dataList);
    return dataList;
};
let labelListForLCHFLineChart = [];
let fatListForLineChart = [];
let carbListForLineChart = [];

createLabelsForLCHFLineChart().then(res => {
    labelListForLCHFLineChart = res;
    createFatData().then(baz => {
        fatListForLineChart = baz;
        createCarbData().then(res => {
            carbListForLineChart = res;
            displayLCHFLineChart();
        });
    });
});




displayLCHFLineChart = () => {
    var ctxL = document.getElementById("LCHFlineChart").getContext('2d');
    var myLineChart = new Chart(ctxL, {
        type: 'line',
        data: {
            labels: labelListForLCHFLineChart,
            datasets: [{
                label: "Fat",
                data: fatListForLineChart,
                backgroundColor: [
                    'rgba(105, 0, 132, .2)',
                ],
                borderColor: [
                    'rgba(200, 99, 132, .7)',
                ],
                borderWidth: 2
            },
            {
                label: "Carbs",
                data: carbListForLineChart,
                backgroundColor: [
                    'rgba(0, 137, 132, .3)',
                ],
                borderColor: [
                    'rgba(0, 10, 130, .7)',
                ],
                borderWidth: 2
            }
            ]
        },
        options: {
            responsive: true
        }
    });
}