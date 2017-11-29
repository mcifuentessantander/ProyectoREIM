using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class EnvioDatosA3 : MonoBehaviour {

    public static EnvioDatosA3 instanciaEDA3;

    private void Awake()
    {
        instanciaEDA3 = this;
    }

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://reims-4d00f.firebaseio.com/");

    }

    public void EnviarDatosA3(string user, int correctas, int incorrectas, int porcentaje, int instruc, int panelOrden, DateTime InicioIntento, DateTime terminoIntento, DateTime inicioActiv, bool retiro, List<string> regProd, string duracion)
    {
        
        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        DatosIntentoA3 dato = new DatosIntentoA3(user, correctas, incorrectas, porcentaje, instruc, panelOrden,InicioIntento.ToLongTimeString(), terminoIntento.ToLongTimeString(), inicioActiv.ToLongTimeString(), retiro, regProd,duracion);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("Matemáticas").Child(user).Child("TerceraActividad").Child("RegistroIntentos").Push().SetRawJsonValueAsync(json);


    }

    public void EnviarDatosActivA3(DateTime InicioActiv, DateTime terminoActiv, string duracion, string user)
    {
        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        DatosActividadA3 dato = new DatosActividadA3(InicioActiv.ToLongTimeString(), terminoActiv.ToLongTimeString(), duracion, user);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("Matemáticas").Child(user).Child("TerceraActividad").Child("RegistroActividad").Push().SetRawJsonValueAsync(json);
    }
}
