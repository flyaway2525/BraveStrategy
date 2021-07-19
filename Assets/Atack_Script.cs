using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack_Script : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _moveTo;
    private float _DEX = 3.0f;
    void Start() {
        _transform = gameObject.GetComponent<Transform>();
        _moveTo = transform.position;
        Vector3 pos = transform.position;
        pos.z -= 1.0f;
        transform.position = pos;
        _moveTo.z += 1.0f;
    }
    void Update() {
        float distance = Vector3.Distance(transform.position, _moveTo);
        if (0.01f < distance) {
            transform.position = Vector3.MoveTowards(transform.position, _moveTo, _DEX * Time.deltaTime * 0.5f);
        } else {
            gameObject.transform.position = _moveTo;
            Destroy(gameObject, 0.1f);//0.1s後に消える
        }
    }
    private void OnTriggerEnter(Collider _collider) {
        if (_collider.gameObject.tag == "Player") {
            Debug.Log("攻撃判定");
            //_collider.gameObject.GetComponent<BasicStatus>().hp -= 100;

        }
    }
}
