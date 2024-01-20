/* This script implements a basic context based steering behavior */
using System.Linq;
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
        Shooting,
    };

    State enemyState;

    DetectionData[] detectionData; // used for object detection
    const float detectionSphereRadius = 10f; // will look for player till 10 units

    const float rayCastDistance = 1f;
    const float totalAngle = 180f;

    Collider2D selfCollider;

    [Header("Steer Data")]
    [SerializeField] float speed;
    [SerializeField] float rotateAmount;
    [Header("Data for detection")]
    [SerializeField] int captureAmount;
    [SerializeField] float fleeWeight;
    [SerializeField] float seekWeight;

    [SerializeField] LayerMask hitLayerMask;

    Vector3 averagedVector;
    RaycastHit2D[] hits; 

    void Start()
    {

        boxCollider = GetComponent<BoxCollider2D>();
        trails.SetActive(false);

        visibleToCamera = false;
        enemyState = State.MoveRandom;
        selfCollider = GetComponent<Collider2D>();
        captureAmount = 5;
        SetDetectionData();
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
            detectionData[i].rayCastHit = Physics2D.RaycastAll(transform.position, detectionData[i].detectionVector,rayCastDistance,hitLayerMask);
        }
    }

    internal Vector2 GetAverageVector() 
    {
        Vector2 sum = Vector2.zero;

        for (int i=0;i<detectionData.Length;i++)
        {
            sum += detectionData[i].detectionVector;
        }

        Vector2 average = sum / detectionData.Length;

        return average;
    }

    internal void Steer()
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

        averagedVector = GetAverageVector();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, averagedVector), Time.fixedDeltaTime * rotateAmount);
        transform.position += transform.up * speed * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        //detection vectors
        for(int i = 0;i < detectionData.Length;i++)
        {
            if (detectionData[i] != null)
                Debug.DrawRay(transform.position, detectionData[i].detectionVector, detectionData[i].debugDrawColor);
            else
                Debug.Log("Detection vectors are null.");
        }
        //detection sphere
        Gizmos.DrawWireSphere(transform.position,detectionSphereRadius);
        //averaged vector
        Debug.DrawRay(transform.position,averagedVector,Color.yellow);
        //unit vector from enemy to player
        //Debug.DrawRay(transform.position,player.transform.position,Color.yellow);
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
