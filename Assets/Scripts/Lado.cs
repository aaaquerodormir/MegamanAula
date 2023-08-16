using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lado : MonoBehaviour
{
    public float startX;       // Posição X inicial.
    public float endX;         // Posição X final.
    public float speed = 1.75f;  // Velocidade de movimento da plataforma.

    private Vector3 targetPosition;
    private bool movingToEnd = true;

    private void Start()
    {
        targetPosition = new Vector3(endX, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        // Move a plataforma em direção ao ponto alvo.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Verifica se a plataforma chegou ao ponto alvo.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Inverte a direção do movimento.
            movingToEnd = !movingToEnd;

            // Define o próximo ponto alvo com base na direção do movimento.
            targetPosition.x = movingToEnd ? endX : startX;
        }
    }
}
