using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertHandler : MonoBehaviour
{
    public PopUpHandler [] Popup;
    public enum POPUPS {
        LOW_BAT,
        CHARGE_BAT,
        BAT_INFO,
        CON,
        ALERT,
        ERROR,
    };

    public bool isPopped = false;

    IEnumerator popCheck() {
        isPopped = true;
        yield return new WaitForSeconds(4f);
        isPopped = false;
    }

    static AlertHandler GetInstance() {
        GameObject go = GameObject.Find("MainCanvas");
        return go.GetComponent<AlertHandler>();
    }

    public void Pop_LowBat() {
        Popup[0].anim.SetTrigger(0);
        StartCoroutine(popCheck());
    }

    public void Pop_ChargeBat(int percent) {
        if (isPopped) return;
        Popup[1].SubText.text = "모아밴드 배터리 잔량 : " + percent + "%";
        Popup[1].anim.SetTrigger(0);
        StartCoroutine(popCheck());
    }

    public void Pop_BatInfo(int percent) {
        if (isPopped) return;
        Popup[2].SubText.text = "모아밴드 배터리 잔량 : " + percent + "%";
        Popup[2].anim.SetTrigger(0);
        StartCoroutine(popCheck());
    }

    public void Pop_Con() {
        if (isPopped) return;
        Popup[3].anim.SetTrigger(0);
        StartCoroutine(popCheck());
    }

    public void Pop_Alert(string alert) {
        Popup[4].SubText.text = alert;
        Popup[4].anim.SetTrigger(0);
        StartCoroutine(popCheck());
    }

    public void Pop_Error(string error) {
        Popup[4].SubText.text = error;
        Popup[4].anim.SetTrigger(0);
        StartCoroutine(popCheck());
    }
}
