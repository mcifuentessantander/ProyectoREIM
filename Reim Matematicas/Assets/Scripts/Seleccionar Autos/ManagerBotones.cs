﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerBotones : MonoBehaviour {

    public void Volver() {

        SceneManager.LoadScene("InicioSesion");
    }
}
