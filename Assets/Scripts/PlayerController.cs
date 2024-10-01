using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    private int count;
    private int numOfPickups = 4;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI WinText;
    public TextMeshProUGUI VelocityText;
    public TextMeshProUGUI distanceText;
    private GameObject previousClosestNode;
    private LineRenderer lineRenderer;

    void Start()
    {
        count = 0;
        WinText.text = "";
        SetCountText();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    private void Update()
    {
        float closestDistance = float.MaxValue;
        GameObject closestPickup = null; // Currently there is no closest pickup
        
            foreach (var pickup in PickUpManager.pickUps)
            {
                pickup.GetComponent<Renderer>().material.color = Color.green;
                Vector3 currentPlayerPosition = transform.position;
                Vector3 currentPickUpPosition = pickup.transform.position;
                var distanceBetween = Vector3.Distance(currentPlayerPosition, currentPickUpPosition);
                
                if (distanceBetween < closestDistance)
                {
                    closestPickup = pickup;
                    closestDistance = distanceBetween;
                }
            }
            setDistanceText(closestDistance);
            closestPickup.GetComponent<Renderer>().material.color = Color.red;
            // Set the start point position vector
            lineRenderer.SetPosition(0, transform.position);

// Set the end point position vector
            lineRenderer.SetPosition(1, closestPickup.transform.position);

// Set the width of the line at both the start and end
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

    }

    private void setDistanceText(float distance)
    {
        distanceText.text = "Distance: " + distance.ToString("F2");
    }
    private void SetCountText()
    {
        ScoreText.text = "Score: " + count.ToString();
        if (count >= numOfPickups)
        {
            WinText.text = "You Win!";
        }
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    private void SetVelocityText(Vector3 movement)
    {
        var velocity = (movement.magnitude) / Time.deltaTime;
        VelocityText.text = "Velocity: " + velocity.ToString("0.00");
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        var force = movement * speed * Time.fixedDeltaTime;
        // var force = movement * speed;
        SetVelocityText(movement);
        GetComponent<Rigidbody>().AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            count += 1;
            SetCountText();
            print(other.gameObject);
            PickUpManager.DeactivatePickUp(other.gameObject);
        }
    }
}