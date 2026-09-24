using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;


public enum QPaperType
{
    SentenceMCQ,
    FigureMCQ
}

public enum QPaperDifficulty
{
    Easy,
    Normal,
    Hard,
    Impossible
}

//ts pmo wont fucking work cause unity keeps changing their shit and its my first time making a scriptable object for datamanagement;
[CreateAssetMenu(fileName = "QPaper", menuName = "QPaper/QPaper Data")]


public class QPaperData : ScriptableObject
{

    public QPaperType Type;
    public QPaperDifficulty Difficulty;

    [TextArea(3, 6)]
    public string Question;

    //the fig will be kinda not mandatroy

    public Sprite Figure;
    public string[] Answers = new string[4];


    public int CorrectAnswer;

}