using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oppssumc : MonoBehaviour
{
    [SerializeField]private float speed;
    private float health;
    private Animator monsterAnim;

    void Start()
    {
        monsterAnim = GetComponent<Animator>();
        health = 200.0f;
    }

    void Update()
    {
        //活动
    }

    public void hurt(float getDamage)
    {
        health -= getDamage;
    }
}
