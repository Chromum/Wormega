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
            other.gameObject.GetComponent<Damageable>().DoDamage(Damage, other.transform.position);
            other.transform.position = PlayerManager.currentRoom.playerSpawn.position;
        }
    }
}
