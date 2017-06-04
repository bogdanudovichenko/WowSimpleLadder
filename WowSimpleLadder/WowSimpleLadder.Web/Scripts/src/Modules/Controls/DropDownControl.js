; (function () {
    function DropDownControl(selector, options) {
        if (!selector) {
            throw 'selector cannot be null or empty';
        }

        if (typeof (selector) !== 'string') {
            throw 'selector must be a string';
        }

        if (!options) {
            throw 'options cannot be null or epmty';
        }

        if (typeof (options) !== 'object') {
            throw 'options must be an object';
        }

        var data = options.data;

        if (!data) {
            data = [];
        }

        if (!Array.isArray(data)) {
            throw 'data must be array';
        }

        this._data = options.data;
        this._text = options.text;
        this._selector = selector;
        this._selectedValue = options.selectedValue ? options.selectedValue : -1;
        this._onChange = options.onChange;

        this._render();

        return this;
    }

    DropDownControl.prototype._render = function () {
        window.onclick = function (event) {
            if (!event.target.matches('.dropbtn')) {

                var dropdowns = document.getElementsByClassName("dropdown-content");
                var i;
                for (i = 0; i < dropdowns.length; i++) {
                    var openDropdown = dropdowns[i];
                    if (openDropdown.classList.contains('show')) {
                        openDropdown.classList.remove('show');
                    }
                }
            }
        }

        var dropDownWrapper = document.createElement('div');
        dropDownWrapper.classList.add('dropdown');

        var text = this._text;

        var dropDownButton = document.createElement('div');
        dropDownButton.classList.add('dropbtn');
        dropDownButton.textContent = text ? text : 'Select item';
        dropDownWrapper.appendChild(dropDownButton);

        this._textContainer = dropDownButton;

        var ul = document.createElement('ul');
        ul.classList.add('dropdown-content');

        var data = this._data;
        var dataLength = data.length;

        for (var i = 0; i < dataLength; i++) {
            var item = data[i];

            if (!item) {
                continue;
            }

            var li = this._createLi(item);
            ul.appendChild(li);
        }

        dropDownWrapper.appendChild(ul);

        dropDownButton.addEventListener('click', function () {
            ul.classList.toggle("show");
        });

        var targetToRender = document.querySelector(this._selector);
        if (!targetToRender) {
            console.error('dropdown cant find target to render on selector: ' + this._selector);
            return;
        }
        targetToRender.innerHTML = '';
        targetToRender.appendChild(dropDownWrapper);
    }

    DropDownControl.prototype._createLi = function (item) {
        if (!item) {
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

        var self = this;

        li.addEventListener('click', function () {
            self._selectedValue = item.value;
            if (self._textContainer && self._textContainer.textContent) {
                self._textContainer.textContent = item.displayValue;
            }

            if (typeof (self._onChange) === 'function') {
                self._onChange(item.value);
            }
        });

        return li;
    }

    window.DropDownControl = DropDownControl;
})();