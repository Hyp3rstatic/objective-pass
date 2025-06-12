using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private Vector2 origin; //the origin of the circle
    private Vector2 target; //what the originObject rotates towards
    private float radius; //the distance between the origin and target
    private float radAngle;
    private float degAngle;

    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        target = targetObject.transform.position;
        origin = transform.position;
        radius = Vector2.Distance(origin, target);

        if (radius == 0)
        {
            return;
        }

        radAngle = Mathf.Asin((target.y - origin.y) / radius)/* * radius + origin.y*/;
        degAngle = radAngle * Mathf.Rad2Deg;

        if (Mathf.Sign(target.x - origin.x) == -1)
        {
            transform.eulerAngles = new Vector3(180f, 180f, (degAngle * -1) - 90);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, degAngle - 90);
        }
    }
}
