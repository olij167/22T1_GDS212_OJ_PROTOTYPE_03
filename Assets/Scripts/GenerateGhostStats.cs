using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script written by Oliver Jenkinson for this project

[CreateAssetMenu(menuName = "Ghost/GhostStats")]
public class GenerateGhostStats : ScriptableObject
{
    public List<string> introTextList, ghostFirstNameList, ghostLastNameList, jobAdjectiveList, identifiedCommentList;
    public List<GhostJobs> ghostJobList;
    public int ageMin, ageMax, birthYearMin, birthYearMax;
   

    public string GenerateIntroText()
    {
        return introTextList[Random.Range(0, introTextList.Count)];
    }

    public string GenerateFirstName()
    {
        return ghostFirstNameList[Random.Range(0, ghostFirstNameList.Count)];
    }

    public string GenerateLastName()
    {
        return ghostLastNameList[Random.Range(0, ghostLastNameList.Count)];
    }

    public string GenerateJobAdjective()
    {
        return jobAdjectiveList[Random.Range(0, jobAdjectiveList.Count)];
    }

    public GhostJobs GenerateJob()
    {
        return ghostJobList[Random.Range(0, ghostJobList.Count)];
    }

    public int GenerateAge()
    {
        return Random.Range(ageMin, ageMax);
    }

    public int GenerateBirthYear()
    {
        return Random.Range(birthYearMin, birthYearMax);
    }

}
