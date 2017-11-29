using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosIntentoA3 : MonoBehaviour {

    public string usuario;
    //public int cant_Touch;
    public int correctas;
    public int incorrectas;
    public int ptjeCompletado;
    public int entrarIntrucciones;
    public int entrarPanelOrden;
    public List<string> regIntento;
    public string fechaIniActividad;
    public string fechaInicioIntento;
    public string fechaTerminoIntento;
    public string tiempoDuracion;
    public bool seRetiro;

    public DatosIntentoA3(string user, int correctas, int incorrectas, int porcentaje, int instruc, int panelOrden, string InicioIntento, string terminoIntento, string inicioActiv, bool retiro, List<string> regProd, string duracion)
    {

        this.usuario = user;
        this.correctas = correctas;
        this.incorrectas = incorrectas;
        this.ptjeCompletado = porcentaje;
        this.entrarIntrucciones = instruc;
        this.entrarPanelOrden = panelOrden;
        this.fechaInicioIntento = InicioIntento;
        this.fechaTerminoIntento = terminoIntento;
        this.fechaIniActividad = inicioActiv;
        this.seRetiro = retiro;
        this.regIntento = regProd;
        this.tiempoDuracion = duracion;

    }
}
