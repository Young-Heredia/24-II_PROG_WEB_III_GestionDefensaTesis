var map;
var pin;

/**
 * 
 * @param {string} credentials
 */
function loadMapForCreate(credentials) {
    map = new Microsoft.Maps.Map(document.getElementById('mapContainer'), {
        credentials: credentials,
        center: new Microsoft.Maps.Location(-17.338097, -66.218838),
        zoom: 15
    });

    Microsoft.Maps.Events.addHandler(map, 'click', addPin);
}

/**
 
 * @param {string} credentials 
 * @param {number} latitude 
 * @param {number} longitude 
 */
function loadMapForEdit(credentials, latitude, longitude) {
    const initialLocation = new Microsoft.Maps.Location(latitude, longitude);

    map = new Microsoft.Maps.Map(document.getElementById('mapContainer'), {
        credentials: credentials,
        center: initialLocation,
        zoom: 15
    });


    addInitialPin(initialLocation);

    Microsoft.Maps.Events.addHandler(map, 'click', addPin);
}

/**
 * 
 * @param {Object} e 
 */
function addPin(e) {
    const location = e.location;


    if (pin) {
        map.entities.remove(pin);
    }


    pin = new Microsoft.Maps.Pushpin(location, { color: 'red' });
    map.entities.push(pin);


    const latitudeInput = document.querySelector('input[name="Latitude"]');
    const longitudeInput = document.querySelector('input[name="Longitude"]');
    if (latitudeInput && longitudeInput) {
        latitudeInput.value = location.latitude.toFixed(6).replace(',', '.');
        longitudeInput.value = location.longitude.toFixed(6).replace(',', '.');
    }
}

/**
 * 
 * @param {Object} location 
 */
function addInitialPin(location) {
    pin = new Microsoft.Maps.Pushpin(location, { color: 'red' });
    map.entities.push(pin);
}
