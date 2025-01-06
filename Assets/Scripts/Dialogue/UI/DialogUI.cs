using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogUI : Singleton<DialogUI>
{
    [Header("Basic Elements")]
    public Image icon;
    public TextMeshProUGUI mainText;
    public Button nextButton;
    public GameObject dialogPanel;

    [Header("Options")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;

    [Header("Data")]
    public DialogData_SO currentData;
    int currentIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        nextButton.onClick.AddListener(ContinueDialog);
    }

    void ContinueDialog()
    {
        if(currentIndex < currentData.dialogPieces.Count)
        {
            UpdateMainDialog(currentData.dialogPieces[currentIndex]);
        } 
        else
        {
            dialogPanel.SetActive(false);
        }
        
    }

    public void UpdateDialogData(DialogData_SO data)
    {
        currentData = data;
        currentIndex = 0;
    }

    public void UpdateMainDialog(DialogPiece dialogPiece)
    {
        dialogPanel.SetActive(true);
        currentIndex++;

        if (dialogPiece.image != null)
        {
            icon.enabled = true;
            icon.sprite = dialogPiece.image;
        } 
        else
        {
            icon.enabled = false;
        }

        mainText.text = "";
        // mainText.text = dialogPiece.text;
        //使呈现打字机的效果
        //mainText.DOText(dialogPiece.text,1f); dotween set窗口点不了tmp
        string text = dialogPiece.text;
        var t = DOTween.To(() => string.Empty, value => mainText.text = value, text, 0.3f).SetEase(Ease.Linear);

        // 富文本
        t.SetOptions(true);

        if(dialogPiece.dialogOptions.Count == 0 && currentData.dialogPieces.Count > 0)
        {
            nextButton.interactable = true;
            nextButton.transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            //直接删除next会影响布局
            nextButton.interactable = false;
            nextButton.transform.GetChild(0).gameObject.SetActive(false);
        }

        // 创建 options
        CreateOptions(dialogPiece);
    }

    void CreateOptions(DialogPiece piece)
    {
        //从后往前删
        for(int i = optionPanel.childCount - 1;i >= 0;i--)
        {
            Destroy(optionPanel.GetChild(i).gameObject);
        }

        //错误写法：删除物体不会立马删除，会造成死循环
        //while(optionPanel.childCount > 0)
        //{
        //    Destroy(optionPanel.GetChild(0).gameObject);
        //}

        for (int i = 0; i < piece.dialogOptions.Count; i++)
        {
            var option = Instantiate(optionPrefab, optionPanel);
            option.UpdateOption(piece, piece.dialogOptions[i]);
        }
    }
}
