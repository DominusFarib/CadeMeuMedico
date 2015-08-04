$(document).ready(function () {

    $("#status").hide();

    $("#botao-enviar").click(function () {

        $.ajax({
            url: "/Contas/RecuperaConta",
            data: { email: $("#txtEmail").val() },
            dataType: "json",
            type: "GET",
            async: true,
            crossDomain: true,
            beforeSend: function () {
                $("#status").html("Aguarde...Estamos recuperando seus dados.");
                $("#status").show();
            },
            success: function (dados) {
                    console.log("sucesso");
                if (dados.OK) {
                    $("#status").removeClass().addClass("alert alert-success fade in");
                    $("#status").html("<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Sucesso! </strong>" + dados.Mensagem);
                    setTimeout(function () { window.location.href = "/Home/Index" }, 5000);
                    $("#status").show();
                }
                else {
                    console.log("Não foi dessa vez");
                    $("#status").removeClass().addClass("alert alert-danger fade in");
                    $("#status").html("<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Erro! </strong>" + dados.Mensagem);
                    $("#status").show();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //alert('error');
                console.log(xhr.status);
                console.log(thrownError);
            }
        });
    });
});
