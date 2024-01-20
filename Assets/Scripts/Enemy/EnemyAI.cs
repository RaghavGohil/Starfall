/* This script implements a basic context based steering behavior */
using UnityEngine;

public class DetectionData 
{
    public RaycastHit2D rayCastHit;
    public Vector2 detectionVector;
    public float weight;
    public Color debugDrawColor;
}

public class EnemyAI : MonoBehaviour
{

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

    DetectionData[] detectionData;

    const float rayCastDistance = 1f;
    const float rayCastOffset = 0.4f;

    const float totalAngle = 180f;

    [Header("Data for detection")]
    [SerializeField] float rotateAmount;
    [SerializeField] int captureAmount;

    [SerializeField] LayerMask hitLayerMask;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); 
        trails.SetActive(false);

        visibleToCamera = false;
        enemyState = State.MoveRandom;
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
            detectionData[i].rayCastHit = Physics2D.Raycast(transform.position, detectionData[i].detectionVector,rayCastDistance,hitLayerMask);
        }
    }

    private void OnDrawGizmos()
    {
        for(int i = 0;i < detectionData.Length;i++)
        {
            if (detectionData[i] != null)
                Debug.DrawRay(((Vector2)transform.position) + ((Vector2)transform.up) * rayCastOffset, detectionData[i].detectionVector, detectionData[i].debugDrawColor);
            else
                Debug.Log("Detection vectors are null.");
        }
    }

    private void Steer()
    {
        transform.position += transform.up * 0.5f * Time.deltaTime;
        if (visibleToCamera) 
        {
            /*if (rayCastHit.collider!= null) 
            {
                *//*foreach (string tag in colliderTags)
                {
                    if (tag == rayCastHit.collider.tag)
                    {
                        transform.Rotate(new Vector3(0f, 0f, rotateAmount*Time.fixedDeltaTime));
                        break;
                    }
                }*//*
            }*/
        }
        
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
}
