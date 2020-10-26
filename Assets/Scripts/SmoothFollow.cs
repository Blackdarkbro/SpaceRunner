using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public float normalDistance = 10.0f;
    public float normalHeight = 5.0f;
    public float zoomedDistance = 3.3f;
    public float zoomedHeight = 1.3f;

    public float heightDamping = 1.0f;
    public float distanceDamping = 1.0f;
    public float rotationDamping = 3.0f;
    public Transform target;

    private float distance;

    private float from;
    private float height;
    private float to;

    private void Start()
    {
        distance = normalDistance;
    }

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target) return;

        if (Input.GetButton("Jump"))
        {
            //distance = zoomedDistance;
            from = normalDistance;
            to = zoomedDistance;
            height = zoomedHeight;
        }
        else
        {
            //distance = normalDistance;
            from = zoomedDistance;
            to = normalDistance;
            height = normalHeight;
        }

        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle =
            Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;

        distance = Mathf.Lerp(from, to, distanceDamping);
        pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;

        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
    }
}