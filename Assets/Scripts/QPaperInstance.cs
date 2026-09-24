using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QPaperInstance : MonoBehaviour
{
    public QPaperData Data;

    public bool answered;
    public bool correct;
    private Animator Anim;

    public TMP_Text Question;
    public TMP_Text[] Answers;
    public Sprite Figure;
    

    private void Awake()
    {
        Anim = GetComponent<Animator>();

        // here gona set every thing to match the data :}

        Question.text = Data.Question;
        for (int i = 0; i < Answers.Length; i++)
            Answers[i].text = Data.Answers[i];
        Figure = Data.Figure;

    }

    public void SubmitAnswer(int answer)
    {
        if (answered)
            return;
        Anim.SetTrigger(Random.Range(0, 2) == 0 ? "foldL" : "foldR"); // gotta love these neat ternary operators

        answered = true;
        correct = answer == Data.CorrectAnswer;
    }

}