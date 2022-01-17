$(document).ready(function () {
    $("#NewMemberForm").ajaxForm({
        success: function (formData, jqForm, options) {
            // sure would be nice if we didn't have to reload the whole page...
            document.location.reload();
        }
    });

    $("#EditMemberForm").ajaxForm({
        success: function (formData, jqForm, options) {
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