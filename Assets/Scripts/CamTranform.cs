using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTranform : MonoBehaviour
{
    public Transform playercam;

    void Update()
    {
        playercam.transform.position = new Vector3(playercam.position.x,playercam.position.y,-7.5f);
    }
}
