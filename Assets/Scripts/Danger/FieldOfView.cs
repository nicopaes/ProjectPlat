using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    [Range(0,50)]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    [Tooltip("Delay from target entering the FOV ")]
    public float viewDelay;

    [Tooltip("Max time to the player in vision to get respawn")]
    public float maxInViewTime;

    [Space]
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    // Targets visible in the FOV of the enemy
    public List<Transform> visibleTargets = new List<Transform>();
    [SerializeField]
    private Vector3 _lastTargetLocation;

    [Space]
    [Header("Security Camera")]
    [Tooltip("Waypoints to the rotation of the camera ")]
    public float RightDirection;
    public float LeftDirection;

    [Tooltip("Rotation Speed of the FOV")]
    public float RotationSpeed;
    [Tooltip("Wait time in each point")]
    public float WaitTimeContinueMoving;
    [Tooltip("Wait time in each point")]
    public float WaitTimeTriggerAlarm;


    // Some Hard Math
    [Space]
    [Header("FOV Mesh visualization")]
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    public TextMesh timeViewer;
    private Mesh viewMesh;
    private float inViewTime = 0;

    private EnemyChase attack;


    private bool isSecurityCamera = false;
    private float currentWaitTimeMovementCamera = 0;
    private float currentWaitTimeTriggerAlarm = 0;
    private float currentSide;
    private bool StartCountDownAlarm = false;



    // Activate enemy and start coroutine that search for a target with the delay
    private void OnEnable()
    {
        currentSide = LeftDirection;

        // Se for camera de seguranca o campo de visão vai rotacionar entre dois
        // waypoints de tempos em tempos (corotina)
        if (this.transform.parent.tag == "Security Camera"){
            isSecurityCamera = true;
            //StartCoroutine(RotateSecurityCamera());
        }

        attack = this.GetComponentInParent<EnemyChase>();

        //viewMesh = new Mesh();
        //viewMesh.name ="View Mesh";
        //viewMeshFilter.mesh = viewMesh;

      //  StartCoroutine("FindTargetsWithDelay", viewDelay);
    }

    // Add targets being saw
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] oldTargetsInViewRadious = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        //Collider2D[] newTargetInViewRadious = Physics2D.OverlapBoxAll(transform.position, new Vector2(viewRadius,5f),0,targetMask);
        

        for (int i = 0; i < oldTargetsInViewRadious.Length; i++) //Change to old back if error
        {
            Transform target = oldTargetsInViewRadious[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            //Debug.DrawLine(transform.position,target.position,Color.red,0.3f);

            if(Vector3.Angle(transform.up, dirToTarget) < viewAngle/2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics2D.Raycast(transform.position,dirToTarget,dstToTarget,obstacleMask))
                {
                    visibleTargets.Add(target);
                    _lastTargetLocation = target.transform.position;

                    if (isSecurityCamera){
                        StartCountDownAlarm = true;
                    }
                }
            }
        }
    }


    void WaitTimeToContinue(){
        
        if (currentWaitTimeMovementCamera < WaitTimeContinueMoving)
        {
            currentWaitTimeMovementCamera += Time.deltaTime;
        }
        else
        {
            if (currentSide == LeftDirection){
                currentSide = RightDirection;
            } else {
                currentSide = LeftDirection;
            }

            currentWaitTimeMovementCamera = 0;
        }
    
    }

    private void Update()
    {
        // Draw FOV
        //DrawFOV();

        // Search for targets
        FindVisibleTargets();

        // Check if have any visible targets
        if(visibleTargets.Count > 0)
        {
            inViewTime += Time.deltaTime;

            // show line pointing towards target
            /*
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LineRenderer>().SetPosition(0,this.transform.position);
            GetComponent<LineRenderer>().SetPosition(1,visibleTargets[0].position);
            */

        } else {

            inViewTime = 0;
            GetComponent<LineRenderer>().enabled = false;
        }

        // Rotate security camera
        if (isSecurityCamera && this.enabled)
        {
            // Rotaciona a camera, quando chega no ponto ela para, roda o tempo e quando o tempo termina
            // ele contina a rotacao da camera
            if (currentSide == LeftDirection && transform.eulerAngles.z - currentSide >= 5.0f) {
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, LeftDirection), Time.deltaTime * RotationSpeed);
            
            } else if (currentSide == RightDirection && currentSide - transform.eulerAngles.z >= 5.0f){
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, RightDirection), Time.deltaTime * RotationSpeed);

            } else {
                WaitTimeToContinue();
            }

            if (StartCountDownAlarm){

                currentWaitTimeTriggerAlarm += Time.deltaTime;

                if (currentWaitTimeTriggerAlarm >= WaitTimeTriggerAlarm){
                    currentWaitTimeTriggerAlarm = 0;
                    StartCountDownAlarm = false;

                    foreach (Transform target in visibleTargets){
                        if (target.tag == "Player"){
                            GameObject box = target.GetComponent<PlayerComponent>().nearBox;

                            //o que é esse if? Porque a funcao checkbox eh usada aqui? (ass. Krauss)
                            //o que isso tudo tem a ver com o StartCountDownAlarm?
                            if (box && box.GetComponent<PushObject>().holdingBox){
                                box.GetComponent<PushObject>().checkBox(target.gameObject);
                            }
                            target.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        /*if(inViewTime < maxInViewTime - 1f) {

            // Nothing is happening
            Debug.Log("Chillin");
            timeViewer.text = inViewTime.ToString("0.00");

        } else {

            // Is seeing danger
            Debug.Log("Seeing target");

            timeViewer.text = "DANGER";
        }*/


        if(inViewTime > maxInViewTime)
        {
            foreach(Transform visObj in visibleTargets)
            {
                if(visObj.GetComponent<PlayerComponent>())
                {
                    // Start following Player

                    if (!isSecurityCamera) {
                        attack.StartChasingTarget();
                    }
                    // Kills her if touches her
                    //visObj.gameObject.SetActive(false);
                }

            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(viewRadius,5,0));
    }


    // Adjust FOV for corners in obstacles
    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    // Update info about FOV in game 
    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);

        if (hit)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    // Adjust angles of FOV
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),0);
    }

    // info about the FOV
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point,float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    // Info about the edges of the obstacles
    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
