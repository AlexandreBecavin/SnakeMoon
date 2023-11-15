using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float SteerSpeed = 180;
    public int Gap = 80;

    public GameObject BodyPrefab;
    public GameObject TailPrefab;


    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();  

    // Start is called before the first frame update
    void Start()
    {    
        GrowSnake();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        PositionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            
            if (index > 1) {
                body.tag = "killSnake";
            }
            index++;
        }
    }

    private void GrowSnake() {
        if (BodyParts.Count >= 1) {
            GameObject.Destroy(BodyParts[BodyParts.Count - 1]);
            BodyParts.RemoveAt(BodyParts.Count - 1);
            GameObject body = Instantiate(BodyPrefab, new Vector3(0f, 7f, 0f), Quaternion.identity);
            BodyParts.Add(body);
        }

        GameObject tail = Instantiate(TailPrefab, new Vector3(0f, 7f, 0f), Quaternion.identity);
        BodyParts.Add(tail);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("killSnake"))
        {
            moveSpeed = 0;
            Debug.Log("Snake à été tué!");
        }

        if (other.CompareTag("eatSnake"))
        {
            Destroy(other.gameObject);
            GrowSnake();
        }
    }
}
