using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerActivity : MonoBehaviour {

    public GameObject instrucActividad;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Volver() {

        SceneManager.LoadScene("SeleccionarAuto");
    }

    public void IrPrimeraActividad()
    {
        SceneManager.LoadScene("Actividad1");
    }

    public void IrTerceraActividad()
    {
        SceneManager.LoadScene("Actividad3");
    }

    public void IrSegundaActividad()
    {
        SceneManager.LoadScene("Actividad2");
    }

    public void AbrirInstrucciones()
    {
        instrucActividad.gameObject.SetActive(true);
    }

    public void CerrarInstrucciones()
    {
        instrucActividad.gameObject.SetActive(false);
    }
}
