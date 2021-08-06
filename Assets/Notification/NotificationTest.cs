using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*  
アプリの中断、タスクキルに関する資料
https://kan-kikuchi.hatenablog.com/entry/OnApplicationPause
ローカルプッシュ通知作成方法
https://qiita.com/townsoft/items/dd5cbd8be7590e12f3cf
時間差計算
 https://itsakura.com/csharp-diffdate
 */
public class NotificationTest : MonoBehaviour{
    void Start() {
        Debug.Log(DateTime.Now.AddDays(1));
        LocalPushNotification.RegisterChannel("channelId", "アプリ名（チャンネル名)", "説明");
    }
    private void OnApplicationPause(bool pauseStatus) {
        //一時停止
        if (pauseStatus) {
            SetLPN();
        }
        //再開時
        else {
            LocalPushNotification.AllClear();
        }
    }
    private void OnApplicationQuit() {
        SetLPN();
    }
    void SetLPN() {
        //翌日の19:00に寂しいよ通知
        //1週間後の19:00に悲しいよ通知

        //通知タイトル、内容、バッチ、時間(何秒後)、チャンネルID
        LocalPushNotification.AddSchedule(
            "CrossBallets", "あなたのキャラクターがさみしがってるよ", 1, Cal1900(), "channelId"
        );
        LocalPushNotification.AddSchedule(
            "CrossBallets", "放置ボーナス獲得！", 2, CalNextWeek(), "channelId"
        );
    }
    public int Cal1900() { //翌日19時までの秒数計算
        DateTime nextDay = new DateTime(
            DateTime.Now.AddDays(1).Year,
            DateTime.Now.AddDays(1).Month,
            DateTime.Now.AddDays(1).Day,
            19,
            0,
            0
        );
        TimeSpan timeSpan = nextDay - DateTime.Now;
        return ((int)timeSpan.TotalSeconds);
    }
    public int CalNextWeek() { //翌週19時までの秒数計算
        DateTime nextDay = new DateTime(
            DateTime.Now.AddDays(7).Year,
            DateTime.Now.AddDays(7).Month,
            DateTime.Now.AddDays(7).Day,
            19,
            0,
            0
        );
        TimeSpan timeSpan = nextDay - DateTime.Now;
        return ((int)timeSpan.TotalSeconds);
    }
}
