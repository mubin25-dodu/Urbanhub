// IMAGE PREVIEW
const imgInp = document.getElementById('imgInp');
const preview = document.getElementById('preview');

if (imgInp) {

    imgInp.onchange = evt => {

        const [file] = imgInp.files;

        if (file) {

            preview.src = URL.createObjectURL(file);

            preview.classList.remove('d-none');
        }
    }
}


// MAP
let map;
let marker;

function initializeMap(lat, lng) {

    map = L.map('map').setView([lat, lng], 15);

    L.tileLayer(
        'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        {
            attribution: '© OpenStreetMap'
        }
    ).addTo(map);

    marker = L.marker([lat, lng], {
        draggable: true
    }).addTo(map);

    map.on('click', function (e) {

        marker.setLatLng(e.latlng);

        updateLocation(
            e.latlng.lat,
            e.latlng.lng
        );
    });

    marker.on('dragend', function () {

        const pos = marker.getLatLng();

        updateLocation(pos.lat, pos.lng);
    });
}

function updateLocation(lat, lng) {

    document.getElementById('latitude').value = lat;

    document.getElementById('longitude').value = lng;
}


// START MAP
initializeMap(
    parkingLatitude,
    parkingLongitude
);