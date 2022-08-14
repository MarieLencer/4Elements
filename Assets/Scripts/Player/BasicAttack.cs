using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [SerializeField] public float attackSpeed = 10.0f;

    [SerializeField] public float duration = 3.0f;

    public int damagePoints = 10;

    private Vector3 movement;

    [SerializeField] private Rigidbody rigidbody;
   
    void Start()
    {
        transform.rotation = transform.parent.rotation;
        movement = transform.position;
        
        Destroy(gameObject, duration);
    }
    
    void Update ()
    {
        movement += transform.forward * attackSpeed * Time.deltaTime;
        transform.position = movement;
    }

    public int getDamagePoints()
    {
        return damagePoints;
    }
}
