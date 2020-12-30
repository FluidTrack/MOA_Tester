using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogHandler : MonoBehaviour
{
    public Text TimeStamp;
    public Text IndexText;

    public void Init(int index,string timeStamp) {
        this.GetComponent<RectTransform>().transform.localPosition = new Vector2(0f,-80f * index);
        TimeStamp.text = timeStamp;
        IndexText.text = ( index + 1 ).ToString();
    }
}
