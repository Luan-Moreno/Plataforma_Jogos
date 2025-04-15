using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateScene : MonoBehaviour
{
    [SerializeField] private string nomeCena;

    public void Carregar()
    {
        SceneManager.LoadScene(nomeCena);
    }
}


