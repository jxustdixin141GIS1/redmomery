function distributioncity() {
    var dom = document.getElementById("container");
    var myChart = echarts.init(dom);
    var app = {};
    option = null;

    var data = [];
    var geoCoordMap = {};
    //下面开始进行生成
    for (var i in staticinfo)
    {       
        var temp = staticinfo[i]; //temp.Key  CityCode,temp.value  统计人数;
        var cityobject=getcitybycitycode(temp.Key);//cityobject :  城市对象
        var datatemp = {};
        if (i == 96)
        {
            console.log(i);
        }
        datatemp["name"] = cityobject.cityname;
        datatemp["value"] = temp.value;
        data.push(datatemp);
        //开始针对地理进行展示
        geoCoordMap[cityobject.cityname]=cityobject.coordination;
    }

    //结束生成
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
            subtext: 'data from PM25.in',
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
            data: ['pm2.5'],
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
                name: 'pm2.5',
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
                        color: '#ddb926'
                    }
                }
            },
            {
                name: 'Top 5',
                type: 'effectScatter',
                coordinateSystem: 'geo',
                data: convertData(data.sort(function (a, b) {
                    return b.value - a.value;
                }).slice(0, 6)),
                symbolSize: function (val) {
                    return val[2] / 10;
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
var data = null;
var geoCoordMap = null;
var isdealwithok = false;//这个表示前一个运算还没有结束
//------------------------历年中日兵力比---------------------------------------
  


 //---------------------老兵省份统计-------------------------------------------
function getcitylblist() {
        $.ajax({
      url: "/Statistics/GetCityLb",
      type: "post",
      dataType: "json",
      success: function (result) {
              citylblist = result;
              getprovinceinfos();
              //对于数据进行处理
              data = [];
              //下面开始进行数据处理，
              //先开始对于citylblist数据进行汇总处理，注意这里的temp name 用citycode代替，value表示统计
              for (var i = 0; i < citylblist.length; i++) {
                      var temp = citylblist[i];//取到临时代码
                      //如果已经在数组内部就在value的时进行加1
                      var isexsit = false;
                      for (var j = 0; j < data.length; j++) {
                              if (data[j].name == temp.CityCode) {
                                      data[j].value = data[j].value + 1;
                                      isexsit = true;
                                  }
                          }
                      if (!isexsit) {
                              var datatemp = {};
                              datatemp["name"] = temp.CityCode;
                              datatemp["value"] = 1;
                              //这里需要对于不能识别的数据进行处理
                              data.push(datatemp);
                          }
                  }
              isdealwithok = true;
          }
    });
}
function getprovinceinfos() {
     $.ajax({
      url: "/Statistics/GetCityinfo",
      type: "post",
      dataType: "json",
      success: function (result) {
              cityinfolist = result;
              geoCoordMap = {};
              var myDate = new Date();
              //用来提示当前页面的错误问题
              while (!isdealwithok) {
                      var temp = new Date();
                      if (Math.abs(temp.getMinutes() - myDate.getMinutes()) > 1) {
                              alert("出现了错误");
                              break;
                          }
                  };
              //开始按照，citylb进行数据展示
              var citylbinfo = data;//首先将name中的citycode转换成对应的省份，最后在进行汇总
              for (var i = 0; i < citylbinfo.length; i++) {
                      var temp = citylbinfo[i];
                      for (var j = 0; j < cityinfolist.length; j++) {
                              if (cityinfolist[j].citycode == temp.name)
                              {
                                      //这里需要针对数据进行处理
                                      citylbinfo[i].name = cityinfolist[j].province.replace("省", "").replace("市", "").replace("自治区", "").trim();;
                              }
                      }
              }
            //开始进行数据的统计
          var provincedata = [];
          for (var i = 0; i < citylbinfo.length; i++) {
              var temp = citylbinfo[i];
              var isexist = false;
              for (var j = 0; j < provincedata.length; j++)
              {
                      if (provincedata[j].name == temp.name)
                      {
                              provincedata[j].value = provincedata[j].value + temp.value;
                      isexist = true;
                  }
           }
        if (!isexist)
        {
                //表示没有找到
                var protemp = {};
            protemp["name"] = temp.name;
            protemp["value"] = temp.value;
            provincedata.push(protemp);
        }
    }
    data = provincedata;
    //数据展示
    myChart.hideLoading();
    loadingprovince();
        }
    });
}
function loadingprovince()
    {
        function findmax(data)
            {
              var max=data[0].value;
          for (var i = 0; i < data.length; i++) {
                  if(data[i].value>=max)
                  {
                          max=data[i].value;
                  }
          }
      return max;
    }
  function findmin(data)
    {
      var min=data[0].value;
        for (var i = 0; i < data.length; i++) {
          if(data[i].value <= min)
          {
                  min=data[i].value;
          }
        }
      return min;
  }
  option = {
        title: {
          text: '老兵省份分布',
          subtext: '数据来源于我爱老兵网',
          sublink: 'http://www.ilaobing.com/forum.php',
          left: 'center',
          top: 'top'
      },
      tooltip: {
        trigger: 'item',
        formatter: function (params) {
              var value = (params.value + '').split('.');
              value = value[0].replace(/(\d{1,3})(?=(?:\d{3})+(?!\d))/g, '$1,')
                   '.' + value[1];
              return params.seriesName + '<br/>' + params.name + ' : ' + value;
          }
        },
      toolbox: {
        show: true,
        orient: 'vertical',
        left: 'right',
        top: 'center',
        feature: {
              mark: { show: true },
              dataView: { show: true, readOnly: false },
              restore: { show: true },
              saveAsImage: { show: true }
          }
        },
      visualMap: {
        type: 'continuous',
        min: findmin(data),
        max: findmax(data),
        text: ['High', 'Low'],
        realtime: false,
        calculable: true,
        color: ['orangered', 'yellow', 'lightskyblue']
        },
      series: [
    {
            name: '老兵省份分布',
            type: 'map',
            mapType: 'china',
            roam: true,
            itemStyle: {
                  emphasis: { label: { show: true } }
              },
        data: data
    }
      ]
  };
  if (option && typeof option === "object") {
        myChart.setOption(option, true);
    }
}
 //-----------------老兵性别统计-------------------------------------------------
function GetLbsexlist()
  {
  if (option && typeof option === "object") {
        myChart.setOption(option, true);
    }
}
//------------------老兵城市分布方法集合------------------------------------------

