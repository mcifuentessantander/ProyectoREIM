using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PulsarBotones : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool pulsado;

    // Use this for initialization
    public void OnPointerDown(PointerEventData eventData)
    {

        pulsado = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {

        pulsado = false;
    }
}
