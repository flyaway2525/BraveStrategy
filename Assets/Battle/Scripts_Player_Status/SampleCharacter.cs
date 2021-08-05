using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCharacter :Player_Status
{
    public override void Start() {
        base.Start();
        _targetController.Player_Script = this;
    }
    public override void passive_Second() {
        base.passive_Second();
        foreach (Player_Status player_Status in _targetController.Player_Scripts) {//Player��Human�S�Ă�HP��2�{�ɂ���p�b�V�u�X�L��
            if (player_Status.status.type == "Human") {
                player_Status.status.maxhp_rate = 2;
                Debug.Log("Human �� HP2�{" + gameObject);
                UIGenerator.instance.AddScrollText("Human��HP2�{" + gameObject);
                UIGenerator.instance.GeneratPopupUI(gameObject.transform, "Human��HP2�{");
                var parent = this.transform;
                //GameObject info = Instantiate(showInfo.text_ShowInfo,Vector3.zero, Quaternion.identity, parent);
                //info.transform.position = parent.transform.position;
            }
        }

    }
    public override void OnTurn() {
        base.OnTurn();
        Debug.Log(this.gameObject.name + " : your turn!");
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject _enemy in _enemies) {
            _enemy.GetComponent<Player_Status>().status.hp -= status.atk;
            Debug.Log(this.gameObject.name + " ATKed " + status.atk + "  HP : " + status.hp + " / " + status.maxhp);
        }
    }
}
