using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CreaProductos : MonoBehaviour {

    public static CreaProductos instanciaCompartida;

    public List<Productos> allProductosPrimerArray;//Contiene todo los productos torta1
    public List<Productos> allProductosSegundoArray;//Contiene todo los productos torta2
    public List<Productos> allProductosTercerArray;//Contiene todo los productos torta3
    public List<Productos> productosCorrectos;//Productos necesarios para torta1
    public List<Productos> productosCorrectos2;//Productos necesarios para torta2
    public List<Productos> productosCorrectos3;//Productos necesarios para torta3
    public List<Productos> productosDesordenados;//Contiene los productos a mostrar de manera desordenada
    public List<Productos> productosPartida;//Lista Vacia para tomar los productos segun la lista que salio elegida
    public List<Productos> productosNecesarios;//Lista para trabajar los productos necesarios


    public List<Productos> productosPanel;//ista para trabajar los productos necesarios
    public List<Text> numeroCantidad;//Lista para ingresar la cantidadd necesaria por producto

    public Transform puntoinicial;
    public Transform puntofinal;
    int cont = 0;
    private Vector2 posittion;
    public Image check;
    public Image uncheck;
    public Image torta;
    public Image torta2;
    public Image torta3;
    public Image tortaGeneral;
    int incompleto = 0;
    int malas = 0;
    int buenas = 0;
    public Text resultBuenas;
    public Text resultMalas;
    public Text resultTiempo;
    public Canvas canvas;
    public GameObject panel;
    public Text titulo;
    public Text textBuenas;
    public Text textMalas;
    public Text textTiempo;
    public Button finalizar;
    public Button reiniciar;
    public GameObject panelInstrucciones;
    

    //Panel para operaciones matematicas

    public Text num1;
    public Text num2;
    public Text oper;
    public GameObject panelOperacion;
    public Text result1;
    public Text result2;
    public Text finishGame;
    public Text porcentaje;
    int resultVer;
    Productos new_producto;
    private int can_intro = 0;
    int ValorPorcentaje = 0;

    //public List<RegistroProd> listareg = new List<RegistroProd>();
    public List<string> listareg = new List<string>();
    //estados de actividad
    //1=introduccion
    //2= Juego
    //3=Finalizar actividad
    //4 Panel Operacion
    //5 Pausa
    public int estado = 1;

    int num_intentos = 0;

    //fechas

    public DateTime fechaInicioActiv;
    public DateTime fechaTerminoActiv;
    public DateTime fechaInicioIntento;
    public DateTime fechaTerminoIntento;
    public DateTime fechaRetiro;
    public bool retiroActiv;

    AudioSource pepe;
    public AudioClip paloma;
    public AudioClip paloma2;


    private void Awake()
    {
        instanciaCompartida = this;
        RandomList();
        PosicionarProductos();
        pintarText();
        DisabledImagen();
        
    }

    void Start() {

        pepe = GetComponent<AudioSource>();
        CambiarAudio(1);
        fechaInicioActiv = DateTime.Now;
        fechaInicioIntento= DateTime.Now;
        posittion = new Vector2(productosDesordenados[cont].gameObject.transform.position.x, productosDesordenados[cont].gameObject.transform.position.y);
        InvokeRepeating("AgregarProducto", 1f, 1.5f);
        Instrucciones(1);
    }

    void RandomList()
    {

        int desorden;
        int ind_partida;
        System.Random rnd = new System.Random();

        ind_partida = rnd.Next(1, 4);

        if (ind_partida == 1) {
            productosPartida = allProductosPrimerArray;
            productosNecesarios = productosCorrectos;
            tortaGeneral = torta;
        }
        else if (ind_partida == 2) {
            productosPartida = allProductosSegundoArray;
            productosNecesarios = productosCorrectos2;
            tortaGeneral = torta2;
        }
        else if (ind_partida == 3) {
            productosPartida = allProductosTercerArray;
            productosNecesarios = productosCorrectos3;
            tortaGeneral = torta3;
        }

        while (productosDesordenados.Count != productosPartida.Count)
        {
            desorden = rnd.Next(0, productosPartida.Count);
            if (!productosDesordenados.Contains(productosPartida[desorden]))
            {
                productosDesordenados.Add(productosPartida[desorden]);
            }
        }
    }

    void pintarText()
    {

        string texto;
        for (int i = 0; i < productosNecesarios.Count; i++)
        {
            productosPanel[i].cant_necesaria = productosNecesarios[i].cant_necesaria;
            texto = productosPanel[i].cant_necesaria.ToString();
            numeroCantidad[i].text = texto;
            productosPanel[i].GetComponent<SpriteRenderer>().sprite = productosNecesarios[i].GetComponent<SpriteRenderer>().sprite;
            productosPanel[i].cod_tip_producto = productosNecesarios[i].cod_tip_producto;
            productosPanel[i].nombre = productosNecesarios[i].nombre;
            productosPanel[i].gameObject.SetActive(true);
        }
    }

    void PosicionarProductos()
    {

        for (int i = 0; i < productosDesordenados.Count; i++)
        {
            productosDesordenados[i].transform.position = new Vector2(-7.66f, -2.87f);

        }
    }

    public void moverProductoAutomatico(Productos producto) {

        producto.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 0.015f);
    }

    public void AgregarProducto() {


        if (productosDesordenados.Count > cont)
        {
            productosDesordenados[cont].gameObject.SetActive(true);
            moverProductoAutomatico(productosDesordenados[cont]);
        }
        else {
            cont = 0;
            productosDesordenados[cont].gameObject.SetActive(true);
            moverProductoAutomatico(productosDesordenados[cont]);
        }
        cont = cont + 1;
    }

    public void OcultarProducto(Collider2D col)
    {
        col.GetComponent<Productos>().gameObject.transform.position = posittion;
        col.GetComponent<Productos>().gameObject.SetActive(false);
    }

    public void OcultaProductoDetectado(Collider2D col) {

        col.GetComponent<Productos>().gameObject.SetActive(false);
        col.GetComponent<Productos>().gameObject.transform.position = posittion;
    }

    public void AbrirOperacion(Productos producto)
    {
        int respuesta = 0;
        for (int i = 0; i < productosPanel.Count; i++)
        {
            if (producto.cod_tip_producto == productosPanel[i].cod_tip_producto)
            {
                respuesta = 1;
                if (productosPanel[i].cant_necesaria != 0)
                {
                    new_producto = producto;
                    GeneraOperacion();
                    TimerA1.instanciaCompartida.PauseTimer();
                }
            }
        }

        if (respuesta == 0)
        {
            StartCoroutine(Uncheck());
            ContarRespuestas(0);
        }

    }

    public void ValidaProducto(Productos producto)
    {
        for (int i = 0; i < productosPanel.Count; i++)
        {
            if (producto.cod_tip_producto == productosPanel[i].cod_tip_producto)
            {

                if (productosPanel[i].cant_necesaria != 0)
                {
                    --productosPanel[i].cant_necesaria;
                    numeroCantidad[i].text = productosPanel[i].cant_necesaria.ToString();
                    StartCoroutine(Check());
                }
            }
        }

        panelOperacion.SetActive(false);
        TimerA1.instanciaCompartida.ResumeTimer();
        FinalizaActividad();
    }

    void FinalizaActividad() {

        for (int i = 0; i < productosPanel.Count; i++)
        {
            if (productosPanel[i].cant_necesaria != 0)
            {
                incompleto = 1;
            }
        }

        if (incompleto == 0) {
            
            resultTiempo.text = TimerA1.instanciaCompartida.GetTime();
            TimerA1.instanciaCompartida.PauseTimer();
            PintaTorta();
            CancelInvoke();
            finishGame.gameObject.SetActive(true);
            this.estado = 3;
            StartCoroutine(WaitSecond());            
        }
        incompleto = 0;
    }

    void PintaTorta()
    {
        tortaGeneral.enabled = true;
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

    void DisabledImagen()
    {
        check.enabled = false;
        uncheck.enabled = false;
        torta.enabled = false;
        torta2.enabled = false;
        torta3.enabled = false;
        panelOperacion.SetActive(false);
    }

    void MuestraResultados()
    {
        num_intentos = num_intentos + 1;
        resultBuenas.text = buenas.ToString();
        resultMalas.text = malas.ToString();
        CalcularPorcentaje();
        panel.gameObject.SetActive(true);
        titulo.gameObject.SetActive(true);//.enabled = true;
        textBuenas.gameObject.SetActive(true);
        textMalas.gameObject.SetActive(true);
        textTiempo.gameObject.SetActive(true);
        resultBuenas.gameObject.SetActive(true);
        resultMalas.gameObject.SetActive(true);
        resultTiempo.gameObject.SetActive(true);
        finalizar.gameObject.SetActive(true);
        reiniciar.gameObject.SetActive(true);
        this.estado = 3;
        fechaTerminoIntento = DateTime.Now;
        //EnvioDatosBD.instanciaEDBD.listaRegistroProductos = listareg;
        EnvioDatosBD.instanciaEDBD.EnviarDatos(PlayerPrefs.GetString("id_usuario"), buenas, malas, ValorPorcentaje, can_intro, fechaInicioIntento,fechaTerminoIntento,fechaInicioActiv,retiroActiv, listareg,DuracionIntento());
        

    }

    public void Reiniciar()
    {
        malas = 0;
        buenas = 0;
        //indicador = 0;
        incompleto = 0;
        cont = 0;
        productosDesordenados.Clear();
        RandomList();
        PosicionarProductos();
        pintarText();
        DisabledImagen();
        panel.gameObject.SetActive(false);
        titulo.gameObject.SetActive(false);//.enabled = true;
        textBuenas.gameObject.SetActive(false);
        textMalas.gameObject.SetActive(false);
        textTiempo.gameObject.SetActive(false);
        resultBuenas.gameObject.SetActive(false);
        resultMalas.gameObject.SetActive(false);
        resultTiempo.gameObject.SetActive(false);
        finalizar.gameObject.SetActive(false);
        reiniciar.gameObject.SetActive(false);
        InvokeRepeating("AgregarProducto", 1f, 2f);
        TimerA1.instanciaCompartida.ResumeTimer();
        TimerA1.instanciaCompartida.ResetTimer();
        this.estado = 2;
        fechaInicioIntento = DateTime.Now;
        fechaInicioIntento = DateTime.Now;
        listareg.Clear();
    }

    public void Finalizar()
    {
        fechaTerminoActiv = DateTime.Now;
        EnvioDatosBD.instanciaEDBD.EnviarDatosActiv(fechaInicioActiv, fechaTerminoActiv, Duracion(), PlayerPrefs.GetString("id_usuario"));
        SceneManager.LoadScene("SeleccionarActividad");

    }

    public void Salir()
    {
        if(estado == 2) {
            retiroActiv = true;
            fechaTerminoActiv = DateTime.Now;
            fechaRetiro = DateTime.Now;
            CalcularPorcentaje();
            EnvioDatosBD.instanciaEDBD.EnviarDatos(PlayerPrefs.GetString("id_usuario"), buenas, malas, ValorPorcentaje, can_intro, fechaInicioIntento, fechaTerminoIntento, fechaInicioActiv, retiroActiv, listareg, DuracionIntento());
        }
               
        EnvioDatosBD.instanciaEDBD.EnviarDatosActiv(fechaInicioActiv,fechaTerminoActiv,Duracion(), PlayerPrefs.GetString("id_usuario"));
        SceneManager.LoadScene("SeleccionarActividad");
    }

    public void GeneraOperacion() {

        int random_num;
        int random_num2;
        int random_oper;
        int random_orden;
        int resultFalse;
        int cascada;
        string simoper;

        System.Random rnum = new System.Random();
        random_num = rnum.Next(1, 11);
        random_num2 = rnum.Next(1, 11);
        random_oper = rnum.Next(1, 3);
        random_orden = rnum.Next(1, 5);

        if (random_num < random_num2) {
            cascada = random_num2;
            random_num2 = random_num;
            random_num = cascada;
        }

        if (random_oper == 1) {
            simoper = "+";
            resultVer = random_num + random_num2;
        }else{
            simoper = "-";
            resultVer = random_num - random_num2;
        }

        resultFalse = random_num + 2;
        if (resultFalse == resultVer) {
            resultFalse = random_num - 1;
        }
        num1.text = random_num2.ToString();
        num2.text = random_num.ToString(); 

        oper.text = simoper;

        if (random_orden == 1 || random_orden == 3)
        {
            result1.text = resultVer.ToString();
            result2.text = resultFalse.ToString();
        }
        else {
            result1.text = resultFalse.ToString();
            result2.text = resultVer.ToString();
        }

        panelOperacion.SetActive(true);
        this.estado = 4;

    }

    public void RespuestaOperacion(int tip_buttom) {

        string request = "";
        string resultAlum = "";

        if (tip_buttom == 1)
        {
            resultAlum = result1.text;
            if (resultVer.ToString() == result1.text)
            {
                ValidaProducto(new_producto);
                ContarRespuestas(1);
                request = "Correcto";
            }
            else {
                panelOperacion.SetActive(false);
                StartCoroutine(Uncheck());
                ContarRespuestas(2);
                TimerA1.instanciaCompartida.ResumeTimer();
                request = "Incorrecto";
            }
        }
        else {
            resultAlum = result2.text;
            if (resultVer.ToString() == result2.text)
            {
                ValidaProducto(new_producto);
                ContarRespuestas(1);
                request = "Correcto";
            }
            else {
                panelOperacion.SetActive(false);
                StartCoroutine(Uncheck());
                ContarRespuestas(2);
                TimerA1.instanciaCompartida.ResumeTimer();
                request = "Incorrecto";
            }
        }

        this.estado = 2;
        //RegistroProd reg = new RegistroProd(num1.text, num2.text, oper.text, resultAlum, request);
        string reg = "Número 1: " +num2.text + " | Operador: " + oper.text + " | Número 2: " + num1.text + " | ResultCorrecto: " + resultVer.ToString() + " | ResultAlumno: " + resultAlum + " | IndRespuesta:" + request;
        //listareg.Add(reg);
        listareg.Add(reg);
    }

    public void FinalizarporTiempo() {
        resultTiempo.text = "0";
        finishGame.gameObject.SetActive(true);
        CancelInvoke();
        StartCoroutine(WaitSecond());
        this.estado = 3;
    }

    IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(3);
        finishGame.gameObject.SetActive(false);
        MuestraResultados();
    }

    public void ContarRespuestas(int respuesta) {
        
        if (respuesta == 1)
        {
            buenas = buenas + 1;
        }
        else
        {
            malas = malas + 1;
        }

    }

    public void Instrucciones(int ind) {

        if (this.estado != 3)
        {
            if (ind == 1)
            {
                CambiarAudio(2);
                panelInstrucciones.SetActive(true);
                TimerA1.instanciaCompartida.PauseTimer();
                this.estado = 1;
                can_intro = can_intro + 1;
            }
            else
            {
                CambiarAudio(1);
                panelInstrucciones.SetActive(false);
                TimerA1.instanciaCompartida.ResumeTimer();
                this.estado = 2;
            }
        }
    }

    public void CalcularPorcentaje()
    {

        int con_prod = productosPartida.Count - 5;
        ValorPorcentaje = ((buenas * 100) / con_prod);
        porcentaje.text = ValorPorcentaje.ToString() + "%";
    }

    public string Duracion() {

        string tiempo = "";

        TimeSpan ts = fechaTerminoActiv - fechaInicioActiv;
        tiempo = ts.Seconds.ToString();

        return tiempo;
    }

    public string DuracionIntento()
    {
        string tiempointento = "";

        TimeSpan ts = fechaTerminoIntento - fechaInicioIntento;
        tiempointento = ts.Seconds.ToString();

        return tiempointento;

    }

    public void CambiarAudio(int ind) {

        if (ind == 1)
        {
            pepe.clip = paloma;
            pepe.Play();

        }
        else {
            pepe.clip = paloma2;
            pepe.Play();
        }

    }


}
