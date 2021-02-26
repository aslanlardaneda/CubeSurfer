using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public GameObject player;
    //public Vector3 offset;
    public float zOffset;

    private void Awake()
    {
        instance = this;
    }

    public void LateUpdate()
    {
        Vector3 cam = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - zOffset);
        //transform.position = Vector3.Lerp(transform.position, player.transform.position - offset, 0.7f);
        transform.position = Vector3.Lerp(transform.position, cam, 0.7f);
    }
}
