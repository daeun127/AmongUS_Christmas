using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollLetter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // �ε�����
    {
        if (other.tag == "crew" || other.tag == "Player")   // �ε��� �� �±װ� ũ��ų� �÷��̾ ũ���� ��
        {
            transform.Translate(new Vector3(0, 0, -700 * Time.deltaTime), Space.World); // �̵�
            Destroy(this.gameObject, 3.0f); // 3���� �����
            GameObject.Find("GameManager").GetComponent<GameManager>().AddTask2();  // �̼�2 ī��Ʈ
        }
    }
}
