document.addEventListener('DOMContentLoaded', function () {
    var tabs = [
        {
            displayValue: '2v2 Arena',
            value: 0
        },
        {
            displayValue: '3v3 Arena',
            value: 1
        },
        {
            displayValue: 'Battlegrounds',
            value: 2
        }
    ];

    var store = new Store({ tabs: tabs });

    var tabsControl = new TabsControl('#bracket-wrapper', {
        tabs: store.getState('tabs'),
        onclick: function (ev) {
            store.setState('currentWowPvpBracket', ev.value);
        }
    });

    store.addOnChangeEventListener(function(ev) {
        debugger;
    });
});