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

    ok = confirm("Вы абсолтно уверены, что хотете удалить этот вопрос?\n\nЭту операцию нельзя отменить!");
    if (!ok) return; 

    $.ajax({
        url: 'api/FaqListItems/' + selectedrowid,
        type: 'DELETE',
        success: function (response) {
            $(getrow(selectedrowid)).remove();
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