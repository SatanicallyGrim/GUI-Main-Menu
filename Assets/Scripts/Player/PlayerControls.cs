using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControls : MonoBehaviour
{
    #region Variables
    [Header("Speed Vars")]
    public float moveSpeed;
    public float walkSpeed, runSpeed, crouchSpeed, jumpHeight;
    public bool isGrounded;
    private Vector3 jump;
    private Rigidbody rb;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (BindingManager.BindingHeld("Forward"))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (BindingManager.BindingHeld("Left"))
        {
            transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }
        if (BindingManager.BindingHeld("Right"))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        if (BindingManager.BindingHeld("Backwards"))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }
        if (BindingManager.BindingHeld("Sprint"))
        {
            moveSpeed = runSpeed;
        }
        else if (!BindingManager.BindingHeld("Sprint"))
        {
            moveSpeed = walkSpeed;
        }
        if (BindingManager.BindingPressed("Jump") && isGrounded)
        {
            rb.AddForce(jump * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }
        if (BindingManager.BindingHeld("Crouch"))
        {
            moveSpeed = crouchSpeed;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    
}
