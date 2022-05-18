$(document).ready(function () {
    $('#TableProfesores').DataTable();
    AddPatternInputsText();
});

function SendAsignaturaNew(ID) {
    var dataForm = $('#FormNewAsignatura').serializeObject();
    if (dataForm && ID) {
        if (dataForm.AsignaturaID && dataForm.AnioAcademicoID) {
            dataForm.Activo = true;
            dataForm.ProfesorID = ID;
            Begin();
            $.ajax({
                url: 'CrearAsignaturaProfesor',
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(dataForm),
                success: function (obj) {
                    if (obj) {
                        if (obj.TransaccionID == 1) {
                            Complete();
                            iziToast.success({
                                title: 'Transacción exitosa',
                                message: obj.Descripcion,
                            });
                            $('#ModalNewProfesor').modal('hide');
                            VerAsignaturasProfesor(ID);
                        } else {
                            Complete();
                            iziToast.info({
                                title: 'Sin procesar',
                                message: obj.Descripcion,
                            });
                        }
                    } else {
                        Complete();
                        iziToast.info({
                            title: 'Sin procesar',
                            message: "Por favor contactar a soporte",
                        });
                    }
                }
            });
        }
    }
}

function Begin() {
    ShowLoader();
}

function Complete() {
    HideLoader();
}

function RespuestaForm(obj) {
    if (obj) {
        if (obj.TransaccionID == 1) {
            iziToast.success({
                title: 'Transacción exitosa',
                message: obj.Descripcion,
            });
            $('#ModalNewProfesor').modal('hide');
            ReloadGridProfesor();
        } else {
            iziToast.info({
                title: 'Sin procesar',
                message: obj.Descripcion,
            });
        }
    } else {
        iziToast.info({
            title: 'Sin procesar',
            message: "Por favor contactar a soporte",
        });
    }
}

function AddPatternInputsText() {
    $('#inputNombre').attr('pattern', '[a-zA-Z\s]+');
    $('#inputApellido').attr('pattern', '[a-zA-Z\s]+');
    $('#inputIdentificacion').attr('pattern', '[0-9\s]{8,11}');
    $('#inputTelefono').attr('pattern', '[0-9\s]+');
    $('#inputEdad').attr('pattern', '[0-9\s]+');
}



