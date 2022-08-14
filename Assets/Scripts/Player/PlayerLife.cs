using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLife :  Life
{
    private PlayerMovement movementScript;

    public PlayerManager playerManager;

    private CharacterController controller;

    private bool dead = false;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        movementScript = gameObject.GetComponent<PlayerMovement>();
        playerManager = GameObject.FindGameObjectWithTag("Player Manager").GetComponent<PlayerManager>();
        controller = gameObject.GetComponent<CharacterController>();
        
        Spawn();
    }


    public override void CheckLife()
    {
        if (lifePoints <= 0)
        {
            Dying();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject trigger = other.gameObject;
        if (trigger.CompareTag("Enemy Attack"))
        {
            lifePoints -= trigger.GetComponent<BasicAttack>().getDamagePoints();
            Destroy(trigger);
            CheckLife();
        }
    }
    
    public override void Dying()
    {
        if (!dead)
        {
            animator.SetBool("dying", true);
            movementScript.SetEnabled(false);
            playerManager.deactivatePlayer();
            dead = true;
        }
    }

    public override void Spawn()
    {
        maxLifePoints = playerManager.getPlayerMaxLife();
        lifePoints = maxLifePoints;
    }

    public bool isDead()
    {
        return dead;
    }
    
}
