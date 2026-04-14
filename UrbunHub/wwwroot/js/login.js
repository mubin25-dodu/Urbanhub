
let hovecard = document.getElementById('hovercard');
let questiopn = document.getElementById('question');
let togglebtn = document.getElementById('togglebtn');   

document.getElementById('togglebtn').addEventListener('click', function (event) {
    console.log('Toggle button clicked');

    if (hovecard.style.left === '50%') {
        hovecard.style.left = '0';
        hovecard.style.borderRadius = '25px 100px 0px 25px';
        questiopn.innerHTML = 'Don\'t have an account?';
        togglebtn.innerHTML = 'Sign Up';
    } else {
        hovecard.style.left = '50%';
        hovecard.style.borderRadius = '100px 25px 25px 0px';
        questiopn.innerHTML = 'Already have an account?';
        togglebtn.innerHTML = 'Login';
    }
});