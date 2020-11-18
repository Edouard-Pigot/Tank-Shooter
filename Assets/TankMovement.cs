using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed;                
    public float turnSpeed;
    public GameObject ground;
    public GameObject objectToRotate;
    public GameObject visor;
    public Skidmarks skid;
    public GameObject trackL;
    public GameObject trackR;

    private int lastSkidL = -1;
    private int lastSkidR = -1;

    private Quaternion newRot;
    private Quaternion lastRot;

    public string movementAxisName = "Vertical";
    public string turnAxisName = "Horizontal";
    private Rigidbody rigidbody;

    private Vector3 clampedMovement;

    private float movementInputValue;        
    private float turnInputValue;

    Quaternion newPosition;
    Quaternion oldPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
    }

    private void FixedUpdate()
    {
        GetInput();
        Move();
        //Turn();
    }

    private void LateUpdate()
    {
        lastSkidL = skid.AddSkidMark(trackL.transform.position, Vector3.up, 20.0f, lastSkidL);
        lastSkidR = skid.AddSkidMark(trackR.transform.position, Vector3.up, 20.0f, lastSkidR);
    }

    private void GetInput()
    {
        Vector3 movementUpDown = ground.transform.forward * movementInputValue;
        Vector3 movementLeftRight = ground.transform.right * turnInputValue;
        Vector3 totalMovement = movementUpDown + movementLeftRight;
        clampedMovement = totalMovement.normalized * speed;
    }

    private void Move()
    {
        /*Vector3 targetDir = clampedMovement;

        float angle = Vector3.Angle(targetDir, objectToRotate.transform.forward);
        angle = Vector3.Angle(targetDir, objectToRotate.transform.forward);

        float step = turnSpeed * Time.deltaTime;

        if (angle > 90.0f || angle < -90.0f)
        {
            objectToRotate.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(-objectToRotate.transform.forward, targetDir, step, 0.0f));
        } else
        {
            objectToRotate.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(objectToRotate.transform.forward, targetDir, step, 0.0f));
        }

        rigidbody.MovePosition(rigidbody.position + clampedMovement);*/
        transform.Translate(clampedMovement);

        if (movementInputValue == 0 && turnInputValue == 0)
        {
            newPosition = oldPosition;
        } else
        {
            newPosition = Quaternion.Euler(clampedMovement);
        }

        Debug.DrawRay(new Vector3(0, 0, 0), clampedMovement * 10, Color.black, .1f);

        Quaternion target = Quaternion.LookRotation(newPosition.eulerAngles, Vector3.up);
        objectToRotate.transform.rotation = Quaternion.Slerp(objectToRotate.transform.rotation, target, Time.deltaTime * 10);

        if(movementInputValue != 0 && turnInputValue != 0)
        {
            oldPosition = Quaternion.Euler(clampedMovement);
        } else
        {
            oldPosition = newPosition;
        }

        //float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, rot, 0);
    }


    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }
}