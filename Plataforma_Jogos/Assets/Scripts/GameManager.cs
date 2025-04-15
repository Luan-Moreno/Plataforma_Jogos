using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text textoPontuacao;
    private int moedas = 0;
    private int moedasPorFase = 10;
    private int totalMoedas = 10; 

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        AtualizarPontuacao();
        textoPontuacao.text = "Pontuação: " + moedas.ToString() + "/" + totalMoedas;
    }

    void AtualizarPontuacao()
    {
        textoPontuacao.text = "Pontuação: " + moedas.ToString() + "/" + totalMoedas;
    }

    public void AdicionarMoeda()
    {
        moedas++;
        AtualizarPontuacao();
        if (moedas % moedasPorFase == 0)
        {
            TrocarCena();
        }
    }

    void TrocarCena()
    {
        totalMoedas += 10;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AtualizarPontuacao();
    }
}

