using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class ControlActividad : MonoBehaviour {

    public static ControlActividad instanciaCompartidaA3;

    public List<Imagen> allImgPArray;//Contiene todo los imagen 1er array
    public List<Imagen> allImgSArray;//Contiene todo los imagen 2do array
    public List<Imagen> allImgTArray;//Contiene todo los imagen 3er array

    public List<CuadroNumero> allCuadroPArray;//Contiene todo los Cuadro 1er array
    public List<CuadroNumero> allCuadroSArray;//Contiene todo los Cuadro 2do array
    public List<CuadroNumero> allCuadroTArray;//Contiene todo los Cuadro 3er array
    public List<ArrayValida> listaValidacion;
    private int ident_array;
    public Image check;
    public Image uncheck;
    private int cant_max = 0;
    private int Cant_Correctas = 0;
    private int Cant_Incorrectas = 0;
    public GameObject panelResultados;
    public Text txTiempo;
    public Text txBuenas;
    public Text txMalas;
    public Text txCompletada;

    public GameObject panelMuffin;
    public GameObject panelQueque;
    public GameObject panelTorta;
    private GameObject PanelSeleccionados;

    public GameObject panelInstrucciones;

    //estados de actividad
    //1=introduccion
    //2= Juego
    //3=Finalizar actividad
    //4 Panel Operacion
    //5 Pausa
    public int estadoA3 = 1;

    //Guardar datos
    public int cant_intro = 0;
    public int cant_Porden = 0;
    public List<string> listarega = new List<string>();
    public DateTime fechaInicioActiv;
    public DateTime fechaTerminoActiv;
    public DateTime fechaInicioIntento;
    public DateTime fechaTerminoIntento;
    public DateTime fechaRetiro;
    public bool retiroActiv = false;
    public CuadroNumero cuadroNum;
    AudioSource musica;
    public AudioClip musicafondo;
    public AudioClip audioinstruc;
    public AudioClip audioqueque;
    public AudioClip audiotorta;
    public AudioClip audiomuffin;

    List<Vector3> posArrayPI = new List<Vector3>();
    List<Vector3> posArrayPC = new List<Vector3>();
    List<Vector3> posArraySI = new List<Vector3>();
    List<Vector3> posArraySC = new List<Vector3>();
    List<Vector3> posArrayTI = new List<Vector3>();
    List<Vector3> posArrayTC = new List<Vector3>();
    int ind_reini1 = 0;
    int ind_reini2 = 0;
    int ind_reini3 = 0;


    private void Awake()
    {
        
        Random_list_A3();
        DisabledImageUi();
        EsconderPanel();
        EsconderOrden();
    }

    private void Start()
    {
        musica = GetComponent<AudioSource>();
        instanciaCompartidaA3 = this;
        AbrirIntruccionesA3();
        fechaInicioActiv = DateTime.Now;
        fechaInicioIntento = DateTime.Now;
    }

    private void Update()
    {

    }

    void Random_list_A3() {


        int ind_partida;
        System.Random rnd = new System.Random();

        ind_partida = rnd.Next(1, 4);

        if (ind_partida == 1)
        {
            for (int i = 0; i < allImgPArray.Count; i++) {

                allImgPArray[i].gameObject.SetActive(true);
                if (ind_reini1 == 0)
                {
                    posArrayPI.Add(allImgPArray[i].gameObject.transform.position);
                }
            }
            for (int j = 0; j < allCuadroPArray.Count; j++)
            {

                allCuadroPArray[j].gameObject.SetActive(true);
                if (ind_reini1 == 0)
                {
                    posArrayPC.Add(allCuadroPArray[j].gameObject.transform.position);
                }
            }
            cant_max = allCuadroPArray.Count;
            ident_array = 1;
            //panelTorta.gameObject.SetActive(true);
        }
        else if (ind_partida == 2)
        {

            for (int i = 0; i < allImgSArray.Count; i++)
            {

                allImgSArray[i].gameObject.SetActive(true);
                if (ind_reini2 == 0)
                {
                    posArraySI.Add(allImgSArray[i].gameObject.transform.position);
                }
            }
            for (int j = 0; j < allCuadroSArray.Count; j++)
            {

                allCuadroSArray[j].gameObject.SetActive(true);
                if (ind_reini2 == 0)
                {
                    posArraySC.Add(allCuadroSArray[j].gameObject.transform.position);
                }

            }

            cant_max = allCuadroSArray.Count;
            ident_array = 2;
            //panelQueque.gameObject.SetActive(true);

        }
        else if (ind_partida == 3)
        {

            for (int i = 0; i < allImgTArray.Count; i++)
            {

                allImgTArray[i].gameObject.SetActive(true);
                if (ind_reini3 == 0)
                {
                    posArrayTI.Add(allImgTArray[i].gameObject.transform.position);
                }
            }
            for (int j = 0; j < allCuadroTArray.Count; j++)
            {

                allCuadroTArray[j].gameObject.SetActive(true);
                if (ind_reini3 == 0)
                {
                    posArrayTC.Add(allCuadroTArray[j].gameObject.transform.position);
                }
            }

            cant_max = allCuadroTArray.Count;
            ident_array = 3;
            //panelMuffin.gameObject.SetActive(true);
        }

    }


    public void ValidaImagen(Imagen image, Vector2 posicion)
    {

        string respuesta = "";
        if (image.orden == cuadroNum.orden)
        {
            Cant_Correctas = Cant_Correctas + 1;
            image.transform.position = cuadroNum.transform.position;
            StartCoroutine(Check());
            //Destroy(image.gameObject.GetComponent<ArrastrarImagen>());//Linea de codigo en duda, por confirmar ya que presenta fallas.
            respuesta = "correcta";

        }
        else
        {
            Cant_Incorrectas = Cant_Incorrectas + 1;
            image.transform.position = posicion;
            StartCoroutine(Uncheck());
            respuesta = "incorrecta";

        }

        string reg = "OrdenImagen: " + image.orden.ToString() + " | OrdenCuadro: " + cuadroNum.orden.ToString() + " | ResultCorrecto: " + respuesta;
        listarega.Add(reg);

        if (cant_max == Cant_Correctas)
        {
            FinalizarActividadA3();
        }


    }

    IEnumerator Check()
    {
        check.enabled = true;
        yield return new WaitForSeconds(1);
        check.enabled = false;

    }

    IEnumerator Uncheck()
    {
        uncheck.enabled = true;
        yield return new WaitForSeconds(1);
        uncheck.enabled = false;
    }

    void DisabledImageUi()
    {
        uncheck.enabled = false;
        check.enabled = false;
    }

    public void FinalizarActividadA3() {

        fechaTerminoIntento = DateTime.Now;
        Timer.instanciaCompartida.PauseTimer();
        txTiempo.text = Timer.instanciaCompartida.GetTime();
        txBuenas.text = Cant_Correctas.ToString();
        txMalas.text = Cant_Incorrectas.ToString();
        MostrarPanel();
        txCompletada.text = CalcularPorcentajeA3().ToString() + "%";

        EnvioDatosA3.instanciaEDA3.EnviarDatosA3(PlayerPrefs.GetString("id_usuario"), Cant_Correctas, Cant_Incorrectas, CalcularPorcentajeA3(), cant_intro, cant_Porden, fechaInicioIntento, fechaTerminoIntento,fechaInicioActiv, retiroActiv, listarega, DuracionIntentoA3());

    }

    void MostrarPanel()
    {
        panelResultados.gameObject.SetActive(true);
        estadoA3 = 3;
    }

    void EsconderPanel()
    {
        panelResultados.gameObject.SetActive(false);
        estadoA3 = 1;
    }

    public int CalcularPorcentajeA3()
    {
        int ValorPorcentaje = ((Cant_Correctas * 100) / cant_max);
        

        return ValorPorcentaje;
    }

    public void ReiniciarA3()
    {
        cant_max = 0;
        Cant_Correctas = 0;
        Cant_Incorrectas = 0;
        cant_intro = 0;
        cant_Porden = 0;
        retiroActiv = false;
        listarega.Clear();
        EsconderPanel();
        
        Timer.instanciaCompartida.ResetTimer();
        ReiniciaArrayA3();
        ident_array = 0;
        Random_list_A3();
        Timer.instanciaCompartida.MostrarTimer(true);
        estadoA3 = 2;
        fechaInicioIntento = DateTime.Now;
    }

    public void Finalizar()
    {
        if(estadoA3 == 2)
        {
            retiroActiv = true;
            fechaTerminoIntento = DateTime.Now;
            EnvioDatosA3.instanciaEDA3.EnviarDatosA3(PlayerPrefs.GetString("id_usuario"), Cant_Correctas, Cant_Incorrectas, CalcularPorcentajeA3(), cant_intro, cant_Porden, fechaInicioIntento, fechaTerminoIntento, fechaInicioActiv, retiroActiv, listarega, DuracionIntentoA3());
        }
        fechaTerminoActiv = DateTime.Now;
        EnvioDatosA3.instanciaEDA3.EnviarDatosActivA3(fechaInicioActiv, fechaTerminoActiv, DuracionA3(), PlayerPrefs.GetString("id_usuario"));
        SceneManager.LoadScene("SeleccionarActividad");
    }

    public void ReiniciaArrayA3() {

        if (ident_array == 1)
        {
            for (int i = 0; i < allImgPArray.Count; i++)
            {
                allImgPArray[i].gameObject.SetActive(false);
                allImgPArray[i].ingresado = false;
                allImgPArray[i].gameObject.transform.position = new Vector3(posArrayPI[i].x, posArrayPI[i].y);
            }
            for (int j = 0; j < allCuadroPArray.Count; j++)
            {
                allCuadroPArray[j].gameObject.SetActive(false);
                allCuadroPArray[j].gameObject.transform.position.Set(posArrayPC[j].x, posArrayPC[j].y, 1);
            }
            ind_reini1 = 1;
        }
        else if (ident_array == 2)
        {

            for (int i = 0; i < allImgSArray.Count; i++)
            {
                allImgSArray[i].gameObject.SetActive(false);
                allImgSArray[i].ingresado = false;
                allImgSArray[i].gameObject.transform.position = new Vector3(posArraySI[i].x, posArraySI[i].y);
            }
            for (int j = 0; j < allCuadroSArray.Count; j++)
            {
                allCuadroSArray[j].gameObject.SetActive(false);
                allCuadroSArray[j].gameObject.transform.position.Set(posArraySC[j].x, posArraySC[j].y, 1);
            }
            ind_reini2 = 1;

        }
        else if (ident_array == 3)
        {

            for (int i = 0; i < allImgTArray.Count; i++)
            {
                allImgTArray[i].gameObject.SetActive(false);
                allImgTArray[i].ingresado = false;
                allImgTArray[i].gameObject.transform.position = new Vector3(posArrayTI[i].x, posArrayTI[i].y);
            }
            for (int j = 0; j < allCuadroTArray.Count; j++)
            {
                allCuadroTArray[j].gameObject.SetActive(false);
                allCuadroTArray[j].gameObject.transform.position.Set(posArrayTC[j].x, posArrayTC[j].y, 1);
            }
            ind_reini3 = 1;
        }

    }

    void EsconderOrden() {

        panelMuffin.gameObject.SetActive(false);
        panelQueque.gameObject.SetActive(false);
        panelTorta.gameObject.SetActive(false);
    }

    public void MuestraOrden() {
        cant_Porden = cant_Porden + 1;
        if (ident_array == 1)
        {
            panelTorta.gameObject.SetActive(true);
            CambiarAudioOrden(1);
            Timer.instanciaCompartida.PauseTimer();
            
        }
        else if (ident_array == 2)
        {
            panelQueque.gameObject.SetActive(true);
            CambiarAudioOrden(2);
            Timer.instanciaCompartida.PauseTimer();
        }
        else if (ident_array == 3) {
            panelMuffin.gameObject.SetActive(true);
            CambiarAudioOrden(3);
            Timer.instanciaCompartida.PauseTimer();
        }
        estadoA3 = 1;
    }

    public void SalirOrden()
    {
        if (ident_array == 1)
        {
            panelTorta.gameObject.SetActive(false);
            Timer.instanciaCompartida.ResumeTimer();
        }
        else if (ident_array == 2)
        {
            panelQueque.gameObject.SetActive(false);
            Timer.instanciaCompartida.ResumeTimer();
        }
        else if (ident_array == 3)
        {
            panelMuffin.gameObject.SetActive(false);
            Timer.instanciaCompartida.ResumeTimer();
        }
        estadoA3 = 2;

        CambiarAudioOrden(4);

    }

    public void AbrirIntruccionesA3() {
        if (estadoA3 != 3)
        {
            CambiarAudioA3(2);
            cant_intro = cant_intro + 1;
            panelInstrucciones.gameObject.SetActive(true);
            Timer.instanciaCompartida.PauseTimer();
            estadoA3 = 1;
        }
    }

    public void CerrarIntruccionesA3()
    {
        panelInstrucciones.gameObject.SetActive(false);
        Timer.instanciaCompartida.ResumeTimer();
        estadoA3 = 2;
        CambiarAudioA3(1);
    }

    public string DuracionA3()
    {

        string tiempo = "";

        TimeSpan ts = fechaTerminoActiv - fechaInicioActiv;
        tiempo = ts.Seconds.ToString();

        return tiempo;
    }

    public string DuracionIntentoA3()
    {
        string tiempointento = "";

        TimeSpan ts = fechaTerminoIntento - fechaInicioIntento;
        tiempointento = ts.Seconds.ToString();

        return tiempointento;

    }

    public void CambiarAudioA3(int ind)
    {

        if (ind == 1)
        {
            musica.clip = musicafondo;
            musica.Play();

        }
        else
        {
            musica.clip = audioinstruc;
            musica.volume = 1.4f;
            musica.Play();
        }

    }

    public void CambiarAudioOrden(int ind)
    {

        if (ind == 1)
        {
            musica.clip = audiotorta;
            musica.Play();

        }
        else if(ind == 2)
        {
            musica.clip = audioqueque;
            musica.Play();
        }
        else if(ind == 3)
        {
            musica.clip = audiomuffin;
            musica.Play();
        }
        else
        {
            musica.clip = musicafondo;
            musica.Play();

        }
       
}
}
