createDataForPieChart = async () => {
    var id = $('#routeDataId').val();
    console.log("this is id: ", id);
    let response = await fetch(`GetDetails/${id}`);
    let dataList = [];
    let data = await response.json();
    for (var i = 0; i < data.nutritioninfo.length; i++) {
        if (data.nutritioninfo[i].label === "Protein") {
            dataList[0] = data.nutritioninfo[i].total;
        }
        if (data.nutritioninfo[i].label === "Carbs") {
            dataList[1] = data.nutritioninfo[i].total;
        }
        if (data.nutritioninfo[i].label === "Fat") {
            dataList[2] = data.nutritioninfo[i].total;
        }
    }
    console.log("this is piechartDataList: ", dataList);
    return dataList;
};
dataListForPieChart = [];
createDataForPieChart().then(res => {
    dataListForPieChart = res;
    displayPieChart();
});


displayPieChart = () => {
    //pie
    var ctxP = document.getElementById("pieChart").getContext('2d');
    var myPieChart = new Chart(ctxP, {
        type: 'pie',
        data: {
            labels: ["Protein","Carbs","Fat"],
            datasets: [{
                data: dataListForPieChart,
                backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
                hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
            }]
        },
        options: {
            responsive: true
        }
    });
}