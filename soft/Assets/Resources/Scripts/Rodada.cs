using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rodada : MonoBehaviour {
    [SerializeField]
    public float delay = 0.4f;
    public enum cor
    {
        azul,
        vermelho,
        amarelo,
        verde
    }
    // Use this for initialization
    void Start() {

        
    }

    // Update is called once per frame
    void Update() {

    }
    public void turno(string vez)
    {
        
        
        if(vez == "") this.GetComponent<Text>().text = "Jogue novamente";
        if(vez!="")this.GetComponent<Text>().text = "Turno do " + vez;
       
        switch (vez)
        {
            case "azul":
                this.GetComponent<Text>().color = Color.blue;
                break;
            case "vermelho":
                this.GetComponent<Text>().color = Color.red;
                break;
            case "amarelo":
                this.GetComponent<Text>().color = Color.yellow;
                break;
            case "verde":
                this.GetComponent<Text>().color = Color.green;
                break;
        }
        this.GetComponent<Animator>().SetTrigger("show");
    }

   
}
    
