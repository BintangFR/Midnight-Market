using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public float jumpHeight;
    public Transform orientation;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float radCircle;
    public bool isGrounded; 
    
    public int maxHealth = 3;
    private Rigidbody player;
    private float defaultSpeed;
    private Vector3 defaultScale;
    private Camera cam;
    private float defaultFOV;
    private bool isOn = true;
    public float stamina = 100;
    public bool canCrouch = true;
    public bool canMove = true;

    
    private void Start()
    {
        player = GetComponent<Rigidbody>();
        defaultSpeed = speed;
        cam = Camera.main;
        defaultFOV = cam.fieldOfView;
        defaultScale = transform.localScale;
    }

    // Update is called once per frame
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
            //transform.Translate(moveX,0,moveZ);
        }
        else
        {
            player.velocity = Vector3.zero;
        }
        gameObject.transform.rotation = orientation.transform.rotation;
        isGrounded = Physics.CheckSphere(groundCheck.position, radCircle, whatIsGround);

        if (Input.GetButton("Jump") && isGrounded)
        {
            player.velocity = new Vector3(player.velocity.z,jumpHeight);
            AudioManager.Instance.PlaySFX("jump", transform.position);

            //transform.Translate(0, jumpHeight * Time.deltaTime, 0);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //chage speed to 120% and change fov to close one
            speed += (speed * 20/100);
            Debug.Log(speed);
            DepleteStamina(0.01f);
            cam.fieldOfView = 60;          
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //chage speed to default and change fov to normal
            cam.fieldOfView = defaultFOV;
            speed = defaultSpeed;
            AudioManager.Instance.PlaySFX("tired", transform.position);
        }
        if (canCrouch)
        {
            
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                //chage speed to 50%
                speed -= (speed * 50/100);
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

    private void DepleteStamina(float amount)
    {
        stamina -= amount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radCircle);

    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(player.velocity.x, 0f, player.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            player.velocity = new Vector3(limitedVel.x, player.velocity.y, limitedVel.z);
        }
    }

    public void TakeDamage()
    {
        maxHealth -= 1;
        AudioManager.Instance.PlaySFX("hit", transform.position);
        if (maxHealth == 0)
        {
            AudioManager.Instance.PlaySFX("die", transform.position);
            Die();
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
