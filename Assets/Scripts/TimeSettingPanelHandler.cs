using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSettingPanelHandler : MonoBehaviour
{
    private MainPanelHandler MainPanel;
    private Animator anim;
    private bool isLocked = false;

    public void Start() {
        MainPanel = MainPanelHandler.GetInstance();
        anim = this.GetComponent<Animator>();
    }

    public void OnCloseButton() {
        if (isLocked) return;
        MainPanel.isPanelOpened = false;
        anim.SetBool("isOpen", false);
    }
}
