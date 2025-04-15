using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class PlayerController : MonoBehaviour
{
    public KeyCode moveRight = KeyCode.D;      
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Ataque = KeyCode.Mouse0;
    public float speed = 10.0f;             
    public float boundY = 2.25f;
    public float jumpForce = 5f;            
    private Rigidbody2D rb2d;
    private bool isGrounded = false;
    public int maxLives = 3;
    private int currentLives;
    public VidaUI uiVida;
    Animator anim;
    public GameManager gm;


    // Ataque
    public Transform AreaAtaque;
    public float raioAtaque = 0.5f;
    public int dano = 1;
    public float tempoEntreAtaques = 0.5f;
    private float tempoProximoAtaque = 0f;

    public GameObject slashPrefab;
    public float tempoExibicaoSlash = 0.2f;

    // Moeda
    public int pontos = 0;
    public TMP_Text textoPontuacao;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentLives = maxLives;
        uiVida.AtualizarVidas(currentLives);
    }

    void Update()
    {
        var vel = rb2d.velocity;                
        if (Input.GetKey(moveRight)) 
        {             
            vel.x = speed;
            anim.SetFloat("xVelocity", vel.x);
            anim.SetFloat("yVelocity", vel.y);
        }
        else if (Input.GetKey(moveLeft)) 
        {      
            vel.x = -speed;
            anim.SetFloat("xVelocity", -vel.x);
            anim.SetFloat("yVelocity", -vel.y);                    
        }
        else 
        {
            vel.x = 0;
            anim.SetFloat("xVelocity", vel.x);
            anim.SetFloat("yVelocity", vel.y);                             
        }
        rb2d.velocity = vel;

        if(Input.GetKey(Jump) && isGrounded)
        {
            anim.SetBool("isJumping", true);
            rb2d.velocity = new Vector2(vel.x, jumpForce);
            isGrounded = false;
        }
        else anim.SetBool("isJumping", false);                    

        var pos = transform.position;          
        transform.position = pos;

        float move = Input.GetAxisRaw("Horizontal");

        if (move < 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (move > 0)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }

        if (Time.time >= tempoProximoAtaque)
        {
            if (Input.GetKey(Ataque))
            {
                anim.SetBool("isAttacking", true);
                Atacar();
                tempoProximoAtaque = Time.time + tempoEntreAtaques;
            }
            else{anim.SetBool("isAttacking", false);}
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Inimigo"))
        {
            TakeDamage(1);
        }

        if (collision.collider.CompareTag("Moeda"))
        {
            Destroy(collision.gameObject); // Destrói a moeda
            gm.AdicionarMoeda();
        }
    }

    void Atacar()
    {
        Collider2D[] inimigos = Physics2D.OverlapCircleAll(AreaAtaque.position, raioAtaque);
        float direcao = transform.localScale.x > 0 ? 1f : -1f;
        Vector3 posAtaque = transform.position + new Vector3(direcao * 1.1f, 0f, 0f);

        GameObject slash = Instantiate(slashPrefab, posAtaque, Quaternion.identity);
        Vector3 escalaSlash = slash.transform.localScale;
        escalaSlash.x = Mathf.Abs(escalaSlash.x) * Mathf.Sign(transform.localScale.x); // espelha se necessário
        slash.transform.localScale = escalaSlash;
        Destroy(slash, tempoExibicaoSlash);

        foreach (Collider2D inimigo in inimigos)
        {
            if (inimigo.CompareTag("Inimigo"))
            {
                inimigo.GetComponent<EnemyBehaviour>()?.TomarDano(dano);
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        if (AreaAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AreaAtaque.position, raioAtaque);
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        uiVida.AtualizarVidas(currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Derrota");
    }
}
