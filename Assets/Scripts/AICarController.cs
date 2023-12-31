using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    //private DifficultyLevel currentDifficulty = DifficultyLevel.Hard;
    public List<Transform> AICheckpoints;
    public float initialMovementSpeed = 2f; 
    public float maxMovementSpeed = 20f;
    public float accelerationExponent = 2f; 
    public float checkpointProximity = 5f; 
    public AICheckpointContainer AICheckpointContainer;

    private CarController carController;
    private int currentCheckpointIndex = 0;
    private float currentMovementSpeed;

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    // Settings
    private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private bool canMove = false; // Flag to control when the car can start moving

    /*(public void SetDifficultyLevel(DifficultyLevel difficulty)
    {
        currentDifficulty = difficulty;

        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                maxMovementSpeed = 15f; // Adjust speed for easy difficulty
                break;
            case DifficultyLevel.Medium:
                maxMovementSpeed = 20f; // Adjust speed for medium difficulty
                break;
            case DifficultyLevel.Hard:
                maxMovementSpeed = 25f; // Adjust speed for hard difficulty
                break;
                // Add more cases if needed
        }
    }*/

    void Start()
    {
        carController = GetComponent<CarController>();

        // Access the AICheckpoints from the AICheckpointContainer script
        AICheckpoints = AICheckpointContainer.AICheckpoints;

        if (AICheckpoints.Count == 0)
        {
            Debug.LogWarning("No checkpoints assigned to AI car.");
            return;
        }

        // Set the initial movement speed
        currentMovementSpeed = initialMovementSpeed;

        // Start the delayed movement coroutine
        StartCoroutine(DelayedMovement(5f)); 
    }

    IEnumerator DelayedMovement(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        // Enable movement after the delay
        canMove = true;
    }

    void Update()
    {
        if (!canMove)
            return;

        MoveToCheckpoint();
        UpdateWheels();
        HandleSteering();
    }

    private void MoveToCheckpoint()
    {
        if (AICheckpoints.Count == 0)
        {
            Debug.LogWarning("No checkpoints assigned to AI car.");
            return;
        }

        Transform currentCheckpoint = AICheckpoints[currentCheckpointIndex];

        // Gradually increase the movement speed with a power curve
        float accelerationFactor = Mathf.Pow(currentMovementSpeed / maxMovementSpeed, accelerationExponent);
        currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, maxMovementSpeed, Time.deltaTime * accelerationFactor);

        // Move towards the checkpoint
        transform.position = Vector3.MoveTowards(transform.position, currentCheckpoint.position, currentMovementSpeed * Time.deltaTime);

        // Check if the car is close to the checkpoint
        if (Vector3.Distance(transform.position, currentCheckpoint.position) < checkpointProximity)
        {
            // Move to the next checkpoint
            currentCheckpointIndex = (currentCheckpointIndex + 1) % AICheckpoints.Count;
        }

        // Face the direction of the checkpoint
        Vector3 lookDir = currentCheckpoint.position - transform.position;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
