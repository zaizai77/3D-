using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog",menuName = "Dialog/Dialog Data")]
public class DialogData_SO : ScriptableObject
{
    public List<DialogPiece> dialogPieces = new List<DialogPiece>();

    public Dictionary<string, DialogPiece> dialogIndex = new Dictionary<string, DialogPiece>();

#if UNITY_EDITOR
    private void OnValidate()
    {
        dialogIndex.Clear();
        foreach (var piece in dialogPieces)
        {
            if(!dialogIndex.ContainsKey(piece.ID))
            {
                dialogIndex.Add(piece.ID, piece);
            }
        }
    }

#else 
    //保证在打包执行的游戏里第一时间获得对话的所有字典匹配 
    void Awake() {
        dialogueIndex.Clear();
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }

#endif
}
