; (function () {
    function GridDropDownControl(options) {
        if(!options) {
            throw 'options cannot be null or epmty';
        }

        if(typeof(options) !== 'object') {
            throw 'options must be an object';
        }

        var data = options.data;

        if(!data) {
            data = [];
        }

        if(!Array.isArray(data)) {
            throw 'data must be array';
        }

        this._data = options.data;

        return this;
    }

    window.GridDropDownControl = GridDropDownControl;
})();