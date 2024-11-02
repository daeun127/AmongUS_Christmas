using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    AudioSource _audio;   // ����� �ҽ�
    public AudioClip killsound; // ���̴� bgm

    public float moveSpeed = 10.0f;     // �̵� �ӵ� ����
    public float rotSpeed = 80.0f;      // ȸ�� �ӵ� ����
    Animation anim;     //Animation ����

    public float killDist = 5.0f;   // ���� �Ÿ�

    public List<float> allDist = new List<float>(); // �Ÿ����� ���� ����Ʈ

    public static GameObject[] crew;    // crew ������Ʈ �迭
    public Button killBtn;  // ���� ��ư
    public int crewnum; // ���� ������ �ִ� crew ��ȣ ���� ����

    private void Start()    // �����ϸ�
    {
        _audio = GetComponent<AudioSource>();   // ����� ã��

        anim = GetComponent<Animation>(); // Animation ������ �Ҵ�
    }

    void Update()   // ���
    {
        float h = Input.GetAxis("Horizontal"); // �¿� �̵�
        float v = Input.GetAxis("Vertical"); // ���� �̵�
        float r = Input.GetAxis("Mouse X"); //ȸ��

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);  // �����¿� �̵� ���� ���� ���
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);   // movespeed �ӵ��� �̵�
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);       // rotspeed �ӵ��� ȸ��

        if (v >= 0.1f || v <= -0.1f || h >= 0.1f || h <= -0.1f) // ���� �¿� �̵��� ��
        {
            anim.CrossFade("RUN", 1f);  // �̵� �ִϸ��̼�
        }
        else
        {
            anim.CrossFade("IDLE", 1f); // ���� �ִϸ��̼�
        }
    }

    private void OnEnable() // ��������� ȣ��
    {
        StartCoroutine(CheckState());   // ���� üũ
    }

    IEnumerator CheckState()    // ���� üũ
    {
        while (true)    // ���
        {
            yield return new WaitForSeconds(1f);    // 1�� ���

            if (this.tag == "PlayerImpo")   // �÷��̾ �������͸�
            {
                crew = GameObject.FindGameObjectsWithTag("crew");   // ũ����� ã�Ƽ�

                for (int i = 0; i < crew.Length; i++)   // ������ŭ
                {
                    allDist[i] = Vector3.Distance(crew[i].transform.position, transform.position);  // �Ÿ� ���
                }
            }
            yield return new WaitForSeconds(0.3f);  // ���
        }

    }
    public void OnClickKillBtn() // �׿��� ��
    {
        crewnum = allDist.IndexOf(allDist.Min());   // ���� ������ �ִ� ũ���� �ѹ� ����

        if (crew[crewnum].GetComponent<AIcrew>().state != AIcrew.State.DIE) // ���� ������ �ִ� ũ�簡 ���׾�����
        {
            for (int i = 0; i < allDist.Count; i++)  // ũ�� ������ŭ
            {
                if (crewnum == i)   // ���� ���� ũ���
                    continue;   // �Ѿ��
                if (allDist[i] <= 15f)  // �ٸ� ũ����� ���� �Ÿ� ���̸�
                {
                    SceneManager.LoadScene("Defeat");   // ����
                }
            }

            _audio.PlayOneShot(killsound, 1.0f);    // ����
            crew[crewnum].GetComponent<AIcrew>().state = AIcrew.State.DIE;  // ���� ���·� �ٲ�
        }

    }
}
