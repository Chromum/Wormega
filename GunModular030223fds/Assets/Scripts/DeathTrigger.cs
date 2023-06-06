using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public float Damage;
    public PlayerManager PlayerManager;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Damageable d = other.gameObject.GetComponent<Damageable>();
            d.DoDamage(d.Health/2, other.transform.position);
            other.transform.position = PlayerManager.currentRoom.playerSpawn.position;
        }
    }
}
