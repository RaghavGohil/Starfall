/* This script implements a basic context based steering behavior */
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class DetectionData 
{
    public RaycastHit2D[] rayCastHit;
    public Vector2 detectionVector;
    public float weight;
    public Color debugDrawColor;
}

public class EnemyAI : MonoBehaviour
{

    /*other references*/
    [HideInInspector]
    public GameObject player;

    /* disable on invisible*/

    [Header("Data for changing visibility")]
    [SerializeField] GameObject trails;
    BoxCollider2D boxCollider;

    /* ray casting */

    bool visibleToCamera;
    private enum State
    {
        MoveRandom,
        FollowPlayer,
    };

    State enemyState;

    DetectionData[] detectionData; // used for object detection

    [Header("Steer Data")]
    [SerializeField] float speed;
    [SerializeField] float rotateAmount;

    [Header("Obstacle detection")]
    [SerializeField] float totalAngle;
    [SerializeField] float rayCastDistance;
    [SerializeField] int captureAmount;
    [SerializeField] float fleeWeight;
    [SerializeField] float seekWeight;
    [SerializeField] float targetWeight;
    [Header("Timing")]
    [SerializeField] float newRandomTime; // new random value must come after this time
    float randomTimeElapsed;
    [SerializeField] LayerMask hitLayerMask;

    [Header("Player detection range")]
    [SerializeField] float detectionSphereRadius; // will look for player till 10 units
    [SerializeField] ContactFilter2D contactFilter;
    [SerializeField] int maxCollidersInView;

    Collider2D selfCollider;
    Vector3 averagedVector;
    Vector2 targetVector;
    Vector2 targetPosition;
    RaycastHit2D[] hits;
    Collider2D[] resultColliders;


    void Start()
    {
        player = SpawnPlayer.player;
        targetVector = GetRandomPosition();
        boxCollider = GetComponent<BoxCollider2D>();
        trails.SetActive(false);

        visibleToCamera = false;
        enemyState = State.MoveRandom;
        selfCollider = GetComponent<Collider2D>();
        SetDetectionData();

        resultColliders = new Collider2D[maxCollidersInView];

        randomTimeElapsed = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Steer();
        SetDetectionVectors();
        SetDetectionRayCasts();
    }

    internal void SetDetectionData() 
    {
        detectionData = new DetectionData[captureAmount];
        for(int i=0; i<detectionData.Length;i++) 
        {
            detectionData[i] = new DetectionData();
        }
    }
    internal void SetDetectionVectors() 
    {
        for(int i = 0;i < detectionData.Length; i++)
        {
            float angle = (i==0)? 0f: Mathf.Lerp(0,totalAngle, (float)i/(detectionData.Length-1));
            angle -= totalAngle/2;
            detectionData[i].detectionVector = Quaternion.AngleAxis(angle , transform.forward) * (transform.up * detectionData[i].weight);
        }         
    }

    internal void SetDetectionRayCasts()
    {
        for(int i = 0;i < detectionData.Length; i++)
        {
            detectionData[i].rayCastHit = Physics2D.RaycastAll(transform.position, detectionData[i].detectionVector,detectionData[i].weight*rayCastDistance,hitLayerMask);
        }
    }

    internal Vector2 GetAverageVector() 
    {
        Vector2 sum = Vector2.zero;

        for (int i=0;i<detectionData.Length;i++)
        {
            sum += detectionData[i].detectionVector;
        }

        sum += targetVector;

        Vector2 average = sum /( detectionData.Length + 1); // +1 is for target vec

        return average;
    }

    internal Vector2 GetRandomPosition() 
    {
        return new Vector2(Random.Range(-90f,90f),Random.Range(-90f,90f));
    }

    internal void DetectObstacle() 
    {
        for(int i=0;i<detectionData.Length;i++) 
        {
            if (detectionData[i].rayCastHit != null)
                hits = FilterOutSelfColliders(detectionData[i].rayCastHit);
            if (hits != null && hits.Length > 0) // means we have hit an obstacle
            {
                detectionData[i].weight = fleeWeight;
                detectionData[i].debugDrawColor = Color.red;
            }
            else
            { 
                detectionData[i].weight = seekWeight;
                detectionData[i].debugDrawColor = Color.green;
            }
        }
    }

    internal void SeekTarget(Vector3 targetPos) 
    {
        targetVector = (targetPos - transform.position).normalized * targetWeight;
    }

    internal void MoveRandom() 
    {
        randomTimeElapsed += Time.deltaTime;
        if (randomTimeElapsed > newRandomTime)
        {
            targetPosition = GetRandomPosition();
            randomTimeElapsed = 0f;
        }
        else 
        {
            SeekTarget(targetPosition);
        }
    }
    
    internal void Steer()
    {
        DetectObstacle();
        ChangeState();
        if (enemyState == State.MoveRandom)
            MoveRandom();
        else if (enemyState == State.FollowPlayer)
            SeekTarget(player.transform.position);
        averagedVector = GetAverageVector();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, averagedVector), Time.fixedDeltaTime * rotateAmount);
        transform.position += transform.up * speed * Time.fixedDeltaTime;
    }

    internal void ChangeState() 
    {
        int objectsDetected = Physics2D.OverlapCircle(transform.position, detectionSphereRadius, contactFilter, resultColliders);
        bool foundPlayer = false;
        if (objectsDetected > 0) 
        {
            foreach (Collider2D col in resultColliders) 
            {
                if (col.transform == player.transform)
                {
                    enemyState = State.FollowPlayer;
                    foundPlayer = true;
                    break;
                }
            }
        }
        if (!foundPlayer) 
        {
            enemyState = State.MoveRandom;
        }
    }

    private void OnDrawGizmos()
    {
        //detection vectors
        for(int i = 0;i < detectionData.Length;i++)
        {
            if (detectionData[i] != null)
                Debug.DrawRay(transform.position, detectionData[i].detectionVector*rayCastDistance, detectionData[i].debugDrawColor);
            else
                Debug.Log("Detection vectors are null.");
        }
        //detection sphere
        Gizmos.DrawWireSphere(transform.position,detectionSphereRadius);
        //averaged vector
        Debug.DrawRay(transform.position,averagedVector,Color.yellow);
        //from enemy to target 
        Debug.DrawRay(transform.position,targetVector,Color.white);
    }

    void OnBecameInvisible()
    {
        visibleToCamera = false;
        boxCollider.enabled = false;
        trails.SetActive(false);
    }

    void OnBecameVisible()
    {
        visibleToCamera = true;
        boxCollider.enabled = true;
        trails.SetActive(true);
    }
    RaycastHit2D[] FilterOutSelfColliders(RaycastHit2D[] hits)
    {
        
        if (selfCollider != null)
        {
            hits = hits.Where(hit => hit.collider != selfCollider).ToArray();
        }

        return hits;
    }
}
