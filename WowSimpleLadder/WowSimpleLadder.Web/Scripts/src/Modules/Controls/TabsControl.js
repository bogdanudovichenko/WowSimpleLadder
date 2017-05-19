; (function () {
    var maxTabsCount = 12;

    var TabsControl = function (selector, options) {

        if (!selector) {
            throw 'selector is null or empty';
        }

        if (!options) {
            throw 'options cant be null or empty';
        }

        var tabs = options.tabs;

        if (!Array.isArray(tabs)) {
            throw 'tabs must be array';
        }

        if (tabs.length > maxTabsCount) {
            throw 'tabs count cannot be greater than ' + maxTabsCount;
        }

        this._tabs = tabs;
        this._selector = selector;
        this._options = options;

        this._render();

        return this;
    }

    window.TabsControl = TabsControl;//export TabControl

    TabsControl.prototype.update = function (tabs) {
        if (!Array.isArray(tabs)) {
            throw 'tabs must be array';
        }

        if (tabs.length > maxTabsCount) {
            throw 'tabs count cannot be greater than ' + maxTabsCount;
        }

        this._tabs = tabs;
        this._render();
    }

    TabsControl.prototype._render = function () {
        var selector = this._selector;

        if (!selector) {
            throw 'selector is null or empty';
        }

        var tabsControlWrapper = document.createElement('div');
        tabsControlWrapper.classList.add('row');
        tabsControlWrapper.classList.add('tabs-control-wrapper');

        this._createTabs(this._tabs, tabsControlWrapper);

        var targetToRender = document.querySelector(selector);
        targetToRender.innerHTML = '';

        if (!targetToRender) {
            throw 'element with this selector: ' + this.selector + 'does not exist';
        }

        targetToRender.appendChild(tabsControlWrapper);
    }

    TabsControl.prototype._createTabs = function (tabs, tabsWrapper) {
        var tabsCount = tabs.length;

        if (!tabsCount) {
            return [];
        }

        var tabColSize = countColSize(tabsCount);

        var options = this._options;


        for (var i = 0; i < tabsCount; i++) {
            var tab = tabs[i];
            var tabDiv = this._createTabDiv(tab, tabColSize);

            tabDiv.addEventListener('click', function () {
                var hiddenSpan = this.querySelector('span.hidden-tab-value');
                var displaySpan = this.querySelector('span.display-tab-value');

                var event = {
                    target: this,
                    value: hiddenSpan.textContent,
                    displayValue: displaySpan.textContent
                };

                var onclick = options.onclick;

                if (typeof (onclick) === 'function') {
                    onclick(event);
                }
            });

            tabsWrapper.appendChild(tabDiv);
        }
    }

    TabsControl.prototype._createTabDiv = function (tab, colSize) {
        if (!tab) {
            throw 'Tab cannot be null or empty';
        }

        var tabDiv = document.createElement('div');
        tabDiv.classList.add('tab-contol-wrapper');
        tabDiv.classList.add('col');
        tabDiv.classList.add('c' + colSize);

        //start display span creation
        var spanForDisplayValue = document.createElement('span');
        spanForDisplayValue.classList.add('display-tab-value');
        spanForDisplayValue.textContent = tab.displayValue ? tab.displayValue : tab.value;

        tabDiv.appendChild(spanForDisplayValue);
        //end display span creation

        //start hidden span creation
        var hiddenSpanForValue = document.createElement('span');
        hiddenSpanForValue.classList.add('hidden-tab-value');
        hiddenSpanForValue.style.visibility = 'hidden';
        hiddenSpanForValue.textContent = tab.value;

        tabDiv.appendChild(hiddenSpanForValue);
        //end hidden span creation

        return tabDiv;
    }

    function countColSize(tabsCount) {
        if (!tabsCount || tabsCount < 1 || tabsCount > maxTabsCount) {
            throw 'Invalid tabsCount';
        }

        return Math.floor(maxTabsCount / tabsCount);
    }
})();