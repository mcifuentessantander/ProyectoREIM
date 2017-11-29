using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDatosTimeA1 : MonoBehaviour {

    public static BaseDatosTimeA1 instanciaBDTA1;

    public DateTime fechaInicioActiv = DateTime.Now;
    public DateTime fechaTerminoActiv = DateTime.Now;
    public DateTime fechaInicioIntento = DateTime.Now;
    public DateTime fechaTerminoIntento = DateTime.Now;
    public DateTime fechaRetiro = DateTime.Now;


    void Awake()
    {
        instanciaBDTA1 = this;
    }

}
