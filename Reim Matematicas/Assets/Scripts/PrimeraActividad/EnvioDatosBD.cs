using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvioDatosBD : MonoBehaviour {
    public static EnvioDatosBD instanciaEDBD;

    DatabaseReference reference;
    private void Awake()
    {
        instanciaEDBD = this;
    }

    private void Start()
    {
        
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://reims-4d00f.firebaseio.com/");

        // Get the root reference location of the database.
        
    }

    public void EnviarDatos(string user, int correctas, int incorrectas, int porcentaje, int instruc, DateTime InicioIntento, DateTime terminoIntento, DateTime inicioActiv, bool retiro, List<string> regProd,string duracion)
    {
        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        DatosClase dato = new DatosClase(user, correctas, incorrectas, porcentaje, instruc, InicioIntento.ToLongTimeString(), terminoIntento.ToLongTimeString(), inicioActiv.ToLongTimeString(), retiro, regProd, duracion);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("Matemáticas").Child(user).Child("PrimeraActividad").Child("RegistroIntentos").Push().SetRawJsonValueAsync(json);

        // Get the root reference location of the database.
        //reference = FirebaseDatabase.DefaultInstance.RootReference;
        //    this.fechaInicioIntento = BaseDatosTimeA1.instanciaBDTA1.fechaInicioIntento;
        //    this.fechaTerminoIntento = BaseDatosTimeA1.instanciaBDTA1.fechaTerminoIntento;
        //reference.Child("Intento").Child(fechaInicioIntento.ToShortDateString()).SetValueAsync(can_intro.ToString(), ValorPorcentaje.ToString(), buenas.ToString(), user, this.fechaInicioIntento.ToShortDateString(),this.fechaTerminoIntento.ToShortDateString());
        //reference.Child("Intento").Push().SetValueAsync(can_intro.ToString(), ValorPorcentaje.ToString(), buenas.ToString(),user,listReg);

    }

    public void EnviarDatosActiv(DateTime InicioActiv, DateTime terminoActiv, string duracion,string user)
    {
        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        DatosClaseActividad dato = new DatosClaseActividad(InicioActiv.ToLongTimeString(), terminoActiv.ToLongTimeString(), duracion, user);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("Matemáticas").Child(user).Child("PrimeraActividad").Child("RegistroActividad").Push().SetRawJsonValueAsync(json);
    }

}
