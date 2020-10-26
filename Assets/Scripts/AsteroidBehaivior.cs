using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private AsteroidsEnum colorBehaviour;
    [SerializeField] private AsteroidsEnum sizeBehaviour;
    [SerializeField] private AsteroidsEnum rotateBehaviour;

    private AsteroidsEnum _asteroidBehavior;

    private void Start()
    {
        _asteroidBehavior = colorBehaviour | sizeBehaviour | rotateBehaviour;

        if (_asteroidBehavior.HasFlag(AsteroidsEnum.BigSize)) gameObject.transform.localScale = new Vector3(2, 2, 2);
        if (_asteroidBehavior.HasFlag(AsteroidsEnum.RedColor)) GetComponent<Material>().color = Color.red;
        if (_asteroidBehavior.HasFlag(AsteroidsEnum.RotateY)) gameObject.transform.Rotate(0, 20 * Time.deltaTime, 0);
    }
}