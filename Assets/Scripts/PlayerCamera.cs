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

    void Start()
    {
        Cursor.visible = false;
    }


    void Update()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputValues = Vector2.Scale(inputValues, new Vector2(mouseSensitivity, mouseSensitivity));
        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f);
        currentLookingPos += smoothedVelocity;

        center.transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
        currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -80f, 90f);

        playerBody.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, playerBody.transform.up);

        Vector3 newRotation = new Vector3(this.transform.localRotation.x * 40, 0, 0);
    }
}
