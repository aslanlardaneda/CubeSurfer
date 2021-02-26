using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoodDisplay : MonoBehaviour
{
    [HideInInspector]
    public static FoodDisplay instance;

    public void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "NotFood")
        {
            gameObject.SetActive(false);
            PlayerManager.instance.StackFood();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LittleObstacle")
        {
            gameObject.SetActive(false);
        }
    }

}
