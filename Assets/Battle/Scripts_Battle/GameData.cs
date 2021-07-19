using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    [SerializeField] private TargetController _targetController;
    [SerializeField] Ground_Controller _ground_Controller;
    private bool TF = true;
    public bool canMove = false;
    public bool player_Turn = false;
    public bool enemy_Turn = false;
    public void Start() {
        Debug.Log("赤がプレイヤー、緑がエネミー、青がグランド");
        Debug.Log("赤を動かしてグランドを移動するゲーム");

    }
    public void Update() {    //全てのStartが終わってUpdateのタイミングになったらcombatを開始する
        if (TF) {
            TF = false;
            PreAction();
            combat();
        }
    }
    public void PreAction() {
        //Passiveのルール
        /***
         * passive_First        : キャラクタのステータスを底上げする効果(HP+100とか)
         * passice_Second    : キャラクタのステータス倍率の変化   
         * passice_Third       : キャラクタのステータス倍率反映          
         * ***/
        //TargetControllerの初期化 : _allGameObjectsに全てのCharaとItemが存在する必要がある(要修正) 
        //パッシブスキルを発動させる(これはパッシブが変化するたびに行われると良い)
        List<All_Status> _allStatus = new List<All_Status>();
        _allStatus = _targetController.Initialize_TargetController();
        foreach (All_Status _script in _allStatus) { _script.passive_First(); }
        foreach (All_Status _script in _allStatus) { _script.passive_Second(); }
        foreach (All_Status _script in _allStatus) { _script.passive_Third(); }
    }
    public void combat() {
        CanMove(false);
        _ground_Controller.turnPlayer = false;
        StartCoroutine("Enemy_FastSkill");
        _ground_Controller.turnPlayer = true;
        CanMove(true);
        //playerTrunが始まる      条件　Playerを触ったら

    }
    public IEnumerator Player_Turn() {
        if (canMove) {
            Debug.Log("3");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("2");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("1");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("0");
            player_Turn = false;
            //_ground_Controller.Selected_Ground.OnPointerUp();
            _ground_Controller.turnPlayer = false;
            Debug.Log("Enemy_Trun");
            StartCoroutine("Enemy_Trun");
        }
    }
    public IEnumerator Enemy_Trun() {
        foreach(Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.OnTurn();
            StartCoroutine(c);
        }
        StartCoroutine("Enemy_EndSkill");
        yield return null;
    }
    public IEnumerator Enemy_FastSkill() {
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.FastSkill();
            StartCoroutine(c);
        }
        yield return null;
    }
    public IEnumerator Enemy_EndSkill() {    
        CanMove(false);
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.EndSkill();
            StartCoroutine(c);
        }
        CanMove(true);
        yield return null;
    }

    public void CanMove(bool TF) {
        _ground_Controller.touchOffCollider.enabled = !TF;
        if (!TF && _ground_Controller.Selected_Ground) {
            _ground_Controller.Selected_Ground.OnPointerUp();//強制的にタッチを解除
        }
        canMove = TF;
    }
}
