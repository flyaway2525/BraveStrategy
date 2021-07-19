using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/***
 *  ターゲットコントローラーの使い方 
 *  GameDataに1つ作成されるTargetControllerから参照すること
 *  プロパティを用いて様々な種類のゲームオブジェクトを参照することができる
 *  参照したいゲームオブジェクトは_allObjectsに自動で入る
 * _allObjectsは他リストにオブジェクトがあることを保証しない
 * 単数形で用いれば<GameObject>
 * でsetのみ可能
 * 複数形で用いればLsit<GameObject>
 * でset,getが可能
 * 現在remove機能は実装されてない。
 * 
 * UPDATE : Passiveを発動するしないのGameObjectは要らないと思われる。
 * 範囲をALLOBJECTSにした場合、結局全部のObjectにPassive_1~3が入るから。
 * であるならばTCは効果発動の”対象物”に絞るべき、
 * 例:毒を受ける可能性のある味方キャラクタ
 * Playerに属する && 毒状態でない(要検討) && !毒耐性を持っている
 * _Players               _Poison                        _Resist_Poison
 * のような形が望ましい。
 * 
 * 毒耐性スキルを封印された場合、remove機能を使い、封印解除時にStartにあるようにTCに登録しなおす。(専用の関数を作るべき)
 * 
 * 
***/
public class TargetController : MonoBehaviour
{     [SerializeField] List<Player_Status> _player_Scripts = new List<Player_Status>();    //全てのPlayerオブジェクトを入れる(Player(Player_Status)を入れる方向にシフトする)
        [SerializeField] List<Enemy_Status> _enemy_Scripts = new List<Enemy_Status>();  //全てのEnemyオブジェクトを入れる(Player(Enemy_Status)を入れる方向にシフトする)
        [SerializeField] List<Item_Status> _item_Scripts = new List<Item_Status>();      //全てのItemsオブジェクトを入れる(Player(Item_Status)を入れる方向にシフトする)
        [SerializeField] List<Buff_Status> _buff_Scripts = new List<Buff_Status>();        //全てのBuffsオブジェクトを入れる(Player(Buff_Status)を入れる方向にシフトする)
    //_player_Scripts
    private void setLists<T>(List<T> gameObjects, List<T> _gameObjects) {
        foreach (T gameObject in gameObjects) {
            setList(gameObject, _gameObjects);
        }
    }
    private void setList<T>(T gameObject, List<T> _gameObjects) {
        _gameObjects.Add(gameObject);
        //Debug.Log(gameObject + " : was added to : " + _gameObjects);
    }
    public List<Player_Status> Player_Scripts { set { setLists(value, _player_Scripts); } get { return _player_Scripts; } }
    public Player_Status Player_Script { set { setList(value, _player_Scripts); } }
    public List<Enemy_Status> Enemy_Scripts { set { setLists(value, _enemy_Scripts); } get { return _enemy_Scripts; } }
    public Enemy_Status Enemy_Script { set { setList(value, _enemy_Scripts); } }
    public List<Item_Status> Item_Scripts { set { setLists(value, _item_Scripts); } get { return _item_Scripts; } }
    public Item_Status Item_Script { set { setList(value, _item_Scripts); } }
    public List<Buff_Status> Buff_Scripts { set { setLists<Buff_Status>(value, _buff_Scripts); } get { return _buff_Scripts; } }
    public Buff_Status Buff_Script { set { setList<Buff_Status>(value, _buff_Scripts); } }

    public List<All_Status> Initialize_TargetController() {//gameObject のリストを入れてScriptのリストを返す
        List<All_Status> allScripts = new List<All_Status>();
        foreach (All_Status _allScripts in _player_Scripts) { allScripts.Add(_allScripts); }
        foreach (All_Status _allScripts in _enemy_Scripts) { allScripts.Add(_allScripts); }
        foreach (All_Status _allScripts in _item_Scripts) { allScripts.Add(_allScripts); }
        foreach (All_Status _allScripts in _buff_Scripts) { allScripts.Add(_allScripts); }
        return allScripts;
    }
}







/*   　過去の遺物 : 全てのGameObjectを入れる箱を作ってみた件
 *   　全部のゲームオブジェクトを対象にした箱は作れた。これによって全てのオブジェクトからパッシブを発動させることは可能になったと思われたが
 *   　プレイヤーにバフが付与されているなどした場合、プレイヤーのオブジェクトとバフの対象オブジェクトがプレイヤーになるため、失敗だった。
 *   　バフのオブジェクトを作成してプレイヤーにアタッチすることも考えたが、バフは装備と違ってゲーム中についたり消えたりする。
 *   　プレイヤーの直下には装備品などのステータスを配置したいので紛らわしくないように全てスクリプトで管理したい。
 *   　バフオブジェクトを生成する場合、プレハブをインスタンス化しようと思う
 *   　
 *   　_allScriptsでＡＬＬＯＢＪＥＣＴを全部格納することも考えたが、違うクラス(Buff_StatusとChracter_Statusなど)をList化するのは不可能
 *   　というか余計にみずらくなるので
 *   　それぞれのクラスでリストを作ることにした。
 *   　以上
 *   　
 *   　            
    //[SerializeField] List<GameObject> _allObjects = new List<GameObject>();//全てのゲームオブジェクトを入れる(これは使わない方向にもっていくつもり)
    //    [SerializeField] List<ALLOBJECTS> _allScripts = new List<ALLOBJECTS>();//全ての対象をとるスクリプトを入れる(Player(Character_Status),Enemy(Character_Status),Buff,Item_Status) 
    //[SerializeField] List<GameObject> _player_Objects = new List<GameObject>();    //全てのPlayerオブジェクトを入れる(Player(Player_Status)を入れる方向にシフトする)
    //[SerializeField] List<GameObject> _enemy_Objects = new List<GameObject>();  //全てのEnemyオブジェクトを入れる(Player(Enemy_Status)を入れる方向にシフトする)
    //[SerializeField] List<GameObject> _item_Objects = new List<GameObject>();      //全てのItemsオブジェクトを入れる(Player(Item_Status)を入れる方向にシフトする)
   
           ********************************************************
            
    //public List<GameObject> Player_Objects { set { setLists<GameObject>(value, _player_Objects); } get { return _player_Objects; } }
    //public GameObject Player_Object { set { setList<GameObject>(value, _player_Objects); } }
    //public List<GameObject> Enemy_Objects { set { setLists<GameObject>(value, _enemy_Objects); } get { return _enemy_Objects; } }
    //public GameObject Enemy_Object { set { setList<GameObject>(value, _enemy_Objects); } }
    //public List<GameObject> Item_Objets { set { setLists<GameObject>(value, _item_Objects); } get { return _item_Objects; } }
    //public GameObject Item_Object { set { setList<GameObject>(value, _item_Objects); } }

           ********************************************************
         
    //public List<GameObject> GameObjects { set { setLists<GameObject>(value, _allObjects); } get { return _allObjects; } }
    //public GameObject GameObject { set { setList<GameObject>(value, _allObjects); } }
    //public List<ALLOBJECTS> Scripts { set { setLists<ALLOBJECTS>(value, _allScripts); } get { return _allScripts; } }
    //public ALLOBJECTS Script { set { setList<ALLOBJECTS>(value, _allScripts); } }


private void setList<T>(T gameObject, List<T> _gameObjects) {
    if (true) {
        Debug.Log(gameObject.Equals(typeof(GameObject)));
        if (gameObject.Equals(typeof(GameObject)) && !_allObjects.Contains(gameObject as GameObject)) { //_allObjectsに入れる場合
            _allObjects.Add(gameObject as GameObject);
            Debug.Log(gameObject + ": new _allObjects");
        } else if (gameObject.Equals(typeof(GameObject))) {
            Debug.LogWarning(gameObject + " : is already in : " + _gameObjects);
        }
        if (gameObject.Equals(typeof(ALLOBJECTS)) && !_allScripts.Contains(gameObject as ALLOBJECTS)) { //_allScriptsに入れる場合
            _allScripts.Add(gameObject as ALLOBJECTS);
            Debug.Log(gameObject + ": new _allScripts");
        } else if (gameObject.Equals(typeof(ALLOBJECTS))) {
            Debug.LogWarning(gameObject + " : is already in : " + _gameObjects);
        }
    }
    if (!_gameObjects.Contains(gameObject)) {
        if (!_allObjects.Contains(gameObject as GameObject)) {                //_allObjectsのリストに追加
            _allObjects.Add(gameObject as GameObject);
            Debug.Log(gameObject + ": new _allObjects");
        } else if (!_allScripts.Contains(gameObject as ALLOBJECTS)) {       //_allScriptsのリストに追加
            _allScripts.Add(gameObject as ALLOBJECTS);
            Debug.Log(gameObject + ": new _allScripts");
        }
        _gameObjects.Add(gameObject);                                                 //本命　対象のリストに追加
        Debug.Log(gameObject + " : was added to : " + _gameObjects);
    } else {
        Debug.LogWarning(gameObject + " : is already in : " + _gameObjects);
    }
}
   */
/*
private void setLists(List<ALLOBJECTS> gameObjects, List<ALLOBJECTS> _gameObjects) {
    foreach (Buff_Status gameObject in gameObjects) {
        setList(gameObject, _gameObjects);
    }
}
private void setList(ALLOBJECTS gameObject, List<ALLOBJECTS> _gameObjects) {
    if (_gameObjects == _allScripts) { //もし対象検索全体リストに入ってなかったら
        if (!_allScripts.Contains(gameObject)) {
            _allScripts.Add(gameObject);
            //Debug.Log("new Object put in the _allObjects" + gameObject);
        } else {
            Debug.LogWarning(gameObject + "is already in" + _gameObjects);
        }
    } else {                                        //もし対象検索全体リストに入っていたら
        if (!_gameObjects.Contains(gameObject)) {
            if (!_allScripts.Contains(gameObject)) {
                _allScripts.Add(gameObject);
                //Debug.Log("new Object put in the _allObjects" + gameObject);
            }
            _gameObjects.Add(gameObject);
            //Debug.Log("add " + gameObject + " in " + _gameObjects);
        } else {
            Debug.LogWarning(gameObject + "is already in" + _gameObjects);
        }
    }
}
*/