﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Animator anima; // Referência ao Animator do personagem.
    float xmov; // Variável para guardar o movimento horizontal.
    public Rigidbody2D rdb; // Referência ao Rigidbody2D do personagem.
    bool jump, doublejump; // Flags para controle de pulo e pulo duplo.
    float jumptime, jumptimeside; // Controla a duração dos pulos.
    public ParticleSystem fire; // Sistema de partículas para o efeito de fogo.

    void Start()
    {
        // Método para inicializações. Não está sendo utilizado neste código.
    }

    void Update()
    {
        // Captura o movimento horizontal do jogador.
        xmov = Input.GetAxis("Horizontal");

        // Verifica se o botão de pulo foi pressionado e controla o pulo duplo.
        if (Input.GetButtonDown("Jump"))
        {
            if (jumptime < 0.1f)
            {
                doublejump = false;
            }
        }

        // Define o estado de pulo com base na entrada do usuário.
        if (Input.GetButton("Jump"))
        {
            jump = true;
        }
        else
        {
            jump = false;
            doublejump = false;
            jumptime = 0;
            jumptimeside = 0;
        }

        // Desativa o estado de "Fire" no Animator.
        anima.SetBool("Fire", false);

        // Ativa o efeito de fogo e define o estado "Fire" no Animator quando o botão de fogo é pressionado.
        if (Input.GetButtonDown("Fire1"))
        {
            fire.Emit(1);
            anima.SetBool("Fire", true);
        }
    }

    void FixedUpdate()
    {
        Reverser(); // Chama a função que inverte o personagem.
        anima.SetFloat("Velocity", Mathf.Abs(xmov)); // Define a velocidade no Animator.

        // Adiciona uma força para mover o personagem.
        rdb.AddForce(new Vector2(xmov * 20 / (rdb.velocity.magnitude + 1), 0));

        RaycastHit2D hit;

        // Faz um raycast para baixo para detectar o chão.
        hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit)
        {
            anima.SetFloat("Height", hit.distance);
            JumpRoutine(hit); // Chama a rotina de pulo.
        }

        RaycastHit2D hitright;

        // Faz um raycast para a direita para detectar paredes.
        hitright = Physics2D.Raycast(transform.position + Vector3.up * 0.5f, transform.right, 1);
        if (hitright)
        {
            if (hitright.distance < 0.3f)
            {
                JumpRoutineSide(hitright); // Chama a rotina de pulo lateral.
            }
            Debug.DrawLine(hitright.point, transform.position + Vector3.up * 0.5f);
        }
    }

    // Rotina de pulo (parte física).
    private void JumpRoutine(RaycastHit2D hit)
    {
        // Verifica a distância do chão e aplica uma força de pulo se necessário.
        if (hit.distance < 0.1f)
        {
            jumptime = 2.5f;
        }

        if (jump)
        {
            jumptime = Mathf.Lerp(jumptime, 0, Time.fixedDeltaTime * 10);
            rdb.AddForce(Vector2.up * jumptime, ForceMode2D.Impulse);
        }
    }

    // Rotina de pulo lateral.
    private void JumpRoutineSide(RaycastHit2D hitside)
    {
        if (hitside.distance < 0.3f)
        {
            jumptimeside = 1;
        }

        if (doublejump)
        {
            PhisicalReverser();
            jumptimeside = Mathf.Lerp(jumptimeside, 0, Time.fixedDeltaTime * 10);
            rdb.AddForce((hitside.normal * 50 + Vector2.up * 80) * jumptimeside);
        }
    }

    // Função para inverter a direção do personagem (visual).
    void Reverser()
    {
        if (xmov > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        if (xmov < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Função para inverter a direção do personagem (física).
    void PhisicalReverser()
    {
        if (rdb.velocity.x > 0.1f) transform.rotation = Quaternion.Euler(0, 0, 0);
        if (rdb.velocity.x < 0.1f) transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Detecção de colisão com objetos marcados com a tag "Damage".
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage"))
        {
            LevelManager.instance.LowDamage(); // Chama a função para aplicar dano.
        }
    }
}
