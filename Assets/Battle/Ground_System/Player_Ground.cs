using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // usingを追加
using UnityEngine;
using System.Linq;
public class Player_Ground : Ground, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPushed = false; // マウスが押されているか押されていないか    
    public List<Ground> pass_grounds;//通った道順            pass_gorundsの[0]はplayer_Groundの位置に固定する？今はしない
    public bool onLastGround = false;
    [SerializeField] Ground _inputFirstGround; //配置データ受け渡し場所(今は規定値)
    Vector3 _inputPosition;     //タッチし始めのPlayerGroundのPosisionで変更されない
    private int _mobility = 0;
    private float _DEX = 2.0f;
    private float _Time = 0;//1になったらリセットすること
    private bool testTrigger = true;//いつでも消していい
    [SerializeField] GameObject pt;
    public override void Start() {
        base.Start();
        pt = (GameObject)Resources.Load("PlayerParticle");
        _mobility = GetComponent<Player_Status>().Mobility;
        ground_Controller.Player_Ground = this;
        if (_inputFirstGround != null) {            //配置データ受け渡し場所(今は規定値)
            pass_grounds.Clear();//Debug.Log(string.Join(",",pass_grounds) + " : クリアしたので何もないはず");
            pass_grounds.Add(_inputFirstGround);//Debug.Log(string.Join(",", pass_grounds) + " : _inputFirstGround　入れたのでなんか入ってるはず");
            ground_Controller.Set_Ground(this, _inputFirstGround);
            SetPos(_inputFirstGround);
        } else {
            Debug.LogWarning(this.gameObject + "初期位置が設定されてません。");
        }
    }
    void Update() {// マウスが押下されている時、オブジェクトを動かす
        if (_isPushed) {
            Vector3 inputTouch;  //実際に触っている場所
            Vector3 objectPosition; //プレイヤーがいるべき場所
            inputTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            inputTouch.z = 0;
            _inputPosition.z = 0;
            float d = Vector3.Distance(_inputPosition, inputTouch);
            if (d < 1.3f) { //Transform.position = objectPosition;     //移動のコードを組むので一旦OFF 
                objectPosition = inputTouch;
                if (position_Ground != null && position_Ground != pass_grounds.Last() && pass_grounds.Count <= _mobility) { //Groundオブジェクトに乗った時の処理
                    pass_grounds.Add(position_Ground);
                    _inputPosition = pass_grounds.Last().gameObject.transform.position;
                    ground_Controller.Remove_Ground(this);
                    ground_Controller.Set_Ground(this, pass_grounds.Last());
                    pass_grounds.Last().renderer.material.color = Color.red;
                }
            } else { 
                //Playerの表示位置を1行動での移動距離限界に制限する
                //使用するとPlayerの位置が変わるため現在は廃止、移動距離限界の可視化のために今後必要なのでとっておく
                /*
                objectPosition = _inputPosition + (inputTouch - _inputPosition) * 1.3f / d;
                Transform.position = objectPosition;
                */
            }
            if (pass_grounds.Count >= 2 && !gameData.player_Turn) {//時が進む
                gameData.player_Turn = true;
                gameData.StartCoroutine("Player_Turn");//コルーチンなのでこれ以下に処理を書くときは順番に注意
            }                                                             
        }
        if (pass_grounds.Count >= 1) {   //Passが増えた時に移動する処理              
            if (testTrigger) {
                //StartCoroutine("Particle");
                //testTrigger =false;
            }
            float distance = Vector3.Distance(transform.position, pass_grounds[0].transform.position);
            if (0.01f < distance) {
                transform.position = Vector3.MoveTowards(transform.position, pass_grounds[0].transform.position, _DEX * Time.deltaTime * 0.5f);
            } else {
                gameObject.transform.position = pass_grounds[0].transform.position;
                if (pass_grounds.Count > 1) {
                    pass_grounds.Remove(pass_grounds[0]);
                    StartCoroutine("Particle");
                    testTrigger = false;
                }
            }
        }
    }
    // クリック検知時、コールバックされる関数
    public void OnPointerClick(PointerEventData eventData) { }
    public void OnPointerDown(PointerEventData eventData) {//触った時
        _isPushed = true;
        _inputPosition = pass_grounds.Last().transform.position;
        //Collider.enabled = false;//触ってるキャラのColliderをOFF
        gameObject.layer = 2;
        ground_Controller.Selected_Ground = this;//触っているPlayerGround
      IEnumerable<Ground[]> _turnOn_Ground = ground_Controller.Ground.Where(i => i[1] == null); //何も乗っていないGroundのColliderをtrueにする。
        foreach (Ground[] g in _turnOn_Ground) {
            if (g[0] != null) {
                g[0].collider.enabled = true;
                //g[0].gameObject.layer = 0;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData) {
        OnPointerUp();
    }
    public void OnPointerUp() {
        _isPushed = false;
        _inputPosition = Vector3.zero;
        //Collider.enabled = true;
        gameObject.layer = 0;
        foreach (Ground ground in ground_Controller.Normal_Grounds) {
            ground.collider.enabled = false; 
            //ground.gameObject.layer = 2;
        }
        if (pass_grounds.Count > 0) {     //指を話したとき移動してたら移動、してなかったら経路をリセットして(初めにリセットするのでリセットしない)初期位置に
            SetPos(pass_grounds.Last());
        }
        foreach (Ground _pass_grounds in pass_grounds) {
            Debug.Log(_pass_grounds);
            _pass_grounds.renderer.material.color = Color.blue;
        }
    }

    private IEnumerator Particle() {
        GameObject particles = Instantiate(pt) as GameObject;
        particles.GetComponent<Transform>().parent = this.gameObject.transform;
        yield return new WaitForSeconds(0.2f);
        testTrigger = true;
    }
}