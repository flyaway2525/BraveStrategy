using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ground_Controller : MonoBehaviour
{
    [SerializeField] List<Player_Ground> _player_Grounds;
    [SerializeField] List<Enemy_Ground> _enemy_Grounds;
    [SerializeField] List<Normal_Ground> _normal_Grounds;
    [SerializeField]
    Ground[][] _grounds = new Ground[][]{
        new Ground[]{null,null }, new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },
        new Ground[]{null,null }, new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },
        new Ground[]{null,null }, new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },
        new Ground[]{null,null }, new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },
        new Ground[]{null,null }, new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },new Ground[]{null,null },
        new Ground[]{null,null }
    };                                                   //初期化のしかた
    private int _max_Ground = 26;
    [SerializeField] Player_Ground _selected_Ground;
    public Collider touchOffCollider;//触ることを禁止するためのEvent用Collider
    public bool turnPlayer = false; //True で PlayerTurn False で EnemyTurn
    private void setLists<T>(List<T> gameObjects, List<T> _gameObjects) {
        foreach (T gameObject in gameObjects) {
            setList(gameObject, _gameObjects);
        }
    }
    private void setList<T>(T gameObject, List<T> _gameObjects) {
        _gameObjects.Add(gameObject);
    }
    public void SetNormal_Ground(Normal_Ground _normal_Ground) {
        for (int i = 0; i < _max_Ground; i++) {
            if (_grounds[i][0] == null) {
                _grounds[i][0] = _normal_Ground;
                return;
            }
        }
    }
    public void SetNormal_Ground(Normal_Ground _normal_Ground, Ground _ground) {
        for (int i = 0; i < _max_Ground; i++) {
            if (_grounds[i][0] == null) {
                _grounds[i][0] = _normal_Ground;
                _grounds[i][1] = _ground;
                return;
            }
        }
    }
    public Ground GetNormal_Ground(Ground _ground) {  //キャラクタのGroundを入れると乗っている場所を示す
        var g = _grounds.First(i => i[1] == _ground);
        return g[0];
    }
    public Ground Charactor_Ground(Ground _ground) { //ノーマルGroundを入れると乗っているキャラクターを示す
        Debug.Log(_grounds[0]);
        //List<Ground[]> l = null;
        _grounds.First(i => i[1] == _ground);
        Debug.Log(_grounds[0]);
        return _grounds[0][1];
    }
    public void Remove_Ground(Ground _ground) { //キャラクターがGroundを離れる時の処理
        for (int i = 0; i < _max_Ground; i++) {
            if (_grounds[i][1] == _ground) {
                _grounds[i][1] = null;
                return;
            }
        }
    }
    //↓forで回しているので何とかしたい。_Normal_Groundにグランドをセットする
    public void Set_Ground(Ground _ground,Ground _normal_Ground) { //Normal_GroundにGroundが乗った時の処理
        for (int i = 0; i < _max_Ground; i++) {
            if (_grounds[i][0] == _normal_Ground) {
                _grounds[i][1] = _ground;
                return;
            }
        }
    }
    public List<Player_Ground> Player_Grounds { set { setLists(value, _player_Grounds); } get { return _player_Grounds; } }
    public Player_Ground Player_Ground { set { setList(value, _player_Grounds); } }
    public List<Enemy_Ground> Enemy_Grounds { set { setLists(value, _enemy_Grounds); } get { return _enemy_Grounds; } }
    public Enemy_Ground Enemy_Ground { set { setList(value, _enemy_Grounds); } }
    public List<Normal_Ground> Normal_Grounds { set { setLists(value, _normal_Grounds); } get { return _normal_Grounds; } }
    public Normal_Ground Normal_Ground { set { setList(value, _normal_Grounds); } }
    public Ground[][] Ground { get { return _grounds; } }
    public Player_Ground Selected_Ground { set { _selected_Ground = value; } get { return _selected_Ground; } }

    //setとaddメソッドを用意する方が見方がいい
    public bool CheckPlayersLife() {
        bool _life = false;
        foreach (Player_Ground item in _player_Grounds) {
            if (item.player_Status.status.life) {
                _life = true;
                break;
            }
        }
        if (!_life) {
            Debug.Log("GAMEOVER");
        }
        return (_life);
    }
}
