using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float pushForce = 3.0f;
    public float rotSpeed = 9.0f;
    public float moveSpeed = 4.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private CharacterController charController;
    private float vertSpeed;
    private ControllerColliderHit contact;
    private Animator animator;

    private void Start()
    {
        vertSpeed = minFall;
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0)
        {
            Vector3 right = target.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            movement = (right * horInput) + (forward * vertInput);
            movement *= moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        bool hitGround = false;
        RaycastHit hit;
        if(vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if(hitGround) {
            if (Input.GetButtonDown("Jump")) {
                vertSpeed = jumpSpeed;
            } else {
                vertSpeed = minFall;
                animator.SetBool("Jumping", false);
            }
        }  else {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity) {
                vertSpeed = terminalVelocity;
            }
            if(contact != null) {
                animator.SetBool("Jumping", true);
            }
            
            if (charController.isGrounded) {
                if (Vector3.Dot(movement, contact.normal) < 0) {
                    movement = contact.normal * moveSpeed;
                } else {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if(body != null && !body.isKinematic) {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
