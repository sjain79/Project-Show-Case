﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerInput : MonoBehaviour
{
    TextMeshProUGUI side1, side2;

    private void Update()
    {
        Debug.Log(Input.GetJoystickNames());
        if (Input.GetButtonDown("Player 1 Start"))
        {
            Debug.Log("Controllrt Start Button Pressed");
        }
    }

}
