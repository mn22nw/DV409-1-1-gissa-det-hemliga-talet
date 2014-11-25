$(document).ready(function () {

    $("input[data-val-length-max]").each(function (index, element) {
        var length = parseInt($(this).attr("data-val-length-max"));
        $(this).prop("maxlength", length);
    });
});