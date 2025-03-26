using System;

using UnityEngine;
public class PlayerFeet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit! Destroying...");
            Destroy(collision.gameObject);
        }
    }
}