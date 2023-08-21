using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animar : MonoBehaviour
{
    public bool isFacingRight;
    private Rigidbody2D rb;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
