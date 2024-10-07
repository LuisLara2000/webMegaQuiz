﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

// estos valores se van a cargar cada vez que se solicite un apregunta
var listaPreguntas = [];
var cantidadTotalg = 0;
var actualg = 0;// en relacion a la cantidad total



// cada que inicia la pagina de listar, se llama a esta funcion
function obtenerLosIds() {
    var cadenaLista = '[';
    var d = document.getElementsByClassName("idPre");
    var c = localStorage.getItem("cargar");
    if (c == "true")
    {
        c = localStorage.setItem("cargar", false);
        console.log(c);
        for (let i = 0; i < d.length; i++) {
            // obtengo el numero del texto en el html
            listaPreguntas.push(parseInt(d.item(i).innerHTML));

            // guardo el valor en una cadena
            cadenaLista += parseInt(d.item(i).innerHTML);
            // cuando sea el ultimo valor ya no le ponemos una coma
            if (i != d.length - 1) {
                cadenaLista += ",";
            }
        }
        //le ponemos un corchete
        cadenaLista += "]";
        // lo guardamos(se guarda como lista)
        localStorage.setItem("listaP", cadenaLista);
        // inicializamos el indice que recorrera la lista guardada
        localStorage.setItem("idP", parseInt(0));
    }


}

// cada que inicie la pagina de mostrar una pregunta
function sigPregunta() {
    // recupero los datos
    cantidadTotalg = parseInt(localStorage.getItem("canTotalPreguntas"));
    actualg = parseInt(localStorage.getItem("actual")) + 1;
    let idUltimo;
    if (actualg <= cantidadTotalg) {
        // recupero la lista de preguntas con orden aleatoreo
        listaPreguntas = JSON.parse(localStorage.getItem("listaP"));
        // trato de obtener el ultimo id en el que se quedo la lista

        try {
            idUltimo = parseInt(localStorage.getItem("idP"));
        }
        catch {
            idUltimo = 0;
        }
        // le sumo uno para avanzar en la lista
        idUltimo += 1;
        // valido que no me vaya a llegar al final de la lista
        // el +1 es por si las dudas (quitarlo)
        
        // devuelvo las 3 preguntas desde donde me quede
        //for (let i = idUltimo; i < idUltimo+3; i++) {
        //   console.log(listaPreguntas[i]);
        //}
        // devuelvo la sig pregunta
        document.getElementById("preId").innerHTML = listaPreguntas[idUltimo];

        // actualizo el ultimo id que me quede
        localStorage.setItem("idP", parseInt(idUltimo));
        
        // actualizo los valores en el html
        document.getElementById("preId").value = listaPreguntas[idUltimo];
        document.getElementById("preTotal").innerHTML = cantidadTotalg;
        document.getElementById("preActual").innerHTML = actualg;
        // los guardo en el localstore
        localStorage.setItem("canTotalPreguntas", parseInt(cantidadTotalg));
        localStorage.setItem("actual", parseInt(actualg));
        localStorage.setItem("idP", parseInt(idUltimo));
    }
    else {
        idUltimo = parseInt(localStorage.getItem("idP"));
        idUltimo += 1;
        localStorage.setItem("idP", parseInt(idUltimo));
        document.getElementById("preId").value = -1;
    }


}

// cada que se presione el boton de seleccionar modo
function inicializarModo(modo) {
    
    if (parseInt(modo) == 3) {
        localStorage.setItem("canTotalPreguntas", parseInt(3));
        localStorage.setItem("actual", parseInt(1));

        // recupero la lista de preguntas con orden aleatoreo
        listaPreguntas = JSON.parse(localStorage.getItem("listaP"));
        //recupero el ultimo id 
        idUltimo = parseInt(localStorage.getItem("idP"));
        // valido si quedan preguntas suficientes
        if (idUltimo + modo <= listaPreguntas.length) {
            document.getElementById("preId").value = listaPreguntas[idUltimo];
            document.getElementById("preTotal").innerHTML = 3;
            document.getElementById("preActual").innerHTML = 1;
        }
        else {
            console.log("reiniciar lista");
            localStorage.setItem("cargar", true);
            obtenerLosIds();
            listaPreguntas = JSON.parse(localStorage.getItem("listaP"));
            idUltimo = parseInt(localStorage.getItem("idP"));
            document.getElementById("preId").value = listaPreguntas[idUltimo];
            document.getElementById("preTotal").innerHTML = 3;
            document.getElementById("preActual").innerHTML = 1;
        }
    }
    else if (parseInt(modo) == 5) {
        localStorage.setItem("canTotalPreguntas", parseInt(5));
        localStorage.setItem("actual", parseInt(1));

        // recupero la lista de preguntas con orden aleatoreo
        listaPreguntas = JSON.parse(localStorage.getItem("listaP"));
        //recupero el ultimo id 
        idUltimo = parseInt(localStorage.getItem("idP"));
        // valido si quedan preguntas suficientes
        if (idUltimo + modo <= listaPreguntas.length) {
            document.getElementById("preId").value = listaPreguntas[idUltimo];
            document.getElementById("preTotal").innerHTML = 5;
            document.getElementById("preActual").innerHTML = 1;
        }
        else {
            console.log("reiniciar lista");
            localStorage.setItem("cargar", true);
            obtenerLosIds();
            listaPreguntas = JSON.parse(localStorage.getItem("listaP"));
            idUltimo = parseInt(localStorage.getItem("idP"));
            document.getElementById("preId").value = listaPreguntas[idUltimo];
            document.getElementById("preTotal").innerHTML = 5;
            document.getElementById("preActual").innerHTML = 1;
        }
    }
    else {
        localStorage.setItem("canTotalPreguntas", parseInt(10));
        localStorage.setItem("actual", parseInt(1));

        // recupero la lista de preguntas con orden aleatoreo
        listaPreguntas = JSON.parse(localStorage.getItem("listaP"));
        //recupero el ultimo id 
        idUltimo = parseInt(localStorage.getItem("idP"));
        // valido si quedan preguntas suficientes
        if (idUltimo + modo <= listaPreguntas.length) {
            document.getElementById("preId").value = listaPreguntas[idUltimo];
            document.getElementById("preTotal").innerHTML = 10;
            document.getElementById("preActual").innerHTML = 1;
        }
        else {
            console.log("reiniciar lista");
            localStorage.setItem("cargar", true);
            obtenerLosIds();
            listaPreguntas = JSON.parse(localStorage.getItem("listaP"));
            idUltimo = parseInt(localStorage.getItem("idP"));
            document.getElementById("preId").value = listaPreguntas[idUltimo];
            document.getElementById("preTotal").innerHTML = 10;
            document.getElementById("preActual").innerHTML = 1;
        }
    }
}

function mostrarNumeroDePregunta() {
    document.getElementById("nPreguntaActual").innerHTML = localStorage.getItem("actual");
    document.getElementById("nPreguntaTotal").innerHTML = localStorage.getItem("canTotalPreguntas");
}

function ocultar(idElementoOcultar,IdElementoMostrar) {
    document.getElementById(idElementoOcultar).style.visibility = "hidden";
    document.getElementById(IdElementoMostrar).style.visibility = "visible";

}

function desOcultar(IdElementoMostrar) {
    document.getElementById(IdElementoMostrar).style.visibility = "visible";
}

function seleccionarBotonModo(botonSeleccionado) {
    document.getElementById("bModo3").style.backgroundColor = "blue";
    document.getElementById("bModo5").style.backgroundColor = "blue";
    document.getElementById("bModo10").style.backgroundColor = "blue";
    document.getElementById(botonSeleccionado).style.backgroundColor = "red";
}