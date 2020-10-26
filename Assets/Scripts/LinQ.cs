using System.Linq;
using UnityEngine;

public class LinQ : MonoBehaviour
{
    public Transform player;
    private GameObject[] _asteroids;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            var firstAsteroid = _asteroids
                .OrderBy(p => Vector3.Distance(player.transform.position, p.transform.position)).First();
            Destroy(firstAsteroid);
        }
    }
}