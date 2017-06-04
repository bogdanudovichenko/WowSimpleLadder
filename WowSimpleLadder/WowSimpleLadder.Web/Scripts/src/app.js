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
        selectedWowClass: 0,
        selectedWowSpec: 0
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
        onChange: function (value) {
            store.setState('selectedWowClass', value);

            if (!value) {
                document.getElementById('wow-specs-drop-down-wrapper').innerHTML = '';
                store.setState('selectedWowSpec', 0);
                return;
            }

            var wowSpecsList = _.filter(wowClassesList, function (wc) { return wc.value === value })[0].specs;

            var specsDropDownList = new DropDownControl('#wow-specs-drop-down-wrapper', {
                data: wowSpecsList,
                text: 'Select spec',
                onChange: function (value) {
                    store.setState('selectedWowSpec', value);
                }
            });
        }
    });

    store.addOnChangeEventListener(function (ev) {
        renderLadder(ev.store);
    });

    function renderLadder(store) {
        var state = store.getAllState();

        var params = {
            pvpBracket: state.currentWowPvpBracket,
            wowclass: state.selectedWowClass,
            specid: state.selectedWowSpec
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
                    logicalName: 'name',
                    template: function (val, item) {
                        var url = 'https://worldofwarcraft.com/' + getBlizzardLocaleStr(item.locale) + '/character/' + item.realmName + '/' + val;
                        url = decodeURIComponent(url);

                        var link = document.createElement('a');
                        link.classList.add('wow-class-name-link');
                        link.href = url;
                        link.innerHTML = '<span class="wow-class-name" style="color:' + wowClassesList[item.classId].color + ';">' + val + '</span>'

                        return link.outerHTML;
                    }
                },
                {
                    displayName: 'Spec',
                    logicalName: 'specId',
                    template: function (val, item) {
                        var specs = _.filter(wowClassesList, function (wc) { return wc.value === item.classId; })[0].specs;
                        var spec = _.filter(specs, function (s) {
                            return s.value == val;
                        })[0]

                        if(spec) {
                            return spec.displayValue;
                        } 

                        return val;
                    }
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

function getBlizzardLocaleStr(locale) {
    switch (locale) {
        case 0:
            return 'en-us';
        case 1:
            return 'en-gb'
    }
}