function getCityLBInfo() {
    $.ajax(
    {
        type: "post",
        url: "/statistics/readcityLB",
        dataType: "json",
        success: function (result) {
            cityLBlist = result;
            getCityinfo();
        }
    });
}
function getCityinfo()
{
    $.ajax(
       {
           type: "post",
           url: "/statistics/readcityInfo",
           dataType: "json",
           success: function (result) {
               cityinfolist = result;
               //-------------------开始进行数据的处理---------------
               data = [];
               geoCoordMap = {};
               //----------------数据展示--------------------------
                 //------先开始进行数据的统计------------
               var lblist = [];//老兵列表信息列表{citycode:,value}
               for (var i = 0; i < cityLBlist.length; i++) {
                   var cityLblisttemp = cityLBlist[i];
                   var isexist = false;

                   //首先检索有没有对应的信息，若是找到就直接记录加一，若是没有找到，就需要重新添加
                   for (var j = 0; j < lblist.length; j++)
                   {
                       var lblisttemp=lblist[j];
                       if (lblisttemp.citycode == cityLblisttemp.citycode)
                       {
                           //表示找到对应的信息
                           lblisttemp[j].value = lblisttemp[j].value + 1;
                           isexist = true;
                           break;
                       }
                   }
                   //开始进行对应对应的添加,没有添加，需要从新添加
                   if (!isexist)
                   {

                   }


               }


               function find(citycode)
               {
                   for (var i = 0; i < cityinfolist.length; i++) {
                       if (cityinfolist[i].citycode == citycode) {
                           return cityinfolist[i];
                       }
                   }
               }
                 

                   //-----------------------------------------------------
              loadingmyChart();
           }
       });
}
function loadingmyChart() {

    myChart.hideLoading();
    var app = {};
    var option = null;
    var convertData = function (data) {
        var res = [];
        for (var i = 0; i < data.length; i++) {
            var datatemp = data[i];
            var geoCoord = geoCoordMap[datatemp.name];
            if (geoCoord) {
                res.push({
                    name: data[i].name,
                    value: geoCoord.concat(data[i].value)
                });
            }
        }
        return res;
    };

    option = {
        backgroundColor: '#404a59',
        title: {
            text: '全国主要城市老兵分布',
            subtext: 'data from ilaobing',
            sublink: 'http://www.pm25.in',
            left: 'center',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {
            trigger: 'item'
        },
        legend: {
            orient: 'vertical',
            y: 'bottom',
            x: 'right',
            data: ['老兵人数'],
            textStyle: {
                color: '#fff'
            }
        },
        geo: {
            map: 'china',
            label: {
                emphasis: {
                    show: false
                }
            },
            roam: true,
            itemStyle: {
                normal: {
                    areaColor: '#323c48',
                    borderColor: '#111'
                },
                emphasis: {
                    areaColor: '#2a333d'
                }
            }
        },
        series: [
            {
                name: '老兵人数',
                type: 'scatter',
                coordinateSystem: 'geo',
                data: convertData(data),
                symbolSize: function (val) {
                    return val[2] / 10;
                },
                label: {
                    normal: {
                        formatter: '{b}',
                        position: 'right',
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#f4e925',
                        shadowBlur: 10,
                        shadowColor: '#333'
                    }
                }
            },
            {
                name: '老兵分布城市',
                type: 'effectScatter',
                coordinateSystem: 'geo',
                data: convertData(data),
                symbolSize: function (val) {
                    return val[2];
                },
                showEffectOn: 'render',
                rippleEffect: {
                    brushType: 'stroke'
                },
                hoverAnimation: true,
                label: {
                    normal: {
                        formatter: '{b}',
                        position: 'right',
                        show: true
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#f4e925',
                        shadowBlur: 10,
                        shadowColor: '#333'
                    }
                },
                zlevel: 1
            }
        ]
    };
    ;
    if (option && typeof option === "object") {
        myChart.setOption(option, true);
    }
}
function loadedChart()
{
    getCityLBInfo();
}