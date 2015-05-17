using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour
{

    //Follow target
    public Transform Target;

    private Transform ThisTransform;

    //linear distance to maintain from target (in world units)
    public float DistanceFromTarget = 0.2f;

    //Height of camera above target
    public float CamHeight = 0.4f;


    public float RotationDamp = 4f;
    public float PosDamp = 4f;
    private bool canFollow = false;
    public Camera cameraComponent;

    public bool CanFollow
    {
        get
        {
            return canFollow;
        }
        set
        {
            canFollow = value;
            if (canFollow)
            {
                cameraComponent.enabled = true;
            }
            else
            {
                cameraComponent.enabled = false;
            }
        }
    }


    void Awake()
    {
        ThisTransform = transform;
        cameraComponent = GetComponent<Camera>();
        CanFollow = false;
    }



    void LateUpdate()
    {
        if (CanFollow)
        {
            //Get output velocity
            Vector3 Velocity = Vector3.zero;
            //Calculate rotation interpolate
            ThisTransform.rotation =
            Quaternion.Slerp(ThisTransform.rotation, Target.rotation,
            RotationDamp * Time.deltaTime);
            //Get new position
            Vector3 Dest = ThisTransform.position =
            Vector3.SmoothDamp(ThisTransform.position, Target.position, ref Velocity, PosDamp * Time.deltaTime);
            //Move away from target
            ThisTransform.position = Dest - ThisTransform.forward *
            DistanceFromTarget;
            //Set height
            ThisTransform.position = new
            Vector3(ThisTransform.position.x, CamHeight,
            ThisTransform.position.z);
            //Look at dest
            ThisTransform.LookAt(Dest);
        }
    }
}
