using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /* Player Movement */
    [SerializeField] float movementSpeed;

    /* Player Projectile */
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;

    /* Player Padding */
    [SerializeField]  float xAxisPadding;
    [SerializeField]  float yAxisPadding;

    /* Player Movement Boundaries */
    float leftBound;
    float rightBound;
    float topBound;
    float bottomBound;


    // Start is called before the first frame update
    void Start()
    {
        SetUpPlayerData();
        SetUpPlayerMovementBuondaries();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
        PlayerShooting();
    }

    private void PlayerMovment()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        /* Calculating new position based on the Input and restrict position with precalculated boundaries */
        float newCoordX = Mathf.Clamp(transform.position.x + deltaX, leftBound, rightBound);
        float newCoordY = Mathf.Clamp(transform.position.y + deltaY, bottomBound, topBound);
        transform.position = new Vector2(newCoordX, newCoordY);
    }

    private void PlayerShooting()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
        }
    }

    private void SetUpPlayerMovementBuondaries()
    {
        Camera gameCamera = Camera.main;
        leftBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xAxisPadding;
        rightBound = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xAxisPadding;
        bottomBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yAxisPadding;
        topBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yAxisPadding;
    }

    private void SetUpPlayerData()
    {
        movementSpeed = 10f;
        projectileSpeed = 10f;
        xAxisPadding = 0.6f;
        yAxisPadding = 0.4f;

        if(projectilePrefab == null)
        {
            Debug.LogError("Player Projectile is missing");
        }
    }
}
