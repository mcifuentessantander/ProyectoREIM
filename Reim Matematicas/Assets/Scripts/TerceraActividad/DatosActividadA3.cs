﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosActividadA3 : MonoBehaviour {

    public string inicioActividad;
    public string terminoActividad;
    public string tiempoDuracion;
    public string usuario;

    public DatosActividadA3(string inicio, string termino, string duracion, string usuario) {

        this.inicioActividad = inicio;
        this.terminoActividad = termino;
        this.tiempoDuracion = duracion;
        this.usuario = usuario;
    }
}
