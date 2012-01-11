String.prototype.format = function () {
    var s = this,
    i = arguments.length;

    while (i--) {
        s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return s;
};

function GetRegionOrCulture(region) {
    var regionInfo = $.formatCurrency.regions[region];
    if (regionInfo) {
        return regionInfo;
    }
    else {
        if (/(\w+)-(\w+)/g.test(region)) {
            var culture = region.replace(/(\w+)-(\w+)/g, "$1");
            return $.formatCurrency.regions[culture];
        }
    }
    // fallback to extend(null) (i.e. nothing)
    return null;
}