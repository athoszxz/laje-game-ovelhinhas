using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class admJogo : MonoBehaviour
{
    public TextMeshProUGUI textoTempo;
    public float tempoInicial = 60f;
    private float tempo;
    private bool contandoTempo;
    private int ovelhasTotal;
    private int ovelhasPerdidas = 0;
    private int ovelhasCapturadas = 0;
    public TextMeshProUGUI textoOvelhas;
    private int ovelhasObjetivo;
    public string nomeproximaFase;
    
    private void proximaFase()
    {
        SceneManager.LoadScene(nomeproximaFase);
    }

    public static admJogo instance;
    private void Awake()
    {
        #region Singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
        #endregion
    }

    private void perderJogo()
    {
        SceneManager.LoadScene("TelaDerrota");
    }

    public void perderOvelha()
    {
        ovelhasPerdidas++;
        configTextoOvelhas();
        if(ovelhasTotal - ovelhasPerdidas < ovelhasObjetivo)
        {
            perderJogo();
        }
    }

    public void capturarOvelha()
    {
        ovelhasCapturadas++;
        configTextoOvelhas();
        if (ovelhasCapturadas >= ovelhasObjetivo)
        {
            proximaFase();
        }
    }

    public int totalOvelhas()
    {
        return GameObject.FindGameObjectsWithTag("Ovelhas").Length;
    }

    public void configTextoOvelhas()
    {
        textoOvelhas.text = ovelhasCapturadas + "/" + ovelhasTotal / 2 + "\n\n\n" + ovelhasPerdidas + "/" + ovelhasTotal;
    } 

    public void startTempo()
    {
        tempo = tempoInicial;
        contandoTempo = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        startTempo();
        ovelhasTotal = totalOvelhas();
        configTextoOvelhas();
        ovelhasObjetivo = ovelhasTotal / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (contandoTempo)
        {
            tempo -= Time.deltaTime;

            //float hours = (tempo / 3600);
            float minutes = Mathf.Floor(tempo / 60);
            float seconds = Mathf.RoundToInt(tempo % 60);
            textoTempo.text = $"{minutes:00}:{seconds:00}";
            
            if(tempo <= 0f)
            {
                tempo = 0;
                
                contandoTempo = false;

                textoTempo.text = $"{0:00}:{0:00}";            
                perderJogo();
            }

        }

    }
}
