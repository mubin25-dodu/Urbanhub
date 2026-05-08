//getDistances();
//async function getDistances() {
//    const cards = document.querySelectorAll(".locations");
//    const dbLocations = [];

//    // 1. Extract data from DOM
//    cards.forEach((card) => {
//        dbLocations.push({
//            x: Number(card.getAttribute("X")), // Usually Longitude
//            y: Number(card.getAttribute("Y")), // Usually Latitude
//            cardId: card.getAttribute("CardID"),
//            element: card // Store reference to update UI later
//        });
//    });

//    if (dbLocations.length === 0) return;

//    // 2. Get User Location with error handling
//    navigator.geolocation.getCurrentPosition(
//        async (position) => {
//            const userLoc = {
//                lat: position.coords.latitude,
//                lng: position.coords.longitude,
//            };

//            const googleMaps = window.google?.maps;
//            if (!googleMaps) {
//                console.error("Google Maps SDK not loaded. Check your script tag.");
//                return;
//            }

//            const service = new googleMaps.DistanceMatrixService();

//            // 3. Prepare Request
//            const request = {
//                origins: [userLoc],
//                // Crucial: Ensure mapping is {lat: Y, lng: X}
//                destinations: dbLocations.map((loc) => ({ lat: loc.y, lng: loc.x })),
//                travelMode: googleMaps.TravelMode.DRIVING,
//                unitSystem: googleMaps.UnitSystem.METRIC
//            };

//            // 4. Get and Display results
//            service.getDistanceMatrix(request, (result, status) => {
//                if (status === "OK") {
//                    const distances = result.rows[0].elements;

//                    distances.forEach((element, index) => {
//                        if (element.status === "OK") {
//                            const distanceText = element.distance.text;
//                            const durationText = element.duration.text;

//                            const cardUI = dbLocations[index].element;
//                            const distanceDisplay = cardUI.querySelector(".Location");
//                            if (distanceDisplay) {
//                                distanceDisplay.innerText = `${distanceText} (${durationText})`;
//                            }

//                            console.log(`Card ${dbLocations[index].cardId}: ${distanceText}`);
//                        } else {
//                            console.warn(`Route not found for card ${dbLocations[index].cardId}`);
//                        }
//                    });
//                } else {
//                    console.error("Distance Matrix failed: " + status);
//                }
//            });
//        },
//        (error) => {
//            console.error("Geolocation failed. User denied permission or signal lost.", error);
//        }
//    );
//}
