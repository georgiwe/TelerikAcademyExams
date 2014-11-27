module.exports = function (initiatives, seasons) {
    var parse = function (arr, ind) {
        var result = [];

        if (typeof(arr) === 'string') {
            return arr[ind];
            return;
        }

        for (var i = 0; i < arr.length; i++) {
            var pair = arr[i];
            var res = (ind === 0) ? initiatives[pair[ind]] : seasons[pair[ind]];
            result.push(res);
        };

        return result;
    };

    return parse;
}