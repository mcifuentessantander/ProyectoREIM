using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectaColision : MonoBehaviour {

    private Button[] respawns;
    private PulsarBotones flecha_izquierda;
    private PulsarBotones flecha_derecha;
    private PulsarBotones flecha_arriba;
    private PulsarBotones flecha_abajo;

    // Use this for initialization
    void Start () {
        
        respawns = Button.FindObjectsOfType<Button>();
        
        foreach (Button guy in respawns)
        {
            
            if (guy.transform.name == "derecha")
            {
                flecha_derecha = guy.GetComponent<PulsarBotones>();
            }
            else if (guy.transform.name == "izquierda")
            {
                flecha_izquierda = guy.GetComponent<PulsarBotones>();
            }
            else if (guy.transform.name == "arriba")
            {
                flecha_arriba = guy.GetComponent<PulsarBotones>();
            }
            else if (guy.transform.name == "abajo")
            {
                flecha_abajo = guy.GetComponent<PulsarBotones>();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {


        flecha_arriba.pulsado = false;
        flecha_izquierda.pulsado = false;
        flecha_derecha.pulsado = false;
        flecha_abajo.pulsado = false;

    }
}
