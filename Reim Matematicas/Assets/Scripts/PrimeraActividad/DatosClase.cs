using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosClase : MonoBehaviour {

     public string usuario;
     //public int cant_Touch;
     public int correctas;
     public int incorrectas;
     public int ptjeCompletado;
     public int entrarIntrucciones;
    public List<string> reginto;
    public string fechaIniActividad;
    //public string fechaTerminoActividad;
    public string fechaInicioIntento;
    public string fechaTerminoIntento;
    public string tiempoDuracion;
    public bool seRetiro;

    public DatosClase(string user, int correctas,int incorrectas,int porcentaje,int instruc, string InicioIntento, string terminoIntento, string inicioActiv,bool retiro, List<string> regProd, string duracion)
    {//List<string> reg_intento

        this.usuario = user;
        this.correctas = correctas;
        this.incorrectas = incorrectas;
        this.ptjeCompletado = porcentaje;
        this.entrarIntrucciones = instruc;
        this.fechaInicioIntento = InicioIntento;
        this.fechaTerminoIntento = terminoIntento;
        this.fechaIniActividad = inicioActiv;
        this.seRetiro = retiro;
        this.reginto = regProd;
        this.tiempoDuracion = duracion;
        //this.reginto = reg_intento;

    }
}
