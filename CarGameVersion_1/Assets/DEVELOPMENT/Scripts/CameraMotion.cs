using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public GameObject cam;
    public Transform target;
    public float follow_speed;

    public float camera_z_index;
 


    void Start()
    {
        cam = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowCamera();
    }

    void FollowCamera()
    {
        Vector3 direction = new Vector3(target.position.x,target.position.y,camera_z_index);
        cam.transform.position = Vector3.Lerp(cam.transform.position, direction, follow_speed);
    }
}
