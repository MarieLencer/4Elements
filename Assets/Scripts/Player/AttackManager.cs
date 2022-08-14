using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [SerializeField] public GameObject attackPrefab;

    [SerializeField] private GameObject shootingHand;

    [SerializeField] public float spawnDelay = 0.25f;
     
    public void OnSpawnAttack()
    {
        StartCoroutine(InstantiateObject());
    }

    IEnumerator InstantiateObject()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(attackPrefab, shootingHand.transform.position, Quaternion.LookRotation(gameObject.transform.forward), gameObject.transform);
    }
}
