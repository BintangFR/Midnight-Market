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
    [SerializeField] private Image bloodSplatterImage3 = null;
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
    
    

    [Header("Player Stamina")]
    public float maxStamina = 100f;
    public float staminaDrainRate;
    public float staminaRechargeRate;
    private float fatigueTimer = 0f;
    private bool isFatigued;
    public bool isRunning;
    //[SerializeField] private Slider staminaBar;

    [Header("Player Condition")]
    public bool canCrouch = true;
    public bool canMove = true;
    public bool canRun = true;
    public bool notInVent = true;
    public bool canJump = true;
    

    //Variables to stop player movement smoothly
    private float timeToStop = 0.3f;
    private float stopTimer = 0f;
    private Vector3 previousVelocity;
    private Vector3 smoothVelocity;

    public UnityEvent OnTakeDamage = new UnityEvent();

    bool hasPlayedTiredAudio = false;


    private void Start()
    {
        stamina = maxStamina;
        player = GetComponent<Rigidbody>();
        defaultSpeed = speed;
        
        defaultScale = transform.localScale;
        player.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        if (isGrounded)
        {
            //staminaBar.value = stamina;
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
        }

        if (!notInVent)
        {
            canCrouch = false;
            canRun = false;
            canJump = false;
        }
        else
        {
            canCrouch = true;
            canRun = true;
            canJump = true;
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
        if (canJump)
        {
            if (isGrounded)
            {
                canRun = true;
                canCrouch = true;

                if (isRunning)
                {
                    canCrouch = false;
                }
            }
            else
            {
                canRun = false;
                canCrouch = false;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                player.velocity = new Vector3(player.velocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y)), player.velocity.z);
            }
        }


        //Player Run & Stamina
        if (canRun)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && isWalking && isGrounded)
            {
                canCrouch = false;
                isRunning = true;
                if (stamina > 0 && !isFatigued)
                {
                    speed += (speed * 20 / 100); 
                    //staminaBar.gameObject.SetActive(true);
                }
            }
            else if (isFatigued)
            {
                if (!hasPlayedTiredAudio)
                {
                    isRunning = false;
                    speed = defaultSpeed;
                    AudioManager.Instance.PlaySFX("Tired", transform.position);
                    hasPlayedTiredAudio = true;
                }
            }
            else
            {
                hasPlayedTiredAudio = false;
            }


            if (Input.GetKeyUp(KeyCode.LeftShift) && isRunning && isGrounded)
            {
                //staminaBar.gameObject.SetActive(false);
                if (isRunning || isFatigued)
                {
                    canCrouch = true;
                    speed = defaultSpeed;
                    isRunning = false;
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

        if (Input.GetKeyUp(KeyCode.LeftShift) && isRunning && !isGrounded)
        {
            isRunning = false;
            speed = defaultSpeed;
        }
        

        //Player Crouch
        if (canCrouch)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                // Change speed to 50% and adjust player's scale and position
                canRun = false;
                canJump = false;
                speed -= (speed * 50 / 100);
                //gameObject.layer = default;
                transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
                Physics.gravity *= 15f;

            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                canRun = true;
                canJump = true;
                speed = defaultSpeed;
                transform.localScale = defaultScale;
                Physics.gravity = new Vector3(0, -9.81f, 0);
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
            Color splatterAlpha3 = bloodSplatterImage3.color;
            splatterAlpha3.a = 0;
            bloodSplatterImage3.color = splatterAlpha3;
        }
        else if (maxHealth == 2)
        {
            Color splatterAlpha = bloodSplatterImage.color;
            splatterAlpha.a = 1;
            bloodSplatterImage.color = splatterAlpha;           
        }
        else if (maxHealth == 1)
        {
            Color splatterAlpha2 = bloodSplatterImage2.color;
            splatterAlpha2.a = 1;
            bloodSplatterImage2.color = splatterAlpha2;
        }
        else if (maxHealth == 0)
        {
            Color splatterAlpha3 = bloodSplatterImage3.color;
            splatterAlpha3.a = 1;
            bloodSplatterImage3.color = splatterAlpha3;
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