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
        }
    };

})();