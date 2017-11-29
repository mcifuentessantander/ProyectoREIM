using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class EnvioContexto : MonoBehaviour {

    public static EnvioContexto instanciaCtxto;

    private void Awake()
    {
        instanciaCtxto = this;
    }

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://reims-4d00f.firebaseio.com/");

    }

    public void EnviarDatosConte( DateTime InicioIntento, DateTime terminoIntento, DateTime fecretiro, bool retiro,string usuario)
    {

        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        BdContexto dato = new BdContexto(InicioIntento.ToLongTimeString(), terminoIntento.ToLongTimeString(), fecretiro.ToLongTimeString(), retiro, usuario);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("Matemáticas").Child(usuario).Child("Laberinto").Push().SetRawJsonValueAsync(json);


    }
}
