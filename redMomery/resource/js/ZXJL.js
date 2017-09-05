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
          thumbnailUrl: "../resource/image/shiliang.jpg",
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


      var mapCenter = new esri.geometry.Point(103.847, 25.0473, map.spatialReference);
      map.centerAndZoom(mapCenter, 4);



  });