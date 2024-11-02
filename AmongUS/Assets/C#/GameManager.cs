using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    AudioSource _audio;   // ����� �ҽ�
    public AudioClip startsound; // start bgm
    public AudioClip votesound; // start bgm

    public GameObject us;   // �÷��̾� ������Ʈ
    public GameObject impo; // �� ������Ʈ
    public GameObject crew; // ũ�� ������Ʈ
    GameObject map; // ���� ��ư
    GameObject report;  // ����Ʈ ��ư

    public Text TaskText;   // �̼� Ƚ�� ǥ��
    float task1count = 0;    // �̼�1 Ƚ��
    float task2count = 0;    // �̼�2 Ƚ��
    float task3count = 0;    // �̼�3 Ƚ��

    public Image TaskBar;   // ���� ����ȭ
    public GameObject txtpref; // report ��ư �ؽ�Ʈ
    GameObject txtinst;    // ������ ��ư text

    GameObject[] crewtext;    // �ڵ����� ������ ũ�� �迭
    static public string impoText;    // impo id ���ڿ�
    static public List<string> textList = new List<string>(); // text ���ڿ��� ���� ����Ʈ

    static public List<bool> dieList = new List<bool>();
    static public int dienum = 5;   // ���� ũ�� �ѹ�
    static public List<float> cdist = new List<float>();    // �ڵ� ���� ũ����� �Ÿ�
    private void Start()// �����ϸ鼭
    {
        _audio = GetComponent<AudioSource>();   // ����� ã��
    }
    public void OnClickCompleteBtn()    // �Ϸ� ��ư�� ������
    {
        _audio.PlayOneShot(startsound, 1.0f);   // ���� �Ҹ� ���

        us.transform.position = new Vector3(0, 0, 0);   // �÷��̾� ��ġ�� 0,0,0

        GameObject.Find("Before").gameObject.SetActive(false);  // ������ ui ����
        GameObject.Find("US").transform.Find("Camera").gameObject.SetActive(true);  //  �÷��̾� ī�޶� �ѱ�
        GameObject.Find("Camera (1)").gameObject.SetActive(false);  // ��� ī�޶� ����


        GameObject.Find("Canvas").transform.Find("Taskgage").gameObject.SetActive(true);    // ���� �� ui ��Ű

        if (Random.Range(0, 2) == 0)    // 50:50�� Ȯ����
        {
            GameObject.Find("Canvas").transform.Find("Kill_btn").gameObject.SetActive(true);    // ų ��ư �ѱ�
            GameObject.Find("Canvas").transform.Find("im").gameObject.SetActive(true);  //��������ui �ѱ�

            us.tag = "PlayerImpo";  // �÷��̾ ��������
            Invoke("imDelay", 2.0f);    // 2���� ȣ��
        }
        else//�ƴϸ�
        {
            GameObject.Find("Canvas").transform.Find("TaskList").gameObject.SetActive(true);    // ���� ��� �ѱ�
            GameObject.Find("Canvas").transform.Find("Report_btn").gameObject.SetActive(true);  // ����Ʈ ��ư �ѱ�
            GameObject.Find("Canvas").transform.Find("crew").gameObject.SetActive(true);    // ũ�� ui �ѱ�

            us.tag = "Player";  // �÷��̾�� ũ���
            Invoke("crewDelay", 2.0f);  // 2���� ȣ��
        }
    }

    private void imDelay()  //���������϶�
    {
        GameObject.Find("im").gameObject.SetActive(false);  // �г� ����

        for (int i = 0; i < 4; i++) // ũ��� �� ��ŭ
        {
            Vector3 randomPosition = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));  // ���� ��ġ��
            Instantiate(crew, randomPosition, Quaternion.identity); // ũ�� 4�� ����
        }

        StartCoroutine(crewDistList()); // ũ����� �Ÿ� ui ����
    }
    private void crewDelay()    // ũ����϶�
    {
        GameObject.Find("crew").gameObject.SetActive(false);    // �г� ����

        Instantiate(impo);  // �������� �ϳ� ����

        for (int i = 0; i < 3; i++) // ũ��� ����ŭ
        {
            Vector3 randomPosition = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));  // ���� ��ġ��
            Instantiate(crew, randomPosition, Quaternion.identity); // ũ��� 3�� ����
        }
    }

    IEnumerator crewDistList()  // ũ����� �Ÿ� ���
    {
        while (true)    // ���
        {
            yield return new WaitForSeconds(1f);    //1�� ����

            if (crew != null)   // ũ����� ������
            {
                dieList = new List<bool>{    // ���� ���� ����Ʈ
            PlayerCtrl.crew[0].GetComponent<AIcrew>().state == AIcrew.State.DIE,    // ũ�簡 �׾����� true, ��������� false
            PlayerCtrl.crew[1].GetComponent<AIcrew>().state == AIcrew.State.DIE,    // ũ�簡 �׾����� true, ��������� false
            PlayerCtrl.crew[2].GetComponent<AIcrew>().state == AIcrew.State.DIE,    // ũ�簡 �׾����� true, ��������� false
            PlayerCtrl.crew[3].GetComponent<AIcrew>().state == AIcrew.State.DIE     // ũ�簡 �׾����� true, ��������� false
            };
            }

            if (dieList[0] == true) // 0��°�� ����
            {
                dienum = 0; // ���� ũ��� 0��
            }
            if (dieList[1] == true)  // 1��°�� ����
            {
                dienum = 1;  // ���� ũ��� 1��
            }
            if (dieList[2] == true) // 2��°�� ����
            {
                dienum = 2;  // ���� ũ��� 2��
            }
            if (dieList[3] == true) // 3��°�� ����
            {
                dienum = 3;  // ���� ũ��� 3��
            }

            yield return new WaitForSeconds(0.3f);  // 0.3�� ���
        }
    }



    public void OnClickMap()    // map ��ư Ŭ���ϸ�
    {
        map = GameObject.Find("Map_btn").transform.GetChild(0).gameObject;  // ��ư �Ʒ� �ڽ� ������Ʈ ã�Ƽ�
        if (map.activeSelf == false)    // ����������
        {
            map.SetActive(true);    // �Ѱ�
        }
        else//����������
        {
            map.SetActive(false);//����
        }
    }

    public void OnClickReport() // report ��ư ���� == �÷��̾�� ũ���
    {
        _audio.PlayOneShot(votesound, 1.0f);//report �Ҹ� ���

        report = GameObject.Find("Report_btn").transform.GetChild(0).gameObject;    //����Ʈui ã��

        impoText = GameObject.FindGameObjectWithTag("impoID").GetComponent<TextMesh>().text;   // impo id ���ڿ� ����
        crewtext = GameObject.FindGameObjectsWithTag("crewID");   // crew id ���ڿ� ����

        for (int j = 0; j < crewtext.Length; j++) // ũ��� ����ŭ
        {
            textList.Add(crewtext[j].GetComponent<TextMesh>().text);  // ���ڿ� list�� ũ�� id �߰�
        }

        textList.Add(impoText); // impo id ���ڿ� list�� �߰� ����

        if (report.activeSelf == false) //����Ʈui�� ����������
        {
            report.SetActive(true); // ui�ѱ�

            for (int i = 0; i < 4; i++) // ũ��� 3�� + �������� 1��
            {
                txtinst = Instantiate(txtpref, new Vector3(50, 0, 0), Quaternion.identity); // ���̵� �ؽ�Ʈ ����
                txtinst.transform.SetParent(GameObject.Find("Button (" + i + ")").transform, false);    // �θ�� ���� ������ ��ġ�� ��ư���� ����
            }
        }
    }

    public void AddTask1()  // ���� 1
    {
        if (task1count < 4) // ī��Ʈ 4�� �ƴϸ�
        {
            ++task1count; //ī��Ʈ ����
            SetTaskText();  // �� �� text ����
            FillTaskBar();  // �� �� �� �� ä���
        }
    }

    public void AddTask2()  // ���� 2
    {
        if (task2count < 4) // ī��Ʈ 4�� �ƴϸ�
        {
            ++task2count; //ī��Ʈ ����
            SetTaskText();  // �� �� text ����
            FillTaskBar();  // �� �� �� �� ä���
        }
    }

    public void AddTask3()  // ���� 3
    {
        if (task3count < 2) // ī��Ʈ 2�� �ƴϸ�
        {
            ++task3count; //ī��Ʈ ����
            SetTaskText();  // �� �� text ����
            FillTaskBar();  // �� �� �� �� ä���
        }
    }

    public void SetTaskText()   // ���� �ؽ�Ʈ ����
    {
        TaskText.text = "��̵� ���� �����ϱ�(" + task1count + "/4)\n������ ���� ������(" + task2count + "/4)\n¡�ۺ� ��ȣ �︮��(" + task3count + "/2)"; //ī��Ʈ ���
    }

    public void FillTaskBar()   // �� �� �� ä���
    {
        TaskBar.fillAmount += 0.1f; // ������ �� ������ 0.1��

        if (TaskBar.fillAmount >= 1.0f)    //1�� ������
        {
            if(us.tag == "Player")
                SceneManager.LoadScene("Victory");  // �¸�
            else if(us.tag == "PlayerImpo")
                SceneManager.LoadScene("Defeat");  // �й�
        }
    }

}
