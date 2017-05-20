; (function () {
    var ajax = {};
    window.ajax = ajax;

    ajax.httpGet = function (url, params, onSuccess, onError) {
        if (!url) {
            throw 'url is null or empty';
        }

        url = addParamsToUrl(url, params);

        var request = new XMLHttpRequest();
        request.open('GET', url, true);

        request.onload = function () {
            if (request.status >= 200 && request.status < 400) {
                if (typeof(onSuccess) === 'function') {
                    var data = JSON.parse(request.responseText);
                    onSuccess(data);
                }
            } else {
                if (typeof (onError) === 'function') {
                    onError(formErrorObject(request));
                }
            }
        };

        request.onerror = function () {
            if (typeof (onError) === 'function') {
                onError(formErrorObject(request));
            }
        };

        request.send();        
    }

    function formErrorObject(request) {
        return {
            status: request.status,
            statusText: request.statusText
        };
    }

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