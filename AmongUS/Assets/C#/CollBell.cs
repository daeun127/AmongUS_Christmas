using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollBell : MonoBehaviour
{
    AudioSource _audio;   // ����� �ҽ�
    public AudioClip bellsound; // ���� bgm

    public GameObject bell; // �� ������Ʈ
    public float angle = 20f;   // ����

    private float lerpTime = 0; // sin �Լ��� ����
    private float speed = 3f;   // �ӵ�

    private void Start()    // �����ϸ�
    {
        _audio = GetComponent<AudioSource>();   // ����� ��������
    }
    private void OnTriggerStay(Collider other)  // �ε����� ���� ��
    {
        if (other.tag == "crew" || other.tag == "Player")  // �ε��� �� �±װ� ũ��ų� �÷��̾ ũ���� ��
        {
            if(_audio.isPlaying == false)
                _audio.PlayOneShot(bellsound, 1f);    // �Ҹ� ���
            lerpTime += Time.deltaTime * speed; // �ӵ� * �ð� ��ŭ ���
            bell.transform.rotation = Rotate(); // ȸ��
            GameObject.Find("GameManager").GetComponent<GameManager>().AddTask3();  // �̼�3 ī��Ʈ
        }
    }

    private void OnTriggerExit(Collider other)  // ����������
    {
        _audio.Stop(); // �Ҹ� ����
    }

    Quaternion Rotate() // ȸ��
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),Quaternion.Euler(Vector3.back * angle), GetLerpTParam()); //���� ������ ������ ���� ���� ��� �Ͽ� �ε巴�� ȸ���ϵ���
    }

    float GetLerpTParam()   // 
    {
        return (Mathf.Sin(lerpTime)+1) * 0.5f;  // lerpTime �ð� +1 ��ŭ ���� �ݺ� �
    }

}
