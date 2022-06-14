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
    public float turbo = 100;
    bool turboOn;
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
        turboOn = Input.GetKey("space");
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
        float speedCoef;

        onGround = fWheel.IsTouching(ground) || bWheel.IsTouching(ground) ? true : false;

        if(onGround)
        {
            targetSpeed = horizontal * maxSpeed;
            Debug.Log(targetSpeed);
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
        float speedDiff = targetSpeed - -(carBody.velocity.x);

        if(carBody.velocity.x > targetSpeed && targetSpeed > 0.01f || carBody.velocity.x < targetSpeed && targetSpeed < -0.01f || Mathf.Abs(rotation) > 8 && targetSpeed == 0)
        {
            accelRate = 0;
        }

        if(this.turboOn)
        {
            speedCoef = 1.4f;
            this.updateTurbo(-1);
        }
        else
        {
            speedCoef = 1.0f;
            this.updateTurbo(1);
        }

        float mov = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1.0f) * Mathf.Sign(speedDiff);

        carBody.AddForce(mov * Vector2.right);
        transform.Rotate(new Vector3(0, 0, rotationRate) * Time.fixedDeltaTime);
        velocity = carBody.velocity.x;
    }

    void updateTurbo(int IncOrDec)
    {
        switch(IncOrDec)
        {
            case -1:
                this.turbo -= (this.turbo >= 2) ? 2 : 0;
                break;
            case 1:
               this.turbo += (this.turbo <= 90) ? 10 : 0;
                break;
        }
    }
}
