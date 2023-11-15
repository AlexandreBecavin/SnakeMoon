using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killSnake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<Collider> ().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Snake"))
    //     {
    //         snakeController snake = other.GetComponent<snakeController>();

    //         if (snake != null)
    //         {
    //             snake.moveSpeed = 0f;
    //             Debug.Log("Snake a été tué!");
    //         }
    //     }
    // }
}
