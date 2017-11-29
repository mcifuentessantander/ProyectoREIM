using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ManagerSesion : MonoBehaviour {

    private GameObject[] respawns;
    public Button salir;
    public Button start;
    public InputField user;
    public InputField pass;
    public GameObject panelvalidaUser;
    public GameObject panelvalidaPass;
    public GameObject panelComparaUser;
    public GameObject panelComparaPass;
    public GameObject panelSinConexion;
    public string usuario = "";
    public string contraseña = "";
    public DateTime fecinicio;
    // Use this for initialization
    void Start() {
        
        CargaAuto();
        pass.onEndEdit.AddListener(delegate { ValidarSesion(pass); });
        user.onEndEdit.AddListener(delegate { ObtenerUsuario(user); });
    }

    // Update is called once per frame
    void Update() {
    }

    void CargaAuto()
    {
        respawns = GameObject.FindGameObjectsWithTag("Auto");

        foreach (GameObject guy in respawns)
        {
            Destroy(guy);
        }
    }

    public void Salir() {

        SceneManager.LoadScene("Inicio");
    }

    public void Comenzar()
    {
        if (usuario == "")
        {
            panelvalidaUser.gameObject.SetActive(true);
            return;
        }
        if (contraseña == "")
        {
            panelvalidaPass.gameObject.SetActive(true);
            return;
        }

        LeerBD(usuario, contraseña);
       
    }

    public void ValidarSesion(InputField pass) {

        contraseña = pass.text;
    }

    public void ObtenerUsuario(InputField user)
    {
        usuario = user.text;
    }

    public void SalirValUSer(int ind)
    {
        if (ind == 1)
        {
            panelvalidaUser.gameObject.SetActive(false);
        }
        else if (ind == 2) {
            panelvalidaPass.gameObject.SetActive(false);
        }
        else if (ind == 3)
        {
            panelComparaUser.gameObject.SetActive(false);
        }
        else if (ind == 4)
        {
            panelComparaPass.gameObject.SetActive(false);
        }
        else if (ind == 5)
        {
            panelComparaPass.gameObject.SetActive(false);
        }


    }

    public void LeerBD(string usuarioconsola, string contraseña)
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://reims-4d00f.firebaseio.com/");
        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance
        .GetReference("Reim-Primero-Basico").Child("alumnos")
        .GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                panelSinConexion.gameObject.SetActive(true);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Child(usuarioconsola).Exists)
                {

                    if (snapshot.Child(usuarioconsola).Child("contraseña").GetValue(true).ToString().Equals(contraseña))
                    {
                        string nombre = snapshot.Child(usuarioconsola).Child("nombre").GetValue(true).ToString();
                        PlayerPrefs.SetString("id_usuario", usuarioconsola);
                        EnviaDatosIS(usuarioconsola, DateTime.Now, DateTime.Now, nombre);
                        SceneManager.LoadScene("SeleccionarAuto");
                    }
                    else
                    {
                        panelComparaPass.gameObject.SetActive(true);
                        return;
                    }
                }
                else
                {
                    panelComparaUser.gameObject.SetActive(true);
                    return;
                }
            }
        });

    }

    public void EnviaDatosIS(string rut, DateTime inicio, DateTime hora,string nombre)
    {

        DatabaseReference referenciaFirebase = FirebaseDatabase.DefaultInstance.RootReference;
        DatosIniSesion dato = new DatosIniSesion(rut, inicio.ToLongDateString(), inicio.ToLongTimeString(),nombre);//listaRegistroProductos
        string json = JsonUtility.ToJson(dato);

        referenciaFirebase.Child("Reim-Primero-Basico").Child("InicioSesion").Push().SetRawJsonValueAsync(json);
    }

}
