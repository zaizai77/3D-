using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionUI : MonoBehaviour
{
    public TextMeshProUGUI optionText;
    private Button thisButton;
    private DialogPiece currentPiece;
    private bool takeQuest;
    private string nextPieceID;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }

    public void UpdateOption(DialogPiece piece,DialogOption option)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
        takeQuest = option.takeQuest;
    }

    public void OnOptionClicked()
    {
        if (currentPiece.quest != null)
        {
            var newTask = new QuestManager.QuestTask
            {
                questData = Instantiate(currentPiece.quest)
            };

            if (takeQuest)
            {
                // 添加到任务列表

                //判断是否已经有任务
                if (QuestManager.Instance.HaveQuest(newTask.questData))
                {
                    // 判断是否完成给予奖励

                }
                else
                {
                    QuestManager.Instance.tasks.Add(newTask);
                    QuestManager.Instance.GetTask(newTask.questData).IsStarted = true;
                }
            }
        }

        if(nextPieceID == "")
        {
            DialogUI.Instance.dialogPanel.SetActive(false);
            return;
        } 
        else
        {
            DialogUI.Instance.UpdateMainDialog(DialogUI.Instance.currentData.dialogIndex[nextPieceID]);
        }
    }
}
