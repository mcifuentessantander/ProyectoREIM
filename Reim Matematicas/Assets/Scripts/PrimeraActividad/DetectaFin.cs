using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaFin : MonoBehaviour {

    public bool trigger;
    public Collider2D col;
    //public CreaProductos funcion;
    

    private void OnTriggerStay2D(Collider2D collision)
    {
            this.trigger = true;
            col = collision;
            //funcion.OcultaProductoDetectado(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.trigger = false;
        
    }
}
