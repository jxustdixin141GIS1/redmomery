  //-----------老兵详细信息-----------//
        var sLBID;
        var sLBname;
        var sLBsex;
        var sLBbirthday;
        var sLBjob;
        var sLBdomicile;
        var sdesignation;
        var sLBexperience;
        var sLBlife;
        var sLBPhoto;
        var ex;


        var qLBname;
        var qLBsex;
        var qLBbirthday;
        var qLBjob;
        var qLBdomicile;
        var qdesignatio;
        var qLBexperien;
        var qLBlife;
        var qLBPhoto;




        var map;
        var symbol;
        var heatmapFeatureLayer;
        var LBGraphicsLayer;
        var highlightGraphic;
        require([
          "esri/basemaps",
          "esri/map",
     

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
          esriBasemaps, Map, BasemapGallery, Measurement, Graphic, InfoTemplate, SpatialReference, Extent,
          GraphicsLayer, FeatureLayer, HeatmapRenderer, SimpleMarkerSymbol,
             SimpleLineSymbol, SimpleFillSymbol, PictureMarkerSymbol, Query, QueryTask, FindTask, FindParameters, navigation,
             on, parser, dom, arrayUtils, connect, ItemFileReadStore, DataGrid, Color, OverviewMap, Scalebar, Bookmarks, Legend, registry, Button,
             TitlePane
          ) {

              var divxy = dojo.byId("divxy");
              var button1 = dojo.byId("Button1");
              var button2 = dojo.byId("Button2");
              var button8 = dojo.byId("Button8");
              var button11 = dojo.byId("Button11");
              var button13 = dojo.byId("Button13");
              var button14 = dojo.byId("Button14");


              parser.parse();

              esriBasemaps.delorme = {
                  baseMapLayers: [{ url: "http://cache1.arcgisonline.cn/arcgis/rest/services/ChinaOnlineCommunity/MapServer" }
                  ],
                  thumbnailUrl: "../resource/image/shiliang.jpg",
                  title: "矢量图"
              };


              map = new Map("mapDiv", {
                  basemap: "delorme",

                  sliderStyle: "small"
              });

              //var home = new HomeButton({
              //    map: map
              //}, "HomeButton");
              //home.startup();


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

            map.on("load", setUpQuery);

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
              dojo.connect(button13, "click", function () {
                  map.graphics.clear();
                  LBGraphicsLayer.show();
                  heatmapFeatureLayer.hide();
              });


              //地图图例
              var legend = new Legend({
                  map: map
              }, "legendDiv");
              legend.startup();

              //------------------------------------QueryTask-----------------------------------//

              function setUpQuery() {

                  var queryTask = new QueryTask
                      ("http://192.168.43.35:6080/arcgis/rest/services/LBDatanew/MapServer/0");

                  //设定查询条件
                  var query = new Query();
                  query.returnGeometry = true;
                  query.outFields = ["LBID", "LBname", "LBjob", "LBsex", "LBbirthday", "LBdomicile", "designation", "LBexperience", "LBlife", "LBPhoto"];
                  query.outSpatialReference = { "wkid": 4326 };
                  query.where = "LBID>0";
                  //信息模板
                  var infoTemplate = new InfoTemplate();


                  var qLBID = "${LBID}";
                  var qLBname = "${LBname}";
                  var qLBsex = "${LBsex}";
                  var qLBbirthday = "${LBbirthday}";
                  var qLBjob = "${LBjob}";
                  var qLBdomicile = "${LBdomicile}";
                  var qdesignation = "${designation}";
                  var qLBexperience = "${LBexperience}";
                  var qLBlife = "${LBlife}";
                  var qLBPhoto = "${LBPhoto}";



                  var content =
                      //"<b>ID: </b> " + qLBID + "<br/>" +
                               "<b>姓名: </b>" + qLBname + "&nbsp&nbsp&nbsp&nbsp" +
                                "<a href='#' data-cta-target='.js-sidebar'>(查看详细信息)</a>" + "<br/>" +
                                "<b>性别: </b> " + qLBsex + "<br/>" +
                                "<b>出生日期: </b>" + qLBbirthday + "<br/>" +
                                "<b>部队职务: </b>" + qLBjob + "<br/>" +
                                "<b>现居住地址: </b>" + qLBdomicile + "<br/>" +
                                "<b>部队番号: </b>" + qdesignation + "<br/>" +
                                //"<b>老兵故事: </b>" + qLBexperience + "<br/>" +
                                "<b>生活现状: </b>" + qLBlife + "<br/>"
                                //"<b>照片: </b>" + qLBPhoto + "<br/>";


                  infoTemplate.setTitle("${LBname}");
                  infoTemplate.setContent(content);

                  map.infoWindow.resize(400, 400);

                  //------------------------------------------------------------


                  queryTask.execute(query);

                  //查询完成执行事件
                  queryTask.on("complete", function (event) {


                      var highlightSymbol = new PictureMarkerSymbol("../resource/image/1432101824.png", 20, 30);

                      var symbol = new PictureMarkerSymbol("../resource/image/1432101726.png", 15, 20);


                      var features = event.featureSet.features;

                      LBGraphicsLayer = new GraphicsLayer();

                      //遍历查询的内容，添加到GraphicsLayer
                      var featureCount = features.length;

                      for (var i = 0; i < featureCount; i++) {
                          //Get the current feature from the featureSet.

                          //Feature is a graphic
                          var graphic = features[i]; 

                          graphic.setSymbol(symbol);
                          graphic.setInfoTemplate(infoTemplate);

                          LBGraphicsLayer.add(graphic);

                      }
                      map.addLayer(LBGraphicsLayer, 0);//添加到地图中

                      map.graphics.enableMouseEvents();   //鼠标事件可用

                      //鼠标在GraphicsLayer移动产生事件



                      var HLGraphicsLayer = new GraphicsLayer();

                      LBGraphicsLayer.on("mouse-over", function (event) {
                          //  map.graphics.clear();  //use the maps graphics layer as the highlight layer

                          var graphic = event.graphic;

                          map.infoWindow.setContent(graphic.getContent());
                          map.infoWindow.setTitle(graphic.getTitle());

                          var highlightGraphic = new Graphic(graphic.geometry, highlightSymbol);

                          sLBID = graphic.attributes['LBID'];
                          sLBname = graphic.attributes['LBname'];
                          sLBsex = graphic.attributes['LBsex'];
                          sLBbirthday = graphic.attributes['LBbirthday'];
                          sLBjob = graphic.attributes['LBjob'];
                          sLBdomicile = graphic.attributes['LBdomicile'];
                          sdesignation = graphic.attributes['designation'];
                          //sLBexperience = graphic.attributes['LBexperience'];
                          ex = graphic.attributes['LBexperience'];
                          sLBlife = graphic.attributes['LBlife'];
                          sLBPhoto = graphic.attributes['LBPhoto'];


                          $.ajax({
                              type: "POST",
                              contentType: "application/json;utf-8",
                              url: " /LBbx/GetLBByLBID",
                              data: "{'sLBID':'" + sLBID + "'}",
                              dataType: "JSON",
                              success: function (results1) {
                                  ex = results1[0].LBexperience;
                                  var divs2 = $("#wz2")[0];
                                  divs2.innerHTML = "<p >" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + ex + "</span>" + "</p>" +
                                                    "<p >" + "<img src= '../resource/image/lbinfo/" + results1[0].LBPhoto + "' width=100%   />" + "</p>";
                                  //下面应该进行启动程序------
                                  LoadLBsByTID(results1[0].T_ID, false);
                                  LoadCommentByTID(results1[0].T_ID, false);
                              }
                          });
                          ;


                          //声明处理////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                         

                              var divs = $("#wz")[0];
                              


                              divs.innerHTML = "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "姓&nbsp&nbsp名：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + sLBname + "</span>" + "</p>" +
                                                                         "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "性&nbsp&nbsp别：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + sLBsex + "</span>" + "</p>" +
                                                                         "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "出生日期：" + "</span>" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + sLBbirthday + "</span>" + "</p>" +
                                                                         "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "部队职务：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + sLBjob + "</span>" + "</p>" +
                                                                         "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "现居住地：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + sLBdomicile + "</span>" + "</p>" +
                                                                         "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "现居住地：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + sdesignation + "</span>" + "</p>" +
                                                                         "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "生活现状：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + sLBlife + "</span>" + "</p>";
                             
                              var divs2 = $("#wz2")[0];
                              divs2.innerHTML = "<p >" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + ex + "</span>" + "</p>" +
                                                "<p >" + "<img src= '../resource/image/lbinfo/" + sLBPhoto + "' width=100%   />" + "</p>";
                          


                          HLGraphicsLayer.add(highlightGraphic);

                          map.addLayer(HLGraphicsLayer, 3);

                          map.infoWindow.show(event.screenPoint,
                          map.getInfoWindowAnchor(event.screenPoint));




                      });


                      //鼠标离开产生的事件
                      LBGraphicsLayer.on("mouse-out", function () {

                          HLGraphicsLayer.clear();

                          //map.infoWindow.hide();
                      });
                  });

                  dojo.connect(button13, "click", function () {
                      map.graphics.clear();
                      heatmapFeatureLayer.hide();
                  });

              }
              //------------------------------------------QueryTask结束--------------------------------------//

              //------------------------------------------FindTask开始--------------------------------------//

              var findTask, findParams;
              var center, zoom;
              var grid, store;
              registry.byId("search").on("click", doFind);
              registry.byId("resetBT").on("click", doreset);

              function doreset() {

                  map.graphics.clear();
              }
              findTask = new FindTask
               ("http://192.168.43.35:6080/arcgis/rest/services/LBDatanew/MapServer");

              map.on("load", function () {
                  //构建寻找参数
                  findParams = new FindParameters();
                  findParams.returnGeometry = true;
                  findParams.layerIds = [0];
                  findParams.searchFields = ["LBID", "LBname", "LBjob", "LBsex", "LBbirthday", "LBdomicile", "designation", "LBexperience", "LBlife"];
                  findParams.outSpatialReference = map.spatialReference;
              });

              function doFind() {

                  findParams.searchText = dom.byId("testName").value;//获取查询的内容
                  findTask.execute(findParams, showResults);//执行查询
              }

              function showResults(results) {

                  //map.graphics.clear();
                  var dtsymbol = new PictureMarkerSymbol("../resource/image/Memory1.gif", 30, 30);


                  //创建一个数组
                  var items = arrayUtils.map(results, function (result) {
                      var graphic = result.feature;
                      graphic.setSymbol(dtsymbol);
                      map.graphics.add(graphic, 0);
                      return result.feature.attributes;//所有信息字段的列表

                      // map.addLayer(findGraphicsLayer, 0);//添加到地图中
                  });

                  //创建数据对象
                  var data = {
                      identifier: "OBJECTID",//关键字段
                      label: "OBJECTID", //显示字段
                      items: items   //绑定
                  };

                  //数据存储对象
                  store = new ItemFileReadStore({
                      data: data
                  });

                  var grid = registry.byId("grid");

                  grid.setStore(store);
                  grid.on("rowclick", onRowClickHandler);

                  //地图居中
                  // map.centerAndZoom(center, zoom);
              }

              //单击信息定位
              function onRowClickHandler(evt) {
                  var clickedTaxLotId = evt.grid.getItem(evt.rowIndex).OBJECTID;//获取这一行的变化



                  var selectedTaxLot = arrayUtils.filter(map.graphics.graphics, function (graphic) {//过滤

                      return ((graphic.attributes) && graphic.attributes.OBJECTID === clickedTaxLotId);//===不只是数值相等,数据类型也要相同

                  });


                  if (selectedTaxLot.length) {

                      var jd = selectedTaxLot[0].geometry.getLatitude();
                      var wd = selectedTaxLot[0].geometry.getLongitude();

                      var ZTP = new esri.geometry.Point(wd, jd);
                      map.centerAndZoom(ZTP, 8);

                  }
              }
              //------------------------------------------FindTask结束--------------------------------------//


              //-------热点图--------//


              dojo.connect(button14, "click", function () {
                  var serviceURL = "http://192.168.43.35:6080/arcgis/rest/services/LBDatanew/FeatureServer/0";

                  heatmapFeatureLayer = new FeatureLayer(serviceURL, {
                      opacity: 0.6,
                      mode: esri.layers.FeatureLayer.MODE_ONDEMAND,
                      outFields: ['*']
                  });

                  var heatmapRenderer = new HeatmapRenderer();
                  heatmapFeatureLayer.setRenderer(heatmapRenderer);
                  map.addLayer(heatmapFeatureLayer);

                  LBGraphicsLayer.hide();
              });
          });

		
			// 过渡页效果
        $(function ($) {
            $('#fmPage').css('background-image', "url(../resource/image/beijing1.jpg)");
            windowHeight = $(window).height();
            windowWidth = $(window).width();
            //文档宽度
            setWidth = (windowHeight) * (1920 / 1080);
            setHeight = (windowWidth) * (1080 / 1920);
            $('#fmPage').height($(window).height());
            $('#fmTitle').height($(window).height());
            $('#fmImg3').height($(window).height());
            $('#fm70').height($(window).height());
            //如果，比较窄
            if (windowWidth / windowHeight < ((1920 / 1080) - 0.02)) {
                var left = (setWidth - windowWidth) / 2;
                $('#fmTitle').css('left', '-' + left + 'px');
                $('#fmImg3').css('left', '-' + left + 'px');
                $('#fm70').css('left', '-' + left + 'px');
                console.log('set')
            }
                //如果，比较矮
            else if (windowWidth / windowHeight > ((1920 / 1080) + 0.02)) {
                left = (windowWidth - setWidth) / 2;
                //console.log(left);
                $('#fmTitle').css('left', left + 'px');
                $('#fmImg3').css('left', left + 'px');
                $('#fm70').css('left', left + 'px');
            }
            else { }
            $('body').css('overflow', 'hidden');
            setTimeout(function () {
                $('#fmTitle').fadeIn(2000);
                $('#fmImg3').delay(1000).fadeIn(1000);
            }, 500);
            setTimeout(function () {
                $('#fmImg3').fadeOut(500);
                $('#fmTitle').delay(1000).fadeOut(1000);
                $('#fmPage').delay(2000).fadeOut(1000, function () {
                    $('body').css('overflow-y', 'auto');
                });
                $('#fm70').delay(2000).fadeOut(1000)
            }, 2000);
        });
        //过渡页效果结束

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

//右侧弹出
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
                    if (target.dataset.disableScroll) {
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

        ////老兵信息框拉伸
        //$(function () {
        //    //绑定需要拖拽改变大小的元素对象 
        //    bindResize(document.getElementById('AllInfoWin'));
        //});

        //function bindResize(el) {
        //    //初始化参数 
        //    var els = el.style,
        //    //鼠标的 X 和 Y 轴坐标 
        //    x = 0;
        //    $(el).mousedown(function (e) {
        //        //按下元素后，计算当前鼠标与对象计算后的坐标 
        //        x = e.clientX - el.offsetWidth
        //        el.setCapture ? (
        //        //捕捉焦点 
        //        el.setCapture(),
        //        //设置事件 
        //        el.onmousemove = function (ev) {
        //            mouseMove(ev || event)
        //        },
        //        el.onmouseup = mouseUp
        //        ) : (
        //        //绑定事件 
        //        $(document).bind("mousemove", mouseMove).bind("mouseup", mouseUp)
        //        )
        //        //防止默认事件发生 
        //        e.preventDefault()
        //    });
        //    //移动事件 
        //    function mouseMove(e) {
        //        els.width = e.clientX - x + 'px'
        //    }
        //    //停止事件 
        //    function mouseUp() {
        //        el.releaseCapture ? (
        //        //释放焦点 
        //        el.releaseCapture(),
        //        //移除事件 
        //        el.onmousemove = el.onmouseup = null
        //        ) : (
        //        //卸载事件 
        //        $(document).unbind("mousemove", mouseMove).unbind("mouseup", mouseUp)
        //        )
        //    }
        //}

        //点赞
        $(document).ready(function () {
            $('body').on("click", '.heart', function () {

                var A = $(this).attr("id");
                var B = A.split("like");
                var messageID = B[1];
                var C = parseInt($("#likeCount" + messageID).html());
                $(this).css("background-position", "")
                var D = $(this).attr("rel");

                if (D === 'like') {
                    $("#likeCount" + messageID).html(C + 1);
                    $(this).addClass("heartAnimation").attr("rel", "unlike");

                }
                else {
                    $("#likeCount" + messageID).html(C - 1);
                    $(this).removeClass("heartAnimation").attr("rel", "like");
                    $(this).css("background-position", "left");
                }
            });
        });

        //评论区
        function LoadLB() {
            var ques = $("#LBname")[0].value;
            LoadLBbyname(ques, false);
        }
        function getaddess() {
            var address = $("#cadd")[0].value;
        }
        function Loadtitle() {
            var ques = $("#LBTIltleID")[0].value;
            LoadTitleByID(ques, false);

        }
        function LoadtitlebyLB() {
            LoadTitleByID(LB_INFO.T_ID, false);
        }
        function LoadLBByTID() {
            LoadLBsByTID(CurrentTitle.T_ID, false);
        }
        function LoadCLByTID() {
            LoadCommentByTID(CurrentTitle.T_ID, false);
        }