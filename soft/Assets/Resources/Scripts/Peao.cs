using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peao : MonoBehaviour
{
    [SerializeField]
    Transform[] pontos;

    [SerializeField]
    float moveSpeed = 0.5f;
    public int posicao = 0;
    public int pontoIndex = 0;
    public GameObject dado;
    public int tirou6 = 0;
    Vector2 originalPos;
    Vector2 currentPos;
    Vector2 finalPos;
    bool moving = false;
    public GameObject casateste;
    //possiveis cores do objeto
    public enum cor
    {
        azul,
        vermelho,
        amarelo,
        verde
    }

    //cor do objeto
    public cor corPeca;

    // Use this for initialization
    void Start()
    {
        originalPos = gameObject.transform.position;
        finalPos = GameObject.Find("PontosDoTabuleiro/Ponto Final").transform.position;
    }

    private void OnMouseDown()
    {

        if (!dado.GetComponent<Dado>().rodando && !dado.GetComponent<Dado>().endGame && dado.GetComponent<Dado>().getTurno() == corPeca.ToString() && dado.GetComponent<Dado>().isAtivo())
        {

            if (currentPos != originalPos)
            {
                pontoIndex = dado.GetComponent<Dado>().getLadoFinal();

                StartCoroutine(Mover(pontoIndex, false));


            }
            else
            {

                if (dado.GetComponent<Dado>().getLadoFinal() == 6 || dado.GetComponent<Dado>().getLadoFinal() == 1)
                {
                    pontoIndex = 1;
                    StartCoroutine(Mover(pontoIndex, true));

                }
                else if(posicao != 0)
                {
                    dado.GetComponent<Dado>().setVez(); //passa a vez
                    dado.GetComponent<Dado>().resetDado(); //reseta o dado
                }
            }
        }

    }

    private IEnumerator Mover(int pi, bool saida)
    {
        // posicao += pontoIndex;
        //Debug.Log("oi");
        moving = true;
        for (int i = 0; i < pi; i++)
        {
            //Debug.Log(posicao.ToString());
            posicao++;
            transform.position = Vector2.MoveTowards(pontos[posicao].transform.position, transform.position, moveSpeed * Time.deltaTime);
            if (posicao == 44)
            {
                Debug.Log(pi - i - 1);
                if (pi - i - 1 == 0)
                {
                    GameObject.Find("UI/pontuacao").GetComponent<Pontuacao>().ponto((int)corPeca);
                    Destroy(this.gameObject);
                }
                else
                {
                    Debug.Log("ultimo" + (pi - i).ToString() + (0 < (pi - i)));
                    for (int j = 0; j < (pi - i - 1); j++)
                    {
                        Debug.Log(posicao.ToString());
                        posicao--;
                        transform.position = Vector2.MoveTowards(pontos[posicao].transform.position, transform.position, moveSpeed * Time.deltaTime);
                        if (i == pontoIndex - 1) moving = false;
                        yield return new WaitForSeconds(0.4f);

                    }
                    break;
                }
            }
            if (i == pontoIndex - 1) moving = false;
            yield return new WaitForSeconds(0.4f);

        }
        if (!saida)
        {
            if (pontoIndex != 6 && tirou6 == 0)
            {
                dado.GetComponent<Dado>().setVez();
                //Debug.Log(dado.GetComponent<Dado>().getTurno());
                dado.GetComponent<Dado>().resetDado();
            }
            else if (pontoIndex == 6 && tirou6 == 0)
            {
                GameObject.Find("UI/Rodada").GetComponent<Rodada>().turno("");
                Debug.Log("Você tirou 6! Jogue novamente");
                dado.GetComponent<Dado>().resetDado();
                tirou6 = 1;
            }
            else if (tirou6 == 1)
            {
                dado.GetComponent<Dado>().setVez();
                //Debug.Log(dado.GetComponent<Dado>().getTurno());
                dado.GetComponent<Dado>().resetDado();
                tirou6 = 0;
            }
        }
        else
        {
            if (tirou6 == 1)
            {
                dado.GetComponent<Dado>().setVez(); //passa a vez
                dado.GetComponent<Dado>().resetDado(); //reseta o dado
                tirou6 = 0;
            }
            if (dado.GetComponent<Dado>().getLadoFinal() == 6 )
            {
                
                GameObject.Find("UI/Rodada").GetComponent<Rodada>().turno("");
                Debug.Log("Você tirou 6! Jogue novamente");
                dado.GetComponent<Dado>().resetDado();
                tirou6 = 1;
            }
            else
            {
                dado.GetComponent<Dado>().setVez(); //passa a vez
                dado.GetComponent<Dado>().resetDado(); //reseta o dado
            }
        }

        Debug.Log("Por favor, jogador " + dado.GetComponent<Dado>().getTurno() + " gire o dado");


    }


    // Update is called once per frame
    void Update()
    {
        currentPos = gameObject.transform.position;
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (!moving && (dado.GetComponent<Dado>().getTurno() == this.corPeca.ToString() ) && this.corPeca.ToString() != collision.GetComponent<Peao>().corPeca.ToString())
        {
            Debug.Log("Turno ("+ dado.GetComponent<Dado>().getTurno()+") =>"+ this.corPeca.ToString()+" comeu "+ collision.GetComponent<Peao>().corPeca.ToString());
            collision.gameObject.transform.position = collision.gameObject.GetComponent<Peao>().originalPos;
            collision.gameObject.GetComponent<Peao>().posicao = 0;

            Debug.Log("colidiu");
        }

    }
    

    private void OnMouseOver()
    {
        if (dado.GetComponent<Dado>().getTurno() == this.corPeca.ToString())
            this.GetComponent<Animator>().SetTrigger("Over");
        this.GetComponent<Animator>().SetBool("moving", (!moving && !dado.GetComponent<Dado>().rodando));
    }
    private void OnMouseExit()
    {
        this.GetComponent<Animator>().SetTrigger("notOver");
        this.GetComponent<Animator>().SetBool("moving", (!moving && !dado.GetComponent<Dado>().rodando));
    }
}
