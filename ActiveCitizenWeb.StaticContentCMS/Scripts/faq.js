var selectedrowid;

function getrow(id) {
    return "#tblrow" + id;
}

function selectquestion(id) {
    $(getrow(id)).css("background-color", "lightblue");
    if (selectedrowid != null && selectedrowid != id) {
        $(getrow(selectedrowid)).css("background-color", "");
    }
    selectedrowid = id;
}

function deletequestion() {
    if (selectedrowid == null) {
        alert("Пожалуйста, выберите вопрос, кликнув на ссылку 'Выбрать' справа от соответствующего вопроса\n\nВнимание, удаление вопроса нельзя отменить, поэтому будьте, пожалуйста, внимательны!");
        return;
    }

    var ok = confirm("Вы действительно хотите удалить вопрос?");
    if (!ok) return;

    ok = confirm("Вы абсолютно уверены, что хотете удалить этот вопрос?\n\nЭту операцию нельзя отменить!");
    if (!ok) return;

    debugger;

    $.ajax({
        url: 'api/FaqListItems/' + selectedrowid,
        type: 'DELETE',
        success: function (response) {
            debugger;
            $(getrow(selectedrowid)).remove();
        },
        error: function (response) {
            alert(response.toString());
        }
    });
}

function deletecategory() {
    if (selectedrowid == null) {
        alert("Пожалуйста, выберите раздел, кликнув на ссылку 'Выбрать' справа от соответствующего раздела\n\nВнимание, удаление раздела нельзя отменить, поэтому будьте, пожалуйста, внимательны!");
        return;
    }

    var ok = confirm("Вы действительно хотите удалить раздел?");
    if (!ok) return;

    ok = confirm("Вы абсолютно уверены, что хотете удалить этот раздел?\n\nЭту операцию нельзя отменить!");
    if (!ok) return;

    $.ajax({
        url: 'http://localhost:51734/api/FaqCategory/' + selectedrowid,
        type: 'DELETE',
        success: function (response) {
            $(getrow(selectedrowid)).remove();
        },
        error: function (response) {
            if(response.status == 409) alert(response.responseText);
        } 
    });
}

function editquestion() {
    if (selectedrowid == null) {
        alert("Пожалуйста, выберите вопрос, кликнув на ссылку 'Выбрать' справа от соответствующего вопроса");
        return;
    }

    window.location.href = "FAQ/EditQuestion/"+selectedrowid;
}

function editcategory() {
    if (selectedrowid == null) {
        alert("Пожалуйста, выберите раздел, кликнув на ссылку 'Выбрать' справа от соответствующего раздела");
        return;
    }

    window.location.href = "./EditCategory/" + selectedrowid;
}

function newquestion() {
    window.location.href = "FAQ/NewQuestion";
}

function newcategory() {
    window.location.href = "./NewCategory";
}