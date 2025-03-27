using TMPro;
using UnityEngine;

public class TeleportCrystal : MonoBehaviour
{
    public Transform tpPoint;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI missionText;
    public GameObject[] enemies;

    public float messageDuration = 3f;

    private void Start()
    {
        interactText.gameObject.SetActive(false);
        if (missionText) missionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (AllEnemiesDefeated())
            {
                other.transform.position = tpPoint.position;
                ShowMissionText("Crush all the slimes to win!");
            }
            else
            {
                interactText.text = "Defeat the enemy first!";
                interactText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
        }
    }

    private bool AllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) return false;
        }
        return true;
    }

    private void ShowMissionText(string message)
    {
        if (missionText)
        {
            missionText.text = message;
            missionText.gameObject.SetActive(true);
            Invoke(nameof(HideMissionText), messageDuration);
        }
    }

    private void HideMissionText()
    {
        if (missionText) missionText.gameObject.SetActive(false);
    }
}
