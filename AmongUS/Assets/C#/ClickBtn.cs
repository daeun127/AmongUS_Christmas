using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickBtn : MonoBehaviour
{
    public void OnClickBtn()    // ��ư�� ������
    {
        if(transform.GetChild(0).GetComponent<Text>().text == GameManager.impoText) //��ư�� �ڽ� ������Ʈ�� �ؽ�Ʈ�� �������� �ؽ�Ʈ�� ��ġ�ϸ�
        {
            SceneManager.LoadScene("Victory");  // ����
        }
        else
        {
            SceneManager.LoadScene("Defeat");   // ����
        }
    }

    public void OnclickHtPBtn() // ���� ��� ��ư ������
    {
        GameObject ui = GameObject.Find("HTP").transform.Find("HTPui").gameObject;  // ui ã�Ƽ�

        if (ui.activeSelf == false)  // ������
            ui.SetActive(true); //�Ѱ�
        else//������
            ui.SetActive(false);//����
    }

    public void OnclickPlayBtn()    // pl
    {
        print("��ȯ");
    }

}
