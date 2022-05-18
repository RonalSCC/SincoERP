$(document).ready(function () {
    $('#TableAlumnos').DataTable();
    AddPatternInputsText();
});

function SendNewCalificacion(ID) {
    var dataForm = $('#FormCalAsignatura').serializeObject();
    if (dataForm && ID) {
        if (dataForm.AsignaturaID && dataForm.AnioAcademico && (dataForm.Calificacion != null && dataForm.Calificacion != '')) {
            if (dataForm.Calificacion >= 0 && dataForm.Calificacion <= 5) {
                dataForm.Activo = true;
                dataForm.AlumnoID = ID;
                Begin();
                $.ajax({
                    url: 'CalificarAsignaturaEstudiante',
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
                                VerAsignaturasAlumno(ID);
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
            } else {
                iziToast.info({
                    title: 'Validación',
                    message: "La calificación es desde 0  a 5",
                });
            }
        } else {
            iziToast.info({
                title: 'Validación',
                message: "Por favor diligencie todos los campos",
            });
        }
    }
}

function Begin() {
    ShowLoader();
}

function Complete() {
    setTimeout(function () {
        HideLoader();
    }, 800);
}

function RespuestaForm(obj) {
    if (obj) {
        if (obj.TransaccionID == 1) {
            iziToast.success({
                title: 'Transacción exitosa',
                message: obj.Descripcion,
            });
            $('#ModalAlumno').modal('hide');
            ReloadGrid();
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



