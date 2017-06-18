; (function () {
    function GridControl(selector, options) {
        if (!selector) {
            throw 'selector is null or empty';
        }

        if (!options) {
            throw 'options cant be null or empty';
        }

        if (typeof (options) !== 'object') {
            throw 'options is not an object';
        }

        var url = options.url;

        if (typeof (url) !== 'string') {
            throw 'url must be a string';
        }

        if (!url) {
            throw 'url cannot be null or empty';
        }

        this._url = url;
        this._tableHeaders = options.tableHeaders;
        this._selector = selector;
        this._onDataBound = options.onDataBound;

        this._render();

        return this;
    }

    GridControl.prototype._render = function () {
        var self = this;

        ajax.httpGet(this._url, null, function (data) {

            if (!data) {
                return;
            }

            if (!Array.isArray(data)) {
                throw 'data must be array';
            }

            self._data = data;
            var table = self._createTable(data);

            var targetToRender = document.querySelector(self._selector);
            targetToRender.innerHTML = '';

            if (!targetToRender) {
                throw 'element with this selector: ' + self._selector + 'does not exist';
            }

            targetToRender.appendChild(table);

            if(self._onDataBound && typeof(self._onDataBound) === 'function') {
                self._onDataBound(data);
            }

        }, function (err) {
            console.error(err);
        });
    }

    GridControl.prototype._createTable = function () {
        var table = document.createElement('table');
        table.classList.add('table');
        table.classList.add('grid-control-table');

        var tableHeader = this._createTableHeader();
        table.appendChild(tableHeader);

        var tableBody = this._createTableBody();
        table.appendChild(tableBody);

        return table;
    }

    GridControl.prototype._createTableHeader = function () {
        var thead = document.createElement('thead');
        thead.classList.add('grid-header');

        var tableHeaders = this._tableHeaders;

        if (!tableHeaders || !Array.isArray(tableHeaders)) {
            tableHeaders = formTableHeadersFromData(this._data);
            this._tableHeaders = tableHeaders;
        }

        var tr = document.createElement('tr');

        var length = tableHeaders.length;

        for (var i = 0; i < length; i++) {
            var tableHeader = tableHeaders[i];
            var th = this._createTh(tableHeader);
            tr.appendChild(th);
        }

        thead.appendChild(tr);

        return thead;
    }

    GridControl.prototype._createTableBody = function () {
        var tbody = document.createElement('tbody');
        tbody.classList.add('grid-body');

        var data = this._data;
        var dataLength = data.length;

        for(var i = 0; i < dataLength; i++) {
            var item = data[i];
            var tr = this._createBodyTr(item);
            tbody.appendChild(tr);
        }

        return tbody;
    }

    GridControl.prototype._createBodyTr = function (item) {
        if(!item) {
            throw 'item cannot be null or empty';
        }

        if(typeof(item) !== 'object') {
            throw 'item must be an object';
        }

        var tr = document.createElement('tr');
        tr.classList.add('grid-row');

        //var headers = _.map(this._tableHeaders, function (h)  { return  h.logicalName });
        var tableHeaders = this._tableHeaders;
        var headersLength = tableHeaders.length;

        var itemWithSortedKeys = {};

        for(var i = 0; i < headersLength; i++) {
            var tableHeader = tableHeaders[i];
            var headerLogicalName = tableHeader.logicalName;
            var value = item[headerLogicalName];
            value = !value ? '' : value;          

            var td = this._createBodyTd(headerLogicalName, value, tableHeader.template, item);
            tr.appendChild(td);
        }

        return tr;
    }

    GridControl.prototype._createBodyTd = function(key, value, template, item) {
        var keySpan = document.createElement('span');
        keySpan.classList.add('grid-td-key-span');
        keySpan.style.visibility = 'hidden';
        keySpan.textContent = key;

        var displaySpan = document.createElement('span');
        displaySpan.classList.add('grid-td-display-span');
        displaySpan.innerHTML = template && typeof(template) === 'function' ? template(value, item) : value;

        var td = document.createElement('td');
        td.classList.add('grid-body-td');
        td.appendChild(displaySpan);
        td.appendChild(keySpan);        

        return td;
    }

    function formTableHeadersFromData(data) {
        if (!data || !Array.isArray(data)) {
            return [];
        }

        var keys = Object.keys(data[0]);
        var headersCount = keys.length;

        var headers = [];

        for (var i = 0; i < headersCount; i++) {
            headers.push({
                logicalName: keys[i],
                displayName: keys[i]
            });
        }

        return headers;
    }

    GridControl.prototype._createTh = function (header) {
        if (!header) {
            throw 'header is null or empty';
        }

        if (typeof (header) !== 'object') {
            throw 'header must be an object';
        }

        var th = document.createElement('th');

        //start display span creation
        var displaySpan = document.createElement('span');
        displaySpan.classList.add('grid-display-header-element-value');
        displaySpan.textContent = header.displayName;
        th.appendChild(displaySpan);
        //end display span creation

        //start value span creation
        var valueSpan = document.createElement('span');

        valueSpan.classList.add('grid-hidden-header-element-value');
        valueSpan.style.visibility = 'hidden';
        valueSpan.textContent = header.logicalName;

        th.appendChild(valueSpan);
        //end value span creation        

        return th;
    }

    window.GridControl = GridControl;
})();