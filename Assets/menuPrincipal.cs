using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    public void comecarJogo()
    {
        AkSoundEngine.PostEvent("uiClick", gameObject);
        SceneManager.LoadScene("Fase1");
    }

}
