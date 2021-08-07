﻿'use strict';

$(document).ready(function () {
    const userTable = $('#users-table')
    const userModal = new bootstrap.Modal(document.getElementById('user-modal-window'));
    const saveUserBtn = document.getElementById('save-user-data');
    const userForm = document.getElementById('user-form');
    
    let saveUserHandler = null;

    const table = userTable
        .DataTable({
                       paging: false,
                       searching: false,
                       sorting: false,
                       ajax: {
                           url: '/user',
                           dataSrc: '',

                       },
                       columns: [
                           { data: 'firstName', title: 'First Name', orderable: false },
                           { data: 'lastName', title: 'Last Name', orderable: false },
                           { data: 'createdDate', title: 'Created Date', orderable: false },
                           { data: 'lastModified', title: 'Last Modified', orderable: false },
                           {
                               title: 'Actions',
                               render: (data, type, row, meta) => {
                                   return '<button type="button" class="btn btn-primary update-user">Update</button>';
                               },
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
        userForm.FirstName.value = userData.firstName;
        userForm.LastName.value = userData.lastName;
    };
    const defaultFormCallback = () => {
        userModal.hide();
        table.ajax.reload()
    }

    document.getElementById('add-user').addEventListener('click', () => {
        prepareUserForm(() => createUser(defaultFormCallback));
        userModal.show();
    });
    userTable.on('click', 'button.update-user', (event) => {
        let rowData = table.row($(event.target.closest('tr'))).data();
        prepareUserForm(() => {
            updateUser(rowData.userId, defaultFormCallback);
        });
        fillUserForm(rowData);
        userModal.show();
    });
});