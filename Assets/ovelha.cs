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
    private bool fugindo;
    private Transform player;
    private int cachorrosPerto = 0;
    public float[] limitesAndar;
    public bool capturada = false;
    public Sprite imagemCaveira;
    public bool desaparecendo = false;
    public Transform alvoFuga;
    public float velocidadeFugir = 6f;
    public Collider2D colisorFisico;
    public float tempoMudarDir = 3f;
    private float tempoMudarAtual = 0f;

    public void morrer()
    {
        GetComponent<SpriteRenderer>().sprite = imagemCaveira;
        Invoke("desaparecer", 4f);
        desaparecendo = true;
        colisorFisico.enabled = false;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        admJogo.instance.perderOvelha();
    }

    public void desaparecer()
    {
        Destroy(this.gameObject);
    }

    public void prenderCerca(float[] limites)
    {
        if (!capturada)
        {
            limitesAndar = limites;
            capturada = true;
            admJogo.instance.capturarOvelha();
        }
        
    }

    void andarAleatorio()
    {
        tempoMudarAtual = tempoMudarDir;
        Vector2 temp;
        alvo = new Vector2(Random.Range(limitesAndar[0], limitesAndar[1]), (Random.Range(limitesAndar[2], limitesAndar[3])));
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

    void fugir()
    {
        Vector2 direcao = new Vector2(myTransform.position.x - player.position.x, myTransform.position.y - player.position.y);
        direcao = new Vector2(direcao.normalized.x + transform.position.x, direcao.normalized.y + transform.position.y);
        myTransform.position = Vector2.MoveTowards(myTransform.position, direcao,  velocidadeFugir * Time.deltaTime);
       
        Vector2 temp;
        
        temp.x = direcao.x - myTransform.position.x;
        temp.y = direcao.y - myTransform.position.y;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        myTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

        //var heading = player.position - transform.position;
        //var distance = heading.magnitude;
        //var direction = - heading / distance;
        //myTransform.position = Vector2.MoveTowards(myTransform.position, direction, velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            fugindo = true;
            cachorrosPerto++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cachorrosPerto--;
            if(cachorrosPerto <= 0)
            {
                fugindo = false;
                andarAleatorio();
            }
        }
    }

    private void Awake()
    {
        myTransform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        andarAleatorio();
    }

    // Update is called once per frame
    void Update()
    {
        AkSoundEngine.PostEvent("sheepBeeh1", gameObject);
        if (!desaparecendo)
        {
            if (fugindo)
            {
                fugir();
            }
            else
            {
                move();
                if(tempoMudarAtual > 0f)
                {
                    tempoMudarAtual -= Time.deltaTime;
                }
                else
                {
                    andarAleatorio();
                }
            }
        }

        
    }
}
