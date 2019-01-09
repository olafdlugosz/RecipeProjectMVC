createLabelsForHPLCLineChart = async () => {

    let labelList = [];
    let response = await fetch(`GetHighProteinLowCarb`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        labelList.push(data[i].label);
    }
    console.log("this is LabelsHPLC", labelList);
    return labelList;
};
createProteinData = async () => {

    let proteinList = [];
    let response = await fetch(`GetHighProteinLowCarb`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        proteinList.push(data[i].protein);
    }
    console.log("this is proteinData", proteinList);
    return proteinList;
};
createHPLCarbData = async () => {
    let dataList = [];
    let response = await fetch(`GetHighProteinLowCarb`);
    let data = await response.json();
    for (var i = 0; i < data.length; i++) {
        dataList.push(data[i].carb);
    }
    console.log("this is HPLcarbData", dataList);
    return dataList;
};
let labelListForHPLCLineChart = [];
let proteinListForHPLCLineChart = [];
let carbListForHPLCLineChart = [];

createLabelsForHPLCLineChart().then(res => {
    labelListForHPLCLineChart = res;
    createProteinData().then(baz => {
        proteinListForHPLCLineChart = baz;
        createHPLCarbData().then(res => {
            carbListForHPLCLineChart = res;
            displayHighProteinLowCarbLineChart();
        });
    });
});






displayHighProteinLowCarbLineChart = () => {
    var ctxL = document.getElementById("HPLClineChart").getContext('2d');
    var myLineChart = new Chart(ctxL, {
        type: 'line',
        data: {
            labels: labelListForHPLCLineChart,
            datasets: [{
                label: "Protein",
                data: proteinListForHPLCLineChart,
                backgroundColor: [
                    'rgba(100, 0, 50, .3)',
                ],
                borderColor: [
                    'rgba(0, 0, 255, .7)',
                ],
                borderWidth: 2
            },
            {
                label: "Carbs",
                data: carbListForHPLCLineChart,
                backgroundColor: [
                    'rgba(0, 0, 255, .3)',
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