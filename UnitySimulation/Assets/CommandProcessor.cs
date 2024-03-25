using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour
{

    public GameObject pivot;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isMoving = false;
    private bool isRotating = false;
    public float moveSpeed = 10.0f;
    public float rotateSpeed = 90.0f;

    void Start()
    {
        targetPosition = pivot.transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            pivot.transform.position = Vector3.MoveTowards(pivot.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if(pivot.transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
        if (isRotating)
        {
            pivot.transform.rotation = Quaternion.RotateTowards(pivot.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(pivot.transform.rotation, targetRotation) < 0.1f) // Adjust threshold as needed
            {
                isRotating = false;
            }

        }
    }

    public void ProcessCommand(string command)
    {
        string[] commandParts = command.Split(' ');

        string action = commandParts[0].ToLower();

        if (float.TryParse(commandParts[1], out float value))
        {
            switch (action)
            {
                case "move":
                    Move(value);
                    break;
                case "rotate":
                    Rotate(value);
                    break;
                //add more robot commands here
                default:
                    Debug.LogWarning($"Unknown Command {action}");
                    break;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid command parameter: {commandParts[1]}");
        }
    }

    public void Move(float distance)
    {
        targetPosition = pivot.transform.position + pivot.transform.forward * distance;
        isMoving = true;
        Debug.Log($"Moved robot forward by {distance} units.");
    }

    public void Rotate(float degrees)
    {
        targetRotation = Quaternion.Euler(pivot.transform.eulerAngles + new Vector3(0, degrees, 0));
        isRotating = true;
    }
}
