var randomColor = function (opacity) {
    var r = Math.round(Math.random() * 255);
    var g = Math.round(Math.random() * 255);
    var b = Math.round(Math.random() * 255);
    return 'rgba(' + r + ',' + g + ',' + b + ',' + (opacity || '.9') + ')';
};