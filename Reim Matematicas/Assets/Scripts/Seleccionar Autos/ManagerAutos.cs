using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ManagerAutos : MonoBehaviour
{

    private Autos auto;
    public List<Autos> listaAutos = new List<Autos>();
    public Canvas mensaje;
    private int ident_selec = 0;
    public GameObject instrucActividad;
    // int estado = 0;


    void Start()
    {
        BlqAuto();
        mensaje.enabled = false;
    }

    void Update()
    {
        SeleccionarAuto();
      }

    void BlqAuto()
    {
        foreach (Autos guy in listaAutos)
        {
            if (guy.bloqueado == true)
            {
                guy.GetComponent<SpriteRenderer>().color = Color.black;
            }

        }
    }

    void SeleccionarAuto() {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                foreach (Autos guy in listaAutos)
                {
                    if (guy.nombre == hit.collider.gameObject.transform.name)
                    {
                        if (guy.bloqueado == false)
                        {
                            guy.seleccionado = true;
                            guy.transform.localScale = new Vector2(1.6f, 1.6f);
                            ident_selec = 1;
                        }
                        else{
                            
                            mensaje.enabled = true;
                            ident_selec = 0;
                        }
                    }
                    else {
                        guy.seleccionado = false;
                        guy.transform.localScale = new Vector2(1.3f, 1.3f);
                        
                    }

                }                
            }
        }

    }

    public void SalirMensaje() {
        mensaje.enabled = false;
    }

    public void ChangeEscene() {

        if (ident_selec == 1)
        {
            SceneManager.LoadScene("Contexto");
        }
    }

    public void Volver()
    {
       SceneManager.LoadScene("InicioSesion");
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
/*PAra detectar touch
 * 
 * 
 * 
 * 
 *         int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            print(nbTouches + " touch(es) detected");

            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i);

                print("Touch index " + touch.fingerId + " detected at position " + touch.position);
            }
        }
*/