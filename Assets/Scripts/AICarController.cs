using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class AICarController : MonoBehaviour
{
    public List<Transform> AICheckpoints;
    public float movementSpeed = 10f;
    public AICheckpointContainer AICheckpointContainer;
    private CarController carController;
    private int currentCheckpointIndex = 0;

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
    }

    void Update()
    {
        MoveToCheckpoint();
    }

    private void MoveToCheckpoint()
    {
        if (AICheckpoints.Count == 0)
        {
            Debug.LogWarning("No checkpoints assigned to AI car.");
            return;
        }

        Transform currentCheckpoint = AICheckpoints[currentCheckpointIndex];

        // Move towards the checkpoint
        transform.position = Vector3.MoveTowards(transform.position, currentCheckpoint.position, movementSpeed * Time.deltaTime);

        // Check if the car has reached the checkpoint
        if (Vector3.Distance(transform.position, currentCheckpoint.position) < 0.1f)
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
}