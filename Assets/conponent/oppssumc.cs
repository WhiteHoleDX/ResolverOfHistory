using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oppssumc : MonoBehaviour
{
    [SerializeField]private float speed;
    private float  health;
    private Animator monsterAnim;
    private float damage = 20f;

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

    public float getItDamage()//没给gameobject加模板，晚点再说，摸了！
    {
        return this.damage;
    }
}
