using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastrarProducto : MonoBehaviour {

    float x;
    float y;
    float z;
    Vector3 desp = new Vector3();
    private DetectaFin detector;
    //private CreaProductos funcion;


    void Awake()
    {
        //Obtener la posicion en el eje z del GameObject
        z = Camera.main.WorldToScreenPoint(new Vector3(0, 0, transform.position.z)).z;
    }

    void Start()
    {
        detector = FindObjectOfType<DetectaFin>();
        //funcion = FindObjectOfType<CreaProductos>();
    }

    void Update()
    {
        //Obtener la posicion del mouse
        x = Input.mousePosition.x;
        y = Input.mousePosition.y;
    }

    void OnMouseDown()
    {
        if (CreaProductos.instanciaCompartida.estado == 2)
        {
            //Calcular el desplazamiento del mouse respecto al centro del objeto
            desp = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        }
    }

    void OnMouseDrag()
    {
        if (CreaProductos.instanciaCompartida.estado == 2)
        {
            //Mover el objeto en funcion de la posicion del mouse (sin variar el eje z), sumando el desplazamiento inicial
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z)) + desp;
        }
    }

    private void OnMouseUp()
    {
        if (CreaProductos.instanciaCompartida.estado == 2)
        {
            if (detector.trigger == true)
            {
                CreaProductos.instanciaCompartida.OcultaProductoDetectado(detector.col);
                CreaProductos.instanciaCompartida.AbrirOperacion(this.GetComponent<Productos>());
                //funcion.ValidaProducto(this.GetComponent<Productos>());
                detector.trigger = false;
            }
            else
            {
                CreaProductos.instanciaCompartida.OcultaProductoDetectado(this.GetComponent<Productos>().GetComponent<Collider2D>());
            }
        }
    }
}
