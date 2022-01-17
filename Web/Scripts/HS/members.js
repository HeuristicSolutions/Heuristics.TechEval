$(document).ready(function () {
    $("#NewMemberForm").validate({
        Name: {
            required: true
        },
        Email: {
            required: true,
            email: true
        },
    });
});