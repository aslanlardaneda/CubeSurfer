using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    public Slider slider;
    public float maxDistance = 700f;

    private void Awake()
    {
        SetMaxDistance(maxDistance);
    }

    private void Update()
    {
        SetDistance();
    }

    public void SetMaxDistance(float distance)
    {
        slider.maxValue = distance;
        slider.value = distance;
    }

    public void SetDistance()
    {
        slider.value = PlayerManager.instance.transform.position.z;
    }

}
