using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosIntentosA2 : MonoBehaviour {

    public string usuario;
    //public int cant_Touch;
    public int cantidadUnaTorta;
    public int cantidadDosTortas;
    public int cantidadTresTortas;
    public int cantidadCincoTortas;
    public int cantidadDiezTortas;
    public bool pasoElTotal;
    public int entrarIntrucciones;
    //public List<string> reginto;
    public string fechaIniActividad;
    public string fechaInicioIntento;
    public string fechaTerminoIntento;
    public string tiempoDuracion;
    public bool seRetiro;

    public DatosIntentosA2(string user, int unatorta, int dostortas, int trestortas, int cincotortas, int dieztortas, int instruc, string InicioIntento, string terminoIntento, string inicioActiv, bool retiro, bool pasTotal, string duracion)
    {//List<string> reg_intento

        this.usuario = user;
        this.cantidadUnaTorta = unatorta;
        this.cantidadDosTortas = dostortas;
        this.cantidadTresTortas = trestortas;
        this.cantidadCincoTortas = cincotortas;
        this.cantidadDiezTortas = dieztortas;
        this.entrarIntrucciones = instruc;
        this.fechaInicioIntento = InicioIntento;
        this.fechaTerminoIntento = terminoIntento;
        this.fechaIniActividad = inicioActiv;
        this.seRetiro = retiro;
        this.pasoElTotal = pasTotal;
        this.tiempoDuracion = duracion;
        //this.reginto = reg_intento;

    }
}
