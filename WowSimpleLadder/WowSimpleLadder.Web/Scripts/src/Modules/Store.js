﻿; (function () {

    function Store(defaultState) {
        if (typeof (defaultState) !== 'object') {
            throw 'defaultState must be an object';
        }

        if (!defaultState) {
            throw 'defaultState cannot be null';
        }

        this._state = _.extend({}, defaultState);
        this._onChangeEventListeners = [];

        return this;
    }

    window.Store = Store;

    Store.prototype.getAllState = function () {
        return _.extend({}, this._state);
    };

    Store.prototype.getState = function (key) {
        if (typeof (key) !== 'string') {
            throw 'key must be string';
        }

        if (!key) {
            throw 'key cannot be null or empty';
        }

        var value = this._state[key];

        if (Array.isArray(value)) {
            return _.extend([], value);
        }

        return typeof (value) !== 'object' ? value : _.extend({}, value);
    };

    Store.prototype.setState = function (key, value) {
        if (typeof (key) !== 'string') {
            throw 'key must be string';
        }

        if (!key) {
            throw 'key cannot be null or empty';
        }

        if (!value && value !== 0) {
            throw 'value cannot be null or empty';
        }

        var valueToSet;

        if (Array.isArray(value)) {
            valueToSet = _.extend([], value);
        } else {
            valueToSet = typeof (value) !== 'object' ? value : _.extend({}, value);
        }

        this._state[key] = valueToSet;

        var event = {
            key: key,
            value: valueToSet,
            store: this
        };

        var onChangeListeners = this._onChangeEventListeners;
        var onChangeListenersLength = onChangeListeners.length;

        for (var i = 0; i < onChangeListenersLength; i++) {
            var onChangeListener = onChangeListeners[i];
            onChangeListener(event);
        }
    };

    Store.prototype.addOnChangeEventListener = function (eventListener) {
        if (typeof (eventListener) !== 'function') {
            throw 'eventListener must be a function';
        }

        this._onChangeEventListeners.push(eventListener);
    }
})();