using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour{
    public void MoveLeft() {
        //Vector3��1.0f�ňړ�����B
        this.transform.DOMove(new Vector3(-3.0f, 0.0f, 0.0f), 1.0f);
    }
    public void MoveRight() {
        this.transform.DOMove(new Vector3(3.0f, 0.0f, 0.0f), 1.0f);
    }
}
