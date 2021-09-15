using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; //0: left 1: middle 2: right
    public float laneDistance = 4; // the distance between two lane

    public float jumpForce;
    public float Gravity = -10;
    private bool isGrounded;
    private bool isFlying;
    private float getDown = -20;

    public Animator animator;
    

    

    void Start()
    {
        controller = GetComponent<CharacterController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10) 
        {
            Die();
        }
       
        //Increase speed
        if(forwardSpeed < maxSpeed)
            forwardSpeed += 1.0f * Time.deltaTime;

       
        direction.z = forwardSpeed;
        

        if (isFlying) 
        {
            Vector3 upPosition = transform.position;
            upPosition.y = 15f;
            transform.position = upPosition;
            //direction.y += jumpForce * Time.deltaTime;

        }

       

        if (controller.isGrounded) 
        {
            
            if (SwipeManager.swipeUp)
            {
                
                Jump();
                
            }
            

        }
        else
        {
            direction.y += Gravity * Time.deltaTime;// For adding gravity and to look like more smooth
            
        }



        if (SwipeManager.swipeDown) 
        {
            StartCoroutine(Slide());
        }
        //Gather the inputs on which lane we should be
        if (SwipeManager.swipeDown)
            GetDown();

        if (SwipeManager.swipeRight) 
        {
            desiredLane++;
            if (desiredLane == 3) 
            {
                desiredLane = 2;
            }
        }
        
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = (transform.position.z * transform.forward) + (transform.position.y * transform.up);

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2) 
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = targetPosition;
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        

    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);

    }

    private void Jump() 
    {
        if (isFlying) 
        {
            return;
        }

        animator.SetBool("isJumping", true);
        Invoke("StopJumping", 0.2f);
        direction.y = jumpForce;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("giftBox")) 
        {
            Destroy(collision.gameObject);
            StartCoroutine(StartFlying());

        }
    }


    private IEnumerator StartFlying() 
    {
        
        animator.SetBool("isFlying", true);
        isFlying = true;
        yield return new WaitForSeconds(4.0f);
        animator.SetBool("isFlying", false);
        isFlying = false;
       

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "obstacle") 
        {
            PlayerManager.gameOver = true;
        }
        if (hit.transform.tag == "nextLevel") 
        {
            PlayerManager.nextLevel = true;
        }
        if (hit.transform.tag == "jumpHigh") 
        {
            StartCoroutine(JumpHigh());
        }
        if (hit.transform.tag == "forSlide") 
        {
            animator.SetBool("isSliding", true);
            Invoke("StopSliding", 0.7f);
        }
        
        

    }

    public void GetDown() 
    {
        direction.y = getDown;
    }

    void StopJumping()
    {
        animator.SetBool("isJumping", false);
    }

    private IEnumerator Slide() 
    {
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.0f);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
    }

    
    private IEnumerator JumpHigh() 
    {
        animator.SetBool("isJumping", true);
        jumpForce += 20;
        yield return new WaitForSeconds(1.0f);
        jumpForce -= 20;
        animator.SetBool("isJumping", false);
       
    }

    public void CallJumpHigh() 
    {
        StartCoroutine(JumpHigh());
    }
    public void Die() 
    {
        PlayerManager.gameOver = true;
    }
    void StopSliding() 
    {
        animator.SetBool("isSliding", false);
    }

   
}
