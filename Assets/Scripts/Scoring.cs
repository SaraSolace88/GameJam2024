using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteScore
{
    Perfect,
    Good,
    Bad,
    Miss
}

public class Scoring : MonoBehaviour
{
    char Grade;

    long Score;

    long MaxScore;

    bool JokeSection;

    int JokeSectionTotalNotes;

    int JokeSectionHitNotes;

    private void UpdateGrade()
    {
        float relScore = MaxScore / Score;
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
        if(noteScore != NoteScore.Miss)
        {
            NoteHitScore += 100;
        }
        else if(noteScore == NoteScore.Perfect)
        {
            MaxScore += 100;
        }else if (noteScore == NoteScore.Good)
        {
            MaxScore += 80;
        }
        else if (noteScore == NoteScore.Bad)
        {
            MaxScore += 50;
        }
      
        MaxScore += 100;
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
        JokeSectionTotalNotes = 0;
        JokeSectionHitNotes = 0;
        JokeSectionCalc(JokeSectionTotalNotes, JokeSectionHitNotes);
    }
    private void JokeSectionCalc(int totalNotes,int hitNotes)
    {
        float relScore = hitNotes / totalNotes;
        if (relScore == 1)
        {
            Score += hitNotes * 1000;
        }
        else if (relScore >= .90)
        {
            Score += hitNotes * 800;
        }
        else if (relScore >= .80)
        {
            Score += hitNotes * 700;
        }
        else if (relScore >= .70)
        {
            Score += hitNotes * 500;
        }
        else if (relScore <= .50)
        {
            Score += 0;
        }

        MaxScore += totalNotes * 1000;
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

        MissingCollision.HitMiss += delegate ()
        {
            NoteCalc(NoteScore.Miss);
        };

        MissingCollision.Hit += delegate (NoteScore y)
        {
            NoteCalc(y);
        };

    }
}
