using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static oppssumc;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    //角色刚体
    [SerializeField]private Transform gunfire;
    private enum State
    //角色状态
    {
        Normal,
        Rolling,
        Falling,
    }
    private State state;
    //状态实例
    private int health;
    //角色生命值
    private Animator playerAnim;
    //动作控制模组
    private float rollSpeed,rollSpeedDropMultipliter,rollMinSpeed,rollMaxSpeed;
    private float rollingInvokeTime;
    [SerializeField]private float rollingWaitingTime;
    private bool isRollingAvailible;
    //翻滚相关
    public float speed;
    //速度参数

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        gunfire = GetComponent<Transform>();
    }  
    private void Start()
    {   
        state = State.Normal;
        health = 6;
        speed = 10f;
        isRollingAvailible = true;
        rollSpeed = 20f;
        rollSpeedDropMultipliter = 2f;
        rollMinSpeed = 5f;
        rollMaxSpeed = 50f;
    }

    private void FixedUpdate()
    {
        Movement();
    }
   
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        //获得玩家水平方向按键输入
        float verticalalmove = Input.GetAxis("Vertical");
        //获得玩家垂直方向按键输入
        float facedirection = Input.GetAxisRaw("Horizontal");
        //获得角色水平朝向
        bool isRollBOttonDown = Input.GetKeyDown(KeyCode.LeftShift);
        //检测玩家是否翻滚

        switch(state)
        {
        case State.Normal:

            rb.velocity = new Vector3(horizontalmove * speed * Time.maximumDeltaTime * 10f, rb.velocity.y,0f);
            //判断玩家水平移动
            playerAnim.SetFloat("running", Mathf.Abs(rb.velocity.x));
            //设置动画为奔跑
            rb.velocity = new Vector3(rb.velocity.x, verticalalmove * speed * Time.maximumDeltaTime * 10f,0f);
            //判断玩家垂直移动
            playerAnim.SetFloat("running", Mathf.Abs(rb.velocity.y));
            //设置动画为奔跑
            if (facedirection != 0)
            //判断角色面朝方向
            {
                transform.localScale = new Vector3(facedirection, 1f, 1f);
            }
            if(isRollBOttonDown && isRollingAvailible)
            {
                state = State.Rolling;
                Debug.Log(State.Rolling);
            }
            else if (!isRollingAvailible)
            {
                rollingInvokeTime -= Time.deltaTime;
                if(rollingInvokeTime <= 0f)
                {
                    isRollingAvailible = true;
                }
            }
            break;
        case State.Rolling:
            if(rollSpeed <= rollMinSpeed)//也许加个计时器判断会好些?
            {
                state = State.Normal;
            }
            else if(rollSpeed < rollMaxSpeed && rollSpeed > rollMinSpeed && isRollingAvailible)
            {
                rollSpeed += rollSpeed * rollSpeedDropMultipliter * Time.maximumDeltaTime;
            }
            else if(rollSpeed >= rollMaxSpeed && isRollingAvailible)
            {
                isRollingAvailible = false;
            }
            else
            {
                rollSpeed -= rollSpeed * rollSpeedDropMultipliter * Time.maximumDeltaTime;
            }
            roll();
            break;
        }
    }

    private void onTriggerEnter2D(GameObject hitInfo)
    //碰撞触发器(待修正)
    {
        switch(state)
        {
        case State.Normal:
            if (hitInfo.tag == "Monster")
            //如果碰撞对象tag为Monster，自身受创
            { 
            //this.hurt(hitInfo.getItDamage());
            }
            else if (hitInfo.tag == "Collection")
            //为Collection时，破坏对象并放入背包
            {
            Destroy(hitInfo.gameObject);
            //pickUp();
            }
            break;
        }
    }
    
    private void hurt(float takeDamege)
    {
        health -= (int)(takeDamege * 0.1f) + 1;
        if (health <= 0f)
        {
            //gameover();
        }
    }

    private void roll()
    {
        rb.velocity = gunfire.right * rollSpeed * Time.maximumDeltaTime * 10f;
    }

    

    /*private Vector3 followMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Mathf.Abs(Camera.main.transform.position.z)));
        return  mousePosition;
    }*/
}