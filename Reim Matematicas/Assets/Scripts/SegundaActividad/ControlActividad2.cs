using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ControlActividad2 : MonoBehaviour {

    public static ControlActividad2 instanciaCompartida;
    public List<int> allArrayResultados;//Contiene todo los imagen 1torta
    public Text textoT1;
    public Text textoT2;
    public Text textoT3;
    public Text textoT5;
    public Text textoT10;
    public Text necesitaTorta;
    private int cont1 = 0;
    private int cont2 = 0;
    private int cont3 = 0;
    private int cont5 = 0;
    private int cont10 = 0;
    private int contTotal = 0;
    private int cantidadTotal;
    private int contMayorResult = 0;
    public GameObject panelResultados;
    public Text TotalTorta1;
    public Text TotalTorta2;
    public Text TotalTorta3;
    public Text TotalTorta5;
    public Text TotalTorta10;
    public Text TiempoCompletado;
    public Text PorcentajeComple;

    private int cant_selec1 = 0;
    private int cant_selec2 = 0;
    private int cant_selec3 = 0;
    private int cant_selec5 = 0;
    private int cant_selec10 = 0;

    public GameObject panelInstrucciones;

    //estados de actividad
    //1=introduccion
    //2= Juego
    //3=Finalizar actividad
    //4 Panel Operacion
    //5 Pausa
    public int estadoA2 = 1;

    //DAtos para enviar
    int cant_instru = 0;
    public DateTime fechaInicioActiv;
    public DateTime fechaTerminoActiv;
    public DateTime fechaInicioIntento;
    public DateTime fechaTerminoIntento;
    public DateTime fechaRetiro;
    public bool retiroActiv = false;
    AudioSource musica;
    public AudioClip musicafondo;
    public AudioClip audioinstruc;


    private void Awake()
    {
        textoT1.text = "0";
        textoT2.text = "0";
        textoT3.text = "0";
        textoT5.text = "0";
        textoT10.text = "0";
        necesitaTorta.text = "0";
        EsconderPanelA2();
    }
    // Use this for initialization
    void Start () {
        musica = GetComponent<AudioSource>();
        CambiarAudioA2(1);
        instanciaCompartida = this;
        fechaInicioActiv = DateTime.Now;
        fechaInicioIntento = DateTime.Now;
        RandomNumber();
        StartCoroutine(Abrir());

    }

    // Update is called once per frame
    void Update () {
        SeleccionarTorta();
        CompruebaCantidad();
    }

    void RandomNumber()
    {

        int ind_partida;
        System.Random rnd = new System.Random();

        ind_partida = rnd.Next(1, 10);

        cantidadTotal = allArrayResultados[ind_partida];
        necesitaTorta.text = allArrayResultados[ind_partida].ToString();
        
    }

    IEnumerator Abrir()
    {
        yield return new WaitForSeconds(0.01f);
        AbrirInstrucciones();
    }


    public void SeleccionarTorta()
    {
        if (estadoA2 == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                bool ind_selec;

                if (hit.collider != null)
                {
                    ind_selec = hit.collider.gameObject.GetComponent<Tortas>().seleccionado;
                    int ind_torta = hit.collider.gameObject.GetComponent<Tortas>().tipo_torta;
                    if (ind_selec == false)
                    {
                        hit.collider.gameObject.GetComponent<Tortas>().seleccionado = true;
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color32(171, 169, 169, 169);
                        hit.collider.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);//GetComponent<SpriteRenderer>().size = new Vector2(0.5f, 0.5f);
                        CalculaCantidad(ind_torta, 1);
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<Tortas>().seleccionado = false;
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                        hit.collider.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        CalculaCantidad(ind_torta, 2);
                    }

                }
            }
        }
    }

    void CalculaCantidad(int ind_torta, int ind_calculo) {
        if (ind_torta == 1)
        {
            if (ind_calculo == 1)
            {
                cont1 = cont1 + 1;
                cant_selec1 = cant_selec1 + 1;
                textoT1.text = cont1.ToString();
                contTotal = contTotal + 1;

            }
            else
            {
                cont1 = cont1 - 1;
                cant_selec1 = cant_selec1 - 1;
                textoT1.text = cont1.ToString();
                contTotal = contTotal - 1;
            }
        }

        if (ind_torta == 2)
        {
            if (ind_calculo == 1)
            {
                cont2 = cont2 + 2;
                cant_selec2 = cant_selec2 + 1;
                textoT2.text = cont2.ToString();
                contTotal = contTotal + 2;

            }
            else
            {
                cont2 = cont2 - 2;
                cant_selec2 = cant_selec2 - 1;
                textoT2.text = cont2.ToString();
                contTotal = contTotal - 2;
            }
        }

        if (ind_torta == 3)
        {
            if (ind_calculo == 1)
            {
                cont3 = cont3 + 3;
                cant_selec3 = cant_selec3 + 1;
                textoT3.text = cont3.ToString();
                contTotal = contTotal + 3;

            }
            else
            {
                cont3 = cont3 - 3;
                cant_selec3 = cant_selec3 - 1;
                textoT3.text = cont3.ToString();
                contTotal = contTotal - 3;
            }
        }

        if (ind_torta == 5)
        {
            if (ind_calculo == 1)
            {
                cont5 = cont5 + 5;
                cant_selec5 = cant_selec5 + 1;
                textoT5.text = cont5.ToString();
                contTotal = contTotal + 5;

            }
            else
            {
                cont5 = cont5 - 5;
                cant_selec5 = cant_selec5 - 1;
                textoT5.text = cont5.ToString();
                contTotal = contTotal - 5;
            }
        }

        if (ind_torta == 10)
        {
            if (ind_calculo == 1)
            {
                cont10 = cont10 + 10;
                cant_selec10 = cant_selec10 + 1;
                textoT10.text = cont10.ToString();
                contTotal = contTotal + 10;
            }
            else
            {
                cont10 = cont10 - 10;
                cant_selec10 = cant_selec10 - 1;
                textoT10.text = cont10.ToString();
                contTotal = contTotal - 10;
            }
        }

    }

    void CompruebaCantidad() {

        if (cantidadTotal == contTotal)
        {
            FinalizarActividad();
            MostrarPanelA2();
            contTotal = 0;
        }

        if (cantidadTotal < contTotal)
        {
            contMayorResult = 1;
        }

        /* if (cantidadTotal == cont1)
         {
             FinalizarActividad();
             MostrarPanelA2();
             cont1 = cont1 + 1;
         }
         else if (cantidadTotal == cont2) {
             FinalizarActividad();
             MostrarPanelA2();
             cont2 = cont2 + 2;
         }
         else if (cantidadTotal == cont3)
         {
             FinalizarActividad();
             MostrarPanelA2();
             cont3 = cont3 + 3;
         }
         else if (cantidadTotal == cont5)
         {
             FinalizarActividad();
             MostrarPanelA2();
             cont5 = cont5 + 5;
         }
         else if (cantidadTotal == cont10)
         {
             FinalizarActividad();
             MostrarPanelA2();
             cont10 = cont10 + 10;
         }*/

    }

    void MostrarPanelA2()
    {
        panelResultados.gameObject.SetActive(true);
    }

    void EsconderPanelA2()
    {
        panelResultados.gameObject.SetActive(false);

    }

    public void FinalizarActividad()
    {
        estadoA2 = 3;
        TimerA2.instanciaCompartida.PauseTimer();
        TotalTorta1.text = cant_selec1.ToString();
        TotalTorta2.text = cant_selec2.ToString();
        TotalTorta3.text = cant_selec3.ToString();
        TotalTorta5.text = cant_selec5.ToString();
        TotalTorta10.text = cant_selec10.ToString();
        TiempoCompletado.text = TimerA2.instanciaCompartida.GetTime();
        fechaTerminoIntento = DateTime.Now;
        bool paso = false;
        if (contMayorResult == 1) {
            paso = true;
        }
        EnvioDatosBDA2.instanciaBDA2.EnviarDatosA2(PlayerPrefs.GetString("id_usuario"), cant_selec1, cant_selec2, cant_selec3, cant_selec5, cant_selec10, cant_instru, fechaInicioIntento, fechaTerminoIntento, fechaInicioActiv, retiroActiv, paso, DuracionIntentoA2());
        
}

    public void Finalizar() {
        if (estadoA2 == 2)
        {
            fechaTerminoIntento = DateTime.Now;
            retiroActiv = true;
            bool paso = false;
            if (contMayorResult == 1)
            {
                paso = true;
            }
            EnvioDatosBDA2.instanciaBDA2.EnviarDatosA2(PlayerPrefs.GetString("id_usuario"), cant_selec1, cant_selec2, cant_selec3, cant_selec5, cant_selec10, cant_instru, fechaInicioIntento, fechaTerminoIntento, fechaInicioActiv, retiroActiv, paso, DuracionIntentoA2());

        }
        fechaTerminoActiv = DateTime.Now;

        EnvioDatosBDA2.instanciaBDA2.EnviarDatosActivA2(fechaInicioActiv, fechaTerminoActiv, DuracionA2(), PlayerPrefs.GetString("id_usuario"));
        SceneManager.LoadScene("SeleccionarActividad");
    }

    public void Reiniciar() {
        //SceneManager.LoadScene("Actividad2");
        estadoA2 = 2;
        cant_selec1 = 0;
        cant_selec2 = 0;
        cant_selec3 = 0;
        cant_selec5 = 0;
        cant_selec10 = 0;
        cont1 = 0;
        cont2 = 0;
        cont3 = 0;
        cont5 = 0;
        cont10 = 0;
        contMayorResult = 0;
        textoT1.text = "0";
        textoT2.text = "0";
        textoT3.text = "0";
        textoT5.text = "0";
        textoT10.text = "0";
        necesitaTorta.text = "0";
        EsconderPanelA2();
         Tortas[] pepe = FindObjectsOfType<Tortas>() as Tortas[];
        foreach (Tortas hinge in pepe)
        {
            hinge.seleccionado = false;
            hinge.gameObject.GetComponent<SpriteRenderer>().color = Color.white; 
        }

        TimerA2.instanciaCompartida.ResetTimer();
        TimerA2.instanciaCompartida.MostrarTimer(true);
        fechaInicioIntento = DateTime.Now;
        RandomNumber();
        

    }

    public void AbrirInstrucciones() {
        CambiarAudioA2(2);
        estadoA2 = 1;
        panelInstrucciones.SetActive(true);
        TimerA2.instanciaCompartida.PauseTimer();
        cant_instru = cant_instru + 1;
    }

    public void CerrarInstrucciones()
    {
        estadoA2 = 2;
        panelInstrucciones.gameObject.SetActive(false);
        TimerA2.instanciaCompartida.ResumeTimer();
        CambiarAudioA2(1);
    }

    public string DuracionA2()
    {
        string tiempo = "";

        TimeSpan ts = fechaTerminoActiv - fechaInicioActiv;
        tiempo = ts.Seconds.ToString();

        return tiempo;
    }

    public string DuracionIntentoA2()
    {
        string tiempointento = "";

        TimeSpan ts = fechaTerminoIntento - fechaInicioIntento;
        tiempointento = ts.Seconds.ToString();

        return tiempointento;

    }

    public void CambiarAudioA2(int ind)
    {

        if (ind == 1)
        {
            musica.clip = musicafondo;
            musica.Play();

        }
        else
        {
            musica.clip = audioinstruc;
            musica.Play();
        }

    }


}
