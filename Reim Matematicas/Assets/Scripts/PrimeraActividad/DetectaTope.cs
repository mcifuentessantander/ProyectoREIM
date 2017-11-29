using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaTope : MonoBehaviour {

    public bool detectaTope;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.detectaTope = true;
        CreaProductos.instanciaCompartida.OcultarProducto(collision);

    }
}
