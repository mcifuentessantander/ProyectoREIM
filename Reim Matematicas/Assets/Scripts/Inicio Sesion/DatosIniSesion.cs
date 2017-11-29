using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosIniSesion : MonoBehaviour {

    public string usuario;
    public string fechaInicio;
    public string hora;
    public string nombre;

    public DatosIniSesion(string user, string inicio, string horas, string nombres) {

        this.usuario = user;
        this.fechaInicio = inicio;
        this.hora = horas;
        this.nombre = nombres;

    }
}
