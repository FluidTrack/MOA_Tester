using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LEDButtonHandler : MonoBehaviour
{

    public void OnRedClicked() {
      if (BluetoothManager.GetInstance()._connected) {
        BluetoothManager.GetInstance().SetRedLED();
      }
    }

    public void OnGreenClicked() {
      if (BluetoothManager.GetInstance()._connected) {
        BluetoothManager.GetInstance().SetGreenLED();
      }
    }

    public void OnBlueClicked() {
      if (BluetoothManager.GetInstance()._connected) {
        BluetoothManager.GetInstance().SetBlueLED();
      }
    }

}
