using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raposaAcoes : MonoBehaviour
{
    public Transform[] tocas;
    private Transform tocaAtual;
    private bool andando;
    public float[] limitesAndar;
    public float offset;
    public Vector2 alvo;
    public float velocidade = 5f;
    private bool seguindo;
    private Transform ovelha;
    private bool voltando;
    private bool esperando;
    public float tempoEspera = 10f;
    public Sprite imagemCaveira;
    public Collider2D colisorFisico;
    private bool desaparecendo;
    private Transform player;
    public float velocidadeFugir = 5f;
    private bool fugindo;
    private int cachorrosPerto;

    public void morrer()
    {
        GetComponent<SpriteRenderer>().sprite = imagemCaveira;
        Invoke("destruir", 4f);
        desaparecendo = true;
        colisorFisico.enabled = false;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void destruir()
    {
        Destroy(this.gameObject);
    }

    public void afastouCachorro()
    {
        cachorrosPerto--;
        if (cachorrosPerto <= 0)
        {
            fugindo = false;
        }
    }

    public void aproximouCachorro(Transform cachorro)
    {
        player = cachorro;
        fugindo = true;
        cachorrosPerto++;
    }

    void fugir()
    {
        Vector2 direcao = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
        direcao = new Vector2(direcao.normalized.x + transform.position.x, direcao.normalized.y + transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, direcao, velocidadeFugir * Time.deltaTime);

        Vector2 temp;

        temp.x = direcao.x - transform.position.x;
        temp.y = direcao.y - transform.position.y;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

        //var heading = player.position - transform.position;
        //var distance = heading.magnitude;
        //var direction = - heading / distance;
        //myTransform.position = Vector2.MoveTowards(myTransform.position, direction, velocidade * Time.deltaTime);
    }

    private void seguir()
    {
        Vector2 temp;
        transform.position = Vector2.MoveTowards(transform.position, ovelha.position, velocidade * Time.deltaTime);
        temp.x = ovelha.position.x - transform.position.x;
        temp.y = ovelha.position.y - transform.position.y;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        if (Vector2.Distance(transform.position, ovelha.position) <= 2f)
        {
            atacar();
        }
    }

    private void atacar()
    {
        ovelha.GetComponent<ovelha>().morrer();
        seguindo = false;
        voltando = true;
    }

    private void desaparecer()
    {
        transform.position = Vector2.one * 200f;
        esperando = true;
        definirToca();
        Invoke("brotar", tempoEspera);
    }

    private void voltarToca()
    {
        Vector2 temp;
        transform.position = Vector2.MoveTowards(transform.position, tocaAtual.position, velocidade * Time.deltaTime);
        temp.x = tocaAtual.position.x - transform.position.x;
        temp.y = tocaAtual.position.y - transform.position.y;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        if (Vector2.Distance(transform.position, tocaAtual.position) <= 2f)
        {
            desaparecer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ovelhas") && !seguindo && !collision.GetComponent<ovelha>().capturada)
        {
            ovelha = collision.transform;
            seguindo = true;
            andando = false;

        }
    }

    public void definirToca()
    {
        int temp = Random.Range(0, tocas.Length);
        tocaAtual = tocas[temp];
    }

    public void brotar()
    {
        transform.position = tocaAtual.position;
        andando = true;
        esperando = false;
        andarAleatorio();
    }

    void andarAleatorio()
    {
        Vector2 temp;
        alvo = new Vector2(Random.Range(limitesAndar[0], limitesAndar[1]), (Random.Range(limitesAndar[2], limitesAndar[3])));
        //myTransform.LookAt(alvo, temper);
        temp.x = alvo.x - transform.position.x;
        temp.y = alvo.y - transform.position.y;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }

    void move()
    {
        Vector2 temp;
        transform.position = Vector2.MoveTowards(transform.position, alvo, velocidade * Time.deltaTime);
        temp.x = alvo.x - transform.position.x;
        temp.y = alvo.y - transform.position.y;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        if (Vector2.Distance(transform.position, alvo) < 0.1f)
        {
            andarAleatorio();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        definirToca();
        brotar();
    }

    // Update is called once per frame
    void Update()
    {
        if (esperando || desaparecendo)
        {
            return;
        }
        else if (fugindo)
        {
            fugir();
        }
        else if(andando)
        {
            move();
        }
        else if (seguindo)
        {
            seguir();
        }
        else if (voltando)
        {
            voltarToca();
        }
    }
}
