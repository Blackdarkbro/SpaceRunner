using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static int dificultyCoef = 0;
    [SerializeField] private GameObject asteroidPrefab;

    public int leftBorderPos = -6;
    public int rightBorderPos = 6;

    private void OnEnable()
    {
        var asteroidAppearChance = Random.Range(1, 8 - dificultyCoef);
        var asteroidCountChance = Random.Range(1, 10 - dificultyCoef);

        if (asteroidAppearChance >= 2) return;
        if (asteroidCountChance < 2)
        {
            var chance = Random.Range(1, 3);
            switch (chance)
            {
                case 1:
                    SpawnTwoAsteroidsBeside();
                    break;
                case 2:
                    SpawnTwoAsteroidsSpaceBetween();
                    break;
            }
        }
        else
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        var middleOfBlockZ = transform.position.z - GetComponent<BoxCollider>().bounds.size.z / 2;
        var asteroidPos = new Vector3(Random.Range(leftBorderPos, rightBorderPos), 1.5f, middleOfBlockZ);

        var asteroid = Instantiate(asteroidPrefab, asteroidPos, Quaternion.identity);

        asteroid.transform.SetParent(gameObject.transform, true);
    }

    private void SpawnTwoAsteroidsSpaceBetween()
    {
        var middleOfBlockZ = transform.position.z - GetComponent<BoxCollider>().bounds.size.z / 2;

        var asteroid1Pos = new Vector3(leftBorderPos + 1, 1.5f, middleOfBlockZ);
        var asteroid2Pos = new Vector3(rightBorderPos - 1, 1.5f, middleOfBlockZ);

        var asteroid1 = Instantiate(asteroidPrefab, asteroid1Pos, Quaternion.identity);
        var asteroid2 = Instantiate(asteroidPrefab, asteroid2Pos, Quaternion.identity);

        asteroid1.transform.SetParent(gameObject.transform, true);
        asteroid2.transform.SetParent(gameObject.transform, true);
    }

    private void SpawnTwoAsteroidsBeside()
    {
        var middleOfBlockZ = transform.position.z - GetComponent<BoxCollider>().bounds.size.z / 2;

        var side = Random.Range(1, 3);

        Vector3 asteroid1Pos;
        Vector3 asteroid2Pos;

        asteroid1Pos = side == 1
            ? new Vector3(leftBorderPos + 1, 1.5f, middleOfBlockZ)
            : new Vector3(rightBorderPos - 1, 1.5f, middleOfBlockZ);

        asteroid2Pos = side == 1
            ? new Vector3(leftBorderPos + 6, 1.5f, middleOfBlockZ)
            : new Vector3(rightBorderPos - 6, 1.5f, middleOfBlockZ);

        var asteroid1 = Instantiate(asteroidPrefab, asteroid1Pos, Quaternion.identity);
        var asteroid2 = Instantiate(asteroidPrefab, asteroid2Pos, Quaternion.identity);

        asteroid1.transform.SetParent(gameObject.transform, true);
        asteroid2.transform.SetParent(gameObject.transform, true);
    }
}