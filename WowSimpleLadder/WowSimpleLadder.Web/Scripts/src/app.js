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
        currentWowPvpBracket: 0,
        selectedWowClass: 0
    });

    renderLadder(store);

    var bracketTabsControl = new TabsControl('#bracket-wrapper', {
        tabs: store.getState('tabs'),
        onclick: function (ev) {
            store.setState('currentWowPvpBracket', ev.value);
        }
    });

    var classesDropDownControl = new DropDownControl('#wow-classes-drop-down-wrapper', {
        data: wowClassesList,
        text: 'Select class',
        onChange: function(value) {
            store.setState('selectedWowClass', value);
        }
    });

    store.addOnChangeEventListener(function (ev) {
        renderLadder(ev.store);
    });

    function renderLadder(store) {
        var state = store.getAllState();

        var params = {
            pvpBracket: state.currentWowPvpBracket,
            wowclass: state.selectedWowClass
        };

        var url = apiService.formUrlForLadderGrid(params);

        var gridControl = new GridControl('#content-ladder-wrapper', {
            url: url,
            tableHeaders: [
                {
                    displayName: '№',
                    logicalName: 'ranking'
                },
                {
                    displayName: 'Name',
                    logicalName: 'name'
                },
                {
                    displayName: 'Realm',
                    logicalName: 'realmName'
                },
                {
                    displayName: 'Wins',
                    logicalName: 'seasonWins'
                },
                {
                    displayName: 'Losses',
                    logicalName: 'seasonLosses'
                },
                {
                    displayName: 'Rating',
                    logicalName: 'rating'
                }
            ]
        });
    }
});