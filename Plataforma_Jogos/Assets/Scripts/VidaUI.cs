using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaUI : MonoBehaviour
{
    public Image[] coracoes;
    public Sprite cheio;
    public Sprite vazio;

    public void AtualizarVidas(int vidasAtuais)
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            if (i < vidasAtuais)
            {
                coracoes[i].sprite = cheio;
            }
               
            else
            {
                coracoes[i].sprite = vazio;
            }
               
        }
    }
}

