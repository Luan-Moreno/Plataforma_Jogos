using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Referência ao Player
    public float smoothing = 5f;  // A suavização do movimento
    public Vector3 offset;        // Distância entre a câmera e o player

    void Start()
    {
        // Se o Player não for atribuído manualmente, pega automaticamente
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        // A câmera começa com o offset correto
        transform.position = player.position + offset;
    }

    void FixedUpdate()
    {
        // Movimento suave da câmera
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}

