var map;
var pin;

/**
 * Carga el mapa para la vista Create.
 * @param {string} credentials - La clave de las credenciales de Bing Maps.
 */
function loadMapForCreate(credentials) {
    map = new Microsoft.Maps.Map(document.getElementById('mapContainer'), {
        credentials: credentials,
        center: new Microsoft.Maps.Location(-17.338097, -66.218838), // Ubicación inicial
        zoom: 15
    });

    Microsoft.Maps.Events.addHandler(map, 'click', addPin);
}

/**
 * Carga el mapa para la vista Edit.
 * @param {string} credentials - La clave de las credenciales de Bing Maps.
 * @param {number} latitude - Latitud inicial.
 * @param {number} longitude - Longitud inicial.
 */
function loadMapForEdit(credentials, latitude, longitude) {
    const initialLocation = new Microsoft.Maps.Location(latitude, longitude);

    map = new Microsoft.Maps.Map(document.getElementById('mapContainer'), {
        credentials: credentials,
        center: initialLocation,
        zoom: 15
    });

    // Colocar un pin en la ubicación inicial
    addInitialPin(initialLocation);

    Microsoft.Maps.Events.addHandler(map, 'click', addPin);
}

/**
 * Agrega un pin al mapa en la ubicación especificada por el evento de clic.
 * @param {Object} e - Evento que contiene la ubicación.
 */
function addPin(e) {
    const location = e.location;

    // Remover pin previo
    if (pin) {
        map.entities.remove(pin);
    }

    // Crear nuevo pin
    pin = new Microsoft.Maps.Pushpin(location, { color: 'red' });
    map.entities.push(pin);

    // Actualizar valores de latitud y longitud
    const latitudeInput = document.querySelector('input[name="Latitude"]');
    const longitudeInput = document.querySelector('input[name="Longitude"]');
    if (latitudeInput && longitudeInput) {
        latitudeInput.value = location.latitude.toFixed(6).replace(',', '.');
        longitudeInput.value = location.longitude.toFixed(6).replace(',', '.');
    }
}

/**
 * Agrega un pin inicial al mapa.
 * @param {Object} location - Ubicación inicial.
 */
function addInitialPin(location) {
    pin = new Microsoft.Maps.Pushpin(location, { color: 'red' });
    map.entities.push(pin);
}
