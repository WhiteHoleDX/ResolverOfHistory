using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    private Transform player;
    [SerializeField]private Transform gunfire;
    private float angle;
    //发射角
    private float invokeTime;
    //用于判断武器当前是否cd
    private bool available = true;
    //判断当前武器是否可以开火
    public GameObject bullet;
    //当前使用子弹
    public float waitingTime;
    //开火cd
    
    private void Awake() 
    {
        gunfire = transform.Find("gunfire");
        player = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Shoot();
        FollowMouse();
    }
 
    public void Shoot() 
    {
        if (!available)
        {
            invokeTime -= Time.deltaTime;
            if (invokeTime <= 0f)
            {
                available = true;
            }
        }
        if (Input.GetButton("Fire1") && available)
        {
            var cloneBullet = Instantiate(bullet, gunfire.transform.position,gunfire.transform.rotation) as GameObject;
            invokeTime = waitingTime;
            available = false;
        }

    }

         private void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Mathf.Abs(Camera.main.transform.position.z)));
        Vector3 firepointPos = gunfire.position;
        Vector3 direction = (new Vector3(mousePos.x -firepointPos.x,mousePos.y - firepointPos.y,0f)).normalized;    
        angle = Vector2.Angle(direction,Vector2.right);
        if(player.localScale.x == 1)
        {
            if (mousePos.y < firepointPos.y)
            {
                angle = -angle;
            }
        }
        else if(player.localScale.x != 1)
        {
            if (mousePos.y > firepointPos.y)
            {
                angle = -angle;
            }
        }
        gunfire.localEulerAngles = new Vector3(0,0,angle);
    }
}