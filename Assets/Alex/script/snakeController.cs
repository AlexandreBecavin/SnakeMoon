using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float SteerSpeed = 180;
    public int Gap = 12;
    public GameObject BodyPrefab;
    public GameObject TailPrefab;
    public GameObject FruitsPrefab;
    public GameObject GoldFruitsPrefab;

    public Score score;
    public RestartButton RestartButton;

    public MenuMusicController menuMusicController;
    public Vector3 minPosition = new Vector3(-9.5F, 0.5F, -9.5F);
    public Vector3 maxPosition = new Vector3(9.5F, 0.5F, 9.5F);


    private List<GameObject> BodyParts = new List<GameObject>();
    public List<Vector3> PositionsHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        addFruits(FruitsPrefab);
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
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[index * Gap];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            body.transform.LookAt(point);

            if (index > 1)
            {
                body.tag = "killSnake";
            }
            index++;
        }

        if (PositionsHistory.Count > index * Gap + Gap + 1)
        {
            PositionsHistory.RemoveRange(index * Gap + Gap + 1, PositionsHistory.Count - (index * Gap + Gap + 1));
        }
    }

    private void GrowSnake()
    {
        if (BodyParts.Count >= 1)
        {
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
            SceneManager.LoadScene(0);
            Debug.Log("Snake à été tué!");
            moveSpeed = 0;
            menuMusicController.StartMusic("deathPlayer");
            menuMusicController.StartMusic("gameOver");
        }

        if (other.CompareTag("eatSnake"))
        {
            GrowSnake();
            menuMusicController.StartMusic("eat");
            if (other.name.Equals("Watermelon_512(Clone)")) {
                score.increaseScore(1);
                addFruits(FruitsPrefab);
                goldFruits();
            } else {
                score.increaseScore(5);
                menuMusicController.StartMusic("goldenMelon");
            }
            Destroy(other.gameObject);
        }
    }

    private void addFruits(GameObject prefab)
    {
        Vector3 coords = new Vector3(Random.Range(minPosition.x, maxPosition.x), minPosition.y, Random.Range(minPosition.z, maxPosition.z));
        while (!IsVectorFarEnough(coords, 2F))
        {
            coords = new Vector3(Random.Range(minPosition.x, maxPosition.x), minPosition.y, Random.Range(minPosition.z, maxPosition.z));
        }
        Instantiate(prefab, coords, Quaternion.identity);
    }

    private void goldFruits()
    {
        if (Random.Range(1, 20) == 1) {
            addFruits(GoldFruitsPrefab);
        }
    }


    public bool IsVectorFarEnough(Vector3 newVector, float minDistance)
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            if (Vector3.Distance(newVector, PositionsHistory[i * Gap]) < minDistance)
            {
                Debug.Log("Distance not good");
                return false;
            }
        }
        return true;
    }
}