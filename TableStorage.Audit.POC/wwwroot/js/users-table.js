$(document).ready(function () {
    let table = $('#users-table')
        .DataTable({
                       paging: false,
                       searching: false,
                       sortable: false,
                       ajax: {
                           url: window.origin + '/user/get-all',
                           dataSrc: '',
                           
                       },
                       columns: [
                           { name: 'firstName', title: 'First Name' },
                           { name: 'lastName', title: 'Last Name' }
                       ]
                   });
});