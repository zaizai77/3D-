using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog",menuName = "Dialog/Dialog Data")]
public class DialogData_SO : ScriptableObject
{
    public List<DialogPiece> dialogPieces = new List<DialogPiece>();
}
