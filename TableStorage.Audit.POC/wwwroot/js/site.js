const globalEventHandlers = {};
const globalSpinner = $('#global-spinner');
let ajaxCounter = 0;

$(document).ajaxSend(() => {
    ajaxCounter++;
    showSpinner();
});
$(document).ajaxComplete(() => {
    if (ajaxCounter > 0){
        ajaxCounter--;
    }
    
    if (ajaxCounter <= 0){
        ajaxCounter = 0;
        hideSpinner();
    }
});
$(document).ajaxError((e, request, settings) => {
    console.error(e, request);
});

function postData(url, data) {
    return $.ajax({
                      url: url,
                      data: JSON.stringify(data),
                      type: 'POST',
                      contentType: 'application/json'
                  });
}

function putData(url, data) {
    return $.ajax({
                      url: url,
                      data: JSON.stringify(data),
                      type: 'PUT',
                      contentType: 'application/json'
                  });
}

function deleteData(url) {
    return $.ajax({
                      url: url,
                      type: 'DELETE'
                  });
}

function convertFormToObject(formElement) {
    if (!formElement) {
        return null;
    }

    let formData = new FormData(formElement); 
    let obj = {};
    for (let [key, value] of formData) {
        obj[key] = value;
    }

    return obj;
}

function showSpinner(){
    globalSpinner.show();
}

function hideSpinner(){
    globalSpinner.hide();
}