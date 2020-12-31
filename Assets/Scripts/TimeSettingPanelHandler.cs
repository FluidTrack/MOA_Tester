using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSettingPanelHandler : MonoBehaviour
{
    private MainPanelHandler MainPanel;
    private Animator anim;
    private bool isLocked = false;
    public Text CurrentTimeStamp;
    public InputField Text_Year;
    public InputField Text_Month;
    public InputField Text_Day;
    public InputField Text_Hour;
    public InputField Text_Min;
    public InputField Text_Sec;
    public Text Notice;
    public AudioClip Beep;

    public void Start() {
        MainPanel = MainPanelHandler.GetInstance();
        anim = this.GetComponent<Animator>();
        System.DateTime now = System.DateTime.Now;
        Text_Year.text = now.Year + "";
        Text_Month.text = (now.Month) + "";
        Text_Day.text = now.Day + "";
        Text_Hour.text = now.Hour + "";
        Text_Min.text = now.Minute + "";
        Text_Sec.text = "0";
        StartCoroutine(tictock());
    }

    IEnumerator tictock() {
        while(true) {
            System.DateTime now = System.DateTime.Now;
            string str = now.Year + "년";
            str += ( now.Month ) + "월";
            str += now.Day + "일\n";
            str += now.Hour + "시";
            str += now.Minute + "분";
            str += now.Second + "초";
            CurrentTimeStamp.text = str;
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnCurrentTimeButton() {
        BluetoothManager.GetInstance().SetCurrentTime();
        OnCloseButton();
    }

    private int[] NormalYearDaysList = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private int[] LeafYearDaysList = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private static bool isLeafYear(int year) {
        if (( year % 4 ) == 0) {
            if (( year % 100 ) == 0) {
                if (( year % 400 ) == 0) {
                    return true;
                } else return false;
            } else return true;
        } else return false;
    }

    public void OnTimeButton() {
        try {

            int yyyy = int.Parse(Text_Year.text);
            bool isLeaf = isLeafYear(yyyy);
            yyyy -= 2000;
            int[] dayList = ( isLeaf ) ? LeafYearDaysList : NormalYearDaysList;
            int MM = int.Parse(Text_Month.text);
            int dd = int.Parse(Text_Day.text);
            int hh = int.Parse(Text_Hour.text);
            int mm = int.Parse(Text_Min.text);
            int ss = int.Parse(Text_Sec.text);

            if (yyyy < 0) {
                Notice.text = "2000년 이상을 입력하세요.";
                Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Beep);
                return;
            } else if (MM < 1 || MM > 12) {
                Notice.text = "1월부터 12월로 입력하세요.";
                Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Beep);
                return;
            } else if (dd < 1 || dd > dayList[MM - 1]) {
                Notice.text = "정확한 날짜를 입력해주세요.";
                Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Beep);
                return;
            } else if (hh < 0 || hh > 23) {
                Notice.text = "정확한 시간(H)을 입력해주세요.";
                Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Beep);
                return;
            } else if (mm < 0 || mm > 59) {
                Notice.text = "정확한 시간(M)을 입력해주세요.";
                Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Beep);
                return;
            } else if (ss < 0 || ss > 59) {
                Notice.text = "정확한 시간(S)을 입력해주세요.";
                Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Beep);
                return;
            }

            BluetoothManager.GetInstance().SetTime(yyyy, MM, dd, hh, mm, ss);
            OnCloseButton();
        } catch (System.Exception e) {
            e.ToString();
            Notice.text = "정확히 입력해주세요.";
            Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(Beep);
            return;
        }
        
    }

    public void OnCloseButton() {
        if (isLocked) return;
        MainPanel.isPanelOpened = false;
        anim.SetBool("isOpen", false);
    }

}
