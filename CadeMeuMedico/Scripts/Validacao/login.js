$(document).ready(function () {


    $("#status").hide();

    $("#botao-entrar").click(function () {
        if (!$("#status").length) {
            $(".jumbotron div p:eq(0)").after("<div id='status'></div>");
        }
        $.ajax({
            url: "/Usuarios/AutenticaUsuario",
            data: { Login: $("#txtLogin").val(), Senha: $("#txtSenha").val() },
            dataType: "json",
            type: "GET",
            async: true,
            beforeSend: function () {
                $("#status").html("Estamos autenticando o usuário. Só um instante...");
                $("#status").show();
            },
            success: function (dados) {
                if (dados.OK) {
                    $("#status").removeClass().addClass("alert alert-success fade in");
                    $("#status").html("<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Sucesso! </strong>" + dados.Mensagem);
                    setTimeout(function () { window.location.href = "/Home/Index" }, 5000);
                    $("#status").show();
                }
                else {
                    $("#status").removeClass().addClass("alert alert-danger fade in");
                    $("#status").html("<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Erro! </strong>" + dados.Mensagem);
                    $("#status").show();
                }
            },
            error: function () {
                $("#status").html(dados.Mensagem);
                $("#status").show()
            }
        });
    });
});
