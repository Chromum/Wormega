using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MapSpriteSelector : MonoBehaviour
{
   
    public GameObject up, down, left, right;
    public int type; // 0: normal, 1: enter
    public Material normalColor, enterColor,enemyRoomColor,shopRoomColor;
    Material mainColor;
    public MeshRenderer rend;
    public float Y;
    public GameObject Sphere;
    public bool PlayerInRoom;

    public delegate void PlayerEntersRoom();
    public PlayerEntersRoom PER;

    public delegate void PlayerExitsRoom();
    public PlayerEntersRoom PEER;

    public PlayerManager Pm;
    public Transform playerSpawn;

    public UnityEvent ev;
    public UnityEvent Leave;
    public RoomManager rm;
    public int EnemiesNeeded;

    void Start ()
    {
        transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        PickColor();
        Pm = FindObjectOfType<PlayerManager>();
        NavMeshSurface s = gameObject.GetComponent<NavMeshSurface>();
        rm = gameObject.GetComponentInChildren<RoomManager>();
    }
    
    public void PostGen()
    {
        Debug.Log("LOL");
        if (up!=null)
            SpawnGrapplePoint(0,up);
        if (down != null)
            SpawnGrapplePoint(1,down);
        if (left != null)
            SpawnGrapplePoint(2,left);
        if (right != null)
            SpawnGrapplePoint(3,right);

    }

    public GameObject SpawnGrapplePoint(int i, GameObject Connector)
    {
        Vector3 Pos = Vector3.zero;

        switch (i)
        {
            case 0:
                Pos = new Vector3(0, Y, 1.066667f);
                break;
            case 1:
                Pos = new Vector3(0, Y, -1.066667f);
                break;
            case 2:
                Pos = new Vector3(-1.066667f, Y, 0);
                break;
            case 3:
                Pos = new Vector3(1.066667f, Y, 0);
                break;
            default:
                break;
        }
        
        GrapplePoint GO = GameObject.Instantiate(Sphere, Pos, Quaternion.identity, transform).GetComponent<GrapplePoint>();
        GO.transform.localPosition = Pos;
        GO.ConnectorOne = this;
        GO.ConnectorTwo = Connector.GetComponent<MapSpriteSelector>();
        return GO.gameObject;

    }

    void PickColor(){ //changes color based on what type the room is

        switch (type)
        {   
            case 0:
                break;
            case 1:
                transform.gameObject.GetComponentInChildren<RoomManager>().enabled = false;
                break;
            case 3:
                transform.gameObject.GetComponentInChildren<BossRoomManager>().enabled = false;
                break;
            case 4:
                transform.gameObject.GetComponentInChildren<RoomManager>().enabled = false;
                break;
            default:
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInRoom = true;
            if (Pm != null)
                Pm.currentRoom = this;
            else
            {
                Pm = GameObject.FindObjectOfType<PlayerManager>();
                Pm.currentRoom = this;
            }

            PER?.Invoke();
            ev?.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInRoom = false;
            PEER?.Invoke();
            Leave?.Invoke();
        }
    }
}


