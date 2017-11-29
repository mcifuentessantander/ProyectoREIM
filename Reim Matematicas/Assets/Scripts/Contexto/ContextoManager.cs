using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ContextoManager : MonoBehaviour {

    public DateTime inicio;
    public DateTime fin;
    public DateTime retiro;
    public bool seRetiro = false;
    public PulsarBotones flecha_izquierda;
    public PulsarBotones flecha_derecha;
    public PulsarBotones flecha_arriba;
    public PulsarBotones flecha_abajo;
    public GameObject auto;
    String nombre_auto;
    private GameObject[] respawns;
    public GameObject instrucciones;

    // Use this for initialization
    void Start()
    {

        CargarAuto();
        inicio = DateTime.Now;
    }

    // Update is called once per frame
    void Update () {

        if (flecha_izquierda.pulsado)
        {
            Mover_Horizontal(-0.02f);
            CambiarImagen('I');
        }
        else if (flecha_derecha.pulsado)
        {
            Mover_Horizontal(0.02f);
            CambiarImagen('D');
        }
        else if (flecha_arriba.pulsado)
        {
            Mover_Vertical(0.02f);
            CambiarImagen('A');
        }
        else if (flecha_abajo.pulsado)
        {
            Mover_Vertical(-0.02f);
            CambiarImagen('B');
        }
    }

    void CargarAuto() {

        respawns = GameObject.FindGameObjectsWithTag("Auto");

        foreach (GameObject guy in respawns)
        {
            if (guy.GetComponent<Autos>().seleccionado == false)
            {
                guy.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                nombre_auto = guy.transform.name;
            }
            Destroy(guy);
        }
        auto.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + nombre_auto + "/auto_derecha");
    }

    public void Mover_Horizontal(float direccion)
    {
        auto.transform.Translate(new Vector2(direccion, 0));
    }

    public void Mover_Vertical(float direccion)
    {
        auto.transform.Translate(new Vector2(0, direccion));
    }

    void CambiarImagen(char val)
    {
        if (val == 'A')
        {
            auto.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + nombre_auto + "/auto_arriba");
            auto.GetComponent<BoxCollider2D>().size = new Vector2 (1.04f, 1.6f);
            auto.GetComponent<BoxCollider2D>().offset = new Vector2(-0.04f, 0.4f);
        }
        else if (val == 'B')
        {
            auto.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + nombre_auto + "/auto_abajo");
            auto.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 1.45f);
            auto.GetComponent<BoxCollider2D>().offset = new Vector2(0.06f, -0.45f);
        }
        else if (val == 'I')
        {
            auto.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + nombre_auto + "/auto_izquierda");
            auto.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 0.9f);
            auto.GetComponent<BoxCollider2D>().offset = new Vector2(-0.35f, -0.05f);
        }
        else if (val == 'D')
        {

            auto.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + nombre_auto + "/auto_derecha");
            auto.GetComponent<BoxCollider2D>().size = new Vector2(1.3f, 0.9f);
            auto.GetComponent<BoxCollider2D>().offset = new Vector2(0.6f, 0.04f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.name == "Pasteleria")
        {
            fin = DateTime.Now;
            retiro = DateTime.Now;
            EnvioContexto.instanciaCtxto.EnviarDatosConte(inicio, fin, retiro, seRetiro, PlayerPrefs.GetString("id_usuario"));
            SceneManager.LoadScene("SeleccionarActividad");

        }
    }

    public void Volver() {
        fin = DateTime.Now;
        retiro = DateTime.Now;
        seRetiro = true;
        SceneManager.LoadScene("SeleccionarAuto");

    }

    public void AbrirInstrucciones() {
        instrucciones.gameObject.SetActive(true);
    }

    public void CerrarInstrucciones()
    {
        instrucciones.gameObject.SetActive(false);
    }


}
