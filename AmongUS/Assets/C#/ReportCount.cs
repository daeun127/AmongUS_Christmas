using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportCount : MonoBehaviour
{
    public Text counttext;  // �ؽ�Ʈ ����

    void Update()   // �ż���
    {
        counttext.text = Reportcoll.count + " / 30";    // �ؽ�Ʈ ����
    }
}
