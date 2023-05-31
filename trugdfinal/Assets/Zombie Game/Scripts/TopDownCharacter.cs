using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopDownCharacter : MonoBehaviour
{
    public float gravity = 1.5f;
    Vector2 direction;
    Vector3 currentMove;
    public Transform lookAtTarget;
    TopDownCharacter pc;
    CharacterController charCon;
    float moveSpeed = 5.5f;
    float runSpeed = 10.5f;
    public Weapons gun;
    public bool canMove;
    float groundedGravity = -0.5f;

    bool shootDown;
    // Start is called before the first frame update
    void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleMovement();
        handleGravity();
        handleShoot();
    }

    void handleMovement()
{
    direction = Vector2.zero;
    if (Input.GetKey(KeyCode.W))
    {
        direction.y += 1;
    }
    if (Input.GetKey(KeyCode.A))
    {
        direction.x -= 1;
    }
    if (Input.GetKey(KeyCode.S))
    {
        direction.y -= 1;
    }
    if (Input.GetKey(KeyCode.D))
    {
        direction.x += 1;
    }
    
    // Normalize the direction to ensure consistent movement speed in all directions
    direction.Normalize();

    // Calculate the movement vector
    currentMove = new Vector3(direction.x, 0, direction.y) * runSpeed;

    // Apply movement to the character controller
    charCon.Move(currentMove * Time.deltaTime);
}

    void handleRotation()
    {
        transform.LookAt(lookAtTarget);
    }
    void handleGravity()
    {
        if (charCon.isGrounded)
        {

            currentMove.y = groundedGravity * Time.deltaTime;

        }
        else
        {
            currentMove.y += 2 * groundedGravity * Time.deltaTime;

        }
    }
    void handleShoot()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            gun.shoot();
        }
    }
}
