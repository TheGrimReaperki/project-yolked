using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float rotationSpeed = 750f;

    Quaternion targetRotation;

    cameraController cameraController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<cameraController>();
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        var moveInput = (new Vector3(h, 0, v)).normalized; //set vector length to 1, keeps diagonal movement same speed

        var moveDirection = cameraController.planarRotation * moveInput;

        if(moveAmount > 0) //if character is moving
        {
            this.transform.position += moveDirection * playerSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDirection); //rotate egg to look where you are going
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 100 * Time.deltaTime); //smoothly rotate player, according to rotationSpeed
        
    }
}
