using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAccelerate : MonoBehaviour
{
    public void SetScale(float value)
    {
        Time.timeScale = value;
    }
}