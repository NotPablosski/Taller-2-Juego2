using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : Fighter
{

    enum States { patrol, persuit }

    [SerializeField]
    States state = States.patrol;
    [SerializeField]
    float searchRange = 1;
    [SerializeField]
    float stoppingDistance = 0.3f;

    Transform player;
    Vector3 target;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SetTarget", 0, 5);
        InvokeRepeating("SetPunch", 0, 5);

    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.DrawWireSphere(target, 4f);

        base.OnDrawGizmosSelected();
    }

    void SendPunch()
    {
        if (state != States.persuit)
            return;
        if (vel.magnitude != 0)
            return;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !anim.GetCurrentAnimatorStateInfo(0).IsName("GetPunch") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            anim.SetTrigger("SendPunch");
        }
    }
    void SetTarget()
    {
        if (state != States.patrol)
            return;
        target = new Vector2(transform.position.x + Random.Range(-searchRange, searchRange), Random.Range(LimitsY.y, LimitsY.x));
    }

    Vector2 vel;
    void Update()
    {
        if (state == States.persuit)
        {
            target = player.transform.position;
            if(Vector3.Distance(target, transform.position) > searchRange * 1.2f)
            {
                target = transform.position;
                state = States.patrol;
                return;

            }
            }
        else if (state == States.patrol)
        {
            var ob = Physics2D.CircleCast(transform.position, searchRange, Vector2.up);
            if (ob.collider !=null)
            {
                if (ob.collider.CompareTag("Player"))
                {
                    state = States.persuit;
                    return;
                }
            }
        }
        vel = target - transform.position;
        sr.flipX = vel.x < 0;
        if (vel.magnitude < stoppingDistance)
            vel = Vector2.zero;
        vel.Normalize();
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !anim.GetCurrentAnimatorStateInfo(0).IsName("GetPunch") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            anim.SetBool("IsWalking", vel.magnitude != 0);
        }
       else
        {
            vel = Vector2.zero;
        }
        rb.velocity = new Vector2(vel.x * horizontalSpeed, vel.y * verticalSpeed);
    }
}
