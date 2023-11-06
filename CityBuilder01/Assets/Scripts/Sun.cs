using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    public static Quaternion rotation;

    private void Start() {
        rotation = transform.rotation;
    }

    private void Update()
    {
        RotateSun();
    }

    private void RotateSun()
    {
        // Rotate the sun around the Y-axis to simulate a day-night cycle
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.right);

        rotation = transform.rotation;
    }
}
