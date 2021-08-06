using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*  
�A�v���̒��f�A�^�X�N�L���Ɋւ��鎑��
https://kan-kikuchi.hatenablog.com/entry/OnApplicationPause
���[�J���v�b�V���ʒm�쐬���@
https://qiita.com/townsoft/items/dd5cbd8be7590e12f3cf
���ԍ��v�Z
 https://itsakura.com/csharp-diffdate
 */
public class NotificationTest : MonoBehaviour{
    void Start() {
        Debug.Log(DateTime.Now.AddDays(1));
        LocalPushNotification.RegisterChannel("channelId", "�A�v�����i�`�����l����)", "����");
    }
    private void OnApplicationPause(bool pauseStatus) {
        //�ꎞ��~
        if (pauseStatus) {
            SetLPN();
        }
        //�ĊJ��
        else {
            LocalPushNotification.AllClear();
        }
    }
    private void OnApplicationQuit() {
        SetLPN();
    }
    void SetLPN() {
        //������19:00�Ɏ₵����ʒm
        //1�T�Ԍ��19:00�ɔ߂�����ʒm

        //�ʒm�^�C�g���A���e�A�o�b�`�A����(���b��)�A�`�����l��ID
        LocalPushNotification.AddSchedule(
            "CrossBallets", "���Ȃ��̃L�����N�^�[�����݂������Ă��", 1, Cal1900(), "channelId"
        );
        LocalPushNotification.AddSchedule(
            "CrossBallets", "���u�{�[�i�X�l���I", 2, CalNextWeek(), "channelId"
        );
    }
    public int Cal1900() { //����19���܂ł̕b���v�Z
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
    public int CalNextWeek() { //���T19���܂ł̕b���v�Z
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
