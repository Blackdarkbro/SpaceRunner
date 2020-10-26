using UnityEngine;

public class MovePlanet : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 1500;

    private void LateUpdate()
    {
        if (!target) return;

        var newPos = new Vector3(transform.position.x, transform.position.y, target.position.z + distance);
        transform.position = newPos;
    }
}