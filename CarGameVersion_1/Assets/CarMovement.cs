using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody2D carBody;
    public Collider2D fWheel;
    public Collider2D bWheel;
    public Collider2D ground;
    public float maxSpeed = 100.0f;
    public float accel = 2.0f;
    public float deccel = -2.0f;
    public float accelInAir = 0.8f;
    public float deccelInAir = 0.8f;
    public float velocity = 0.0f;
    public float rotation;
    float horizontal;
    public bool onGround = true;

    private float movement;

    // Start is called before the first frame update
    void Start()
    {
        carBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        rotation = carBody.rotation;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float targetSpeed;
        float accelRate;
        float rotationRate;

        onGround = fWheel.IsTouching(ground) || bWheel.IsTouching(ground) ? true : false;

        if(onGround)
        {
            targetSpeed = horizontal * maxSpeed;
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;
            rotationRate = 0;
        }
        else
        {
            targetSpeed = 0;
            Debug.Log("Im flying");
            accelRate = deccel * deccelInAir;
            rotationRate = -(horizontal) * 60;
        }
        float speedDiff = targetSpeed - carBody.velocity.x;

        if(carBody.velocity.x > targetSpeed && targetSpeed > 0.01f || carBody.velocity.x < targetSpeed && targetSpeed < -0.01f || Mathf.Abs(rotation) > 8 && targetSpeed == 0)
        {
            accelRate = 0;
        }

        float mov = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1.0f) * Mathf.Sign(speedDiff);

        carBody.AddForce(mov * Vector2.right);
        transform.Rotate(new Vector3(0, 0, rotationRate) * Time.fixedDeltaTime);
        velocity = carBody.velocity.x;
    }
}
