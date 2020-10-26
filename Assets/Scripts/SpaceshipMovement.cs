using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipMovement : MonoBehaviour
{
    public bool canMove;

    [SerializeField] private GameController gameController;

    [SerializeField] private float originSpeed = 50f;
    [SerializeField] private float sideSpeed = 5f;
    [SerializeField] private float rotateSpeed = 1000f;

    [SerializeField] private float leftBorderPos = -6f;
    [SerializeField] private float rightBorderPos = 6f;
    private Vector3 moveVector;

    private Quaternion originShipRotation;
    private float rotateShipZ;
    private float speed;

    private float x;

    private void Start()
    {
        moveVector = new Vector3(0, 0, 1);
        originShipRotation = transform.rotation;
        speed = originSpeed;
    }


    private void Update()
    {
        var input = Input.GetAxis("Horizontal");

        if (canMove)
        {
            moveVector.z += speed * Time.deltaTime;

            if (Mathf.Abs(input) > .1f)
            {
                // ship tilts in different direction
                rotateShipZ += Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
                rotateShipZ = Mathf.Clamp(rotateShipZ, -40, 40);

                var newRotateShipZ = Quaternion.AngleAxis(-rotateShipZ, Vector3.forward);
                transform.rotation = originShipRotation * newRotateShipZ;

                // movement along x-axis
                x = transform.position.x + input;
                x = Mathf.Clamp(x, leftBorderPos, rightBorderPos);
            }

            // acceleration movement
            if (Input.GetKey(KeyCode.Space))
                speed = originSpeed * 2;
            else
                speed = originSpeed;

            transform.position = new Vector3(x, 0, moveVector.z);
        }
    }

    // Game over from crash with asteroid
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Asteroid")
        {
            Destroy(collision.gameObject);

            gameController.StopGame();
            gameObject.SetActive(false);
            moveVector = new Vector3(0, 0, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            gameController.score++;
            gameController.passedAsteroids++;
        }
    }
}