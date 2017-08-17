<<<<<<< HEAD
﻿
var map;
var symbol;
var legend;
require([
  "dojo/parser",
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
     
  "esri/toolbars/navigation",

  "dojo/on",
  
  "dojo/dom",
  "dojo/_base/array",
  "dojo/_base/connect",
  "dojo/data/ItemFileReadStore",
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
  parser, esriBasemaps, Map, HomeButton, BasemapGallery, Measurement, Graphic,
  InfoTemplate, SpatialReference, Extent,
  GraphicsLayer, navigation,
     on,  dom, arrayUtils, connect,
     ItemFileReadStore, Color, OverviewMap,
     Scalebar, Bookmarks, Legend, registry, Button,
     TitlePane
  ) {

      var divxy = dojo.byId("divxy");
      var button1 = dojo.byId("Button1");
      var button2 = dojo.byId("Button2");    
      var button7 = dojo.byId("Button7");
      var button8 = dojo.byId("Button8");
      var button9 = dojo.byId("Button9");

      //------
      var bt_zhitu = dojo.byId("bt_zhitu");




      parser.parse();
      esriBasemaps.delorme = {
          baseMapLayers: [{ url: "http://cache1.arcgisonline.cn/arcgis/rest/services/ChinaOnlineCommunity/MapServer" }
          ],
          thumbnailUrl: "~resource/image/shiliang.jpg",
          title: "矢量图"
      };
      map = new Map("map", {
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

     // map.on("load");

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

      dojo.connect(button10, "click", function (evt) {
          var Books = new esri.dijit.Bookmarks({      //添加书签
              map: map,
              editable: "true"
          }, dojo.byId("bookmarks"));
      });
      //--------------------------------------------------------------------------------------------

      //地图图例
       legend = new Legend({
          map: map
      }, "legendDiv");
       legend.startup();
      
  });


var editorCtl = true;
function Start_editor() {

    legend.destroy();

    
    require([
    
       "esri/tasks/GeometryService",

  
       "esri/layers/FeatureLayer",

       "esri/Color",
       "esri/symbols/SimpleMarkerSymbol",
       "esri/symbols/SimpleLineSymbol",

       "esri/dijit/editing/Editor",
       "esri/dijit/editing/TemplatePicker",

       "esri/config",
       "dojo/i18n!esri/nls/jsapi",

       "dojo/_base/array", "dojo/parser", "dojo/keys",

       "dijit/layout/BorderContainer", "dijit/layout/ContentPane",
       "dojo/domReady!"
    ], function (
       GeometryService,
        FeatureLayer,
       Color, SimpleMarkerSymbol, SimpleLineSymbol,
       Editor, TemplatePicker,
       esriConfig, jsapiBundle,
       arrayUtils, parser, keys
     ) {

        if (editorCtl)
        { 



        parser.parse();

        // snapping is enabled for this sample - change the tooltip to reflect this
        jsapiBundle.toolbars.draw.start = jsapiBundle.toolbars.draw.start + "<br>Press <b>ALT</b> to enable snapping";

        // 更多信息请参阅“使用代理页面”:  https://developers.arcgis.com/javascript/3/jshelp/ags_proxy.html
        esriConfig.defaults.io.proxyUrl = "/proxy/";

        //此服务仅用于开发和测试。我们建议您在应用程序中创建自己的几何服务。
        esriConfig.defaults.geometryService = new GeometryService("https://utility.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");

       

        map.on("layers-add-result", initEditor);

        var baseUrl = "http://172.16.100.178:6080/arcgis/rest/services/Contest/Legend/FeatureServer/";
        var Point_legend = new FeatureLayer(baseUrl + "0", {
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ['*']
        });
        var Line_legend = new FeatureLayer(baseUrl + "1", {
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ['*']
        });
        var Polygon_legend = new FeatureLayer(baseUrl + "2", {
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ['*']
        });

        map.addLayers([Point_legend, Line_legend, Polygon_legend]);


        function initEditor(evt) {
            var templateLayers = arrayUtils.map(evt.layers, function (result) {
                return result.layer;
            });
            var templatePicker = new TemplatePicker({
                featureLayers: templateLayers,
                grouping: true,
                rows: "auto",
                columns: 2,
                useLegend:false
            }, "templateDiv");
            templatePicker.startup();

            var layers = arrayUtils.map(evt.layers, function (result) {
                return { featureLayer: result.layer };
            });
            var settings = {
                map: map,
                enableUndoRedo: true,
                templatePicker: templatePicker,
                layerInfos: layers,
                toolbarVisible: true,
                createOptions: {
                    polylineDrawTools: [
                           Editor.CREATE_TOOL_FREEHAND_POLYLINE,
                           Editor.CREATE_TOOL_ARROW,
                           Editor.CREATE_TOOL_AUTOCOMPLETE,

                    ],
                    polygonDrawTools: [
                     Editor.CREATE_TOOL_FREEHAND_POLYGON,
                     Editor.CREATE_TOOL_AUTOCOMPLETE,
                     Editor.CREATE_TOOL_RECTANGLE,
                     Editor.CREATE_TOOL_TRIANGLE,
                     Editor.CREATE_TOOL_CIRCLE,
                     Editor.CREATE_TOOL_ELLIPSE
                    ]
                },
                toolbarOptions: {
                    reshapeVisible: true,
                    cutVisible: true,
                    mergeVisible: true
                }
            };

            var params = { settings: settings };
            var myEditor = new Editor(params, 'editorDiv');
            //定义捕捉选项
            var symbol = new SimpleMarkerSymbol(
              SimpleMarkerSymbol.STYLE_CROSS,
              15,
              new SimpleLineSymbol(
                SimpleLineSymbol.STYLE_SOLID,
                new Color([255, 0, 0, 0.5]),
                5
              ),
              null
            );
           
            map.enableSnapping({
                snapPointSymbol: symbol,
                tolerance: 20,
                snapKey: keys.ALT
            });

            myEditor.startup();
        }
        editorCtl = false;
        }
        else {
            myEditor.destroy();
            editorCtl = true;
        }

    });


    var divss1 = $("#insertEditor")[0];
    var addiv1 = "<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"region:'left'\" style=\"width: auto;overflow:hidden;\"><div id=\"templateDiv\"></div></div>";
    divss1.innerHTML = addiv1;

}
function dayin1() {
    $('#windayin').window('open');
}
function dayin() {
  
    var print_title1 = $("#print_title").val();
    var print_author1 = $("#print_author").val();
    var print_heigth1 = $("#print_heigth").val();
    var print_width1 = $("#print_width").val();
    var print_dpi1 = $("#print_dpi").val();
   var print_units1 = $("#print_units option:selected").val();
    var print_format1 = $("#print_format option:selected").val();
    var print_layout1 = $("#print_layout option:selected").val();

   
    require(["dojo/dom","dojo/on","dojo/query",
            "esri/layers/ArcGISDynamicMapServiceLayer",
            "esri/symbols/SimpleMarkerSymbol",
            "esri/symbols/SimpleLineSymbol",
            "esri/symbols/SimpleFillSymbol",
            "esri/toolbars/draw",
            "esri/graphic",
            "esri/tasks/LegendLayer",
            "esri/tasks/PrintTask",
            "esri/tasks/PrintTemplate",
            "esri/tasks/PrintParameters",
            "dojo/colors",
            "dojo/domReady!"
    ], function (
        dom,on,query,
                ArcGISDynamicMapServiceLayer,
                SimpleMarkerSymbol,
                SimpleLineSymbol,
                SimpleFillSymbol,
                Draw,
                Graphic,LegendLayer,
                PrintTask,PrintTemplate,PrintParameters,
                Color
        )
           {    
            //创建地图打印对象
            var printMap = new PrintTask
            ("http://172.16.100.178:6080/arcgis/rest/services/Contest/ExportWebMap2/GPServer/%E5%AF%BC%E5%87%BA%20Web%20%E5%9C%B0%E5%9B%BE");

        //创建地图打印模版


            var template = new PrintTemplate();
            //创建地图的打印参数，参数里面包括：模版和地图
            var params = new PrintParameters();
            //输出图片的空间参考
            printMap.outSpatialReference = map.SpatialReference
        //打印图片的各种参数

            template.exportOptions = {
                width: print_width1,
                height: print_heigth1,
                dpi: print_dpi1
            };

            var layouts = [{//设置打印地图排版信息
                options: {
                    scalebarUnit: print_units1,
                    titleText: print_title1,
                    authorText: print_author1,
                    copyrightText: "测试版权信息",
                    //legendLayer: legendLayer
                }
            }];
            template.layoutOptions = layouts[0].options;



            //打印输出的格式
            template.format = print_format1;
            //输出地图的布局
            template.layout = print_layout1;
        
            //设置参数地图
            params.map = map;
            //设置参数模版
            params.template = template;
            //运行结果
            printMap.execute(params, function (result) {
                if (result != null) {
                    //网页打开生成的地图
                    window.open(result.url);
                    alert("正在打印");
                }
            })
        
    });  
}

//右侧制图菜单
var closeFn;
		function closeShowingModal2() {
			var showingModal2 = document.querySelector('.modal2.show');
			if (!showingModal2) return;
			showingModal2.classList.remove('show');
			document.body.classList.remove('disable-mouse');
			document.body.classList.remove('disable-scroll');
			if (closeFn) {
				closeFn();
				closeFn = null;
			}
		}
		
		document.addEventListener('click', function (e) {
			var target = e.target;
			if (target.dataset.ctaTarget) {
				closeFn = cta(target, document.querySelector(target.dataset.ctaTarget), { relativeToWindow: true }, function showModal2(modal2) {
					modal2.classList.add('show');
					document.body.classList.add('disable-mouse');
					if(target.dataset.disableScroll){
						document.body.classList.add('disable-scroll');
					}
				});
			}
			else if (target.classList.contains('modal2-close-btn')) {
				closeShowingModal2();
			}
		});
		document.addEventListener('keyup', function (e) {
			if (e.which === 27) {
				closeShowingModal2();
			}
		})
//右侧制图菜单结束

        //按钮菜单
		var PathStatus = 0;
		var angle = Math.PI / ((3 - 1) * 2);
		var mainButton = [
            { 'bg': '~/resource/image/bg-2x.png', 'css': '', 'cover': '~/resource/image/icon-2x.png', 'html': '<span class="cover"></span>' },
            { 'bg': '', 'css': '', 'cover': '', 'html': '', 'angle': -405, 'speed': 200 }
		];
		var Radius = 86;		//小图出来的半径
		var Offset = 90;		//小图出来后的偏移量
		var Path = 1;		//出现方式，1：左上，2:左下，3：右上，4：右下
		var OutSpeed = 80;		//小图出现的速度
		var OutIncr = 100;		//小图出来的旋转
		var OffsetSpeed = 200;		//小图出来的旋转速度
		var InSpeed = 280;		//小图进去的速度
		var InIncr = -80;		//小图进去的旋转
		function PathRun() {
		    var PathMenu1 = $('#PathMenu1');
		    var PathItems = PathMenu1.children('.PathItem').slice(0, 3);
		    if (PathStatus == 0) {
		        var Count = PathItems.size();
		        PathItems.each(function (SP) {
		            var ID = $(this).index();
		            if (ID == 1) {
		                var X = Radius;
		                var Y = 0;
		                var X1 = X + Offset;
		                var Y1 = Y;
		            } else if (ID == Count) {
		                var X = 0;
		                var Y = Radius;
		                var X1 = X;
		                var Y1 = Y + Offset;
		            } else {
		                var X = Math.cos(angle * (ID - 1)) * Radius;
		                var Y = Math.sin(angle * (ID - 1)) * Radius;
		                var X1 = X + Offset;
		                var Y1 = Y + Offset;
		            }

		            if (Path == 2) {
		                Y = -Y;
		                Y1 = -Y1;
		            } else if (Path == 3) {
		                X = -X;
		                Y = -Y;
		                X1 = -X1;
		                Y1 = -Y1;
		            } else if (Path == 4) {
		                X = -X;
		                X1 = -X1;
		            }

		            $(this).children().children().animate({ rotate: 720 }, 600);

		            $(this).animate({ left: X1, bottom: Y1 }, OutSpeed + SP * OutIncr, function () {
		                $(this).animate({ left: X, bottom: Y }, OffsetSpeed);
		            });
		        });

		        if (mainButton[1]['angle']) {
		            $(PathMenu1.children('.PathMain').find('.rotate')).animate({ rotate: mainButton[1]['angle'] }, mainButton[1]['speed']);
		        }
		        if (mainButton[1]['bg'] != '') $(this).children().css('background-image', 'url(' + mainButton[1]['bg'] + ')')
		        if (mainButton[1]['css'] != '') $(this).children().css(mainButton[1]['css']);
		        if (mainButton[1]['cover'] != '') $(this).children().children().css('background-image', 'url(' + mainButton[1]['cover'] + ')');
		        if (mainButton[1]['html'] != '') $(this).children().html(mainButton[1]['html']);

		        PathStatus = 1;
		    } else if (PathStatus == 1) {
		        PathItems.each(function (SP) {
		            if (parseInt($(this).css('left')) == 0) {
		                X1 = 0;
		            } else {
		                if (Path <= 2) {
		                    X1 = parseInt($(this).css('left')) + Offset;
		                } else if (Path >= 3) {
		                    X1 = parseInt($(this).css('left')) - Offset;
		                }
		            }

		            if (parseInt($(this).css('bottom')) == 0) {
		                Y1 = 0;
		            } else {
		                if (Path == 3 || Path == 2) {
		                    Y1 = parseInt($(this).css('bottom')) - Offset;
		                } else if (Path == 1 || Path == 4) {
		                    Y1 = parseInt($(this).css('bottom')) + Offset;
		                }
		            }
		            $(this).children().children().animate({ rotate: 0 }, 600);
		            $(this).animate({ left: X1, bottom: Y1 }, OffsetSpeed, function () {
		                $(this).animate({ left: 0, bottom: 0 }, InSpeed + SP * InIncr);

		            });
		        });

		        if (mainButton[1]['angle']) {
		            $(PathMenu1.children('.PathMain').find('.rotate')).animate({ rotate: 0 }, mainButton[1]['speed']);
		        }

		        if (mainButton[0]['bg'] != '') $(this).children().css('background-image', 'url(' + mainButton[0]['bg'] + ')')
		        if (mainButton[0]['css'] != '') $(this).children().css(mainButton[0]['css']);
		        if (mainButton[0]['cover'] != '') $(this).children().children().css('background-image', 'url(' + mainButton[0]['cover'] + ')');
		        if (mainButton[0]['html'] != '') $(this).children().html(mainButton[0]['html']);

		        PathStatus = 0;
		    }
		}

		// 过渡页效果
		$(function ($) {
		    $('#fmPage').css('background-image', 'url(http://localhost:9003/resource/image/zhchbj2.png)');
		    $('#fmPage').height($(window).height());
		    $('body').css('overflow', 'hidden');
		    setTimeout(function () {
		        $('#fmPage').delay(2000).fadeOut(1000, function () {
		            $('body').css('overflow-y', 'auto');
		        });
		    });
		});
		$(function () {
		    $('.text1').textillate({ in: { effect: 'rollIn' } });
		    $('.text2').textillate({
		        initialDelay: 1000, 	//设置动画开始时间
		        in: {
		            effect: 'flipInX'	//设置动画名称
		        }
		    });
		})


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
=======
﻿
var map;
var symbol;
var legend;
require([
  "dojo/parser",
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
     
  "esri/toolbars/navigation",

  "dojo/on",
  
  "dojo/dom",
  "dojo/_base/array",
  "dojo/_base/connect",
  "dojo/data/ItemFileReadStore",
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
  parser, esriBasemaps, Map, HomeButton, BasemapGallery, Measurement, Graphic,
  InfoTemplate, SpatialReference, Extent,
  GraphicsLayer, navigation,
     on,  dom, arrayUtils, connect,
     ItemFileReadStore, Color, OverviewMap,
     Scalebar, Bookmarks, Legend, registry, Button,
     TitlePane
  ) {

      var divxy = dojo.byId("divxy");
      var button1 = dojo.byId("Button1");
      var button2 = dojo.byId("Button2");    
      var button7 = dojo.byId("Button7");
      var button8 = dojo.byId("Button8");
      var button9 = dojo.byId("Button9");

      //------
      var bt_zhitu = dojo.byId("bt_zhitu");




      parser.parse();
      esriBasemaps.delorme = {
          baseMapLayers: [{ url: "http://cache1.arcgisonline.cn/arcgis/rest/services/ChinaOnlineCommunity/MapServer" }
          ],
          thumbnailUrl: "~resource/image/shiliang.jpg",
          title: "矢量图"
      };
      map = new Map("map", {
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

     // map.on("load");

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

      dojo.connect(button10, "click", function (evt) {
          var Books = new esri.dijit.Bookmarks({      //添加书签
              map: map,
              editable: "true"
          }, dojo.byId("bookmarks"));
      });
      //--------------------------------------------------------------------------------------------

      //地图图例
       legend = new Legend({
          map: map
      }, "legendDiv");
       legend.startup();
      
  });


var editorCtl = true;
function Start_editor() {

    legend.destroy();

    
    require([
    
       "esri/tasks/GeometryService",

  
       "esri/layers/FeatureLayer",

       "esri/Color",
       "esri/symbols/SimpleMarkerSymbol",
       "esri/symbols/SimpleLineSymbol",

       "esri/dijit/editing/Editor",
       "esri/dijit/editing/TemplatePicker",

       "esri/config",
       "dojo/i18n!esri/nls/jsapi",

       "dojo/_base/array", "dojo/parser", "dojo/keys",

       "dijit/layout/BorderContainer", "dijit/layout/ContentPane",
       "dojo/domReady!"
    ], function (
       GeometryService,
        FeatureLayer,
       Color, SimpleMarkerSymbol, SimpleLineSymbol,
       Editor, TemplatePicker,
       esriConfig, jsapiBundle,
       arrayUtils, parser, keys
     ) {

        if (editorCtl)
        { 



        parser.parse();

        // snapping is enabled for this sample - change the tooltip to reflect this
        jsapiBundle.toolbars.draw.start = jsapiBundle.toolbars.draw.start + "<br>Press <b>ALT</b> to enable snapping";

        // 更多信息请参阅“使用代理页面”:  https://developers.arcgis.com/javascript/3/jshelp/ags_proxy.html
        esriConfig.defaults.io.proxyUrl = "/proxy/";

        //此服务仅用于开发和测试。我们建议您在应用程序中创建自己的几何服务。
        esriConfig.defaults.geometryService = new GeometryService("https://utility.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");

       

        map.on("layers-add-result", initEditor);

        var baseUrl = "http://172.16.100.178:6080/arcgis/rest/services/Contest/Legend/FeatureServer/";
        var Point_legend = new FeatureLayer(baseUrl + "0", {
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ['*']
        });
        var Line_legend = new FeatureLayer(baseUrl + "1", {
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ['*']
        });
        var Polygon_legend = new FeatureLayer(baseUrl + "2", {
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ['*']
        });

        map.addLayers([Point_legend, Line_legend, Polygon_legend]);


        function initEditor(evt) {
            var templateLayers = arrayUtils.map(evt.layers, function (result) {
                return result.layer;
            });
            var templatePicker = new TemplatePicker({
                featureLayers: templateLayers,
                grouping: true,
                rows: "auto",
                columns: 2,
                useLegend:false
            }, "templateDiv");
            templatePicker.startup();

            var layers = arrayUtils.map(evt.layers, function (result) {
                return { featureLayer: result.layer };
            });
            var settings = {
                map: map,
                enableUndoRedo: true,
                templatePicker: templatePicker,
                layerInfos: layers,
                toolbarVisible: true,
                createOptions: {
                    polylineDrawTools: [
                           Editor.CREATE_TOOL_FREEHAND_POLYLINE,
                           Editor.CREATE_TOOL_ARROW,
                           Editor.CREATE_TOOL_AUTOCOMPLETE,

                    ],
                    polygonDrawTools: [
                     Editor.CREATE_TOOL_FREEHAND_POLYGON,
                     Editor.CREATE_TOOL_AUTOCOMPLETE,
                     Editor.CREATE_TOOL_RECTANGLE,
                     Editor.CREATE_TOOL_TRIANGLE,
                     Editor.CREATE_TOOL_CIRCLE,
                     Editor.CREATE_TOOL_ELLIPSE
                    ]
                },
                toolbarOptions: {
                    reshapeVisible: true,
                    cutVisible: true,
                    mergeVisible: true
                }
            };

            var params = { settings: settings };
            var myEditor = new Editor(params, 'editorDiv');
            //定义捕捉选项
            var symbol = new SimpleMarkerSymbol(
              SimpleMarkerSymbol.STYLE_CROSS,
              15,
              new SimpleLineSymbol(
                SimpleLineSymbol.STYLE_SOLID,
                new Color([255, 0, 0, 0.5]),
                5
              ),
              null
            );
           
            map.enableSnapping({
                snapPointSymbol: symbol,
                tolerance: 20,
                snapKey: keys.ALT
            });

            myEditor.startup();
        }
        editorCtl = false;
        }
        else {
            myEditor.destroy();
            editorCtl = true;
        }

    });


    var divss1 = $("#insertEditor")[0];
    var addiv1 = "<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"region:'left'\" style=\"width: auto;overflow:hidden;\"><div id=\"templateDiv\"></div></div>";
    divss1.innerHTML = addiv1;

}
function dayin1() {
    $('#windayin').window('open');
}
function dayin() {
  
    var print_title1 = $("#print_title").val();
    var print_author1 = $("#print_author").val();
    var print_heigth1 = $("#print_heigth").val();
    var print_width1 = $("#print_width").val();
    var print_dpi1 = $("#print_dpi").val();
   var print_units1 = $("#print_units option:selected").val();
    var print_format1 = $("#print_format option:selected").val();
    var print_layout1 = $("#print_layout option:selected").val();

   
    require(["dojo/dom","dojo/on","dojo/query",
            "esri/layers/ArcGISDynamicMapServiceLayer",
            "esri/symbols/SimpleMarkerSymbol",
            "esri/symbols/SimpleLineSymbol",
            "esri/symbols/SimpleFillSymbol",
            "esri/toolbars/draw",
            "esri/graphic",
            "esri/tasks/LegendLayer",
            "esri/tasks/PrintTask",
            "esri/tasks/PrintTemplate",
            "esri/tasks/PrintParameters",
            "dojo/colors",
            "dojo/domReady!"
    ], function (
        dom,on,query,
                ArcGISDynamicMapServiceLayer,
                SimpleMarkerSymbol,
                SimpleLineSymbol,
                SimpleFillSymbol,
                Draw,
                Graphic,LegendLayer,
                PrintTask,PrintTemplate,PrintParameters,
                Color
        )
           {    
            //创建地图打印对象
            var printMap = new PrintTask
            ("http://172.16.100.178:6080/arcgis/rest/services/Contest/ExportWebMap2/GPServer/%E5%AF%BC%E5%87%BA%20Web%20%E5%9C%B0%E5%9B%BE");

        //创建地图打印模版


            var template = new PrintTemplate();
            //创建地图的打印参数，参数里面包括：模版和地图
            var params = new PrintParameters();
            //输出图片的空间参考
            printMap.outSpatialReference = map.SpatialReference
        //打印图片的各种参数

            template.exportOptions = {
                width: print_width1,
                height: print_heigth1,
                dpi: print_dpi1
            };

            var layouts = [{//设置打印地图排版信息
                options: {
                    scalebarUnit: print_units1,
                    titleText: print_title1,
                    authorText: print_author1,
                    copyrightText: "测试版权信息",
                    //legendLayer: legendLayer
                }
            }];
            template.layoutOptions = layouts[0].options;



            //打印输出的格式
            template.format = print_format1;
            //输出地图的布局
            template.layout = print_layout1;
        
            //设置参数地图
            params.map = map;
            //设置参数模版
            params.template = template;
            //运行结果
            printMap.execute(params, function (result) {
                if (result != null) {
                    //网页打开生成的地图
                    window.open(result.url);
                    alert("正在打印");
                }
            })
        
    });  
}

//右侧制图菜单
var closeFn;
		function closeShowingModal2() {
			var showingModal2 = document.querySelector('.modal2.show');
			if (!showingModal2) return;
			showingModal2.classList.remove('show');
			document.body.classList.remove('disable-mouse');
			document.body.classList.remove('disable-scroll');
			if (closeFn) {
				closeFn();
				closeFn = null;
			}
		}
		
		document.addEventListener('click', function (e) {
			var target = e.target;
			if (target.dataset.ctaTarget) {
				closeFn = cta(target, document.querySelector(target.dataset.ctaTarget), { relativeToWindow: true }, function showModal2(modal2) {
					modal2.classList.add('show');
					document.body.classList.add('disable-mouse');
					if(target.dataset.disableScroll){
						document.body.classList.add('disable-scroll');
					}
				});
			}
			else if (target.classList.contains('modal2-close-btn')) {
				closeShowingModal2();
			}
		});
		document.addEventListener('keyup', function (e) {
			if (e.which === 27) {
				closeShowingModal2();
			}
		})
//右侧制图菜单结束

        //按钮菜单
		var PathStatus = 0;
		var angle = Math.PI / ((3 - 1) * 2);
		var mainButton = [
            { 'bg': '~/resource/images/bg-2x.png', 'css': '', 'cover': '~/resource/images/icon-2x.png', 'html': '<span class="cover"></span>' },
            { 'bg': '', 'css': '', 'cover': '', 'html': '', 'angle': -405, 'speed': 200 }
		];
		var Radius = 86;		//小图出来的半径
		var Offset = 90;		//小图出来后的偏移量
		var Path = 1;		//出现方式，1：左上，2:左下，3：右上，4：右下
		var OutSpeed = 80;		//小图出现的速度
		var OutIncr = 100;		//小图出来的旋转
		var OffsetSpeed = 200;		//小图出来的旋转速度
		var InSpeed = 280;		//小图进去的速度
		var InIncr = -80;		//小图进去的旋转
		function PathRun() {
		    var PathMenu1 = $('#PathMenu1');
		    var PathItems = PathMenu1.children('.PathItem').slice(0, 3);
		    if (PathStatus == 0) {
		        var Count = PathItems.size();
		        PathItems.each(function (SP) {
		            var ID = $(this).index();
		            if (ID == 1) {
		                var X = Radius;
		                var Y = 0;
		                var X1 = X + Offset;
		                var Y1 = Y;
		            } else if (ID == Count) {
		                var X = 0;
		                var Y = Radius;
		                var X1 = X;
		                var Y1 = Y + Offset;
		            } else {
		                var X = Math.cos(angle * (ID - 1)) * Radius;
		                var Y = Math.sin(angle * (ID - 1)) * Radius;
		                var X1 = X + Offset;
		                var Y1 = Y + Offset;
		            }

		            if (Path == 2) {
		                Y = -Y;
		                Y1 = -Y1;
		            } else if (Path == 3) {
		                X = -X;
		                Y = -Y;
		                X1 = -X1;
		                Y1 = -Y1;
		            } else if (Path == 4) {
		                X = -X;
		                X1 = -X1;
		            }

		            $(this).children().children().animate({ rotate: 720 }, 600);

		            $(this).animate({ left: X1, bottom: Y1 }, OutSpeed + SP * OutIncr, function () {
		                $(this).animate({ left: X, bottom: Y }, OffsetSpeed);
		            });
		        });

		        if (mainButton[1]['angle']) {
		            $(PathMenu1.children('.PathMain').find('.rotate')).animate({ rotate: mainButton[1]['angle'] }, mainButton[1]['speed']);
		        }
		        if (mainButton[1]['bg'] != '') $(this).children().css('background-image', 'url(' + mainButton[1]['bg'] + ')')
		        if (mainButton[1]['css'] != '') $(this).children().css(mainButton[1]['css']);
		        if (mainButton[1]['cover'] != '') $(this).children().children().css('background-image', 'url(' + mainButton[1]['cover'] + ')');
		        if (mainButton[1]['html'] != '') $(this).children().html(mainButton[1]['html']);

		        PathStatus = 1;
		    } else if (PathStatus == 1) {
		        PathItems.each(function (SP) {
		            if (parseInt($(this).css('left')) == 0) {
		                X1 = 0;
		            } else {
		                if (Path <= 2) {
		                    X1 = parseInt($(this).css('left')) + Offset;
		                } else if (Path >= 3) {
		                    X1 = parseInt($(this).css('left')) - Offset;
		                }
		            }

		            if (parseInt($(this).css('bottom')) == 0) {
		                Y1 = 0;
		            } else {
		                if (Path == 3 || Path == 2) {
		                    Y1 = parseInt($(this).css('bottom')) - Offset;
		                } else if (Path == 1 || Path == 4) {
		                    Y1 = parseInt($(this).css('bottom')) + Offset;
		                }
		            }
		            $(this).children().children().animate({ rotate: 0 }, 600);
		            $(this).animate({ left: X1, bottom: Y1 }, OffsetSpeed, function () {
		                $(this).animate({ left: 0, bottom: 0 }, InSpeed + SP * InIncr);

		            });
		        });

		        if (mainButton[1]['angle']) {
		            $(PathMenu1.children('.PathMain').find('.rotate')).animate({ rotate: 0 }, mainButton[1]['speed']);
		        }

		        if (mainButton[0]['bg'] != '') $(this).children().css('background-image', 'url(' + mainButton[0]['bg'] + ')')
		        if (mainButton[0]['css'] != '') $(this).children().css(mainButton[0]['css']);
		        if (mainButton[0]['cover'] != '') $(this).children().children().css('background-image', 'url(' + mainButton[0]['cover'] + ')');
		        if (mainButton[0]['html'] != '') $(this).children().html(mainButton[0]['html']);

		        PathStatus = 0;
		    }
		}

		// 过渡页效果
		$(function ($) {
		    $('#fmPage').css('background-image', 'url(~/resource/image/zhchbj2.png)');
		    $('#fmPage').height($(window).height());
		    $('body').css('overflow', 'hidden');
		    setTimeout(function () {
		        $('#fmPage').delay(2000).fadeOut(1000, function () {
		            $('body').css('overflow-y', 'auto');
		        });
		    });
		});
		$(function () {
		    $('.text1').textillate({ in: { effect: 'rollIn' } });
		    $('.text2').textillate({
		        initialDelay: 1000, 	//设置动画开始时间
		        in: {
		            effect: 'flipInX'	//设置动画名称
		        }
		    });
		})


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
>>>>>>> d80a3d1fac9b2cc38f316903fe8ecb6bda32078a
