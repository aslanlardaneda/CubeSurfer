using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpperPlayerTrigger"))
        {
            PlayerManager.instance.isFinished = true;
            GameManager.instance.isSucces = false;
            StartCoroutine(GameManager.instance.FinishGame());
        }

        if (other.CompareTag("UpperPlayerFinalTrigger"))
        {
            PlayerManager.instance.isFinished = true;
            StartCoroutine(GameManager.instance.FinishGame());
        }

    }
}
