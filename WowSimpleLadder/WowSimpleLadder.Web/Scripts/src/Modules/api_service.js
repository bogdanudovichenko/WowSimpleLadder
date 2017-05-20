; (function () {
    var baseUrl = '/api/WowPvpLadder';

    window.apiService = {
        getPvpLadder: function(params, onSuccess, onError) {
            var url = baseUrl + '/getpvpladder';

            ajax.httpGet(url, params, function(data) {
                if(typeof(onSuccess) === 'function') {
                    onSuccess(data);
                }
            }, function(err) {
                if(typeof(onError) === 'function') {
                    onError(err);
                }
            });
        },

        formUrlForLadderGrid: function(params) {
            var url = baseUrl + '/getpvpladder';
            return addParamsToUrl(url, params);
        }
    };

    function addParamsToUrl(url, params) {
        if (!url || typeof(url) !== 'string' || !params || typeof(params) !== 'object') {
            return url;
        }

        var resultUrl = url;

        var paramsKeys = Object.keys(params);
        var paramsCount = paramsKeys.length;

        if (paramsCount === 0) {
            return resultUrl;
        }

        if (resultUrl.indexOf('?') === -1) {
            resultUrl += '?';
        } else {
            resultUrl += '&';
        }

        for (var i = 0; i < paramsCount; i++) {
            var paramsKey = paramsKeys[i];
            var paramsValue = params[paramsKey];

            if (!paramsValue) {
                continue;
            }

            var buf = paramsKey.toLowerCase() + '=' + paramsValue;

            if (i + 1 !== paramsCount) {
                buf += '&';
            }

            resultUrl += buf;
        }

        return resultUrl;
    }

})();