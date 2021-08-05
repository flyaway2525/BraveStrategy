using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : Player_Status
{
    public override void Start() {
        base.Start();
        _targetController.Player_Script = this;//ƒ^ƒO•t‚¯‚µ‚ÄˆêŠ‡‚ÅTC‚É‘—‚è‚½‚¢‚Ë
    }
    public override void passive_First() {
        base.passive_First();
        status.maxhp_add += 50;
        //Debug.Log(gameObject + "maxHP " + 50 + " UP");
        UIGenerator.instance.AddScrollText(gameObject + "maxHP" + 50 + "UP");
    }
    public override void OnTurn() {
        Debug.Log(this.gameObject.name + " : your turn!");
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject _enemy in _enemies) {
            _enemy.GetComponent<Enemy_Status>().status.hp -= status.atk;
            Debug.Log(this.gameObject.name + " ATKed " + status.atk + "  HP : " + status.hp + " / " + status.maxhp);
        }
        //status.hp += 1;
    }
}
