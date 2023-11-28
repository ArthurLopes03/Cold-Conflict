using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;
    public PlayerCam playerCam;


    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    private float horizontalInput;
    private float verticalInput;

    Vector3 inputDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(pm.sprintKey) && Input.GetKeyDown(pm.crouchKey) && (horizontalInput != 0 || verticalInput != 0) && pm.moveSpeed == pm.sprintSpeed)
        {
            StartSlide();
        }

        if ((Input.GetKeyUp(pm.sprintKey) || Input.GetKeyUp(pm.crouchKey)) && pm.isSliding)
            StopSlide();
    }

    private void FixedUpdate()
    {
        if (pm.isSliding)
            SlidingMovement(inputDirection);
    }

    private void StartSlide()
    {
        Debug.Log("StartSlide Called");
        pm.isSliding = true;

        playerCam.sensX = playerCam.sensX / 3;
        playerCam.sensY = playerCam.sensY / 3;

        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement(Vector3 inputDirection)
    {

        // sliding normal
        if(!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }

        // sliding down a slope
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        pm.isSliding = false;

        playerCam.sensX = playerCam.sensX * 3;
        playerCam.sensY = playerCam.sensY * 3;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
