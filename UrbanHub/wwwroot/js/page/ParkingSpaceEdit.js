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


// SCHEDULE JSON
let schedules = [];

// LOAD OLD SCHEDULE DATA
const availableInput =
    document.getElementById('Available');

if (availableInput && availableInput.value) {

    try {

        schedules = JSON.parse(
            availableInput.value
        );

    } catch {

        schedules = [];
    }
}


// ADD SCHEDULE
function addSchedule() {

    const day =
        document.getElementById('day').value;

    const startTime =
        document.getElementById('startTime').value;

    const endTime =
        document.getElementById('endTime').value;

    // VALIDATION
    if (!startTime || !endTime) {

        document.getElementById('scheduleError')
            .innerText =
            "Start time and end time are required.";

        return;
    }

    document.getElementById('scheduleError')
        .innerText = "";

    // JSON OBJECT
    const data = {

        Day: day,
        StartTime: startTime,
        EndTime: endTime
    };

    schedules.push(data);

    updateScheduleUI();
}


// UPDATE SCHEDULE UI
function updateScheduleUI() {

    const scheduleList =
        document.getElementById('scheduleList');

    scheduleList.innerHTML = "";

    schedules.forEach((item, index) => {

        scheduleList.innerHTML += `

            <div class="d-flex
                        justify-content-between
                        align-items-center
                        border
                        rounded-4
                        p-3
                        mb-2">

                <div>

                    <strong>${item.Day}</strong>

                    <div class="text-muted">
                        ${item.StartTime} - ${item.EndTime}
                    </div>

                </div>

                <button type="button"
                        class="btn btn-sm btn-danger"
                        onclick="removeSchedule(${index})">

                    Remove

                </button>

            </div>
        `;
    });

    // SAVE JSON STRING
    if (schedules.length === 0) {

        document.getElementById('Available').value = "";

    }
    else {

        document.getElementById('Available').value =
            JSON.stringify(schedules);
    }
}


// REMOVE SCHEDULE
function removeSchedule(index) {

    schedules.splice(index, 1);

    updateScheduleUI();
}


// REQUIRED SCHEDULE VALIDATION
document.querySelector("form")
    .addEventListener("submit", function (e) {

        if (schedules.length === 0) {

            e.preventDefault();

            const error =
                document.getElementById('scheduleError');

            error.innerText =
                "Available schedule is required.";

            error.scrollIntoView({

                behavior: "smooth",
                block: "center"
            });
        }
    });


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


// LOAD OLD SCHEDULE UI
window.addEventListener('load', function () {

    updateScheduleUI();

});