using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �� ��ȯ

public class ManageS : MonoBehaviour
{
    static public void OnClickStartBtn()   // ���� ��ư�� ������
    {
        SceneManager.LoadScene("AmongUs");  // �� �ε�

        SetId.ids = new List<string>() { "�Ƹ�׺ҿ��ϰ�", "������������", "�������׷���", "�����Ǹ��һ�", "�Ͽ��������̴¼�", "�𸣴°���å" };   // id ����Ʈ
        GameManager.dieList = null; // ũ�� �׾����� ��Ҵ��� �迭
        GameManager.cdist = null;    // �ڵ� ���� ũ����� �Ÿ�
        GameManager.textList = new List<string>(); // text ���ڿ��� ���� ����Ʈ
        GameManager.impoText = "";  // ���� ���̵� ����
        GameManager.dienum = 5;   // ���� ũ�� �ѹ�
        PlayerCtrl.crew = null;     // ���̾��Ű ũ�� ��
        AIcrew.dieCount = 0;   // crew ���� Ƚ��
        Reportcoll.count = 0;   // ���׸��� �ٲ� Ƚ�� ��
    }

    public void OnClickHTPBtn()   // ���ӹ�� ��ư�� ������
    {
        GameObject.Find("Canvas").transform.Find("htpui").gameObject.SetActive(true);   // ���̾��Ű â���� �г� ã�Ƽ� �ѱ�
    }

    public void OnClickCloseBtn()   // �ݱ� ��ư ������
    {
        GameObject.Find("htpui").gameObject.SetActive(false);   // �г� ����
    }

    public void OnClickQuitBtn()   // ������ ��ư�� ������
    {
        SceneManager.LoadScene("Start");  // �� �ε�
    }
}
