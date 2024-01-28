using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System.Diagnostics;

public enum NoteScore
{
    Perfect,
    Good,
    Bad,
    Miss,
    None
}

public class Scoring : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI ScoreText1;
    [SerializeField] TextMeshProUGUI LetterGradeText;
    [SerializeField] TextMeshProUGUI LetterGrade1Text;
    [SerializeField] CrowedSystem Crowed;

    char Grade;

    long Score;

    long MaxScore;

    bool JokeSection;

    int JokeSectionTotalNotes;

    int JokeSectionHitNotes;

    public long JokeSectionPointsEarned;

    public long JokeSectionPercent;

    private void OnGUI()
    {
        ScoreText.text = Score.ToString();
        ScoreText1.text = Score.ToString();
        LetterGradeText.text = Grade.ToString();
        LetterGrade1Text.text = Grade.ToString();
    }
    private void UpdateGrade()
    {
        if (Score == 0 && MaxScore == 0)
        {
            Grade = 'S';
            return;
        }
        float relScore = (float)Score / (float)MaxScore;
        if (relScore == 1)
        {
            Grade = 'S';
        }
        else if (relScore >= .90)
        {
            Grade = 'A';
        }
        else if (relScore >= .80)
        {
            Grade = 'B';
        }
        else if (relScore >= .70)
        {
            Grade = 'C';
        }
        else if (relScore <= .50)
        {
            Grade = 'D';
        }
    }

    void NoteCalc(NoteScore noteScore)
    {

        //For different note hits
        int NoteHitScore = 0;
        if(noteScore == NoteScore.None)
        {
            NoteHitScore -= 10;
        }
        else if(noteScore == NoteScore.Miss)
        {
            NoteHitScore += 0;
        }
        else if(noteScore == NoteScore.Perfect)
        {
            NoteHitScore += 100;
        }else if (noteScore == NoteScore.Good)
        {
            NoteHitScore += 80;
        }
        else if (noteScore == NoteScore.Bad)
        {
            NoteHitScore += 50;
        }
      
        if(NoteHitScore >= 0)
        {
            MaxScore += 100;
        }
        if (JokeSection)
        {
            JokeSectionTotalNotes += 1;
            if(NoteHitScore > 0) {
                JokeSectionHitNotes += 1;
            }
            
        }
        Score += NoteHitScore;
        UpdateGrade();
    }

    void StartJokeSection()
    {
        JokeSection = true;
    }

    void EndJokeSection()
    {
        JokeSection = false;
        JokeSectionCalc(JokeSectionTotalNotes, JokeSectionHitNotes);
        JokeSectionTotalNotes = 0;
        JokeSectionHitNotes = 0;
    }
    private void JokeSectionCalc(int totalNotes,int hitNotes)
    {
        if(totalNotes == 0)
        {
            Debug.Log("No Noes");
            return;
        }
        float JokeSectionPercent = hitNotes / totalNotes;
        if (JokeSectionPercent == 1)
        {
            JokeSectionPointsEarned = hitNotes * 1000;
            Crowed.PlayEmotes(3);
        }
        else if (JokeSectionPercent >= .90)
        {
            JokeSectionPointsEarned = hitNotes * 800;
            Crowed.PlayEmotes(3);
        }
        else if (JokeSectionPercent >= .80)
        {
            JokeSectionPointsEarned = hitNotes * 700;
            Crowed.PlayEmotes(2);
        }
        else if (JokeSectionPercent >= .70)
        {
            JokeSectionPointsEarned = hitNotes * 500;
            Crowed.PlayEmotes(2);
        }
        else if (JokeSectionPercent <= .50)
        {
            JokeSectionPointsEarned = 0;
            Crowed.PlayEmotes(1);
        }
        


        MaxScore += totalNotes * 1000;
        Score += JokeSectionPointsEarned;
        UpdateGrade();
    }
    // Start is called before the first frame update
    void Start()
    {
        Grade = 'S';
        Score = 0;

        //
        RhythmSystem.OnJokeStart += delegate (float x)
        {
            StartJokeSection();
        };
        RhythmSystem.OnJokeEnd += delegate ()
        {
            EndJokeSection();
        };

        EndHit.HitMiss += delegate ()
        {
            NoteCalc(NoteScore.Miss);
        };

        MissingCollision.Hit += delegate (NoteScore y)
        {
            NoteCalc(y);
        };

        EarlyLateHit.Late += delegate ()
        {
            NoteCalc(NoteScore.None);
        };

    }
}
