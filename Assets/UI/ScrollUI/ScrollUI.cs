using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using DG.Tweening;//DoTweenのやつ

public class ScrollUI : MonoBehaviour{
    public Scrollbar scrollbar;
    public TextMeshProUGUI textMeshProUGUI;
    [SerializeField] int maxLine = 30;
    [SerializeField] float scrollbarValue = 0.0f;
    [SerializeField] List<string> contents = new List<string>();
    private int testInt = 0;
    public ScrollRect scrollRect;
    private bool scrollUpdate = false;

    private void Start() {
        if(scrollbar == null) {
            Debug.LogError("Scrollbarがない");
        }
        if(textMeshProUGUI == null) {
            Debug.LogError("textMeshProUGUIがない");
        }
    }
    private void LateUpdate() {

    }
    private void OnPostRender() {
        if (scrollUpdate) {
            scrollUpdate = false;
            scrollRect.verticalNormalizedPosition = 0.0f;
        }
    }
    public void ChangeInfomation() {
        string content;
        content = string.Join("\n",contents);
        textMeshProUGUI.text = content;
        //scrollUpdate = true;
        DOTween.To(() => scrollRect.verticalNormalizedPosition,
            num => scrollRect.verticalNormalizedPosition = num, 0.0f, 1.0f).SetEase(Ease.InBounce);
        ;
        //
        //0.1sかけて0.0にする
        /*
        scrollbarValue = 0;
        scrollbar.value = scrollbarValue;
        scrollRect.verticalNormalizedPosition = 0.0f;
        */
    }
    public void AddScrollText(string addText) {
        //addText = addText.Replace(" ", "");   //Unicode /u0020 半角空白がTMPで使えないのでひとまずの処理
        while (contents.Count > maxLine) {
            contents.RemoveAt(0);//多くなったら1要素目を削除する
        }
        contents.Add(addText);
        ChangeInfomation();
    }

    public void AddInfoTest() {
        Debug.Log("テストコードが実行されました" + testInt);
        AddScrollText("テストコードが実行されました " + testInt);
        testInt++;
    }
    public void ScrollbarResetTest() {
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
}
