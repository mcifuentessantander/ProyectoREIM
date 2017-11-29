using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaImagen : MonoBehaviour {

    public bool trigger;
    public Collider2D col;
    public CuadroNumero pep;
   


    private void OnTriggerStay2D(Collider2D collision)
    {
        this.trigger = true;
        col = collision; 
        collision.gameObject.GetComponent<Imagen>().ingresado = true;
        ControlActividad.instanciaCompartidaA3.cuadroNum = this.GetComponent<CuadroNumero>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.trigger = false;
        collision.gameObject.GetComponent<Imagen>().ingresado = false;

    }
}
