using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �� �ٲ� ��
using UnityEngine.UI;   // �ؽ�Ʈ

public class ReportTimer : MonoBehaviour
{
    public float sec = 30; // �ð� 30��

    public Text timer;  // Ÿ�̸� �ؽ�Ʈ
    Color initiClor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);  // �ؽ�Ʈ ���ۻ� == ���
    Color currColor = new Vector4(1.0f, 0, 0, 1.0f);    // �ؽ�Ʈ ���� ��

    GameObject htpui;   // �÷��� ��� ui �г�

    private void Start()    // �����ϸ�
    {
        timer.color = initiClor;    // Ÿ�̸� �ؽ�Ʈ ���
        htpui = GameObject.Find("htpui");   // htpui ã��
    }

    public void OnClickPlay()   // �÷��� ��ư ������
    {
        htpui.SetActive(false); // htpui ����
    }

    void Update()   // �� ������ ����
    {
        if(htpui.activeSelf == false)   // htpui�� �������� ����
        {
            sec -= Time.deltaTime;  // Ÿ�̸� �۵�
            timer.text = string.Format("{0:D2}:{1:D2}", 0, (int)sec);   // ��� ���� �ٲٰ� Ÿ�̸� �ؽ�Ʈ ���

            if (sec < 5)    // 5�� ī��Ʈ
            {
                timer.color = currColor;    // �ؽ�Ʈ ������
            }

            if (sec < 0)    // 0�� 
            {
                if (Reportcoll.count > 10)  // 10�� �̻� ���� �ٲ����� 
                    SceneManager.LoadScene("Victory");  // ����
                else
                    SceneManager.LoadScene("Defeat");   // ����
            }
        }
    }
}
