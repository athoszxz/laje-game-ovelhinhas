using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passos : MonoBehaviour
{
    public float timeToStep;
    public string eventName;
    public Move move;
    private bool primeiraVez = true;
    //public Rigidbody2D rigidbody2D;
    private float timer;


    // Update is called once per frame
    void Update()
    {
        //if (true) return;
        if (move.isWalking && primeiraVez)
        {
            AkSoundEngine.PostEvent(eventName, gameObject);
            primeiraVez = false;
        }
        if (move.isWalking)
        {
            if (timer >= timeToStep)
            {
                AkSoundEngine.PostEvent(eventName, gameObject);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            primeiraVez = false;
        }
    }

}