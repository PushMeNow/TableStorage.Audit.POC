'use strict';

$(document).ready(function () {
// ------------------------------ User ----------------------------------

    const userTableElement = $('#users-table')
    const userModal = new bootstrap.Modal(document.getElementById('user-modal-window'));
    const saveUserBtn = document.getElementById('save-user-data');
    const userForm = document.getElementById('user-form');

    let saveUserHandler = null;

    const userDataTable = userTableElement
        .DataTable({
                       paging: false,
                       searching: false,
                       sorting: false,
                       ajax: {
                           url: '/user',
                           dataSrc: ''
                       },
                       columns: [
                           { data: 'firstName', title: 'First Name', orderable: false },
                           { data: 'lastName', title: 'Last Name', orderable: false },
                           { data: 'createdDate', title: 'Created Date', orderable: false },
                           { data: 'lastModified', title: 'Last Modified', orderable: false },
                           {
                               title: 'Actions',
                               render: (data, type, row, meta) => {
                                   return '<div class="btn-group" role="group">' +
                                       '<button type="button" class="btn btn-outline-dark update-user">Update</button>' +
                                       '<button type="button" class="btn btn-outline-danger delete-user">Delete</button>' +
                                       '</div>';
                               },
                               className: 'text-center',
                               orderable: false
                           }
                       ]
                   });

    const createUser = (callback) => {
        let formData = convertFormToObject(userForm);
        if (!formData) {
            return;
        }

        postData('/user', formData)
            .then(response => {
                if (callback) {
                    callback();
                }
            });
    };
    const updateUser = (id, callback) => {
        let formData = convertFormToObject(userForm);
        if (!formData) {
            return;
        }

        putData('/user/' + id, formData)
            .then(response => {
                if (callback) {
                    callback();
                }
            });
    };
    const deleteUser = (id, callback) => {
        deleteData('/user/' + id)
            .then(response => {
                if (callback) {
                    callback();
                }
            });
    };

    const rebindSaveUserHandler = (handler) => {
        saveUserBtn.removeEventListener('click', saveUserHandler);
        saveUserHandler = handler;
        saveUserBtn.addEventListener('click', saveUserHandler, false);
    };
    const prepareUserForm = (saveHandler) => {
        userForm.reset();
        rebindSaveUserHandler(saveHandler);
    };
    const fillUserForm = (userData) => {
        userForm.FirstName.value = userData.firstName || '';
        userForm.LastName.value = userData.lastName || '';
    };
    const defaultFormCallback = () => {
        userModal.hide();
        userDataTable.ajax.reload()
    }
    const getReloadBtnHandler = (dataTable) => {
      return (event) => {
          const spinnerClass = 'fa-spin';
          const reloadButton = event.currentTarget;
          const icon = reloadButton.getElementsByTagName('i')[0];

          reloadButton.disabled = true;
          icon.classList.add(spinnerClass);

          dataTable.ajax.reload(() => {
              reloadButton.disabled = false;
              icon.classList.remove(spinnerClass);
          });
      }
    }

    document.getElementById('reload-users-table').addEventListener('click', getReloadBtnHandler(userDataTable));
    document.getElementById('add-user').addEventListener('click', () => {
        prepareUserForm(() => createUser(defaultFormCallback));
        userModal.show();
    });
    userTableElement.on('click', 'button.update-user', (event) => {
        let rowData = userDataTable.row($(event.target.closest('tr'))).data();
        prepareUserForm(() => {
            updateUser(rowData.userId, defaultFormCallback);
        });
        fillUserForm(rowData);
        userModal.show();
    });
    userTableElement.on('click', 'button.delete-user', (event) => {
        let rowData = userDataTable.row($(event.target.closest('tr'))).data();
        if (confirm(`Confirm you want to delete user ${ rowData.firstName } ${ rowData.lastName }`)) {
            deleteUser(rowData.userId, userDataTable.ajax.reload);
        }
    });

// ------------------------------ Audit ----------------------------------

    const auditTableElement = $('#audit-table');
    const auditDataTable = auditTableElement
        .DataTable({
                       paging: false,
                       searching: false,
                       sorting: false,
                       ajax: {
                           url: '/audit',
                           dataSrc: ''
                       },
                       columns: [
                           {
                               title: 'Show Properties',
                               render: () => {
                                   return '<a class="show-audit-properties" href="#"><i class="fas fa-arrow-right"></i></a>';
                               }
                           },
                           { data: 'entityTypeName', title: 'Entity Type Name' },
                           { data: 'createdDate', title: 'Created Date' },
                           { data: 'state', title: 'State' },
                       ],
                       drawCallback: function () {
                           let rows = this.api().rows();
                           rows.every(function () {
                               this.child(getAuditPropertiesTable(this.data().properties));
                           });
                       }
                   });

    const getAuditPropertiesTable = (properties) => {
        let table = '<table class="table table-bordered table-striped">' +
            '<thead>' +
            '<th>Property Name</th>' +
            '<th>Old Value</th>' +
            '<th>New Value</th>' +
            '</thead>' +
            '<tbody>';

        if (!properties.length) {
            table += '<tr><td colspan="3">Nothing found</td></tr>>'
        } else {
            for (let property of properties) {
                table += '<tr>' +
                    `<td>${ property.propertyName }</td>` +
                    `<td>${ property.oldValueFormatted }</td>` +
                    `<td>${ property.newValueFormatted }</td>` +
                    '</tr>';
            }
        }

        table += '</tbody></table>';

        return table;
    }

    auditDataTable.on('click', '.show-audit-properties', function (e) {
        e.preventDefault();
        
        let row = auditDataTable.row($(this.closest('tr')));
        let icon = e.currentTarget.getElementsByTagName('i')[0];
        if (row.child.isShown()) {
            icon.classList.replace('fa-arrow-down', 'fa-arrow-right')
            row.child.hide();
        } else {
            icon.classList.replace('fa-arrow-right', 'fa-arrow-down')
            row.child.show();
        }
    })
    document.getElementById('reload-audit-table').addEventListener('click', getReloadBtnHandler(auditDataTable));
});