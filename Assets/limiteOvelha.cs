using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteOvelha : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OvelhaCorpo") && !collision.GetComponentInParent<ovelha>().desaparecendo)
        {
            admJogo.instance.perderOvelha();
            collision.GetComponentInParent<ovelha>().morrer();
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
