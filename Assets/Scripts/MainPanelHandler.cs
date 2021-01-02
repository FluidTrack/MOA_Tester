using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelHandler : MonoBehaviour
{
    public Animator TimeSettingPanel;
    public Animator ScanPanel;
    public Text ScanButtonText;
    public Text BTStateText;
    public Image ScanButtonIcon;
    public Button ScanButton;
    public Sprite Link;
    public Sprite Unlink;
    public bool isPanelOpened = false;

    public Color BlueColor;
    public Color BlueColor2;
    public Color RedColor;
    public Color RedColor2;

    public GameObject HistoryPanel;
    public GameObject RealtimePanel;
    public RectTransform HistoryRect;
    public RectTransform RealtimeRect;
    public ScrollRect HistoryScroll;
    public ScrollRect RealtimeScroll;

    public GameObject WaterLog;
    public GameObject PooLog;
    public GameObject PeeLog;

    private List<GameObject> HistoryList;
    private List<GameObject> RealtimeList;

    public AudioClip LogCreateSound;
    public GameObject Blind;
    public GameObject Opening;

    public enum LOG_TYPE {
        WATER,POO,PEE
    };

    public static MainPanelHandler GetInstance() {
        return GameObject.Find("MainPanel").GetComponent<MainPanelHandler>();
    }

    public void Awake() {
        HistoryList = new List<GameObject>();
        RealtimeList = new List<GameObject>();
        Opening.SetActive(true);
    }

    public void ScanButtonClick() {
        if (isPanelOpened) return;

        if (!BluetoothManager.GetInstance()._connected) {
            isPanelOpened = true;
            ScanPanel.SetBool("isOpen", true);
        } else {
            BluetoothManager.GetInstance()
                .SetState(BluetoothManager.States.Disconnect, 0.1f);
        }
    }

    public void TimeButtonClick() {
        if (isPanelOpened) return;
        if (!BluetoothManager.GetInstance()._connected) {
            AlertHandler.GetInstance().Pop_Alert("모아밴드와 연결을 먼저 해 주세요");
            return;
        }
        isPanelOpened = true;
        TimeSettingPanel.SetBool("isOpen", true);
    }

    public void BatteryButtonClick() {
        if (isPanelOpened) return;
        BluetoothManager.GetInstance().QueryBattery();
    }

    public void BlindControl(bool value) {
        Blind.SetActive(value);
    }

    public void AddHistoryLog(LOG_TYPE type, string timeStamp) {
        GameObject target = null;
        switch (type) {
            case LOG_TYPE.WATER: target = Instantiate(WaterLog, HistoryPanel.transform); break;
            case LOG_TYPE.POO: target = Instantiate(PooLog, HistoryPanel.transform); break;
            case LOG_TYPE.PEE: target = Instantiate(PeeLog, HistoryPanel.transform); break;
        }
        Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(LogCreateSound);
        HistoryList.Add(target);

        int index = HistoryList.Count;
        target.GetComponent<LogHandler>().Init(index-1, timeStamp);

        HistoryRect.sizeDelta = new Vector2(1200f, 80f * ( index));
        HistoryScroll.verticalNormalizedPosition = 0f;

    }

    public void AddRealtimeLog(LOG_TYPE type, string timeStamp) {
        GameObject target = null;
        switch (type) {
            case LOG_TYPE.WATER: target = Instantiate(WaterLog, RealtimePanel.transform); break;
            case LOG_TYPE.POO: target = Instantiate(PooLog, RealtimePanel.transform); break;
            case LOG_TYPE.PEE: target = Instantiate(PeeLog, RealtimePanel.transform); break;
        }
        Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(LogCreateSound);

        RealtimeList.Add(target);
        int index = RealtimeList.Count;
        target.GetComponent<LogHandler>().Init(index-1, timeStamp);

        RealtimeRect.sizeDelta = new Vector2(1200f, 80f * ( index));
        RealtimeScroll.verticalNormalizedPosition = 0f;
    }

    public void ClearHistoryLog() {
        int length = HistoryList.Count;
        for(int i = 0; i < length; i++)
            Destroy(HistoryList[i]);
        HistoryList.Clear();
        HistoryRect.sizeDelta = new Vector2(1200f, 0f);
    }

    public void ClearRealtimeLog() {
        int length = RealtimeList.Count;
        for (int i = 0; i < length; i++)
            Destroy(RealtimeList[i]);
        RealtimeList.Clear();
        HistoryRect.sizeDelta = new Vector2(1200f, 0f);
    }

    public void Connect() {
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.normalColor = RedColor;
        colorBlock.highlightedColor = RedColor;
        colorBlock.pressedColor = RedColor2;
        colorBlock.selectedColor = RedColor;
        ScanButtonText.text = "모아밴드 연결 해제";
        ScanButtonIcon.sprite = Unlink;
        colorBlock.colorMultiplier = 1;
        colorBlock.fadeDuration = 0.1f;
        ScanButton.colors = colorBlock;
        BTStateText.text = BluetoothManager.GetInstance().DeviceName;
    }

    public void Disconnect() {
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.normalColor = BlueColor;
        colorBlock.highlightedColor = BlueColor;
        colorBlock.pressedColor = BlueColor2;
        colorBlock.selectedColor = BlueColor;
        ScanButtonText.text = "모아밴드 연결 시작";
        ScanButtonIcon.sprite = Link;
        colorBlock.colorMultiplier = 1;
        colorBlock.fadeDuration = 0.1f;
        ScanButton.colors = colorBlock;
        BTStateText.text = "블루투스 미연결";
        ClearHistoryLog();
        ClearRealtimeLog();
    }
}
