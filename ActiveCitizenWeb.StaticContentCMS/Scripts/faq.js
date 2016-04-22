function deletecategory(id) {
    var ok = confirm("Вы действительно хотите удалить раздел?");
    if (!ok) return;

    window.location.href = "/FAQ/DeleteCategory/" + id;
}

function deletequestion(id) {
    var ok = confirm("Вы действительно хотите удалить вопрос?");
    if (!ok) return;

    window.location.href = "/FAQ/DeleteQuestion/" + id;
}

function newquestion() {
    window.location.href = "/FAQ/NewQuestion";
}

function newcategory() {
    window.location.href = "/FAQ/NewCategory";
}