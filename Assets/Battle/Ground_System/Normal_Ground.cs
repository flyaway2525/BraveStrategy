using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // using��ǉ�
using UnityEngine;
public class Normal_Ground : Ground, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public override void Start() {
        base.Start();
        ground_Controller.Normal_Ground = this;  //�㏑�����Ă�悤�Ɍ�����̂�Add���\�b�h��ǉ�����

        ground_Controller.SetNormal_Ground(this);
        //Ground on_Ground = null;
        //gameObject.GetComponent<Renderer>().material.color = Color.green;

        //
    }
    public void OnPointerEnter(PointerEventData eventData) {
        ground_Controller.Selected_Ground.position_Ground = this;
        //Ground_Controller.Selected_Ground.lastGround.Collider.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData) {
        ground_Controller.Selected_Ground.position_Ground = null;
    }              
    public void OnPointerUp(PointerEventData eventData) {
        ground_Controller.Selected_Ground.position_Ground = null;
    }
}
