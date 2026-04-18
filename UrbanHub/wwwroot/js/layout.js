let arrow = document.getElementById('arrow');  
let navbar = document.getElementById('navbar');
let btn_titles = document.querySelectorAll('.btn_titles');
//let 

//navbar shrink and expand

document.getElementById('arrow').addEventListener('click', function (event) {
    if (navbar.style.width < '50px') {
        console.log('Arrow button shrink');
        navbar.style.width = '50px';
        arrow.style.transform = 'rotate(180deg)';
        setTimeout(function () { 
        btn_titles.forEach(function(b) {
            b.style.display = 'none';
        });
        }, 250); 
    } else {
        navbar.style.width = '250px';
        console.log('Arrow button expand');
        arrow.style.transform = 'rotate(0deg)';
        setTimeout(function () { 
        btn_titles.forEach(function (b) {
            b.style.display = 'inline-block';
        });
        }, 200);
    }
});

//submit the data

//let submitBtn = document.getElementById('submit-btn');
//submitBtn.addEventListener('click', function (event) {

//});