var map;
var symbol;
var heatmapFeatureLayer;
var LBGraphicsLayer;
var highlightGraphic;
require([
  "esri/basemaps",
  "esri/map",
  "esri/dijit/HomeButton",

  "esri/dijit/BasemapGallery",
  "esri/dijit/Measurement",
  "esri/graphic",
  "esri/InfoTemplate",
  "esri/SpatialReference",
  "esri/geometry/Extent",
  "esri/layers/GraphicsLayer",
  "esri/layers/FeatureLayer",
  "esri/renderers/HeatmapRenderer",

  "esri/symbols/SimpleMarkerSymbol",
  "esri/symbols/SimpleLineSymbol",
  "esri/symbols/SimpleFillSymbol",
  "esri/symbols/PictureMarkerSymbol",
  "esri/tasks/query",
  "esri/tasks/QueryTask",
  "esri/tasks/FindTask",
  "esri/tasks/FindParameters",
  "esri/toolbars/navigation",

  "dojo/on",
  "dojo/parser",
  "dojo/dom",
  "dojo/_base/array",
  "dojo/_base/connect",
  "dojo/data/ItemFileReadStore",
  "dojox/grid/DataGrid",

  "esri/Color",

  "esri/dijit/OverviewMap",
  "esri/dijit/Scalebar",
  "esri/dijit/Bookmarks",
  "esri/dijit/Legend",
  "dijit/registry",
  "dijit/form/Button",
  "dijit/TitlePane",

  "dijit/layout/BorderContainer",
  "dijit/layout/ContentPane",
  "dojo/domReady!"
],
  function (
  esriBasemaps, Map, HomeButton, BasemapGallery, Measurement, Graphic, InfoTemplate, SpatialReference, Extent,
  GraphicsLayer, FeatureLayer, HeatmapRenderer, SimpleMarkerSymbol,
     SimpleLineSymbol, SimpleFillSymbol, PictureMarkerSymbol, Query, QueryTask, FindTask, FindParameters, navigation,
     on, parser, dom, arrayUtils, connect, ItemFileReadStore, DataGrid, Color, OverviewMap, Scalebar, Bookmarks, Legend, registry, Button,
     TitlePane
  ) {

      var divxy = dojo.byId("divxy");
      var button1 = dojo.byId("Button1");
      var button2 = dojo.byId("Button2");
      var button7 = dojo.byId("Button7");
      var button8 = dojo.byId("Button8");


      parser.parse();

      esriBasemaps.delorme = {
          baseMapLayers: [{ url: "http://cache1.arcgisonline.cn/arcgis/rest/services/ChinaOnlineCommunity/MapServer" }
          ],
          thumbnailUrl: "~/resource/image/shiliang.jpg",
          title: "矢量图"
      };


      map = new Map("mapDiv", {
          basemap: "delorme",

          sliderStyle: "small"
      });

      var home = new HomeButton({
          map: map
      }, "HomeButton");
      home.startup();


      var basemapGallery = new esri.dijit.BasemapGallery({
          showArcGISBasemaps: true,
          map: map
      }, "basemapGallery");

      var selectionHandler = dojo.connect(basemapGallery, "onSelectionChange", function () {
          dojo.disconnect(selectionHandler);
          //add the esri population layer to the map  
      });
      basemapGallery.startup();

      dojo.connect(basemapGallery, "onError", function (msg) { console.log(msg); });


      var mapCenter = new esri.geometry.Point(103.847, 36.0473, map.spatialReference);
      map.centerAndZoom(mapCenter, 4);

      //测量工具
      var measurement = new Measurement({
          map: map
      }, dom.byId("measurementDiv"));
      measurement.startup();


      var navToolbar = new esri.toolbars.Navigation(map);
      var scalebar = new Scalebar({ map: map });

      //------------地图坐标显示--------------//
      dojo.connect(map, "onMouseMove", function (e) {    //添加坐标位置信息
          var mp = e.mapPoint;     //地图坐标
          var sp = e.screenPoint;  //屏幕点
          divxy.innerHTML = "大地坐标X：" + mp.x + "&nbsp;&nbsp;" + "屏幕坐标X：" + sp.x + "<br>" + "大地坐标Y：" + mp.y + "&nbsp;&nbsp;" + "屏幕坐标Y：" + sp.y;

      });

      //-------------地图控件--------------//

      //-----------------------地图小控件------------------------//

      dojo.connect(button1, "click", function (evt)
      { navToolbar.activate(esri.toolbars.Navigation.ZOOM_IN); });

      dojo.connect(button2, "click", function (evt)
      { navToolbar.activate(esri.toolbars.Navigation.ZOOM_OUT); });

      dojo.connect(button7, "click", function (evt)
      { map.centerAndZoom(mapCenter, 4); });

      dojo.connect(button8, "click", function (evt)
      { navToolbar.activate(esri.toolbars.Navigation.PAN); });

      var overviewMapDijit = new OverviewMap({
          map: map,
          attachTo: "bottom-right",
          color: " #D84E13",
          opacity: 0.40,
          visible: false,
          maximizeButton: true,
          height: 150,
          width: 250
      });
      overviewMapDijit.startup();


  });


//添加老兵轨迹
$(function () {
    var source = [];

    function resetTabullet() {
        $("#table").tabullet({
            data: source,
            action: function (mode, data) {
                console.dir(mode);
                if (mode === 'save') {
                    source.push(data);
                }
                if (mode === 'edit') {
                    for (var i = 0; i < source.length; i++) {
                        if (source[i].id == data.id) {
                            source[i] = data;
                        }
                    }
                }
                if (mode == 'delete') {
                    for (var i = 0; i < source.length; i++) {
                        if (source[i].id == data) {
                            source.splice(i, 1);
                            break;
                        }
                    }
                }
                resetTabullet();
            }
        });
    }

    resetTabullet();
});

/*登陆注册*/
$(function () {
    $(".btn").click(function () {
        $("#mymodal").modal();
    });
});

$(document).ready(function () {
    $("#username").focus();
    //记住用户名和密码
    $('#remember').click(function () {
        if ($("#username").val() == "") {
            alert("用户名不能为空！");
        }
        if ($("#password").val() == "") {
            alert("密码不能为空！");
        }
        else {
            if ($('#remember')[0].checked) {//------修改-------------
                setCookie("uname", $("#username").val(), 60);
                setCookie("upwd", $("#password").val(), 60);
            }
            else {
                delCookie("uname");
                delCookie("upwd");
            }
        }
    });
    if (getCookie("uname") != null) {
        $('#remember').attr("checked", "checked");
        $('#username').val(getCookie("uname"));
        $('#password').val(getCookie("upwd"));
    }
})
//写cookies
function setCookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
//读取cookies 
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg)) return unescape(arr[2]);
    else return null;
}
//删除cookies 
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
/*登陆注册结束*/