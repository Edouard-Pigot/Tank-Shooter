using UnityEngine;
using System.Collections;
using UnityEngine;

public class TurretLookAt : MonoBehaviour
{
    public GameObject objectToRotate;
    public GameObject visor;

    public float yVisorOffset;

    public GameObject terrain;
    private float distance;

    private int maxRayCastIterations = 10;
    private int currentRayCastIteration = 0;

    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, terrain.transform.position);
    }

    void Update()
    {
        Vector2 mousePos = new Vector2();
        Vector3 point = new Vector3();

        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        point = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        int layerMask = 1 << 9;

        Vector3 heading = point - Camera.main.transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        RaycastHit hit;
        Physics.Raycast(point, direction, out hit, Mathf.Infinity, layerMask);

        Debug.DrawRay(point, direction, Color.yellow);

        Vector3 visorPosition = new Vector3(hit.point.x, hit.point.y + yVisorOffset, hit.point.z);

        visor.transform.position = visorPosition;
        objectToRotate.transform.LookAt(new Vector3(hit.point.x, objectToRotate.transform.position.y, hit.point.z));
    }
}