using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemies;

    [Header("UI Elements")]
    public GameObject trophy;

    void Start()
    {
        if (trophy) trophy.SetActive(false);
    }

    void Update()
    {
        if (AllEnemiesDefeated())
        {
            if (trophy) trophy.SetActive(true);
        }
        else
        {
            if (trophy) trophy.SetActive(false);
        }
    }

    bool AllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) return false;
        }
        return true;
    }
}
