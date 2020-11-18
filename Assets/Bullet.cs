using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;

    public LayerMask layerMask = -1;

    public int nbOfRicochet = 2;

    void Update()
    {
        Ricochet();
    }

    void FixedUpdate()
    {

    }

    /*void MoveBullet()
    {
        Vector3 movementThisStep = this.transform.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        newPosition = this.transform.position + this.transform.forward * speed;
        newVelocity = this.transform.forward * speed;
        
        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            Debug.Log("pop");
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            {
                Debug.DrawRay(previousPosition, movementThisStep, Color.green, 5f);
                --nbOfRicochet;
                if(nbOfRicochet == 0)
                {
                    Destroy(this.gameObject);
                }
                Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.yellow, 5f);

                Vector3 r = this.transform.forward - 2 * (Vector3.Dot(this.transform.forward, hitInfo.normal)) * hitInfo.normal;
                r = new Vector3(r.x, 0, r.z);

                transform.rotation = Quaternion.LookRotation(r);

                newPosition = hitInfo.point;

                float speed = newVelocity.magnitude;
                newVelocity = transform.forward * speed;
            }
            else
            {
                Debug.DrawRay(previousPosition, movementThisStep, Color.red, 5f);
            }
        }
        previousPosition = this.transform.position;
    }*/

    void Ricochet()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, layerMask))
        {
            --nbOfRicochet;
            if (nbOfRicochet == 0)
            {
                Destroy(this.gameObject);
            }
            Debug.DrawRay(hit.point, hit.normal, Color.red, 5f);
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Vector3 v = Vector3.Reflect(transform.up, collision.contacts[0].normal);
        float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(90, rot, 0);
    }*/
}