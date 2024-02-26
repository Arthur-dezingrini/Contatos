$(document).ready(function () {

    $('#ddlEstado').change(function () {
        var estado = $(this).val();
        $.ajax({
            url: '/Home/GetMunicipios',
            type: 'POST',
            data: { estado: estado },
            success: function (data) {
                $('#ddlMunicipio').empty();
                $('#ddlMunicipio').append($('<option>').text('Selecione um município'));
                $.each(data, function (index, municipio) {
                    $('#ddlMunicipio').append($('<option>').text(municipio.nome).attr('value', municipio.nome));
                });
            }
        });
    });


    $('#Telefone').on('input', function () {
        var telefone = $(this).val().replace(/\D/g, '');
        if (telefone.length > 2) {
            telefone = telefone.replace(/^(\d{2})(\d{0,5})/, '$1 $2');
        }
        if (telefone.length > 7) {
            telefone = telefone.replace(/^(\d{2}) (\d{5})(\d{0,4})/, '$1 $2-$3');
        }
        if (telefone.length > 12) {
            telefone = telefone.substring(0, 14);
        }
        $(this).val(telefone);
    });


    $('#excluirModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('id');
        $(this).find('#excluirId').val(id);
    });

    $('#btnConfirmarExclusao').click(function () {
        var id = $('#excluirId').val();
        console.log(id);
        $.ajax({
            url: '/Home/ExcluirContato',
            type: 'POST',
            data: { id: id },
            success: function () {
                location.reload();
            },
            error: function () {
                alert('Erro ao excluir o contato.');
            }
        });
    });
});


