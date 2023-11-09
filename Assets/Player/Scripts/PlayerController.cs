using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement & Health")]
    public float speed;
    public float jumpHeight;
    public float stamina = 100;
    public int maxHealth = 3;
    [SerializeField] private Image bloodSplatterImage = null;
    [SerializeField] private Image bloodSplatterImage2 = null;
    public bool isWalking;

    [Header("Player Physics")]
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

    [Header("Player Stamina")]
    public float maxStamina = 100f;
    public float staminaDrainRate;
    public float staminaRechargeRate;
    private float fatigueTimer = 0f;
    private bool isFatigued;
    private bool isRunning;
    [SerializeField] private Slider staminaBar;

    [Header("Player Condition")]
    public bool canCrouch = true;
    public bool canMove = true;
    public bool canRun = true;
    public bool notInVent = true;


    //Variables to stop player movement smoothly
    private float timeToStop = 0.3f;
    private float stopTimer = 0f;
    private Vector3 previousVelocity;
    private Vector3 smoothVelocity;

    public UnityEvent OnTakeDamage = new UnityEvent();


    private void Start()
    {
        stamina = maxStamina;
        player = GetComponent<Rigidbody>();
        defaultSpeed = speed;
        cam = Camera.main;
        //defaultFOV = cam.fieldOfView;
        defaultScale = transform.localScale;
        player.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        staminaBar.value = stamina;
        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        SpeedControl();

        if (canMove)
        {
            Vector3 moveDirection = orientation.forward * moveZ + orientation.right * moveX;
            moveDirection.y = 0;
            Vector3 newVelocity = moveDirection.normalized * speed;

            newVelocity.y = player.velocity.y;

            player.velocity = newVelocity;

            player.velocity = Vector3.SmoothDamp(player.velocity, newVelocity, ref smoothVelocity, 0.1f);

        }
        else
        {
            player.velocity = Vector3.zero;
        }

        //Stop player movement smoothly
        if (player.velocity != Vector3.zero)
        {
            stopTimer = 0f;
            previousVelocity = player.velocity;
            isWalking = true;
            
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
            isWalking = false;

        }


        isGrounded = Physics.CheckSphere(groundCheck.position, radCircle, whatIsGround);

        // Player Jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            player.velocity = new Vector3(player.velocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y)), player.velocity.z);
            AudioManager.Instance.PlaySFX("Jump", transform.position);
        }


        //Player Run & Stamina
        if (canRun)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && isWalking)
            {
                if (stamina > 0 && !isFatigued)
                {
                    speed += (speed * 20 / 100); 
                    //cam.fieldOfView = defaultFOV - 20;
                    isRunning = true;
                    staminaBar.gameObject.SetActive(true);
                }
                else
                {
                    if (isRunning || isFatigued)
                    {
                        speed = defaultSpeed; 
                        //cam.fieldOfView = defaultFOV;
                        isRunning = false;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                staminaBar.gameObject.SetActive(false);
                if (isRunning || isFatigued)
                {
                    speed = defaultSpeed;
                    //cam.fieldOfView = defaultFOV;
                    isRunning = false;
                    AudioManager.Instance.PlaySFX("Tired", transform.position);
                }
            }

            if (isRunning)
            {
                stamina -= Time.deltaTime * staminaDrainRate;
            }
            else if (!isRunning && stamina > 0 && stamina < maxStamina)
            {
                stamina += Time.deltaTime * staminaRechargeRate;
            }
           

            if (stamina <= 0 && fatigueTimer <= 5)
            {
                isFatigued = true;
                speed = 5.0f;
                //cam.fieldOfView = defaultFOV;
                fatigueTimer += Time.deltaTime;
            }
            else if (fatigueTimer >= 5) 
            {
                isFatigued = false;
                speed = defaultSpeed;
                stamina += 100;
                fatigueTimer = 0;
            }

            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }

        //Player Crouch
        if (canCrouch)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                // Change speed to 50% and adjust player's scale and position
                canRun = false;
                speed -= (speed * 50 / 100);
                //gameObject.layer = default;
                transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
                Debug.Log(speed);

            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                canRun = true;
                speed = defaultSpeed;
                transform.localScale = defaultScale;
                //gameObject.layer = 6;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radCircle);
    }

    //Player Taking damage
    public void TakeDamage()
    {
        maxHealth -= 1;
        AudioManager.Instance.PlaySFX("Hit", transform.position);
        OnTakeDamage.Invoke();

        if (maxHealth == 3)
        {
            Color splatterAlpha = bloodSplatterImage.color;
            splatterAlpha.a = 0;
            bloodSplatterImage.color = splatterAlpha;
            Color splatterAlpha2 = bloodSplatterImage2.color;
            splatterAlpha2.a = 0;
            bloodSplatterImage2.color = splatterAlpha2;
        }
        else if (maxHealth == 2)
        {
            Color splatterAlpha = bloodSplatterImage.color;
            splatterAlpha.a = 1;
            bloodSplatterImage.color = splatterAlpha;
            Color splatterAlpha2 = bloodSplatterImage2.color;
            splatterAlpha2.a = 0;
            bloodSplatterImage2.color = splatterAlpha2;
        }
        else if (maxHealth == 1)
        {
            Color splatterAlpha = bloodSplatterImage.color;
            splatterAlpha.a = 0;
            bloodSplatterImage.color = splatterAlpha;
            Color splatterAlpha2 = bloodSplatterImage2.color;
            splatterAlpha2.a = 1;
            bloodSplatterImage2.color = splatterAlpha2;
        }
        else if (maxHealth == 0)
        {
            AudioManager.Instance.PlaySFX("Dead", transform.position);
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }
}