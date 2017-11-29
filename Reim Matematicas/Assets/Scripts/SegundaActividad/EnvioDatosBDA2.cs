using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvioDatosBDA2 : MonoBehaviour {

    public static EnvioDatosBDA2 instanciaBDA2;

    DatabaseReference reference;
    private void Awake()
    {
        instanciaBDA2 = this;
    }

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://reims-4d00f.firebaseio.com/");
  
    }

    public void EnviarDatosA2(string user, int unatorta, int dostortas, int trestortas, int cincotortas, int dieztortas, int instruc, DateTime InicioIntento, DateTime terminoIntento, DateTime inicioActiv, bool retiro, bool pasTotal, string duracion)
    {
        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        DatosIntentosA2 dato = new DatosIntentosA2(user, unatorta, dostortas, trestortas, cincotortas, dieztortas, instruc, InicioIntento.ToLongTimeString(), terminoIntento.ToLongTimeString(), inicioActiv.ToLongTimeString(), retiro, pasTotal, duracion);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("Matemáticas").Child(user).Child("SegundaActividad").Child("RegistroIntentos").Push().SetRawJsonValueAsync(json);

    }

    public void EnviarDatosActivA2(DateTime InicioActiv, DateTime terminoActiv, string duracion, string user)
    {
        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        DatosActividadA2 dato = new DatosActividadA2(InicioActiv.ToLongTimeString(), terminoActiv.ToLongTimeString(), duracion, user);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("Matemáticas").Child(user).Child("SegundaActividad").Child("RegistroActividad").Push().SetRawJsonValueAsync(json);
    }
}
