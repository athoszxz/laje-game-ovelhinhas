using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class telaDerrota : MonoBehaviour
{
    public void voltarMenu()
    {
        AkSoundEngine.PostEvent("uiClick", gameObject);
        SceneManager.LoadScene("MenuPrincipal");
    }
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("derrota", gameObject);
    }
    public void OnDestroy()
    {
        AkSoundEngine.StopAll();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
