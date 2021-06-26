using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovelha : MonoBehaviour
{
    Transform myTransform;
    public Vector2 alvo;
    public float velocidade = 5f;
    private bool andando = true;
    public Vector3 temper;
    public float offset;

    void andarAleatorio()
    {
        Vector2 temp;
        alvo = new Vector2(Random.Range(6f, -6f), (Random.Range(-4f, 4f)));
        //myTransform.LookAt(alvo, temper);
        temp.x = alvo.x - myTransform.position.x;
        temp.y = alvo.y - myTransform.position.y;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        myTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }

    void move()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, alvo, velocidade*Time.deltaTime);
        if (Vector2.Distance(myTransform.position, alvo) < 0.1f)
        {
            andarAleatorio();
        }
    }

    private void Awake()
    {
        myTransform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (andando)
        {
            move();
        }
        
    }
}
