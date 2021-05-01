using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VibrateButtonHandler : MonoBehaviour
{
    public void OnClicked() {
      if (BluetoothManager.GetInstance()._connected) {
        BluetoothManager.GetInstance().SetVibrate();
      }
    }
}
