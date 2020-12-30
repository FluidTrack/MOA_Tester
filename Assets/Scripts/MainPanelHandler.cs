using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelHandler : MonoBehaviour
{
    public Animator TimeSettingPanel;
    public Animator ScanPanel;
    public Text ScanButtonText;
    public Image ScanButtonIcon;
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

    public GameObject WaterLog;
    public GameObject PooLog;
    public GameObject PeeLog;

    private List<GameObject> HistoryList;
    private List<GameObject> RealtimeList;

    public enum LOG_TYPE {
        WATER,POO,PEE
    };

    public static MainPanelHandler GetInstance() {
        return GameObject.Find("MainPanel").GetComponent<MainPanelHandler>();
    }

    public void Awake() {
        HistoryList = new List<GameObject>();
        RealtimeList = new List<GameObject>();
    }

    public void ScanButtonClick() {
        if (isPanelOpened) return;
        isPanelOpened = true;
        ScanPanel.SetBool("isOpen", true);
    }

    public void TimeButtonClick() {
        if (isPanelOpened) return;
        isPanelOpened = true;
        TimeSettingPanel.SetBool("isOpen", true);
    }

    public void BatteryButtonClick() {
        Debug.Log("BatteryButtonClick");
    }

    public void AddHistoryLog(LOG_TYPE type, string timeStamp) {
        GameObject target = null;
        switch (type) {
            case LOG_TYPE.WATER: target = Instantiate(WaterLog, HistoryPanel.transform); break;
            case LOG_TYPE.POO: target = Instantiate(PooLog, HistoryPanel.transform); break;
            case LOG_TYPE.PEE: target = Instantiate(PeeLog, HistoryPanel.transform); break;
        }

        int index = HistoryList.Count;
        target.GetComponent<LogHandler>().Init(index, timeStamp);

        float currentHeight = 80f * ( index + 1 );
        float height = ( currentHeight > 400f ) ? currentHeight : 400f;
        HistoryRect.sizeDelta = new Vector2(990f,height);
    }

    public void AddRealtimeLog(LOG_TYPE type, string timeStamp) {
        GameObject target = null;
        switch (type) {
            case LOG_TYPE.WATER: target = Instantiate(WaterLog, RealtimePanel.transform); break;
            case LOG_TYPE.POO: target = Instantiate(PooLog, RealtimePanel.transform); break;
            case LOG_TYPE.PEE: target = Instantiate(PeeLog, RealtimePanel.transform); break;
        }

        int index = RealtimeList.Count;
        target.GetComponent<LogHandler>().Init(index, timeStamp);

        float currentHeight = 80f * ( index + 1 );
        float height = ( currentHeight > 400f ) ? currentHeight : 400f;
        HistoryRect.sizeDelta = new Vector2(990f, height);
    }

    public void ClearHistoryLog() {
        int length = HistoryList.Count;
        for(int i = 0; i < length; i++)
            Destroy(HistoryList[i]);
        HistoryList.Clear();
    }

    public void ClearRealtimeLog() {
        int length = RealtimeList.Count;
        for (int i = 0; i < length; i++)
            Destroy(RealtimeList[i]);
        RealtimeList.Clear();
    }
}
