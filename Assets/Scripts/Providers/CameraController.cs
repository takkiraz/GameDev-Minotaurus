using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;    
    }

    private void LateUpdate()
    {
        // Follow the Player

        if (!player.CameraIsAnimated)
        { 
            transform.position = player.transform.position + offset;
        }
    }
}
