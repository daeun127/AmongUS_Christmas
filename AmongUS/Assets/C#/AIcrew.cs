using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AIcrew : MonoBehaviour
{
	public enum State	// ����
	{
		PATROL,	// ����
		TRACE,	// ����
		DIE	// ����
	}

	AudioSource _audio;   // ����� �ҽ�
	public AudioClip reportsound; // ���� bgm

	public State state = State.PATROL;	// ���� ���� ����
	public float traceDist = 8.0f;	// 10f �̳� ����
	GameObject player;	// �÷��̾� ������Ʈ
	Transform playerTr;	// �÷��̾� ��ġ

	static public int dieCount = 0;   // crew ���� Ƚ��

	WaitForSeconds ws;	// ��� �ð�
	MoveAgent moveAgent;	// moveagent ��ũ��Ʈ�� ����
	Animation anim;     //Animation ����


	private void Awake()	// ���� ����
	{
		player = GameObject.Find("US");	// �÷��̾� ã��
		if (player != null)	// �÷��̾� ã����
		{
			playerTr = player.transform;	// �÷��̾� ��ġ ã�Ƽ� ���� �Ҵ�
		}
		
		moveAgent = GetComponent<MoveAgent>();	// moveagent ã��
		anim = GetComponent<Animation>(); // Animation ã��
		_audio = GetComponent<AudioSource>();	// ����� ã��

		ws = new WaitForSeconds(0.3f);	// 0.3�ʷ� ����
	}

	private void OnEnable()	// ��������� ȣ��
	{
		StartCoroutine(CheckState());	//���� üũ
		StartCoroutine(Action());	//�ൿ
	}

	IEnumerator CheckState()	//����üũ
	{
		while (state != State.DIE)	// ���� ���� �ƴ� ����
		{
			if (Vector3.Distance(playerTr.position, transform.position) <= traceDist)   // �Ÿ� �� ����
			{
				state = State.TRACE;    // ���� ����
			}
			else
			{
				state = State.PATROL;   // ����
			}

			if (player.tag == "PlayerImpo")	// �÷��̾ ���������϶�
			{
				if (GameManager.dienum < 5)
				{
					if (Vector3.Distance(PlayerCtrl.crew[GameManager.dienum].transform.position, transform.position) <= traceDist)  // ���� �ְ� ������ ������
					{
						state = State.TRACE;    // ���� ����
					}
				}
			}

			yield return ws;	// ���
		}
	}

	IEnumerator Action()	// �ൿ
	{
		while (true)	// ���
		{
			yield return ws;	// ���

			switch (state) //���º���
			{
				case State.PATROL:	// ���� ����
					moveAgent.SetPatrolling(true);	//���� ok
					anim.CrossFade("RUN", 1f);  // �̵� �ִϸ��̼�
					break;	// ��

				case State.TRACE:	// ���� ����
					anim.CrossFade("RUN", 1f);  // �̵� �ִϸ��̼�
					if(GameManager.dienum < 5)	// ���� ũ�簡 �ְ�
                    {
						if (Vector3.Distance(PlayerCtrl.crew[GameManager.dienum].transform.position, transform.position) <= 20f)  // ���� �ְ� ������ ������
						{
							moveAgent.SetTraceTarget(PlayerCtrl.crew[GameManager.dienum].transform.position); // ������ ����
						}
					}
					else//�ƴϸ�
                    {
						moveAgent.SetTraceTarget(playerTr.position); // �÷��̾� ����
                    }
					break; // ��

				case State.DIE: // ���� ����
					anim.CrossFade("IDLE", 1f);  // idle �ִϸ��̼�
					moveAgent.Stop(); // ���߱�
					OnPlayerDie();	// �׾��� �� �Լ� ȣ��
					break;	// �ݺ��� ��
			}
		}
	}

	public void OnPlayerDie()	// �׾��� ��
	{
		++dieCount; // ���� ũ�� �� ����
		StopAllCoroutines();	//��� �ڷ�ƾ ���߱�

		if (dieCount >= 3 && player.tag == "Player")	// 3���̻� �װ� �÷��̾ ũ��
			SceneManager.LoadScene("Defeat");	// ����
		else if (dieCount >= 3 && player.tag == "PlayerImpo")	// 3���̻� �װ� �÷��̾ ���������� ��
			SceneManager.LoadScene("Victory");	// �¸�
	}

	private void OnTriggerEnter(Collider other)	// �浹 ��
    {
		if(player.tag == "PlayerImpo" && other.tag == "crew")	// �÷��̾ ���������̸鼭 �浹�� ������Ʈ�� ũ���� ��
        {
			if (other.GetComponent<AIcrew>().state == State.DIE)	// �浹�� ũ�簡 �׾��� ��
            {
				GameObject.Find("Canvas").transform.Find("Report_ui").gameObject.SetActive(true);	// ��ü ����Ʈ
				_audio.PlayOneShot(reportsound, 1.0f);  // ����Ʈ �Ҹ� ���
				Invoke("ReportSc", 2.0f);	// 2�� �Ŀ� ȣ��
            }
		}
	}

	void ReportSc()
    {
		SceneManager.LoadScene("Report"); // �� ��ȯ
	}
}
