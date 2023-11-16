using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public GameObject[] spikeModels; // Tableau de modèles de pic
    public float intervalBetweenSpikes = 5f; // Intervalle entre les pics
    public float warningTime = 3f; // Temps de préavis avant que le pic ne sorte
    public float spikeHeight = 50f; // Hauteur du pic lorsqu'il est sorti
    public float groundLevel = 0f; // Niveau du sol
    public float transitionDuration = 1f; // Durée de la transition
    private List<int> selectedIndices = new List<int>(); // Liste des indices des pics à modifier


    public GameObject warningSurfacePrefab; // Préfabriqué de la surface d'avertissement
    public float warningSurfaceDuration = 1.5f; // Durée d'affichage de la surface d'avertissement

    private bool spikeActive = false;
    private Vector3[] initialPositions;
    private Vector3[] targetPositions;
    private GameObject warningPlane; // Référence à la surface d'avertissement

    void Start()
    {
        initialPositions = new Vector3[spikeModels.Length];
        targetPositions = new Vector3[spikeModels.Length];

        // Stocke les positions initiales de chaque pic
        for (int i = 0; i < spikeModels.Length; i++)
        {
            initialPositions[i] = spikeModels[i].transform.position;
            targetPositions[i] = initialPositions[i]; // Initialise les positions cibles à celles initiales
        }

        InvokeRepeating("ToggleSpike", intervalBetweenSpikes, intervalBetweenSpikes);
    }

    void ToggleSpike()
    {
        spikeActive = !spikeActive;


        if (spikeActive)
        {

            // CreateWarningSurface();
            // ShowWarning();
            // Invoke("HideWarning", warningTime);

            selectedIndices.Clear(); // Efface les indices sélectionnés
            // Choisis aléatoirement les indices des pics à modifier
            for (int i = 0; i < spikeModels.Length; i++)
            {
                if (Random.value > 0.5f) // Ajustez la probabilité selon vos besoins
                {
                    selectedIndices.Add(i); // Ajoute l'indice à la liste des indices sélectionnés
                }
            }



            foreach (int index in selectedIndices)
            {
                targetPositions[index] = new Vector3(initialPositions[index].x, initialPositions[index].y + spikeHeight, initialPositions[index].z);
            }
        }
        else
        {
            // Définit les positions cibles au niveau du sol pour les pics sélectionnés
            foreach (int index in selectedIndices)
            {
                targetPositions[index] = new Vector3(initialPositions[index].x, initialPositions[index].y, initialPositions[index].z);
            }

            //DestroyWarningSurface();
        }
    }

    void Update()
    {
        // Effectue une transition douce vers les positions cibles
        for (int i = 0; i < spikeModels.Length; i++)
        {
            spikeModels[i].transform.position = Vector3.Lerp(spikeModels[i].transform.position, targetPositions[i], Time.deltaTime / transitionDuration);
        }
    }

    void CreateWarningSurface()
    {
        // Crée la surface d'avertissement au-dessus des pics
        warningSurfacePrefab = Instantiate(warningSurfacePrefab, new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.identity);
        Destroy(warningSurfacePrefab, warningSurfaceDuration); // Détruit la surface après la durée spécifiée
    }

    void DestroyWarningSurface()
    {
        // Détruit la surface d'avertissement si elle existe encore
        if (warningSurfacePrefab != null)
        {
            Destroy(warningSurfacePrefab);
        }
    }


    void ShowWarning()
    {
        // Effectuez ici l'avertissement visuel si nécessaire
    }

    void HideWarning()
    {
        // Arrêtez l'avertissement visuel ici si nécessaire
    }
}