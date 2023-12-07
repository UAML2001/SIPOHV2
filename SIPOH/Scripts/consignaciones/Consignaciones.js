
const enlaces = document.querySelectorAll(".onSplash");
const nombreSpan = document.getElementById("dataSplash");

// Itera sobre los enlaces y agrega un evento clic a cada uno
enlaces.forEach((enlace) => {
    enlace.addEventListener("click", () => {

        const nombre = enlace.innerHTML
        nombreSpan.innerHTML = nombre;
        
    });
});

const radioButtonsQuienIngresa = document.querySelectorAll('input[name="flexRadioQuienIngresa"]');

radioButtonsQuienIngresa.forEach(radioButtonQuienIngresa => {
    radioButtonQuienIngresa.addEventListener('change', function () {
        if (this.checked) {
            const selectedValue = this.id;
            let valueSpan = document.getElementById('tipoPersona');
            valueSpan.textContent = selectedValue;
        }
    })
});

//const dropDownVictimaPersona = document.getElementById('inputVictimaPersona');

//dropDownVictimaPersona.addEventListener('change', function () {
//    const selectedValue = dropDownVictimaPersona.value;
//    console.log(selectedValue);
//       var elementosMoral.document.querySelectorAll('tipoPersonaMoral');
//       var elementosFisica.document.querySelectorAll('tipoPersonaFisica');
//    if (selectedValue == 'Moral') {
//        console.log(selectedValue + "1");
//    } else if (selectedValue == 'Fisica') {
//        console.log(selectedValue + "2");

//    }
//});
const dropDownVictimaPersona = document.getElementById('inputVictimaPersona');
const elementosMoral = document.querySelectorAll('.tipoPersonaMoral');
const elementosFisica = document.querySelectorAll('.tipoPersonaFisica');

dropDownVictimaPersona.addEventListener('change', function () {
    const selectedValue = dropDownVictimaPersona.value;

    if (selectedValue === 'Moral') {
        elementosMoral.forEach(function (element) {
            element.style.display = 'block'; 
        });
        elementosFisica.forEach(function (element) {
            element.style.display = 'none';  
        });
    } else if (selectedValue === 'Fisica') {
        elementosMoral.forEach(function (element) {
            element.style.display = 'none';  
        });
        elementosFisica.forEach(function (element) {
            element.style.display = 'block'; 
        });
    }
});



