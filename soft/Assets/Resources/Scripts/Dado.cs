using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dado : MonoBehaviour {

    //Array dos sprites dos lados do dado na pasta
    private Sprite[] dadoLados;

    //referencia pra spriterenderer pra mudar as sprites
    private SpriteRenderer rend;
    private int ladofinal;
    public float delay = 0.05f;
    private int vez = 0; //de quem é a vez
    private int ativo = 0; //dado ativo = 1 nao ativo = 0
    public bool rodando = false;
    public bool endGame = false;
    public bool dadoViciado = false;
    int inic=0;

    //jogador ja jogou, entao reseta o dado
    public void resetDado()
    {
        this.ativo = 0;
    }

    //dado ativo?
    public bool isAtivo()
    {
        if (ativo == 1)
            return true;
        else
            return false;
    }

    //passa a vez pro proximo jogador
    public void setVez()
    {

        if (this.vez == 3)
        {
            this.vez = 0;
        }
        else
            this.vez = this.vez + 1;
        GameObject.Find("UI/Rodada").GetComponent<Rodada>().turno(Turno[vez]);
    }


    //private int deQuemEaVez = 1;
    //private bool coroutineAllowed = true;

    public string[] Turno = { "azul", "vermelho", "verde", "amarelo" };
    public string getTurno()
    {
        return Turno[vez];
    }
    public string getTurnoAnterior()
    {
        if (vez == 0) return Turno[3];
        return Turno[vez-1];
    }
    // Use this for initialization
    private void Start () {
        if (dadoViciado) inic = 5;
        GameObject.Find("UI/Rodada").GetComponent<Rodada>().turno(Turno[vez]);
        //atribuir componente do renderizador de sprite
        rend = GetComponent<SpriteRenderer>();

        //carregar imagens da Pasta de origem das sprites para o array 
        dadoLados = Resources.LoadAll<Sprite>("Sprites/Lados_Dado/");
        rend.sprite = dadoLados[5];
	}

    //ao clicar com o mouse a co-rotina é iniciada
    private void OnMouseDown()
    {
        if (!endGame)
        {
            StartCoroutine("GireODado");
            ativo = 1;
        }
    }

    public int girarDadoTeste()
    {
        StartCoroutine("GireODado");
        return this.getLadoFinal();
    }


    //co-rotina que gira o dado
    private IEnumerator GireODado()
    {
        rodando = true;
        //variavel que contem um lado aleatorio do dado
        //precisa ser definido
        int ladoDadoRandom = 0;

        //lado final que o dado lê no final da execuçao da co-rotina
        int ladoFinal = 0;

        //loop de embaralhamento
        for( int i = 0; i < 20; i++)
        {
            //pega um valor aleatorio de 0 a 5
            ladoDadoRandom = Random.Range(inic, 6);

            //coloca a face pra cima do numero aleatorio
            rend.sprite = dadoLados[ladoDadoRandom];

            //breve pausa antes da proxima iteraçao
            yield return new WaitForSeconds(delay);
        }

        //atribuindo valor final
        ladoFinal = ladoDadoRandom + 1;
        this.ladofinal = ladoFinal;
        rodando = false;
        //mostrar no console
        if((ladoFinal<6 && ladoFinal > 1) && !isOutOfBase())
        {
            Debug.Log((ladoFinal < 6) +" "+ (ladoFinal > 1) +" "+ (!isOutOfBase()));
            Debug.Log(ladofinal.ToString() + " " + isOutOfBase());
            setVez(); //passa a vez
            resetDado(); //reseta o dado
            GameObject.Find("UI/Rodada").GetComponent<Rodada>().turno(getTurno());
        }
        Debug.Log("Turno do jogador " + Turno[vez]);
        Debug.Log("Escolha uma peça e avance " +ladoFinal +" casas");
    }
      
    public int getLadoFinal()
    {
        return this.ladofinal;
    }
    
// Update is called once per frame
void Update () {
        if (Input.anyKeyDown && endGame) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public bool isOutOfBase()
    {
        int cont = 0;
        GameObject[] pinos = GameObject.FindGameObjectsWithTag("pino");
        foreach (GameObject pin in pinos)
        {
            if (pin.GetComponent<Peao>().corPeca.ToString() == getTurno() && pin.GetComponent<Peao>().posicao == 0)
                cont++;
        }
        if (cont == 4) return false;
        return true;
    }
    private void OnMouseOver()
    {
        
        if(!rodando && !endGame)this.GetComponent<Animator>().SetTrigger("Over");
        else this.GetComponent<Animator>().SetTrigger("notOver");

    }
    private void OnMouseExit()
    {
        this.GetComponent<Animator>().SetTrigger("notOver");
        
    }
    
}
