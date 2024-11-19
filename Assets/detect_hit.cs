using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect_hit : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player enetered the sphere");
        }
    }
}