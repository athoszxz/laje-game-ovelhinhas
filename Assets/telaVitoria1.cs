using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class telaVitoria1 : MonoBehaviour
{
    public string proximaFase;

    void Start()
    {
        AkSoundEngine.PostEvent("tadadadaah", gameObject);
    }
    public void proximaCena()
    {
        AkSoundEngine.PostEvent("uiClick", gameObject);
        SceneManager.LoadScene(proximaFase);
    }
}
