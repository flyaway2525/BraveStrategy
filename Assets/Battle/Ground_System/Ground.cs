using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Ground_Controller ground_Controller;        // groundController            ����߂����
    public Collider ground_Collider;
    public Renderer ground_Renderer;
    //public Transform transform;
    public Ground position_Ground;
    public GameData gameData; //���ݏ���Ă���GameData_Object
    //public GameObject
    public virtual void Start() {
        //transform = this.gameObject.GetComponent<Transform>();
        ground_Controller = transform.parent.parent.gameObject.GetComponent<Ground_Controller>();
        gameData = transform.parent.parent.gameObject.GetComponent<GameData>();
        ground_Collider = GetComponent<Collider>();
        ground_Renderer = GetComponent<Renderer>();


    }
    public void SetPos(Ground _normal_Ground) {
        Vector3 pos = _normal_Ground.transform.position;
        pos.z = 0;
        //Transform.position = pos;  //�ړ��̃R�[�h��g�ނ̂ň�UOFF
    }
}
