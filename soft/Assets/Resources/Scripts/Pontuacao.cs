using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pontuacao : MonoBehaviour {
    public int[] pontuacao = new int[4];
    // Use this for initialization
    GameObject[] marcadores= new GameObject[4];
    public GameObject dado;
    public enum cor
    {
        azul,
        vermelho,
        amarelo,
        verde
    }
    void Start () {
        
        marcadores[0] = GameObject.Find("azulPlac");
        marcadores[1] = GameObject.Find("vermelhoPlac");
        marcadores[2] = GameObject.Find("amareloPlac");
        marcadores[3] = GameObject.Find("verdePlac");

        
        Debug.Log(marcadores.ToString());
    }
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < 4; i++)
        {
           marcadores[i].gameObject.GetComponent<Text>().text  = new string('.', pontuacao[i]); ;
        }
	}
    public void ponto(int peca)
    {
        pontuacao[peca]++;
        if (pontuacao[peca] == 4)
        {
            dado.GetComponent<Dado>().endGame = true;
            GameObject.Find("Win").GetComponent<Text>().text = ((cor)peca).ToString() + " Ganhou!!";
            GameObject.Find("Win").GetComponent<Animator>().SetTrigger("win");
            
        }
    }
}
