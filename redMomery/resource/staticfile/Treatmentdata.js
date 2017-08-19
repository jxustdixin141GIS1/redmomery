var citylblist = null;//老兵的分布数据
var cityinfolist = null;//城市信息
var myChart = null;//这个必须声明
var data = null;
var geoCoordMap = null;
var isdealwithok = false;//这个表示前一个运算还没有结束

//-----------------老兵性别统计-------------------------------------------------
function GetLbsexlist()
{
    $.ajax({
        url: "/Statistics/GetLbsexstatstic",
        type: "post",
        dataType: "json",
        success: function (result) {
            data = result;
            //进行数据处理
            var temp = [];
            for (var i = 0; i < data.length; i++) {
                var datatemp = {};
                datatemp["name"] = data[i].Key;
                datatemp["value"] = data[i].Value;
                if(data[i])

                temp.push(datatemp);
            }
            data = temp;
            myChart.hideLoading();
            //下面开始进行统计的展示计算，这里单独变成一个方法
            loadedLbsexList();
        }
    });
}
function loadedLbsexList()
{
    var app = {};
    var option = null;
        option = {
            title : {
                text: '老兵性别比',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: 'left',
                data: ['男','女']
            },
            series : [
                {
                    name: '老兵性别',
                    type: 'pie',
                    radius : '55%',
                    center: ['50%', '60%'],
                    data:data,
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };

    app.currentIndex = -1;

    setInterval(function () {
        var dataLen = option.series[0].data.length;
        // 取消之前高亮的图形
        myChart.dispatchAction({
            type: 'downplay',
            seriesIndex: 0,
            dataIndex: app.currentIndex
        });
        app.currentIndex = (app.currentIndex + 1) % dataLen;
        // 高亮当前图形
        myChart.dispatchAction({
            type: 'highlight',
            seriesIndex: 0,
            dataIndex: app.currentIndex
        });
        // 显示 tooltip
        myChart.dispatchAction({
            type: 'showTip',
            seriesIndex: 0,
            dataIndex: app.currentIndex
        });
    }, 1000);

    myChart.setOption(option);
}
//------------------老兵城市分布方法集合------------------------------------------
function loadedmyChart()
{
    var app = {};
    option = null;
    myChart.hideLoading();
    var convertData = function (data) {
        var res = [];
        for (var i = 0; i < data.length; i++) {
            var geoCoord = geoCoordMap[data[i].name];
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
                name: 'Top 10',
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
    if (option && typeof option === "object") {
        myChart.setOption(option, true);
    }
}
function getcitylbs()
{
    $.ajax({
        url: "/Statistics/GetCityLb",
        type:"post",
        dataType:"json",
        success: function (result)
        {
            citylblist = result;
            getcityinfos();
            //对于数据进行处理
            data = [];
            //下面开始进行数据处理，
            //先开始对于citylblist数据进行汇总处理，注意这里的temp name 用citycode代替，value表示统计
            for (var i = 0; i < citylblist.length; i++) {
                var temp = citylblist[i];//取到临时代码
                //如果已经在数组内部就在value的时进行加1
                var isexsit = false;
                for (var j = 0; j < data.length; j++) {
                    if (data[j].name == temp.CityCode)
                    {
                        data[j].value = data[j].value + 1;
                        isexsit = true;
                    }
                }
                if (!isexsit)
                {
                    var datatemp = {};
                    datatemp["name"] = temp.CityCode;
                    datatemp["value"] = 1;
                    data.push(datatemp);
                }
            }
            isdealwithok = true;
        }
    });
}
function getcityinfos()
{
    $.ajax({
        url: "/Statistics/GetCityinfo",
        type: "post",
        dataType: "json",
        success: function (result) {
            cityinfolist = result;
            geoCoordMap = {};
            var myDate = new Date();
            while (!isdealwithok) {
                var temp = new Date();
                if (Math.abs(temp.getMinutes() - myDate.getMinutes()) > 1)
                {
                    alert("出现了错误");
                    break;
                }
            };
            //开始按照，citylb进行数据展示
            for (var i = 0; i < data.length; i++) {
                var temp = data[i];
                //开始查找对应的代码
                for (var j = 0; j < cityinfolist.length; j++) {
                    var cityinfotemp = cityinfolist[j];
                    if (cityinfotemp.citycode == temp.name)
                    { //表示找到相同的代码文件
                        data[i].name = cityinfotemp.cityname;
                        //同时将对应的城市坐标压入数组中
                        var coodirationtemp = [];
                        coodirationtemp.push(parseFloat(cityinfotemp.lng));
                        coodirationtemp.push(parseFloat(cityinfotemp.lat));
                        //将数据压入json数据中
                        geoCoordMap[data[i].name] = coodirationtemp;
                    }
                }
            }
            //数据展示
            loadedmyChart();
        }
    });
}