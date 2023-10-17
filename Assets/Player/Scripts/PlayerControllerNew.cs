using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNew : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpHeight;
    public Transform orientation;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float radCircle;
    public bool isGrounded;

    private Rigidbody player;
    private float defaultSpeed;
    private Vector3 defaultScale;
    private Camera cam;
    private float defaultFOV;
    private bool isOn = true;
    public float stamina = 100;
    public bool canCrouch = true;
    public bool canMove = true;
    public int maxHealth = 3;


    //Variabel untuk player berenti smooth
    private float timeToStop = 0.3f; 
    private float stopTimer = 0f;
    private Vector3 previousVelocity;


    private void Start()
    {
        player = GetComponent<Rigidbody>();
        defaultSpeed = speed;
        cam = Camera.main;
        defaultFOV = cam.fieldOfView;
        defaultScale = transform.localScale;
    }

    private void Update()
    {
        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        SpeedControl();

        if (canMove)
        {
            Vector3 moveDirection = orientation.forward * moveZ + orientation.right * moveX;
            moveDirection.y = 0;
            player.velocity = moveDirection.normalized * speed;

        }
        else
        {
            player.velocity = Vector3.zero;
        }

        //Biar pas berenti gk lgsg berenti
        if (player.velocity != Vector3.zero)
        {
            stopTimer = 0f; 
            previousVelocity = player.velocity; 
        }
        else
        {
            stopTimer += Time.deltaTime; 

            //decrease player speed
            if (stopTimer < timeToStop)
            {
                float t = stopTimer / timeToStop;
                player.velocity = Vector3.Lerp(previousVelocity, Vector3.zero, t);
            }
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, radCircle, whatIsGround);

        if (Input.GetButton("Jump") && isGrounded)
        {
            player.velocity = new Vector3(player.velocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y)), player.velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Change speed to 120% and change FOV to a closer value
            speed += (speed * 20 / 100);
            Debug.Log(speed);
            DepleteStamina(0.01f);
            cam.fieldOfView = 60;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Reset speed and FOV to default
            cam.fieldOfView = defaultFOV;
            speed = defaultSpeed;
        }

        if (canCrouch)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                // Change speed to 50% and adjust player's scale
                speed -= (speed * 50 / 100);
                gameObject.layer = default;
                transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
                Debug.Log(speed);
               
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                speed = defaultSpeed;
                transform.localScale = defaultScale;
                gameObject.layer = 3;
                
            }
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(player.velocity.x, 0f, player.velocity.z);

        if (flatVel.magnitude > speed || flatVel.magnitude < speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            player.velocity = new Vector3(limitedVel.x, player.velocity.y, limitedVel.z);
        }
    }

    private void DepleteStamina(float amount)
    {
        stamina -= amount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radCircle);
    }

    public void TakeDamage()
    {
        maxHealth -= 1;
        if (maxHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}