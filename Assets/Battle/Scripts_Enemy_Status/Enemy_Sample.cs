using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sample : Enemy_Status
{
    public override void Start() {
        base.Start();
        //_targetController.Enemy_Object = this.gameObject;//タグ付けして一括でTCに送りたいね
        _targetController.Enemy_Script = this;//タグ付けして一括でTCに送りたいね     
    }
    public override IEnumerator FastSkill() {
        base.FastSkill();
        if (true) {//プレイヤーに毒を与える
            foreach(Player_Status _player_Status in _targetController.Player_Scripts) {
                _player_Status.gameObject.AddComponent<Poison>();
                Debug.Log(_player_Status + " was get poison!");
            }
        }
        yield return null;
    }
    public override IEnumerator OnTurn() {
        Debug.Log(gameObject + " : Turn");
        foreach(Player_Status player_Status in _targetController.Player_Scripts) {
            player_Status.status.hp -= 100;
        }
        Material material = this.gameObject.GetComponent<Renderer>().material;
        Color c = material.color;
        material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        material.color = Color.green;
        yield return new WaitForSeconds(0.4f);
        material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        material.color = Color.green;
        yield return new WaitForSeconds(0.4f);
        material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        material.color = Color.green;
        yield return new WaitForSeconds(0.4f);
        material.color = c;
        StartCoroutine("Atack");
        Debug.Log(gameObject + " : Turn over");
        /*
        Debug.Log(this.gameObject.name + " : your turn!");
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject _player in _players) {
            _player.GetComponent<Player_Status>().status.hp -= status.atk;
            Debug.Log(this.gameObject.name + " ATKed " + status.atk + "  HP : " + status.hp + " / " + status.maxhp);
        }
        //status.hp += 1;
        */
    }
    public override IEnumerator EndSkill() {
        return base.EndSkill();
    }
    private IEnumerator Atack() {
        GameObject bullet = Instantiate(enemyBullets.bullet);
        bullet.transform.position = this.transform.position;
        bullet.transform.position += Vector3.down;
        yield return new WaitForSeconds(0.3f);
        GameObject bullet2 = Instantiate(enemyBullets.bullet);
        bullet2.transform.position = this.transform.position;
        bullet2.transform.position += Vector3.down * 2;
        yield return new WaitForSeconds(0.3f);
        GameObject bullet3 = Instantiate(enemyBullets.bullet);
        bullet3.transform.position = this.transform.position;
        bullet3.transform.position += Vector3.down * 3;
        yield return new WaitForSeconds(0.3f);
        GameObject bullet4 = Instantiate(enemyBullets.bullet);
        bullet4.transform.position = this.transform.position;
        bullet4.transform.position += Vector3.down * 4;

    }
}
