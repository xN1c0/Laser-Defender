using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /* Player Movement */
    [Header("Player Movement")]
    [SerializeField] float movementSpeed;
    [SerializeField] float health = 200;

    /* Player Projectile */
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileFiringPeriod;

    /* Shooting Coroutine */
    Coroutine firingRoutine;

    /* Player Padding */
    [SerializeField]  float xAxisPadding;
    [SerializeField]  float yAxisPadding;

    /* Player Movement Boundaries */
    float leftBound;
    float rightBound;
    float topBound;
    float bottomBound;

    [SerializeField] AudioClip dieClip;
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0, 1)] float dieClipVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float shootingClipVolume = 0.7f;

    Level level;


    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        SetUpPlayerData();
        SetUpPlayerMovementBuondaries();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
        PlayerShooting();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(dieClip, Camera.main.transform.position, dieClipVolume);
            Destroy(gameObject);
            level.LoadGameOverScene();
        }
    }

    public float GetHealth()
    {
        return health;
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
            firingRoutine = StartCoroutine(FiringLoop());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingRoutine);
        }
    }

    IEnumerator FiringLoop()
    {
        while(true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingClipVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
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
        projectileFiringPeriod = 0.1f;
        xAxisPadding = 0.6f;
        yAxisPadding = 0.4f;

        if(projectilePrefab == null)
        {
            Debug.LogError("Player Projectile is missing");
        }
    }
   
    /*
     * Basic structure of a Coroutine - to call it StartCoroutine(name())
     * 
    IEnumerator TestCoroutine()
    {
        Debug.Log("In TestCoroutine BEFORE YIELD");
        yield return new WaitForSeconds(3);
        Debug.Log("In TestCoroutine AFTER YIELD 3 SECONDS LATER");
    }
    */
}
