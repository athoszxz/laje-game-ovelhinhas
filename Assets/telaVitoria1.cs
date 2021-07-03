using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class telaVitoria1 : MonoBehaviour
{
    public string proximaFase;

    public void proximaCena()
    {
        SceneManager.LoadScene(proximaFase);
    }
}
