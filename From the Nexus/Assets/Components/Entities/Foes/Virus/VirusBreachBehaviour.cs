using System.Collections.Generic;
using UnityEngine;

public class VirusBreachBehaviour : MonoBehaviour
{
    [Header("Affected area")]
    [SerializeField] GameObject infected;
    [SerializeField] GameObject cleansed;

    [Header("Cores")]
    [SerializeField] List<GameObject> cores;

    void Start()
    {
        infected.SetActive(true);
        cleansed.SetActive(false);
    }

    void Update()
    {
        int nullified = 0;

        foreach (GameObject core in cores) if (core == null) nullified++;

        if (nullified >= cores.Count) Cleanse();
    }

    void Cleanse()
    {
        infected.SetActive(false);
        cleansed.SetActive(true);
        Destroy(gameObject);
    }
}
