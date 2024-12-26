using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;

    public float speed = 2f;

    private bool walk = false;

    [HideInInspector]
    public bool attack = false;

    [HideInInspector]
    public int MaxHealth;
    public int currentHealth;
    public int MaxEnergy;
    public int currentEnergy;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        AttackUpdate();
    }

    private void MoveUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            walk = true;
        }
        else
        {
            walk = false;
        }
        
        animator.SetBool("Walk", walk);
        animator.SetFloat("HorizontalWalk", horizontal);
        animator.SetFloat("VerticalWalk", vertical);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        if (walk)
        {
            transform.Translate(Vector3.right * speed * horizontal * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * vertical * Time.deltaTime);
        }
    }

    private void AttackUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            attack = true;
        }
    }
}
