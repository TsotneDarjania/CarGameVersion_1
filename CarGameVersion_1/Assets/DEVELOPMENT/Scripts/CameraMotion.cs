using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public GameObject main;
    public Transform target;
    public float follow_speed;

    public float main_z_index;
 


    // Update is called once per frame
    void FixedUpdate()
    {
        FollowCamera();
    }

    void FollowCamera()
    {
        Vector3 direction = new Vector3(target.position.x,target.position.y,main_z_index);
        main.transform.position = Vector3.Lerp(main.transform.position, direction, follow_speed);
    }
}
