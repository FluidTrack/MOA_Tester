using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanPanelHandler : MonoBehaviour
{
    private MainPanelHandler MainPanel;
    private Animator anim;
    private bool isLocked = false;

    public Color BlueColor;
    public Color BlueColor2;
    public Color RedColor;
    public Color RedColor2;
    public Button ScanButton;
    public Text ScanButtonText;

    public Animator RingAnim;

    public void Start() {
        MainPanel = MainPanelHandler.GetInstance();
        anim = this.GetComponent<Animator>();
    }

    public void OnCloseButton() {
        if (isLocked) return;
        MainPanel.isPanelOpened = false;
        anim.SetBool("isOpen", false);
    }

    public void ScanButtonClick() {
        ColorBlock colorBlock = new ColorBlock();
        if(!isLocked) {
            colorBlock.normalColor = RedColor;
            colorBlock.highlightedColor = RedColor;
            colorBlock.pressedColor = RedColor2;
            colorBlock.selectedColor = RedColor;
            ScanButtonText.text = "주변 BLE 기기 탐색 종료";
        } else {
            colorBlock.normalColor = BlueColor;
            colorBlock.highlightedColor = BlueColor;
            colorBlock.pressedColor = BlueColor2;
            colorBlock.selectedColor = BlueColor;
            ScanButtonText.text = "주변 BLE 기기 탐색 시작";
        }
        colorBlock.colorMultiplier = 1;
        colorBlock.fadeDuration = 0.1f;
        ScanButton.colors = colorBlock;
        isLocked = !isLocked;
        RingAnim.SetBool("RingRing",isLocked);
    }
}
