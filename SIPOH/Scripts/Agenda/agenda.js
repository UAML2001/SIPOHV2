var currentDateOfWeek = new Date();
var getDates = function (startDate, endDate) {
    var dates = [],
        currentDate = startDate,
        addDays = function (days) {
            var date = new Date(this.valueOf());
            date.setDate(date.getDate() + days);
            return date;
        };
    while (currentDate <= endDate) {
        dates.push(currentDate);
        currentDate = addDays.call(currentDate, 1);
    }
    return dates;
};

$(document).ready(function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
        editable: true,
        initialView: 'resourceTimelineDay',
        initialDate: new Date(),
        locale: 'es',
        customButtons: {
            prev: {
                text: 'Anterior',
                click: function () {
                    currentDateOfWeek.setDate(currentDateOfWeek.getDate() - 7);
                    calendar.prev();
                }
            },
            next: {
                text: 'Siguiente',
                click: function () {
                    currentDateOfWeek.setDate(currentDateOfWeek.getDate() + 7);
                    calendar.next();
                }
            },
        },
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'resourceTimelineDay,resourceTimelineTwoDays,resourceTimelineMonth'
        },
        views: {
            resourceTimelineDay: {
                slotLabelFormat: {
                    hour: 'numeric',
                    minute: '2-digit',
                    hour12: true
                }
            },
            resourceTimelineTwoDays: {
                type: 'resourceTimeline',
                duration: { days: 2 },
                buttonText: '2 días',
                slotLabelFormat: {
                    hour: 'numeric',
                    minute: '2-digit',
                    hour12: true
                }
            },
            resourceTimelineMonth: {
                type: 'resourceTimeline',
                duration: { month: 1 },
                buttonText: 'Mes',
                slotLabelFormat: [
                    { weekday: 'short' }, // Display day of the week
                    { day: 'numeric', month: 'numeric' } // Display day and month
                ]
            }
        },
        buttonText: {
            today: 'Hoy',
            day: 'Día',
            resourceTimelineDay: 'Día',
            resourceTimelineTwoDays: '2 días',
            resourceTimelineMonth: 'Mes'
        },
        height: 'auto',
        contentHeight: 'auto',
        slotMinWidth: 50,
        refetchResourcesOnNavigate: true,
        resourceAreaHeaderContent: 'Salas',
        resources: [
            { id: 'sala1', title: 'Sala 1' },
            { id: 'sala2', title: 'Sala 2' },
            { id: 'sala3', title: 'Sala 3' },
            { id: 'sala4', title: 'Sala 4' },
            { id: 'sala5', title: 'Sala 5' },
            { id: 'sala6', title: 'Sala 6' },
            { id: 'sala7', title: 'Sala 7' },
            { id: 'sala8', title: 'Sala 8' },
            { id: 'sala9', title: 'Sala 9' },
            {
                id: 'sala9F', title: 'Foráneos', children: [
                    { id: 'subsalaA', title: 'Sub Sala A' },
                    { id: 'subsalaB', title: 'Sub Sala B' }
                ]
            }
        ],
        events: [
            {
                title: 'Evento Diario Sala 1',
                start: new Date().toISOString().substr(0, 10) + 'T08:00:00',
                resourceId: 'sala1'
            },
            {
                title: 'Evento Diario Sala 2',
                start: new Date().toISOString().substr(0, 10) + 'T10:00:00',
                resourceId: 'sala2'
            },
            {
                title: 'Evento Diario Sala 3',
                start: new Date().toISOString().substr(0, 10) + 'T12:00:00',
                resourceId: 'sala3'
            },
            {
                title: 'Evento Diario Sala 4',
                start: new Date().toISOString().substr(0, 10) + 'T14:00:00',
                resourceId: 'sala4'
            },
            {
                title: 'Evento Diario Sala 5',
                start: new Date().toISOString().substr(0, 10) + 'T16:00:00',
                resourceId: 'sala5'
            },
            {
                title: 'Evento Diario Sala 6',
                start: new Date().toISOString().substr(0, 10) + 'T18:00:00',
                resourceId: 'sala6'
            },
            {
                title: 'Evento Diario Sala 7',
                start: new Date().toISOString().substr(0, 10) + 'T20:00:00',
                resourceId: 'sala7'
            },
            {
                title: 'Evento Diario Sala 8',
                start: new Date().toISOString().substr(0, 10) + 'T22:00:00',
                resourceId: 'sala8'
            },
            {
                title: 'Evento Diario Sala 9',
                start: new Date().toISOString().substr(0, 10) + 'T23:59:00',
                resourceId: 'sala9'
            },
            {
                title: 'Evento Diario Sub Sala A',
                start: new Date().toISOString().substr(0, 10) + 'T15:00:00',
                resourceId: 'subsalaA'
            },
            {
                title: 'Evento Diario Sub Sala B',
                start: new Date().toISOString().substr(0, 10) + 'T17:00:00',
                resourceId: 'subsalaB'
            }
        ]
    });

    calendar.render();
});
