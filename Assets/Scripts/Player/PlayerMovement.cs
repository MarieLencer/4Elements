using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 2.5f;
    [SerializeField] public float jumpHeight = 10.0f;
    [SerializeField] public Animator animator;
    [SerializeField] private bool enabled = false;
    [SerializeField] public AttackManager attackManager;

    private Vector2 InputMovement;

    private Vector3 jumpVelocity;

    private CharacterController controller;

    private bool jumpPressed = false;

    private bool canJump = true;
    
    private float leftTurn = 270f;

    private float rightTurn = 90f;

    private bool firing = false;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        
    }
    
    void FixedUpdate()
    {
        if (enabled)
        {
            Movement();
        }
    }
    

    void OnMove(InputValue inputValue)
    {
        InputMovement = inputValue.Get<Vector2>();
    }
    

    void OnFire()
    {
        if (!firing && enabled)
        {
            firing = true;
            StartCoroutine(Attack());
        } 
    }

    IEnumerator Attack()
    {
        animator.SetBool("fire", true);
        attackManager.OnSpawnAttack();
        yield return new WaitForSeconds(1.75f);
        animator.SetBool("fire", false);
        firing = false;
    }

    void Movement()
    {
        Vector3 movement = new Vector3(InputMovement.x, 0.0f, InputMovement.y);
        switch (movement.x)
        {
            case 1: 
                controller.transform.rotation = Quaternion.Euler(0, rightTurn, 0);
                animator.SetBool("isRunningRight", true);
                animator.SetBool("isRunningLeft", false);
                break;
            case 0:
                switch (movement.z)
                {
                    case 1:
                        controller.transform.rotation = Quaternion.Euler(0, 0, 0);
                        animator.SetBool("isRunningRight", true);
                        animator.SetBool("isRunningLeft", false);
                        break;
                    case 0:
                        animator.SetBool("isRunningRight", false);
                        animator.SetBool("isRunningLeft", false);
                        break;
                    case -1:
                        controller.transform.rotation = Quaternion.Euler(0, 180, 0);
                        animator.SetBool("isRunningLeft", true);
                        animator.SetBool("isRunningRight", false);
                        break;
                }
                break;
            case -1: 
                controller.transform.rotation = Quaternion.Euler(0, leftTurn, 0);
                animator.SetBool("isRunningLeft", true);
                animator.SetBool("isRunningRight", false);
                break;
        }
        controller.SimpleMove(movement * speed );
    }
    

    public void SetEnabled(bool active)
    {
        enabled = active;
    }
}