using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//以下の記事(コルーチンの結果を受け取る)
//https://tsubakit1.hateblo.jp/entry/2015/04/06/060608
public class GameData : MonoBehaviour
{
    public enum TURN {
        STANDBY_PHASE,//ゲーム毎に初めに実行される
        FAST_PHASE,//FastSkill()のフェイズ、ターンの初めに1回実行される
        PLAYER_PHASE,//プレイヤーのターン
        ENEMY_PHASE,//敵のターン
        END_PHASE,//EndSkill()のフェイズ、ターンの終わりに1回実行される
        CLEAR_PHASE,//ゲームのクリア時
        GAMEOVER_PHASE//ゲーム失敗時
            //面クリアのフェイズも必要だが、とりあえず未実装
    }
    public TURN turn = TURN.STANDBY_PHASE;
    [SerializeField] private TargetController _targetController;
    [SerializeField] Ground_Controller _ground_Controller;
    private bool TF = true;
    public bool canMove = false;
    public bool player_Turn = false;
    public bool enemy_Turn = false;
    private bool _activeCoroutine = false;
    public void Start() {
        UIGenerator.instance.AddScrollText("スクロールに追加したよ");
        UIGenerator.instance.AddInfomationText("1ターン目とか出すよ");
        Debug.Log("赤がプレイヤー、緑がエネミー、青がグランド");
        Debug.Log("赤を動かしてグランドを移動するゲーム");

    }
    public void Update() {    //全てのStartが終わってUpdateのタイミングになったらcombatを開始する
        switch (turn) {
            case TURN.STANDBY_PHASE:
                if (canMove) { CanMove(false); }
                PreAction();
                turn = TURN.PLAYER_PHASE;
                    break;
            case TURN.FAST_PHASE:
                if (canMove) { CanMove(false); }
                UIGenerator.instance.AddScrollText("Fast_Phase");
                Coroutine coroutine = StartCoroutine(Enemy_FastSkill());   //ほんとは味方のFastSkillも必要
                if (!_activeCoroutine) {
                    UIGenerator.instance.AddScrollText("Fast_Phase_FIN");
                    turn = TURN.PLAYER_PHASE;
                }
                break;
            case TURN.PLAYER_PHASE:
                if (!canMove) { CanMove(true); }
                //Player_Turn()の実行の待機
                break;
            case TURN.ENEMY_PHASE:
                break;
            case TURN.END_PHASE:
                break;
            case TURN.CLEAR_PHASE:
                break;
            case TURN.GAMEOVER_PHASE:
                break;
            default:
                Debug.LogError("TURNが無効な値です");
                break;
        }
        if (TF) {
            TF = false;
            PreAction();
            combat();
        }
    }
    public void PreAction() {
        if (_activeCoroutine) { Debug.LogError("コルーチンの終了を待たずに次のコルーチンを動かしています"); }
        _activeCoroutine = true;
        UIGenerator.instance.AddInfomationText("PreAction");
        UIGenerator.instance.AddScrollText("PreAction実行");
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
        _activeCoroutine = false;
    }
    public void combat() {
        CanMove(false);
        _ground_Controller.turnPlayer = false;
        UIGenerator.instance.AddScrollText("戦闘開始");
        StartCoroutine("Enemy_FastSkill");
        _ground_Controller.turnPlayer = true;
        CanMove(true);
        //playerTrunが始まる      条件　Playerを触ったら

    }
    public IEnumerator Player_Turn() {
        if (_activeCoroutine) { Debug.LogError("コルーチンの終了を待たずに次のコルーチンを動かしています"); }
        _activeCoroutine = true;
        if (canMove) {
            Debug.Log("3");
            UIGenerator.instance.AddScrollText("敵行動まで3秒");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("2");
            UIGenerator.instance.AddScrollText("敵行動まで2秒");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("1");
            UIGenerator.instance.AddScrollText("敵行動まで1秒");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("0");
            UIGenerator.instance.AddScrollText("敵行動開始");
            _activeCoroutine = false;
        }
        turn = TURN.ENEMY_PHASE;
    }
    public IEnumerator Enemy_Trun() {
        if (_activeCoroutine) { Debug.LogError("コルーチンの終了を待たずに次のコルーチンを動かしています"); }
        _activeCoroutine = true;
        player_Turn = false;
        _ground_Controller.turnPlayer = false;
        Debug.Log("Enemy_Trun");
        UIGenerator.instance.AddScrollText("敵のターン開始");

        List<Coroutine> cList = new List<Coroutine>();
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator ie = enemy_script.OnTurn();
            cList.Add(StartCoroutine(ie));
        }
        foreach(Coroutine c in cList) {yield return c;}

        Debug.Log("ここにプレイヤーの移動を待つ処理を入れる必要がある");
        UIGenerator.instance.AddInfomationText("ターン終了/nエンドフェイズに移行");
        StartCoroutine("Enemy_EndSkill");
        _activeCoroutine = false;
        yield return null;
    }
    public IEnumerator Enemy_FastSkill() {
        List<Coroutine> cList = new List<Coroutine>();
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator ie = enemy_script.FastSkill();
            cList.Add(StartCoroutine(ie));
        }
        foreach (Coroutine c in cList) { yield return c; }
        yield return null;
    }
    public IEnumerator Enemy_EndSkill() {
        List<Coroutine> cList = new List<Coroutine>();
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator ie = enemy_script.EndSkill();
            cList.Add(StartCoroutine(ie));
        }
        foreach (Coroutine c in cList) { yield return c; }
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
