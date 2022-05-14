$(document).ready(function () {
    $('#TableProfesores').DataTable();
    AddPatternInputsText();
    CargarDatosProfesores();
});

function CargarDatosProfesores() {
    $.ajax({
        url: 'Comunications/ConsultarProfesores',
        type: "POST",
        data: {},
        success: function (data) {
            if (data) {
                if (data.TransaccionID == 1) {
                    iziToast.success({
                        title: 'Transacción exitosa',
                        message: 'Se ha guardado la información del análisis',
                    });
                } else if (data.TransaccionID == 0 && !data.SessionNotFound) {
                    iziToast.warning({
                        title: 'Sin procesar',
                        message: data.Descripcion,
                    });
                } else if (data.TransaccionID == 0 && data.SessionNotFound == true) {
                    iziToast.warning({
                        title: 'Sin datos de sesión',
                        message: "Por favor recargue la pantalla y entre nuevamente",
                    });
                }
            }
            $("#loadPanel").dxLoadPanel("instance").hide();
        }
    });
}

function AbrirModal() {
    $('#ModalNewProfesor').modal('show');
}

function SendFormProfesor() {

}

function AddPatternInputsText() {
    $('#inputNombre').attr('pattern', '[a-zA-Z\s]+');
    $('#inputApellido').attr('pattern', '[a-zA-Z\s]+');
    $('#inputIdentificacion').attr('pattern', '[0-9\s]{8,11}');
    $('#inputTelefono').attr('pattern', '[0-9\s]+');
    $('#inputEdad').attr('pattern', '[0-9\s]+');
}

