using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Status : MonoBehaviour
{
    public TargetController _targetController;
    public GameData gameData;
    public SO_ShowInfo showInfo;
    public EnemyBullets enemyBullets;
    //すべてを統括するオブジェクトクラス！
    /***
     *  Character_StatusとItem_Statusを共存させたクラス
     *  パッシブ発動タイミングなどをoverrideすることでアイテムに能力を付与できる
     *  構成 ALLOBJECTS | Character_Status | それぞれのキャラクタステータス
     *                            | Item_Status        | それぞれのアイテムステータス
     *  TargetControllerにALLOBJECTSのGameObjectを入れておくことで場にあるGameObjectの役割を識別する(これってTagで良くないか?)                            
     ***/
    public void Awake() {
        if(showInfo == null) {
            showInfo = Resources.Load("ShowInfo") as SO_ShowInfo;
            if(showInfo == null) {
                Debug.LogError("Nothing showInfo!");
            }
        }
    }
    public virtual void passive_First() { }
    public virtual void passive_Second() { }
    public virtual void passive_Third() { }
}
