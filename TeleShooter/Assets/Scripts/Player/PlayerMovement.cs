using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    //Components
    PlayerStats playerStats;
    Rigidbody playerRigidbody;
    PlayerWallCheck playerWallCheck;
    Clock playerClock;

    float modifiedMovementSmoothTime;
    float modifiedJumpSmoothTime;
    float modifiedWallJumpSmoothTime;

    bool shouldJump = false;
    bool shouldWallJump = false;

    //Other
    int movementDirection = 0;
    Vector3 refVel = Vector3.zero;
    #endregion

    #region Functions
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerWallCheck = GetComponentInChildren<PlayerWallCheck>();
    }

    void Update()
    {
        playerClock = Timekeeper.instance.Clock("Player");

        ModifyValues();
        HorizontalInput();
        VerticalInput();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();

        if (shouldJump)
        {
            Jump();
        }
        if (shouldWallJump)
        {
            WallJump();
        }
    }
    #endregion

    #region CustomFunctions
    private void CheckForCrouchAndSprint() 
    {
        //Checks if Player is Crouching and Sprinting
        if (Input.GetKey(KeyCode.LeftControl) && playerStats.canCrouch && Input.GetKey(KeyCode.LeftShift) && playerStats.canSprint)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                playerStats.isCrouching = true;
                playerStats.isSprinting = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerStats.isSprinting = true;
                playerStats.isCrouching = false;
            }
        }
        //Checks if Player is Crouching
        else if (Input.GetKey(KeyCode.LeftControl) && playerStats.canCrouch)
        {
            playerStats.isCrouching = true;
            playerStats.isSprinting = false;
        }
        //Checks if Player is Sprinting
        else if (Input.GetKey(KeyCode.LeftShift) && playerStats.canSprint)
        {
            playerStats.isSprinting = true;
            playerStats.isCrouching = false;
        }
        else
        {
            playerStats.isCrouching = false;
            playerStats.isSprinting = false;
        }
    }

    private void ModifyValues() 
    {
        CheckForCrouchAndSprint();

        if (playerStats.isCrouching)
        {
            playerStats.modifiedSpeed = (playerStats.speed * playerStats.crouchSpeedMultiplier) * playerClock.timeScale;
            playerStats.modifiedJumpHeight = (playerStats.jumpHeight * playerStats.crouchJumpMultiplier) * playerClock.timeScale;
            playerStats.modifiedWallJumpForce = (playerStats.wallJumpForce * playerStats.crouchWallJumpForceMultiplier) * playerClock.timeScale;
            playerStats.modifiedWallJumpHeight = (playerStats.wallJumpHeight * playerStats.crouchWallJumpHeightMultiplier) * playerClock.timeScale;
        }
        else if (playerStats.isSprinting)
        {
            playerStats.modifiedSpeed = (playerStats.speed * playerStats.sprintSpeedMultiplier) * playerClock.timeScale;
            playerStats.modifiedJumpHeight = (playerStats.jumpHeight * playerStats.sprintJumpMultiplier) * playerClock.timeScale;
            playerStats.modifiedWallJumpForce = (playerStats.wallJumpForce * playerStats.sprintWallJumpForceMultiplier) * playerClock.timeScale;
            playerStats.modifiedWallJumpHeight = (playerStats.wallJumpHeight * playerStats.sprintWallJumpHeightMultiplier) * playerClock.timeScale;
        }
        else
        {
            playerStats.modifiedSpeed = playerStats.speed * playerClock.timeScale;
            playerStats.modifiedJumpHeight = playerStats.jumpHeight * playerClock.timeScale;
            playerStats.modifiedWallJumpForce = playerStats.wallJumpForce * playerClock.timeScale;
            playerStats.modifiedWallJumpHeight = playerStats.wallJumpHeight * playerClock.timeScale;
        }
        //Multiplies everything with Player Clock's Timescale;
        modifiedMovementSmoothTime = playerStats.movementSmoothTime * playerClock.timeScale;
        modifiedJumpSmoothTime = playerStats.jumpSmoothTime * playerClock.timeScale;
        modifiedWallJumpSmoothTime = playerStats.wallJumpSmoothTime * playerClock.timeScale;
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
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movementDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementDirection = 1;
        }
        else movementDirection = 0;
    }

    private void HorizontalMovement()
    {
        if (!playerStats.canMove) return;

        playerRigidbody.velocity = Vector3.SmoothDamp(playerRigidbody.velocity, new Vector3(playerStats.modifiedSpeed * movementDirection, playerRigidbody.velocity.y, playerRigidbody.velocity.z), ref refVel, modifiedMovementSmoothTime);
        if (movementDirection == 1) playerStats.isMovingBackwards = false; else if(movementDirection == -1) playerStats.isMovingBackwards = true;
    }

    private void VerticalInput() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldWallJump = playerWallCheck.canWallJump && playerStats.canWallJump;
            shouldJump = playerStats.jumpsLeft > 0 && playerStats.canJump;
        }
    }

    private void Jump() 
    {
        playerRigidbody.velocity = Vector3.SmoothDamp(playerRigidbody.velocity, new Vector3(playerRigidbody.velocity.x, playerStats.modifiedJumpHeight, playerRigidbody.velocity.z), ref refVel, modifiedJumpSmoothTime);
        playerStats.jumpsLeft--;
        shouldJump = false;
        playerStats.totalJumps++;
    }

    private void WallJump()
    {
        var isBackwardsModifiedWallJumpForce = playerStats.isMovingBackwards ? playerStats.modifiedWallJumpForce : -playerStats.modifiedWallJumpForce;
        playerRigidbody.velocity = Vector3.SmoothDamp(playerRigidbody.velocity, new Vector3(isBackwardsModifiedWallJumpForce, playerStats.modifiedWallJumpHeight, playerRigidbody.velocity.z), ref refVel, modifiedWallJumpSmoothTime);
        shouldWallJump = false;
        playerStats.jumpsLeft++;
        playerStats.totalWallJumps++;
    }
    #endregion
}
