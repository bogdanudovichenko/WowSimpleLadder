document.addEventListener('DOMContentLoaded', function () {
    var tabs = [
        {
            displayValue: '2v2 Arena',
            value: 0,
            selected: true
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

    var store = new Store({ 
        tabs: tabs,
        currentWowPvpBracket: 0
     });

    renderLadder(store);

    var tabsControl = new TabsControl('#bracket-wrapper', {
        tabs: store.getState('tabs'),
        onclick: function (ev) {
            store.setState('currentWowPvpBracket', ev.value);
        }
    });

    store.addOnChangeEventListener(function (ev) {
        renderLadder(ev.store);
    });

    function renderLadder(store) {
        var state = store.getAllState();

        var params = {
            pvpBracket: state.currentWowPvpBracket
        };

        apiService.getPvpLadder(params, function (data) {
            console.log('data', data);
        }, function (err) {
            console.error(err)
        });
    }
});