using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private GameObject center;
    [SerializeField] private Vector2 smoothedVelocity;
    [SerializeField] private Vector2 currentLookingPos;
    [SerializeField] private float maxDistance = 40;
    [SerializeField] private float minDistance = 5;
    [SerializeField] private float currentDistance = 40;
    [SerializeField] private float proximitySpeed = 0.1f;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputValues = Vector2.Scale(inputValues, new Vector2(mouseSensitivity, mouseSensitivity));
        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f);
        currentLookingPos += smoothedVelocity;

        center.transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
        currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -80f, 90f);

        playerBody.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, playerBody.transform.up);

        RaycastHit hit;

        Debug.DrawRay(center.transform.position, transform.TransformDirection(Vector3.back) * currentDistance, Color.green);
        if (Physics.Raycast(center.transform.position, transform.TransformDirection(Vector3.back), out hit, currentDistance + proximitySpeed))
        {
            Debug.DrawRay(center.transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            if(currentDistance > minDistance) currentDistance -= proximitySpeed;
        }
        else
        {
            if (currentDistance < maxDistance)
            {
                if (!Physics.Raycast(center.transform.position, transform.TransformDirection(Vector3.back), out hit, currentDistance + (proximitySpeed * 2)))
                {
                    currentDistance += proximitySpeed;
                }
            }
        }
        this.transform.localPosition = new Vector3(0, 0, -currentDistance);
    }
}
