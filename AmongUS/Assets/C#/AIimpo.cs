using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AIimpo : MonoBehaviour
{
    AudioSource _audio;   // ����� �ҽ�
    public AudioClip killsound; // ���� bgm

    public enum State       // ����
    {
        PATROL, // ����
        TRACE,  // ����
        KILL    // ����
    }   

    public State state = State.PATROL;  // �⺻�� ����
    public float traceDist = 5.0f; // ���� ���� �Ÿ�
    Transform playerTr; // �÷��̾���ġ


    GameObject[] crew;  // ũ��� ���� �迭
    List<float> allDist = new List<float>(4) { 0, 0, 0, 0 };    // ũ����� �Ÿ� ����Ʈ
    public int crewnum; // ���� ������ �ִ� crew ��ȣ ���� ����

    public int count = 0;  // ī��Ʈ 0

    WaitForSeconds ws;  // ��� �ð� ����
    MoveAgent moveAgent;  // moveagent ��ũ��Ʈ�� ����
    Animation anim;     //Animation ����

    private void Awake()    // ���� ����
    {
        _audio = GetComponent<AudioSource>();   // ����� ã��

        GameObject player = GameObject.Find("US");
        if (player != null) // �÷��̾� ã����
        {
            playerTr = player.transform;    // �÷��̾� ��ġ ã�Ƽ� ���� �Ҵ�
        }
        moveAgent = GetComponent<MoveAgent>();	// moveagent ã��
        anim = GetComponent<Animation>(); // Animation ������ �Ҵ�
        
        ws = new WaitForSeconds(0.3f);	// 0.3�ʷ� ����
    }

    private void OnEnable() // ��������� ȣ��
    {
        StartCoroutine(CheckState());   //���� üũ
        StartCoroutine(Action());   //�ൿ
    }

    IEnumerator CheckState()   //���� üũ
    {
        while (true) // ���
        {
            crew = GameObject.FindGameObjectsWithTag("crew"); // ���̾��Ű â���� ũ��� ã�Ƽ� �迭�� ����
            if (crew != null)   // ũ�簡 ������
            {
                allDist[0] = Vector3.Distance(playerTr.position, transform.position);   // �迭 0��°�� �÷��̾�� ���������� �Ÿ� ��

                for (int i = 1; i <= crew.Length; i++)  // ũ�� ���� ��ŭ
                {
                    allDist[i] = Vector3.Distance(crew[i - 1].transform.position, transform.position);  // �迭�� ����
                }
                crewnum = allDist.IndexOf(allDist.Min());   // �������Ϳ� ���� �������ִ� ũ���� �迭 ��ȣ  
            }

            if (allDist.Min() <= traceDist) // ���� ������ �ִ� ũ����� �Ÿ��� ���� ���� �Ÿ� ���� �� 
                state = State.TRACE;    // ����
            else// ���̸�
                state = State.PATROL;   // ����


            yield return ws;    // ���
        }
    }
    private void OnTriggerStay(Collider other) // �ε��� ����
    {
        if (other.tag == "Player")  // �÷��̾� ũ��� �ε���
        {
            count = 0;  // ī��Ʈ 0
            for (int i = 1; i < allDist.Count; i++) // ũ��Ÿ� ���� ����Ʈ ��������
            {
                if (allDist[i] > traceDist)    // �ٸ� ũ����� ���� �Ÿ� ���� ��
                {
                    ++count;    // ī��Ʈ ����

                    if (count >= (crew.Length)-1) // �� ���̸�
                    {
                        _audio.PlayOneShot(killsound, 1.0f);    // �״� �Ҹ� ���
                        Invoke("Defeat", 1.5f); // 1.5�� �� �� ��ȯ
                    }
                    else// �ƴϸ�
                    {
                        state = State.PATROL;   // ���� ���� ��ȯ
                        break; // �ݺ� ����
                    }
                }
            }
            state = State.PATROL;   // ���� ���� ��ȯ

        }
        else if (other.tag == "crew")   // �ε��� �� ũ���� ��
        {
            count = 0;  // ī��Ʈ 0
            for (int i = 1; i < allDist.Count; i++)  // ũ��Ÿ� ���� ����Ʈ �������� 
            {
                if (crewnum == i) // ���� ũ�� �ѹ��� ����� ���� �ѹ��� ������
                    continue;   // �ǳʶٰ�
                if (allDist[i] > traceDist) // ������ ũ����� ���� �Ÿ� ���� ��
                {
                    ++count;    // ī��Ʈ ����
                }
            }

            if (count >= (crew.Length) - 1) // ���� ���� ���� ũ�簡 ���� �Ÿ� ���̸�
            {
                anim.CrossFade("IDLE", 1f);  // �̵� �ִϸ��̼�
                crew[crewnum - 1].GetComponent<AIcrew>().state = AIcrew.State.DIE;  // ����
                crew[crewnum - 1].tag = "notme";    //  ���� ��󿡼� ����
                _audio.PlayOneShot(killsound, 1.0f);    // �״� �Ҹ� ���
            }
            else//�ƴϸ�
            {
                state = State.PATROL;   // ���� ���� ��ȯ
            }
        }
    }

    IEnumerator Action() // �ൿ
    {
        while (true)    // ���
        {
            yield return ws;    // ���

            switch (state)  // ���º���
            {
                case State.PATROL:  // ����
                    moveAgent.SetPatrolling(true);  // ���� ok
                    anim.CrossFade("RUN", 1f);  // �̵� �ִϸ��̼�
                    break;  // ��

                case State.TRACE:   // ����
                    anim.CrossFade("RUN", 1f);  // �̵� �ִϸ��̼�
                    if (crewnum != 0 && crew[crewnum - 1] != null)   // ũ�簡 �÷��̾ �ƴϰ� �ױ����̸�
                    {
                        moveAgent.SetTraceTarget(crew[crewnum - 1].transform.position);   // �ش� ũ�� ����
                    }
                    else if (crewnum == 0) //�÷��̾��̸�
                        moveAgent.SetTraceTarget(playerTr.position);    // �÷��̾� ����
                    else// �ƴϸ�
                        state = State.PATROL;   // ���� ���� ��ȯ
                    break;  // ��
            }
        }
    }

    void Defeat()   // �� ��ȯ
    {
        SceneManager.LoadScene("Defeat"); //�� ��ȯ
    }
}
