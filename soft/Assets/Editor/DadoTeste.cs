using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DadoTeste{

	[Test]
    public void getTurno_teste()
    {
        var Dado = new Dado();
        Dado.setVez();
        Dado.setVez();
        var esperado = "verde";

        var teste = Dado.getTurno();

        Assert.That(teste, Is.EqualTo(esperado));

    }

    [Test]
    public void getTurnoAnterior_teste()
    {
        var Dado = new Dado();
        Dado.setVez();
        var esperado = "azul";

        var teste = Dado.getTurnoAnterior();

        Assert.That(teste, Is.EqualTo(esperado));
    }
    
    

    [Test]
    public void isativo_teste()
    {
        var Dado = new Dado();
        var teste = Dado.isAtivo();

        Assert.That(teste, Is.False);
    }

    [Test]
    public void isOutofBase_teste()
    {
        var Dado = new Dado();
        Dado.setVez();
        var teste = Dado.isOutOfBase();

        Assert.That(teste, Is.False);
    }
    [Test]
    public void dado()
    {
        GameObject dado = GameObject.Find("Dado");
        
        Debug.Log(dado.GetComponent<Dado>().girarDadoTeste());
    }


}
