using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private SpawnManagerX spawnManagerXScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        // Find the player goal object by its tag
        playerGoal = GameObject.FindGameObjectWithTag("PlayerGoal");

        // Corrected GetComponent call
        spawnManagerXScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        speed = spawnManagerXScript.enemySpeed;

        // Check if the playerGoal is found
        if (playerGoal == null)
        {
            Debug.LogError("Player Goal not found. Make sure it has the correct tag or is present in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if playerGoal is assigned
        if (playerGoal != null)
        {
            // Set enemy direction towards player goal and move there
            Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Player Goal is not assigned. Please check for errors in the scene.");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.CompareTag("EnemyGoal") || other.gameObject.CompareTag("PlayerGoal"))
        {
            Destroy(gameObject);
        }
    }
}
