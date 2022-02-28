using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script written by Oliver Jenkinson for this project

[CreateAssetMenu(menuName = "Ghost/Job")]
public class GhostJobs : ScriptableObject
{
    public string job;
    public List<string> jobClues;
}
