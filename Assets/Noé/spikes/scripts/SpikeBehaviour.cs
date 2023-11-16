using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpikeController : MonoBehaviour
{
    public GameObject SpikePrefab;

    public GameObject WarningPrefab;

    public SnakeController snakeController;

    public Vector3 minPosition = new Vector3(-9.5F, -3F, -9.5F);
    public Vector3 maxPosition = new Vector3(9.5F, -3F, 9.5F);

    private List<GameObject> spikes = new List<GameObject>();
    private List<GameObject> warningSurfaces = new List<GameObject>();

    public float warningSurfaceDuration = 1.5f; // Durée d'affichage de la surface d'avertissement

    public float groundLevel = 1f; // Niveau du sol

    public float warningTime = 3f; // Temps de préavis avant que le pic ne sorte

    public float intervalBetweenSpikes = 5f; // Intervalle entre les pics
    public float transitionDuration = 1f; // Durée de la transition

    void Start()
    {
        InvokeRepeating("ToggleSpike", intervalBetweenSpikes, intervalBetweenSpikes);
    }

    void ToggleSpike()
    {

        clear();


        for (int i = 0; i < Random.Range(1, 15); i++)
        {
            Vector3 coords = new Vector3(Random.Range(minPosition.x, maxPosition.x), -1f, Random.Range(minPosition.z, maxPosition.z));
            while (!snakeController.IsVectorFarEnough(coords, 2F))
            {
                coords = new Vector3(Random.Range(minPosition.x, maxPosition.x), -1f, Random.Range(minPosition.z, maxPosition.z));
            }
            GameObject spike = Instantiate(SpikePrefab, coords, Quaternion.Euler(-90f, 0f, 0f));
            spikes.Insert(0, spike);

            GameObject warning = Instantiate(WarningPrefab, coords, Quaternion.identity);
            Vector3 warningPosition = warning.transform.position;
            warningPosition.y = 0.5f;
            warning.transform.position = warningPosition;
            warningSurfaces.Insert(0, warning);
        }


        StartCoroutine(RaiseSpikes());
    }



    IEnumerator RaiseSpikes()
    {
        yield return new WaitForSeconds(2f);

        foreach (var warningSurface in warningSurfaces)
        {
            Destroy(warningSurface);
        }

        foreach (var spike in spikes)
        {
            Vector3 spikePosition = spike.transform.position;
            spikePosition.y = 1f;
            StartCoroutine(MoveSpike(spike, spikePosition));
        }

        yield return new WaitForSeconds(2f);
        foreach (var spike in spikes)
        {
            Vector3 spikePosition = spike.transform.position;
            spikePosition.y = -3f;
            StartCoroutine(MoveSpike(spike, spikePosition));
        }

    }

    IEnumerator MoveSpike(GameObject spike, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = spike.transform.position;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            spike.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / transitionDuration);
            yield return null;
        }

        spike.transform.position = targetPosition;
    }



    void clear()
    {
        if (spikes.Count > 0)
        {
            foreach (var spike in spikes)
            {
                Destroy(spike);
            }
            spikes.Clear();
        }
    }

    void Update()
    {

    }


}