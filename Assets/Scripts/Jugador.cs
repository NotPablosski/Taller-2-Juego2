using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jugador : Fighter
{

    public static Vector2 LimitsY = new Vector2(0f, -7f);

    [SerializeField]
    float verticalSpeed;
    [SerializeField]
    float horizontalSpeed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    Vector2 cntrl;
    void Update()
    {
        cntrl = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (cntrl.x != 0)
            sr.flipX = cntrl.x < 0;

        if (Input.GetKeyDown(KeyCode.Z))
            anim.SetTrigger("SendPunch");

        anim.SetBool("IsGuard", Input.GetKey(KeyCode.X));

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !anim.GetCurrentAnimatorStateInfo(0).IsName("GetPunch") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
            
        {
        anim.SetBool("IsWalking", cntrl.magnitude != 0);
        rb.velocity = new Vector2(cntrl.x * horizontalSpeed, cntrl.y * verticalSpeed);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, LimitsY.y, LimitsY.x), transform.position.z);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
