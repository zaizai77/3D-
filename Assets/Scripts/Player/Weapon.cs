using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Collider collider;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        collider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        player.animator.SetTrigger("Attack");
        //伤害函数  调用碰撞到的物体的受伤函数
    }
}
