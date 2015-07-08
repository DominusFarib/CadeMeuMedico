$(document).ready(function () {
    $("#listaMedicos tr:nth-child(odd)").css("backgroundColor", "#FFCC66");
    $(".btn-editar").prepend("<span class='glyphicon glyphicon-pencil' aria-hidden='true'></span> ");
    $(".btn-detalhar").prepend("<span class='glyphicon glyphicon-eye-open' aria-hidden='true'></span> ");
    $(".btn-apagar").prepend("<span class='glyphicon glyphicon-remove' aria-hidden='true'></span> ");

    $(".btn-apagar").hover(
        function () {
            $(this).removeClass("btn-default");
            $(this).addClass("btn-danger");
        },
        function () {
            $(this).addClass("btn-default");
            $(this).removeClass("btn-danger");
        }
    );
});
