using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

//script written by Oliver Jenkinson for this project (except GetRandomSpawn(), which was found online for a previous personal project and not written by me)

public class GenerateGrave : MonoBehaviour
{
    [HideInInspector] public string introText, firstName, lastName, jobAdjective;
    [HideInInspector] public GhostJobs job, potentialJob;
    [HideInInspector] public int age, birthYear, deathYear;

    public GenerateGhostStats ghostStats;

    public TextMeshProUGUI graveText;

    public GameObject ghostPrefab;
    GameObject ghostPartner;
    Ghost ghost;
    List<GameObject> floatArea;
    public Transform graveLookPos;

    public GameStateUI gameStateUI;

    Vector3 ghostSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        floatArea = new List<GameObject>();
        floatArea.AddRange(GameObject.FindGameObjectsWithTag("FloatArea"));

        ghostSpawnPos = floatArea[Random.Range(0, floatArea.Count)].transform.position;

        introText = ghostStats.GenerateIntroText();
        firstName = ghostStats.GenerateFirstName();
        lastName = ghostStats.GenerateLastName();
        jobAdjective = ghostStats.GenerateJobAdjective();
        potentialJob = ghostStats.GenerateJob();
        age = ghostStats.GenerateAge();
        birthYear = ghostStats.GenerateBirthYear();
        deathYear = birthYear + age;

        for (int i = 0; i < gameStateUI.gravesList.Count; i++)
        {
            if (gameStateUI.gravesList[i] != gameObject && potentialJob == gameStateUI.gravesList[i].GetComponent<GenerateGrave>().potentialJob)
            {
                potentialJob = ghostStats.GenerateJob();
            }
        }

        job = potentialJob;


        graveText.text = introText + "\n" + firstName + " " + lastName + "\n" + "\n" + jobAdjective + " " + job.job + "\n" + "\n" + "Died age " + age + "\n" + birthYear + "-" + deathYear;
        ghostPartner = Instantiate(ghostPrefab, ghostSpawnPos, Quaternion.identity);

        ghost = ghostPartner.GetComponent<Ghost>();

        ghost.ghostName = firstName + " " + lastName;
        ghost.ghostJob = job;
        ghost.Grave = gameObject;

        transform.LookAt(graveLookPos);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //private Vector3 GetRandomSpawn()
    //{
    //    NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

    //    int maxIndices = navMeshData.indices.Length - 3;

    //    // pick the first indice of a random triangle in the nav mesh
    //    int firstVertexSelected = UnityEngine.Random.Range(0, maxIndices);
    //    int secondVertexSelected = UnityEngine.Random.Range(0, maxIndices);

    //    // spawn on verticies
    //    Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

    //    Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
    //    Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];

    //    // eliminate points that share a similar X or Z position to stop spawining in square grid line formations
    //    if ((int)firstVertexPosition.x == (int)secondVertexPosition.x || (int)firstVertexPosition.z == (int)secondVertexPosition.z)
    //    {
    //        point = GetRandomSpawn(); // re-roll a position - I'm not happy with this recursion it could be better
    //    }
    //    else
    //    {
    //        // select a random point on it
    //        point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, UnityEngine.Random.Range(0.05f, 0.95f));
    //    }

    //    return point;
    //}
}
