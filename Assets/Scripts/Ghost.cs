using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//script written by Oliver Jenkinson for this project (with reference to previously written code from Game 2)

public class Ghost : MonoBehaviour
{
    public string ghostName;
    
    public GhostJobs ghostJob;

    public GeneralComments generalComments;
    public GameObject Grave, ghostCommentPanel; 


    public List<GameObject> floatArea;

    public TextMeshProUGUI ghostComment;

    public string forgottenIdentity, identityString, detectableIdentity;

    public float minCommentTimer, maxCommentTimer, floatSpeed, maxYPos;
    float commentTimer;

    public Rigidbody ghostRB;
    public MeshRenderer ghostRender;

    Color fullOpacity;

    public Vector3 wayPoint;

    public bool isIdentified;

    private void Start()
    {
        //resetCommentTimer = Random.Range(minCommentTimer, maxCommentTimer);

        floatArea.AddRange(GameObject.FindGameObjectsWithTag("FloatArea"));


        ghostRender.material.color = Random.ColorHSV(0f, 1f, 0.75f, 1f, 0.75f, 1f, 0.75f, 0.75f);

        fullOpacity = ghostRender.material.color;

        fullOpacity.a = 1f;

        identityString = ghostName + "\n" + Grave.GetComponent<GenerateGrave>().jobAdjective + " " + ghostJob.job + "\n" + "Died age " + Grave.GetComponent<GenerateGrave>().age.ToString() + "\n" + Grave.GetComponent<GenerateGrave>().birthYear + "-" + Grave.GetComponent<GenerateGrave>().deathYear;

        detectableIdentity = forgottenIdentity;

        FloatAround();
    }

    private void Update()
    {
        // make comment text face camera
        ghostCommentPanel.transform.LookAt(Camera.main.transform);
        ghostCommentPanel.transform.Rotate(0, 180, 0);

        commentTimer -= Time.deltaTime;

        if (commentTimer <= 0 && !isIdentified)
        {
            GenerateComment();
            FloatAround();
            commentTimer = Random.Range(minCommentTimer, maxCommentTimer); ;
        }
        else if (commentTimer <= 0 && isIdentified)
        {
            ghostComment.text = Grave.GetComponent<GenerateGrave>().ghostStats.identifiedCommentList[Random.Range(0, Grave.GetComponent<GenerateGrave>().ghostStats.identifiedCommentList.Count)];
            //FloatAtGrave();
            commentTimer = Random.Range(minCommentTimer, maxCommentTimer); ;
        }

        // float around
        if (wayPoint != null)
        {
            transform.LookAt(wayPoint);
            transform.position = Vector3.MoveTowards(transform.position, wayPoint, floatSpeed * Time.deltaTime);
                //wayPoint * (floatSpeed * Time.deltaTime);
        }
        //else FloatAround();

        if (isIdentified)
        {
            detectableIdentity = identityString;
            ghostRender.material.color= fullOpacity;
            transform.position = AtGrave();
            
        }
        else detectableIdentity = forgottenIdentity;
    }

    void GenerateComment()
    {

        int randComment = Random.Range(0, 2);

        switch (randComment)
        {
            case 0:
                {
                    ghostComment.text = ghostJob.jobClues[Random.Range(0, ghostJob.jobClues.Count)];
                    break;
                }

            case 1:
                {
                    ghostComment.text = generalComments.generalCommentList[Random.Range(0, generalComments.generalCommentList.Count)];
                    break;
                }

        }
        
    }

    public Vector3 FloatAround()
    {
        return wayPoint = floatArea[Random.Range(0, floatArea.Count)].transform.position;
    }

    public Vector3 AtGrave()
    {
        //float randX = Random.Range(-0.5f, 0.5f);
        //float randY = Random.Range(0.5f, 1.5f);
        //float randZ = Random.Range(-0.5f, 0.5f);

        //return wayPoint = new Vector3(Grave.transform.position.x + randX, randY, Grave.transform.position.z + randZ);
        return wayPoint = new Vector3(Grave.transform.position.x, Grave.transform.position.y + 1f, Grave.transform.position.z - 1.5f);


    }


}
