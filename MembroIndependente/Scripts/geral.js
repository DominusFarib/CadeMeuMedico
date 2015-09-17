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

    $(".btn-apagar").click(function () {
        var pai = $(this).parent();
        if(confirm("Deseja realmente excluir este Membro?")){
            $.ajax({
                method: "POST",
                url: "/Medicos/MedicosDelet/",
                //data: { ID: "35" }
                data: { ID: $(this).attr("dataref") }
            }).done(function (dados) {
                if (dados.erro != "") {
                    pai.parent().parent().prepend('<tr class="alerta"><td colspan="7"> <div class="editor-field alert alert-danger fade in"><a href="" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>' + dados.erro + '</div></td></tr>');
                }
                else {
                    $('<tr class="alerta"><td colspan="7"> <div class="editor-field alert alert-success fade in">  <a href="" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span> ' + dados.msg + ' </div></td></tr>').insertAfter(pai.parent());
                    pai.parent().remove();
                }
            });
        }else
        {
            return false;
        }
    });

    $(document).on("click", ".alerta", function (e) {
        if (e.target.className == "close") {
            $(this).fadeOut(500, function () { $(this).remove(); });
        }
    });


});
