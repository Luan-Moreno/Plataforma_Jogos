using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float velocidade = 2f;
    public Transform alvo;
    public int vidaMaxima = 2;
    private int vidaAtual;

    private Vector3 escalaOriginal; // <- salva a escala original

    void Start()
    {
        vidaAtual = vidaMaxima;
        escalaOriginal = transform.localScale; 
        Collider2D[] moedas = GameObject.FindGameObjectsWithTag("Moeda").Select(go => go.GetComponent<Collider2D>()).ToArray();                   

        foreach (Collider2D moeda in moedas)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), moeda);
        }
    }

    void Update()
    {
        if (alvo == null) return;

        Vector3 direcao = (alvo.position - transform.position).normalized;
        transform.position += direcao * velocidade * Time.deltaTime;

        if (direcao.x != 0)
        {
            transform.localScale = new Vector3(
                (Mathf.Sign(direcao.x) * Mathf.Abs(escalaOriginal.x)) * -1f,
                escalaOriginal.y,
                escalaOriginal.z
            );
        }
    }

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject);
    }
}
