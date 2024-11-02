using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollLetter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // 부딪히면
    {
        if (other.tag == "crew" || other.tag == "Player")   // 부딪힌 게 태그가 크루거나 플레이어가 크루일 때
        {
            transform.Translate(new Vector3(0, 0, -700 * Time.deltaTime), Space.World); // 이동
            Destroy(this.gameObject, 3.0f); // 3초후 사라짐
            GameObject.Find("GameManager").GetComponent<GameManager>().AddTask2();  // 미션2 카운트
        }
    }
}
