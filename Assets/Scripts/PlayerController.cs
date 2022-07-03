using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0f;
    public TextMeshProUGUI countText;
    public GameObject victoryText;
    public Vector3 originalPosition;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializeCount();
        originalPosition = gameObject.transform.position;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void InitializeCount()
    {
        count = 0;
        SetCountText();
        victoryText.SetActive(false);
    }

    void IncrementCount()
    {
        count++;
        SetCountText();
        if (count > 12)
        {
            victoryText.SetActive(true);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = originalPosition;
        }
    }

    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup")) 
        {
            other.gameObject.SetActive(false);
            IncrementCount();
        }
    }
}
