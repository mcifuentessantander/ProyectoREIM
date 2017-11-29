﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerA1 : MonoBehaviour {

    public static TimerA1 instanciaCompartida;

    float tiempoMaximo;
    float TiempoActividad;
    float tiempoGuardado;       //Variable en que guardo el tiempo que va al hacer una pausa, por ej. para mostrar las instrucciones
    public Text timerTextUI;
    public Image timerLegendTextUI;

    // Use this for initialization
    void Start()
    {
        instanciaCompartida = this;
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {

        tiempoMaximo -= Time.deltaTime;
        timerTextUI.text = "" + tiempoMaximo.ToString("f0");
        if (timerTextUI.text.Equals("0"))
        {
            PauseTimer();
            CreaProductos.instanciaCompartida.FinalizarporTiempo();
         }

    }

    public void ResetTimer()
    {
        tiempoMaximo = 90f;
        TiempoActividad = 90f;
    }

    public void PauseTimer()
    {
        tiempoGuardado = tiempoMaximo;
        tiempoMaximo = -1.0f;
        MostrarTimer(false);
    }


    public void ResumeTimer()
    {
        tiempoMaximo = tiempoGuardado;
        MostrarTimer(true);
    }


    public void MostrarTimer(bool mostrarlo)
    {
        if (mostrarlo)
        {
            timerTextUI.enabled = true;
            timerLegendTextUI.enabled = true;
        }
        else
        {
            timerTextUI.enabled = false;
            timerLegendTextUI.enabled = false;
        }
    }

    public string GetTime()
    {

        float tiempoTotal;
        string textTime;
        tiempoTotal = TiempoActividad - tiempoGuardado;
        textTime = tiempoTotal.ToString();
        return textTime;
    }
}
