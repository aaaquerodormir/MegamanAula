using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentos : MonoBehaviour
{
    public float moveDistance = 3.5f; //define a distância total a se movimentar
    public float moveSpeed = 1.75f;   //define a velocidade

    private Vector3 initialPosition;  //define o Y inicial
    private Vector3 targetPosition;   //define para onde a plataforma vai se mover
    private bool movingUp = true;     //define se a plataforma está indo para cima ou para baixo

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
            if (transform.position == initialPosition)
            {
                movingUp = true;
            }
        }
    }
}
