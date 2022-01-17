$(document).ready(function () {

    $('.alert').alert();

    $("#NewMemberForm").ajaxForm({
        success: function (formData, jqForm, options) {

            if (formData.status == "error") {
                window.alert(formData.message);
                return;
            }

            // sure would be nice if we didn't have to reload the whole page...
            document.location.reload();
        }
    });

    $("#EditMemberForm").ajaxForm({
        success: function (formData, jqForm, options) {

            if (formData.status == "error") {
                window.alert(formData.message);
                return;
            }

            // sure would be nice if we didn't have to reload the whole page...
            document.location.reload();
        }
    });
});

function openEditModal(id, name, email) {
    $("#EditName").val(name);
    $("#EditEmail").val(email);
    $("#EditId").val(id);
    $("#EditMemberModal").modal("show");
}