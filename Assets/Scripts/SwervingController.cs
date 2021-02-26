using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwervingController : MonoBehaviour
{
    public static SwervingController instance;

    private float difference;
    private float mousePoint;
    private float mouseDistance_1, mouseDistance_2;
    public float sensivity;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!GameManager.instance.isGameRunning)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            mousePoint = Input.mousePosition.x;
            mouseDistance_1 = Input.mousePosition.x;
            GameSceneManager.instance.tutorialImage.SetActive(false);
        }


        if (Input.GetMouseButton(0))
        {
            mouseDistance_2 = Input.mousePosition.x;
            difference = (mouseDistance_2 - mouseDistance_1) * sensivity;
            transform.position = new Vector3(transform.position.x + difference, transform.position.y, transform.position.z);
        }
    }


}
