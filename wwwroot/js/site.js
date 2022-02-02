$(function () {
    $(".custom-file-input").change(function () {
        $(this).next(".custom-file-label").html($(this).val().split("\\").pop());
    });
});
