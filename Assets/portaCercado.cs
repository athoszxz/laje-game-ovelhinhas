using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class portaCercado : MonoBehaviour
{
    public float[] limitesAndar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OvelhaCorpo"))
        {
            collision.GetComponentInParent<ovelha>().prenderCerca(limitesAndar);
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
