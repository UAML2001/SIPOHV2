function toggleSeleccion(elemento) {
    // Desseleccionar todos los elementos
    var todosElementos = document.querySelectorAll(".btn-outline-light");
    todosElementos.forEach(function (item) {
        var dashSquare = item.querySelector("#x-squareJuicio");
        var checkSquare = item.querySelector("#check-square");
        var labelColorName = item.querySelector("#labelNombre");
        var labelColorLastname = item.querySelector("#labelApellido");
        var bgBoxVictima = item.querySelector("#ItemSelectedBox");
        var itemName = document.querySelector("#labelGetNameSelected");

        dashSquare.style.display = "inline";
        checkSquare.style.display = "none";
        labelColorName.classList.remove("text-success");
        labelColorName.classList.add("text-secondary");
        labelColorLastname.classList.remove("text-black");
        labelColorLastname.classList.add("text-secondary");
        bgBoxVictima.classList.remove("btn-light");
        itemName.innerHTML = "";
    });

    // Obtener los elementos del elemento actual
    var dashSquare = elemento.querySelector("#x-squareJuicio");
    var checkSquare = elemento.querySelector("#check-square");
    var labelColorName = elemento.querySelector("#labelNombre");
    var labelColorLastname = elemento.querySelector("#labelApellido");
    var bgBoxVictima = elemento.querySelector("#ItemSelectedBox");
    var itemName = document.querySelector("#labelGetNameSelected");

    // Toggle entre mostrar y ocultar para el elemento actual
    if (dashSquare.style.display === "none") {
        dashSquare.style.display = "inline";
        checkSquare.style.display = "none";
        labelColorName.classList.remove("text-success");
        labelColorName.classList.add("text-secondary");
        labelColorLastname.classList.remove("text-black");
        labelColorLastname.classList.add("text-secondary");
        bgBoxVictima.classList.remove("btn-light");
        itemName.innerHTML = "";
    } else {
        dashSquare.style.display = "none";
        checkSquare.style.display = "inline";
        labelColorName.classList.remove("text-secondary");
        labelColorName.classList.add("text-success");
        labelColorLastname.classList.remove("text-secondary");
        labelColorLastname.classList.add("text-black");
        bgBoxVictima.classList.add("btn-light");
        var a = labelColorLastname.innerHTML;
        itemName.innerHTML = a;
    }
}
function selectCheckbox(checkboxId) {
    // Desactivar todos los checkboxes
    document.querySelectorAll('.form-check-input').forEach(function (checkbox) {
        checkbox.checked = false;
    });

    // Activar solo el checkbox clicado
    document.getElementById(checkboxId).checked = true;
}
