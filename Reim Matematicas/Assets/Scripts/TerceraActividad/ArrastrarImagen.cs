using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastrarImagen : MonoBehaviour {

    public static ArrastrarImagen instanciaCompartida;
    float x;
    float y;
    float z;
    Vector3 desp = new Vector3();
    public CuadroNumero cuadro ;
    private Vector2 posittion;


    void Awake()
    {
        instanciaCompartida = this;
        //Obtener la posicion en el eje z del GameObject
        z = Camera.main.WorldToScreenPoint(new Vector3(0, 0, transform.position.z)).z;
        posittion = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    void Start()
    {

    }

    void Update()
    {
        //Obtener la posicion del mouse
        x = Input.mousePosition.x;
        y = Input.mousePosition.y;
    }

    void OnMouseDown()
    {
        if (ControlActividad.instanciaCompartidaA3.estadoA3 == 2)
        {
            //Calcular el desplazamiento del mouse respecto al centro del objeto
            desp = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        }
    }

    void OnMouseDrag()
    {
        if (ControlActividad.instanciaCompartidaA3.estadoA3 == 2)
        {
            //Mover el objeto en funcion de la posicion del mouse (sin variar el eje z), sumando el desplazamiento inicial
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z)) + desp;
        }
    }

    private void OnMouseUp()
    {
        if (ControlActividad.instanciaCompartidaA3.estadoA3 == 2)
        {
            if (this.gameObject.GetComponent<Imagen>().ingresado == true)
            {
                //funcion.ValidaImagen(this.GetComponent<Imagen>(), cuadro, posittion);
                ControlActividad.instanciaCompartidaA3.ValidaImagen(this.gameObject.GetComponent<Imagen>(), posittion);
            }
            else
            {
                this.gameObject.GetComponent<Imagen>().transform.position = posittion;
                //funcion.OcultaProductoDetectado(this.GetComponent<Productos>().GetComponent<Collider2D>());
            }
        }
        
    }
}
