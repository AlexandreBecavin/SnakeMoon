using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SnakeWallDetector : MonoBehaviour
{
    public float wallActivationDistance = 3f; // Distance à partir de laquelle les murs seront activés

    public GameObject[] walls; // Tableau contenant tous les murs

    public SnakeController snakeController;
    void Update()
    {
        foreach (GameObject wall in walls)
        {
            float distance = Vector3.Distance(snakeController.PositionsHistory[0], wall.transform.position);

            // Active/désactive le mur en fonction de la distance
            wall.SetActive(distance < wallActivationDistance);
        }
    }
}