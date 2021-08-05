using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using DG.Tweening;//DoTween�̂��

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
            Debug.LogError("Scrollbar���Ȃ�");
        }
        if(textMeshProUGUI == null) {
            Debug.LogError("textMeshProUGUI���Ȃ�");
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
        //0.1s������0.0�ɂ���
        /*
        scrollbarValue = 0;
        scrollbar.value = scrollbarValue;
        scrollRect.verticalNormalizedPosition = 0.0f;
        */
    }
    public void AddScrollText(string addText) {
        //addText = addText.Replace(" ", "");   //Unicode /u0020 ���p�󔒂�TMP�Ŏg���Ȃ��̂łЂƂ܂��̏���
        while (contents.Count > maxLine) {
            contents.RemoveAt(0);//�����Ȃ�����1�v�f�ڂ��폜����
        }
        contents.Add(addText);
        ChangeInfomation();
    }

    public void AddInfoTest() {
        Debug.Log("�e�X�g�R�[�h�����s����܂���" + testInt);
        AddScrollText("�e�X�g�R�[�h�����s����܂��� " + testInt);
        testInt++;
    }
    public void ScrollbarResetTest() {
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
}
