using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BdContexto : MonoBehaviour {

    public string fechaHoyInicio;
    public string fechaHoyTermino;
    public string fechaHoyRetiro;
    public bool seretiro;
    public string usuario;
    // Use this for initialization

    public BdContexto(string inicio,string termino, string retiro,bool seRetiro,string user)
    {
        this.fechaHoyInicio = inicio;
        this.fechaHoyTermino = termino;
        this.fechaHoyRetiro = retiro;
        this.seretiro = seRetiro;
        this.usuario = user;
    }
}
