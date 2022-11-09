using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luchador : MonoBehaviour
{
    protected static Vector2 LimitsY = new Vector2(0f, -7f);

    [SerializeField]
    protected float verticalSpeed;
    [SerializeField]
    protected float horizontalSpeed;
    [SerializeField]
    protected Transform leftPunch;
    [SerializeField]
    protected Transform rightPunch;
    [SerializeField]
    protected Transform punchRadius;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;

    protected virtual void OnDrawGizmosSelected()
    {
        if (leftPunch == null || rightPunch == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(leftPunch.position, punchRadius);
        Gizmos.DrawWireSphere(rightPunch, punchRadius);

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    }
