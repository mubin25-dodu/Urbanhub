// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let notibtn = document.getElementById("notificationbtn");
let refreashbtn = document.getElementById("Refreash");
let list = document.getElementById("notificationList");

if (notibtn) {
  notibtn.addEventListener("click", function () {
    loaddata();
  });
}
if (refreashbtn) {
  refreashbtn.addEventListener("click", function () {
    loaddata();
  });
}

async function loaddata() {
  const response = await fetch("api/Notification");
  const data = await response.json();
  console.log(data);
  //Get a container

    let container = document.getElementById("notificationList");
    container.innerHTML = "";
  for (let item of data.data) {
    container.innerHTML += `<div class="card border-primary mb-3" style="max-width: 20rem;">
               <div class="card-header">${item.title}</div>
               <div class="card-body">
                 <p class="card-text">${item.message}</p>
               </div>
             </div>`;
  }
}
