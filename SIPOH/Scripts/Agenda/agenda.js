$(function () {

    /* inicializar los eventos externos */
    function ini_events(ele) {
        ele.each(function () {
            // crear un Objeto Evento
            var eventObject = {
                title: $.trim($(this).text()) // usar el texto del elemento como título del evento
            }

            // almacenar el Objeto Evento en el elemento DOM para poder acceder a él más tarde
            $(this).data('eventObject', eventObject)

            // hacer el evento arrastrable usando jQuery UI
            $(this).draggable({
                zIndex: 1070,
                revert: true, // hará que el evento vuelva a su
                revertDuration: 0  // posición original después de arrastrar
            })
        })
    }

    ini_events($('#external-events div.external-event'))

    /* inicializar el calendario */
    var date = new Date()
    var d = date.getDate(),
        m = date.getMonth(),
        y = date.getFullYear()

    var Calendar = FullCalendar.Calendar;
    var Draggable = FullCalendar.Draggable;

    var containerEl = document.getElementById('external-events');
    var checkbox = document.getElementById('drop-remove');
    var calendarEl = document.getElementById('calendar');

    // inicializar los eventos externos
    new Draggable(containerEl, {
        itemSelector: '.external-event',
        eventData: function (eventEl) {
            return {
                title: eventEl.innerText,
                backgroundColor: window.getComputedStyle(eventEl, null).getPropertyValue('background-color'),
                borderColor: window.getComputedStyle(eventEl, null).getPropertyValue('background-color'),
                textColor: window.getComputedStyle(eventEl, null).getPropertyValue('color'),
            };
        }
    });

    var calendar = new Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        themeSystem: 'bootstrap',
        locale: 'es', // Establecer el idioma a español
        buttonText: { // Traducir los botones
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Día'
        },
        allDayText: 'Todo el día', // Traducir "all-day"
        events: [
            {
                title: 'Audiencia 1023/2023',
                start: new Date(y, m, 1),
                backgroundColor: '#f56954', // rojo
                borderColor: '#f56954', // rojo
                allDay: true
            },
            {
                title: 'Audiencia 0356/2023',
                start: new Date(y, m, d - 5),
                end: new Date(y, m, d - 2),
                backgroundColor: '#f39c12', // amarillo
                borderColor: '#f39c12' // amarillo
            }
         
        ],
        editable: true,
        droppable: true, // esto permite que cosas se arrastren al calendario
        drop: function (info) {
            // Verificar si la casilla "Remover después de asignar" está marcada
            if (checkbox.checked) {
                // Si está marcada, eliminar el elemento de la lista de eventos externos
                info.draggedEl.parentNode.removeChild(info.draggedEl);
            }
        },
        eventClick: function (info) {
            if (confirm('¿Quieres eliminar esta audiencia?')) {
                // Eliminar el evento del calendario
                info.event.remove();
            }
        }
    });

    calendar.render();

    // Configurar el "basurero"
    $('#trash').droppable({
        accept: '.fc-event',
        drop: function (event, ui) {
            var eventId = ui.helper.data('fcSeg').eventRange.def.id;
            var event = calendar.getEventById(eventId);
            if (event) {
                event.remove();
            }
        }
    });

    /* AÑADIR EVENTOS */
    var currColor = '#3c8dbc' // Rojo por defecto
    // Botón del selector de color
    $('#color-chooser > li > a').click(function (e) {
        e.preventDefault()
        // Guardar color
        currColor = $(this).css('color')
        // Añadir efecto de color al botón
        $('#add-new-event').css({
            'background-color': currColor,
            'border-color': currColor
        })
    })
    $('#add-new-event').click(function (e) {
        e.preventDefault()
        // Obtener valor y asegurarse de que no es nulo
        var val = $('#new-event').val()
        if (val.length == 0) {
            return
        }

        // Crear eventos
        var event = $('<div />')
            .css({
                'background-color': currColor,
                'border-color': currColor,
                'color': '#fff'
            }).addClass('external-event')
            .text(val)
        $('#external-events').prepend(event)

        // Añadir funcionalidad de arrastrar
        ini_events(event)

        // Eliminar evento del campo de texto
        $('#new-event').val('')
    })
});
