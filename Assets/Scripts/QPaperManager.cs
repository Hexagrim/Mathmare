using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;


[Serializable]

public class QPaperReq
{

    public QPaperType Type;

    public QPaperDifficulty Difficulty;
}

public class QPaperManager : MonoBehaviour
{
    public QPaperInstance[] QPapers;
    public List<QPaperData> PossibleQPapers;

    public void MakePapers(QPaperReq[] requirement)

    {
        HashSet<QPaperData> selected = new();

        //idk if this shit is performance okey..

        for (int i = 0; i < QPapers.Length; i++)
        {

            List<QPaperData> possible = new();

            foreach (QPaperData paper in PossibleQPapers)
            {
                if (paper.Type == requirement[i].Type &&
                    paper.Difficulty == requirement[i].Difficulty &&
                    !selected.Contains(paper))
                {
                    possible.Add(paper);
                }
            }

            if (possible.Count == 0)
            {
                Debug.LogWarning("make more paper dumbass" + i);
                continue;
            }

            QPaperData chosen = possible[UnityEngine.Random.Range(0, possible.Count)];

            selected.Add(chosen);

            QPapers[i].Data = chosen;
            QPapers[i].answered = false;
            QPapers[i].correct = false;
        }

    }
}