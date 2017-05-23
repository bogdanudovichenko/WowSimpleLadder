; (function () {
    function DropDownControl(selector, options) {
        if(!selector) {
            throw 'selector cannot be null or empty';
        }

        if(typeof(selector) !== 'string') {
            throw 'selector must be a string';
        }

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
        this._selector = selector;

        this._render();

        return this;
    }

    DropDownControl.prototype._render = function() {
        var dropDownWrapper = document.createElement('span');
        dropDownWrapper.classList.add('drop-down-control-wrapper');

        var ul = document.createElement('ul');
        ul.classList.add('dropdown-control-list');

        var data = this._data;
        var dataLength = data.length;

        for(var i = 0; i < dataLength; i++) {
            var item = data[i];

            if(!item) {
                continue;
            }

            var li = this._createLi(item);
            ul.appendChild(li);
        }

        dropDownWrapper.appendChild(ul);

        var targetToRender = document.querySelector(this._selector);

        if(!targetToRender) {
            console.error('dropdown cant find target to render on selector: ' + this._selector);
            return;
        }

        targetToRender.appendChild(dropDownWrapper);
    }

    DropDownControl.prototype._createLi = function(item) {
        if(!item) {
            throw 'Item cannot be null or empty';
        }

        var li = document.createElement('li');
        li.classList.add('drop-down-control-li');

        var displaySpan = document.createElement('span');
        displaySpan.classList.add('dropdown-control-li-display');
        displaySpan.textContent = item.displayValue;

        li.appendChild(displaySpan);

        var hiddenSpan = document.createElement('span');
        hiddenSpan.classList.add('dropdown-control-li-hidden');
        hiddenSpan.style.visibility = 'hidden';
        hiddenSpan.textContent = item.value;

        li.appendChild(hiddenSpan);

        return li;
    }

    window.DropDownControl = DropDownControl;
})();