using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class PlayerMovement : MonoBehaviour
{
    #region Events
    public delegate void WallJumped();
    public event WallJumped wallJumped;
    #endregion

    #region Variables
    //Components
    PlayerStats playerStats;
    Rigidbody rigidbody;
    PlayerGroundCheck pgc;
    PlayerWallCheck pwc;
    Clock playerClock;

    //Speed
    float playerSpeed = 5;
    float crouchSpeedMultiplier = 0.5f;
    float movementSmoothTime = 0.1f;

    float modifiedPlayerSpeed;
    float modifiedMovementSmoothTime;

    //Jump
    float jumpHeight = 10;
    float crouchJumpMiltiplier = 0.75f;
    float jumpSmoothTime = 0.05f;

    float modifiedJumpHeight;
    float modifiedJumpSmoothTime;

    bool shouldJump = false;

    //Wall Jump
    float wallJumpForce = 10;
    float crouchWallJumpForceMultiplier = 0.5f;
    float wallJumpHeight = 5;
    float crouchWallJumpHeightMultiplier = 0.75f;
    float wallJumpSmoothTime = 0f;

    float modifiedWallJumpForce;
    float modifiedWallJumpHeight;
    float modifiedWallJumpSmoothTime;

    bool shouldWallJump = false;

    //Other
    int movementDirection = 0; //If both A and D are pressed it determines which one was pressed last, -1 for A and 1 for D
    int moveDir = 0; //If input is A and D it's 1, if input is A it's 2, if input is D it's 3 and if there is not input it's 0

    Vector3 refVel = Vector3.zero;
    #endregion

    private void AssignValuesToVariables() 
    {
        //Speed
        playerSpeed = playerStats.Speed;
        crouchSpeedMultiplier = playerStats.CrouchSpeedMultiplier;
        movementSmoothTime = playerStats.MovementSmoothTime;

        //Jump
        jumpHeight = playerStats.JumpHeight;
        crouchJumpMiltiplier = playerStats.CrouchJumpMultiplier;
        jumpSmoothTime = playerStats.JumpSmoothTime;

        //Wall Jump
        wallJumpForce = playerStats.WallJumpForce;
        crouchWallJumpForceMultiplier = playerStats.CrouchWallJumpForceMultiplier;
        wallJumpHeight = playerStats.WallJumpHeight;
        crouchWallJumpHeightMultiplier = playerStats.CrouchWallJumpHeightMultiplier;
        wallJumpSmoothTime = playerStats.WallJumpSmoothTime;
    }

    private void Awake()
    {
        //Components
        playerStats = GetComponent<PlayerStats>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        pgc = GetComponentInChildren<PlayerGroundCheck>();
        pwc = GetComponentInChildren<PlayerWallCheck>();
    }

    void Start()
    {
        AssignValuesToVariables();
    }

    void Update()
    {
        playerClock = Timekeeper.instance.Clock("Player");

        //Modified Values
        if (Input.GetKey(KeyCode.LeftControl))
        {
            modifiedPlayerSpeed = (playerSpeed * crouchSpeedMultiplier) * playerClock.timeScale;
            modifiedJumpHeight = (jumpHeight * crouchJumpMiltiplier) * playerClock.timeScale;
            modifiedWallJumpForce = (wallJumpForce * crouchWallJumpForceMultiplier) * playerClock.timeScale;
            modifiedWallJumpHeight = (wallJumpHeight * crouchWallJumpHeightMultiplier) * playerClock.timeScale;
        }
        else
        {
            modifiedPlayerSpeed = playerSpeed * playerClock.timeScale;
            modifiedJumpHeight = jumpHeight * playerClock.timeScale;
            modifiedWallJumpForce = wallJumpForce * playerClock.timeScale;
            modifiedWallJumpHeight = wallJumpHeight * playerClock.timeScale;
        }
        modifiedMovementSmoothTime = movementSmoothTime * playerClock.timeScale;
        modifiedJumpSmoothTime = jumpSmoothTime * playerClock.timeScale;
        modifiedWallJumpSmoothTime = wallJumpSmoothTime * playerClock.timeScale;

        // Getting Inputs
        HorizontalInput();
        VerticalInput();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y + (rigidbody.velocity.y / 15), rigidbody.velocity.z);
        }

        if (shouldJump)
        {
            Jump();
        }
        if (shouldWallJump)
        {
            WallJump(playerStats.IsMovingBackwards);
        }
    }

    private void HorizontalInput()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                movementDirection = -1;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                movementDirection = 1;
            }
            moveDir = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDir = 2;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDir = 3;
        }
        else moveDir = 0;
    }

    private void HorizontalMovement() 
    {
        if (moveDir == 0)
        {
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z), ref refVel, modifiedMovementSmoothTime);
        }
        if (moveDir == 1)
        {
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, new Vector3(modifiedPlayerSpeed * movementDirection, rigidbody.velocity.y, rigidbody.velocity.z), ref refVel, modifiedMovementSmoothTime);
            if (movementDirection == 1) playerStats.IsMovingBackwards = false; else playerStats.IsMovingBackwards = true;
        }
        else if (moveDir == 2)
        {
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, new Vector3(-modifiedPlayerSpeed, rigidbody.velocity.y, rigidbody.velocity.z), ref refVel, modifiedMovementSmoothTime);
            playerStats.IsMovingBackwards = true;
        }
        else if (moveDir == 3)
        {
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, new Vector3(modifiedPlayerSpeed, rigidbody.velocity.y, rigidbody.velocity.z), ref refVel, modifiedMovementSmoothTime);
            playerStats.IsMovingBackwards = false;
        }
    }

    private void VerticalInput() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pwc.canWallJump)
            {
                shouldWallJump = true;
            }

            if (playerStats.JumpsLeft > 0)
            {
                shouldJump = true;
            }
        }
    }

    private void Jump() 
    {
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, new Vector3(rigidbody.velocity.x, modifiedJumpHeight, rigidbody.velocity.z), ref refVel, modifiedJumpSmoothTime);
        playerStats.JumpsLeft--;
        shouldJump = false;
        playerStats.TotalJumps++;
    }

    private void WallJump(bool isBackwards)
    {
        if (!isBackwards)
        {
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, new Vector3(-modifiedWallJumpForce, modifiedWallJumpHeight, rigidbody.velocity.z), ref refVel, modifiedWallJumpSmoothTime);
        }
        else 
        {
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, new Vector3(modifiedWallJumpForce, modifiedWallJumpHeight, rigidbody.velocity.z), ref refVel, modifiedWallJumpSmoothTime);
        }
        shouldWallJump = false;
        playerStats.TotalWallJumps++;
        wallJumped.Invoke();
    }
}