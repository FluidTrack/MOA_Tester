using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LEDButtonHandler : MonoBehaviour
{
    public string color;

    public void OnClicked() {
      if (BluetoothManager.GetInstance()._connected) {
        BluetoothManager.GetInstance().SetLED(color);
      }
    }
}
