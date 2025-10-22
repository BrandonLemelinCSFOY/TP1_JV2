using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private string format = "{0}";
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        // Trouver le joueur SpaceMarine pour obtenir sa santé actuelle
        var spaceMarine = GameObject.FindWithTag("Player")?.GetComponent<SpaceMarine>();
        if (spaceMarine != null)
        {
            int currentHealth = spaceMarine.GetCurrentHealth();
            text.text = string.Format(format, currentHealth);
        }
        else
        {
            text.text = string.Format(format, 0);
        }
    }
}
