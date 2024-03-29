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
    [SerializeField] TextMeshProUGUI JokeSectionPoints;
    [SerializeField] private GameObject Scoringtext;
    [SerializeField] CrowedSystem Crowed;

    public char Grade;

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
        JokeSectionPoints.text = JokeSectionPointsEarned.ToString();
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
            NoteHitScore -= 30;
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
            NoteHitScore += 90;
        }
        else if (noteScore == NoteScore.Bad)
        {
            NoteHitScore += 70;
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

    void StartJokeSection( float _)
    {
        JokeSection = true;
    }

    void EndJokeSection()
    {
        JokeSection = false;
        Scoringtext.SetActive(true);
        StartCoroutine(EndJokeScore());
        JokeSectionCalc(JokeSectionTotalNotes, JokeSectionHitNotes);
        JokeSectionTotalNotes = 0;
        JokeSectionHitNotes = 0;
    }

    IEnumerator EndJokeScore()
    {
        yield return new WaitForSeconds(2f);
        Scoringtext.SetActive(false);
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
            if(!Crowed.Crowed2)
            {
                Crowed.MoveCrowedLevel2Up();
            }
            if(!Crowed.Crowed3)
            {
                Crowed.MoveCrowedLevel3Up();
            }
        }
        else if (JokeSectionPercent >= .90)
        {
            JokeSectionPointsEarned = hitNotes * 800;
            Crowed.PlayEmotes(3);
            if (Crowed.Crowed2)
            {
                if (Crowed.Crowed3)
                {
                    return;
                }
                else
                {
                    Crowed.MoveCrowedLevel3Up();
                }
            }
            else
            {
                Crowed.MoveCrowedLevel2Up();
            }
        }
        else if (JokeSectionPercent >= .80)
        {
            JokeSectionPointsEarned = hitNotes * 700;
            Crowed.PlayEmotes(2);
            if (Crowed.Crowed2)
            {
                if (Crowed.Crowed3)
                {
                    Crowed.MoveCrowedLevel3Down();
                }
            }
            else
            {
                Crowed.MoveCrowedLevel2Up();
            }
        }
        else if (JokeSectionPercent >= .70)
        {
            JokeSectionPointsEarned = hitNotes * 500;
            Crowed.PlayEmotes(2);
            if (Crowed.Crowed2)
            {
                if (Crowed.Crowed3)
                {
                    Crowed.MoveCrowedLevel3Down();
                }
            }
            else
            {
                Crowed.MoveCrowedLevel2Up();
            }
        }
        else if (JokeSectionPercent <= .50)
        {
            JokeSectionPointsEarned = 0;
            Crowed.PlayEmotes(1);

            if(Crowed.Crowed3)
            {
                Crowed.MoveCrowedLevel3Down();
            }
            if (Crowed.Crowed2)
            {
                Crowed.MoveCrowedLevel2Down();
            }
        }
        


        MaxScore += totalNotes * 1000;
        Score += JokeSectionPointsEarned;
        UpdateGrade();
    }

    void NoteCalcMiss() {
        NoteCalc(NoteScore.Miss);
    }

    void NoteCalcLate() {
        NoteCalc(NoteScore.None);
    }

    // Start is called before the first frame update
    void Start()
    {
        Grade = 'S';
        Score = 0;
        MaxScore = 0;
    }

    void OnEnable() {

        RhythmSystem.OnJokeStart += StartJokeSection;
        RhythmSystem.OnJokeEnd += EndJokeSection;

        EndHit.HitMiss += NoteCalcMiss;

        MissingCollision.Hit += NoteCalc;

        EarlyLateHit.Late += NoteCalcLate;
    }

    void OnDisable() {
        RhythmSystem.OnJokeStart -= StartJokeSection;
        RhythmSystem.OnJokeEnd -= EndJokeSection;

        EndHit.HitMiss -= NoteCalcMiss;

        MissingCollision.Hit -= NoteCalc;

        EarlyLateHit.Late -= NoteCalcLate;
    }
}


