
var osmLayer = new ol.layer.Tile({
    source: new ol.source.OSM(),
    visible: true,
    preload: 0
});

var map = new ol.Map({
    layers: [osmLayer],
    target: "map"
});

var view = new ol.View({     
    zoom: 4,
    maxZoom: 18,
    minZoom: 3
});
map.setView(view);

function setCenterAt(lat, lon) {
    console.log (">>> Message received from JS: Setting center of map.")
    view.setCenter(ol.proj.transform([lon, lat], "EPSG:4326", "EPSG:3857"));
}

function sendMessage(text) {    
    console.log(">>> Message received from .NET: " + text);
}