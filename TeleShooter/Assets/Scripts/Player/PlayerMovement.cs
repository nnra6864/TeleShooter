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
    PlayerGroundCheck playerGroundCheck;
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

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerGroundCheck = GetComponentInChildren<PlayerGroundCheck>();
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

        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y + (playerRigidbody.velocity.y / 15), playerRigidbody.velocity.z);
        }

        if (shouldJump && playerStats.CanJump)
        {
            Jump();
        }
        if (shouldWallJump && playerStats.CanWallJump)
        {
            WallJump(playerStats.IsMovingBackwards);
        }
    }

    private void CheckForCrouchAndSprint() 
    {
        //Checks if Player is Crouching and Sprinting
        if (Input.GetKey(KeyCode.LeftControl) && playerStats.CanCrouch && Input.GetKey(KeyCode.LeftShift) && playerStats.CanSprint)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                playerStats.IsCrouching = true;
                playerStats.IsSprinting = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerStats.IsSprinting = true;
                playerStats.IsCrouching = false;
            }
        }
        //Checks if Player is Crouching
        else if (Input.GetKey(KeyCode.LeftControl) && playerStats.CanCrouch)
        {
            playerStats.IsCrouching = true;
            playerStats.IsSprinting = false;
        }
        //Checks if Player is Sprinting
        else if (Input.GetKey(KeyCode.LeftShift) && playerStats.CanSprint)
        {
            playerStats.IsSprinting = true;
            playerStats.IsCrouching = false;
        }
        else
        {
            playerStats.IsCrouching = false;
            playerStats.IsSprinting = false;
        }
    }

    private void ModifyValues() 
    {
        CheckForCrouchAndSprint();

        if (playerStats.IsCrouching)
        {
            playerStats.ModifiedSpeed = (playerStats.Speed * playerStats.CrouchSpeedMultiplier) * playerClock.timeScale;
            playerStats.ModifiedJumpHeight = (playerStats.JumpHeight * playerStats.CrouchJumpMultiplier) * playerClock.timeScale;
            playerStats.ModifiedWallJumpForce = (playerStats.WallJumpForce * playerStats.CrouchWallJumpForceMultiplier) * playerClock.timeScale;
            playerStats.ModifiedWallJumpHeight = (playerStats.WallJumpHeight * playerStats.CrouchWallJumpHeightMultiplier) * playerClock.timeScale;
        }
        else if (playerStats.IsSprinting)
        {
            playerStats.ModifiedSpeed = (playerStats.Speed * playerStats.SprintSpeedMultiplier) * playerClock.timeScale;
            playerStats.ModifiedJumpHeight = (playerStats.JumpHeight * playerStats.SprintJumpMultiplier) * playerClock.timeScale;
            playerStats.ModifiedWallJumpForce = (playerStats.WallJumpForce * playerStats.SprintWallJumpForceMultiplier) * playerClock.timeScale;
            playerStats.ModifiedWallJumpHeight = (playerStats.WallJumpHeight * playerStats.SprintWallJumpHeightMultiplier) * playerClock.timeScale;
        }
        else
        {
            playerStats.ModifiedSpeed = playerStats.Speed * playerClock.timeScale;
            playerStats.ModifiedJumpHeight = playerStats.JumpHeight * playerClock.timeScale;
            playerStats.ModifiedWallJumpForce = playerStats.WallJumpForce * playerClock.timeScale;
            playerStats.ModifiedWallJumpHeight = playerStats.WallJumpHeight * playerClock.timeScale;
        }
        //Multiplies everything with Player Clock's Timescale;
        modifiedMovementSmoothTime = playerStats.MovementSmoothTime * playerClock.timeScale;
        modifiedJumpSmoothTime = playerStats.JumpSmoothTime * playerClock.timeScale;
        modifiedWallJumpSmoothTime = playerStats.WallJumpSmoothTime * playerClock.timeScale;
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
        if (!playerStats.CanMove) return;

        playerRigidbody.velocity = Vector3.SmoothDamp(playerRigidbody.velocity, new Vector3(playerStats.ModifiedSpeed * movementDirection, playerRigidbody.velocity.y, playerRigidbody.velocity.z), ref refVel, modifiedMovementSmoothTime);
        if (movementDirection == 1) playerStats.IsMovingBackwards = false; else if(movementDirection == -1) playerStats.IsMovingBackwards = true;
    }

    private void VerticalInput() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerWallCheck.canWallJump)
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
        playerRigidbody.velocity = Vector3.SmoothDamp(playerRigidbody.velocity, new Vector3(playerRigidbody.velocity.x, playerStats.ModifiedJumpHeight, playerRigidbody.velocity.z), ref refVel, modifiedJumpSmoothTime);
        playerStats.JumpsLeft--;
        shouldJump = false;
        playerStats.TotalJumps++;
    }

    private void WallJump(bool isBackwards)
    {
        if (!isBackwards)
        {
            playerRigidbody.velocity = Vector3.SmoothDamp(playerRigidbody.velocity, new Vector3(-playerStats.ModifiedWallJumpForce, playerStats.ModifiedWallJumpHeight, playerRigidbody.velocity.z), ref refVel, modifiedWallJumpSmoothTime);
        }
        else 
        {
            playerRigidbody.velocity = Vector3.SmoothDamp(playerRigidbody.velocity, new Vector3(playerStats.ModifiedWallJumpForce, playerStats.ModifiedWallJumpHeight, playerRigidbody.velocity.z), ref refVel, modifiedWallJumpSmoothTime);
        }
        shouldWallJump = false;
        playerStats.JumpsLeft++;
        playerStats.TotalWallJumps++;
    }
}