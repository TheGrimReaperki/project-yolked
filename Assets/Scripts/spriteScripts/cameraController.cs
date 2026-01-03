using Unity.Mathematics;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    //to add:
    //change camera distance via mouse scroll, set up minimum and maximum


    [SerializeField] Transform followTarget;

    [SerializeField] public float cameraDistance = 7f;
    [SerializeField] public float cameraHeight = 2f;
    [SerializeField] Vector2 targetOffset;

    [SerializeField] public float minVerticleAngle = -10;
    [SerializeField] public float maxVerticleAngle = 45;

    [SerializeField] float yawSpeed = 2;
    [SerializeField] float pitchSpeed = 2;

    [SerializeField] bool invertYaw;
    [SerializeField] bool invertPitch;

    float rotationX;
    float rotationY;

    float invertXval;
    float invertYval;





    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
       invertXval =  (invertYaw) ? -1 : 1; //if invert is true, then rotation * -1, else rotation * 1
       invertYval =  (invertPitch) ? -1 : 1;

        rotationX -= Input.GetAxis("Mouse Y") * invertYval * pitchSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticleAngle, maxVerticleAngle);
        rotationY += Input.GetAxis("Mouse X") * invertXval * yawSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPosition = followTarget.position + new Vector3(targetOffset.x, targetOffset.y);
        
        transform.position = focusPosition -  targetRotation * new Vector3(0, ( 0 - cameraHeight), cameraDistance);
        transform.rotation = targetRotation;
    }

    public Quaternion planarRotation => Quaternion.Euler(0, rotationY, 0);
}
