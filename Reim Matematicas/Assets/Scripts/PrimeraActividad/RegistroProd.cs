using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistroProd : MonoBehaviour {

    public string numero1;
    public string numero2;
    public string operacion;
    public string respuesta;
    public string respCorrecta;

    public RegistroProd(string num1, string num2,string oper, string request,string correcta) {

        this.numero1 = num1;
        this.numero2 = num2;
        this.respuesta = request;
        this.respCorrecta = correcta;
        this.operacion = oper;
    }
}
