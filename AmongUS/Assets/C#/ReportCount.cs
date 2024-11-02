using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportCount : MonoBehaviour
{
    public Text counttext;  // 텍스트 변수

    void Update()   // 매순간
    {
        counttext.text = Reportcoll.count + " / 30";    // 텍스트 변경
    }
}
