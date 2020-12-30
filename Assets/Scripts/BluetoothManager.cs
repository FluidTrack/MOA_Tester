using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager : MonoBehaviour
{
    public static BluetoothManager GetInstance() {
        return GameObject.Find("MainCanvas").GetComponent<BluetoothManager>();
    }
}
