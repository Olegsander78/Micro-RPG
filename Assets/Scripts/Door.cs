using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform DoorEntryPosition;

    public void OpenDoor()
    {
        FindObjectOfType<Player>().transform.position = DoorEntryPosition.position;
    }
}
