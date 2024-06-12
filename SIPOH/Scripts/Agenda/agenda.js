$(document).ready(function () {
    function ini_events(ele) {
        ele.each(function () {
            var eventObject = {
                title: $.trim($(this).text())
            };
            $(this).data('eventObject', eventObject);
            $(this).draggable({
                zIndex: 1070,
                revert: true,
                revertDuration: 0
            });
        });
    }

    ini_events($('#external-events div.external-event'));

    var Calendar = FullCalendar.Calendar;
    var Draggable = FullCalendar.Draggable;

    var containerEl = document.getElementById('external-events');
    var checkbox = document.getElementById('drop-remove');
    var calendarEl = document.getElementById('calendar');

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
        initialView: 'timeGridDay',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'timeGridDay'
        },
        themeSystem: 'bootstrap',
        locale: 'es',
        buttonText: {
            today: 'Hoy',
            day: 'Día'
        },
        allDayText: 'Todo el día',
        editable: true,
        droppable: true,
        drop: function (info) {
            if (checkbox.checked) {
                info.draggedEl.parentNode.removeChild(info.draggedEl);
            }
        },
        eventClick: function (info) {
            $('#modalAudiencias').modal('show');
            $('#new-event').val(info.event.title);
            $('#descripcionAgenda').val(info.event.extendedProps.description);
            $('#ddlJuecesAgenda').val(info.event.extendedProps.judge);

            $('#add-new-event').off('click').on('click', function () {
                info.event.setProp('title', $('#new-event').val());
                info.event.setExtendedProp('description', $('#descripcionAgenda').val());
                info.event.setExtendedProp('judge', $('#ddlJuecesAgenda').val());
                $('#modalAudiencias').modal('hide');
            });

            $('#delete-event').off('click').on('click', function () {
                info.event.remove();
                $('#modalAudiencias').modal('hide');
            });
        },
        eventDidMount: function (info) {
            if (info.view.type === 'timeGridDay') {
                var tr = $(info.el).closest('tr');
                if (!tr.find('.fc-name-cell').length) {
                    for (var i = 1; i <= 9; i++) {
                        tr.append('<td class="fc-name-cell">Sala ' + i + '</td>');
                    }
                }
            }
        },
        viewDidMount: function (view) {
            addDummyRows(view);
        }
    });

    function addDummyRows(view) {
        if (view.type === 'timeGridDay') {
            var table = $(view.el).find('table');
            if (!table.find('thead tr .fc-name-header').length) {
                var headerRow = '<tr>';
                headerRow += '<th></th>'; // Añadir una celda vacía para la primera columna
                for (var i = 1; i <= 9; i++) {
                    headerRow += '<th class="fc-name-header">Sala ' + i + '</th>';
                }
                headerRow += '</tr>';
                table.find('thead').append(headerRow);
            }
            table.find('tbody tr').each(function () {
                if (!$(this).find('.fc-name-cell').length) {
                    var row = '';
                    row += '<td></td>'; // Añadir una celda vacía para la primera columna
                    for (var i = 1; i <= 9; i++) {
                        row += '<td class="fc-name-cell"></td>';
                    }
                    $(this).append(row);
                }
            });

            // Agregar filas dummy para mostrar las celdas incluso sin eventos
            var tbody = table.find('tbody');
            if (tbody.find('.fc-dummy-row').length === 0) {
                for (var i = 0; i < 1; i++) {
                    var tr = $('<tr class="fc-dummy-row"></tr>');
                    tr.append('<td></td>'); // Añadir una celda vacía para la primera columna
                    for (var j = 0; j < 9; j++) {
                        tr.append('<td class="fc-name-cell">Sala ' + (j + 1) + '</td>');
                    }
                    tbody.append(tr);
                }
            }
        }
    }

    // Inicializar el calendario y asegurar que las celdas se agreguen correctamente
    calendar.render();
    addDummyRows(calendar.view);

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

    var currColor = '#3c8dbc';
    $('#color-chooser > li > a').click(function (e) {
        e.preventDefault();
        currColor = $(this).css('color');
        $('#add-new-event').css({
            'background-color': currColor,
            'border-color': currColor
        });
    });
    $('#add-new-event').click(function (e) {
        e.preventDefault();
        var val = $('#new-event').val();
        if (val.length == 0) {
            return;
        }

        var newEvent = {
            title: val,
            start: new Date(),
            backgroundColor: currColor,
            borderColor: currColor,
            textColor: '#fff'
        };

        calendar.addEvent(newEvent);
        $('#new-event').val('');
    });

    $('#abrirModalAgenda').click(function () {
        $('#modalAudiencias').modal('show');
        $('#add-new-event').off('click').on('click', function () {
            var title = $('#new-event').val();
            var description = $('#descripcionAgenda').val();
            var judge = $('#ddlJuecesAgenda').val();
            var newEvent = {
                title: title,
                start: new Date(),
                backgroundColor: currColor,
                borderColor: currColor,
                description: description,
                judge: judge,
                allDay: true
            };
            calendar.addEvent(newEvent);
            $('#modalAudiencias').modal('hide');
        });
    });

    // Asegurar que la vista se monte correctamente después de cualquier postback parcial
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {
        calendar.changeView('timeGridDay');
        addDummyRows(calendar.view);
    });

    // Asegurarse de que las celdas se inicialicen correctamente al cargar la página
    window.onload = function () {
        calendar.changeView('timeGridDay');
        addDummyRows(calendar.view);
    };
});
