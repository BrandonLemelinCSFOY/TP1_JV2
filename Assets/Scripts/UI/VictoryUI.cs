using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    private GameObject victoryPanel;
    private Text victoryText;
    
    private void Start()
    {
        // Trouver automatiquement les éléments UI
        FindUIElements();
        
        // S'abonner à l'événement de victoire
        Finder.EventChannels.OnGameVictory.AddListener(OnGameVictory);
        
        // Configurer le texte et cacher le panneau au début
        SetupUI();
    }
    
    private void OnDestroy()
    {
        // Se désabonner de l'événement
        if (Finder.EventChannels != null)
        {
            Finder.EventChannels.OnGameVictory.RemoveListener(OnGameVictory);
        }
    }
    
    private void FindUIElements()
    {
        // Le panneau est ce GameObject lui-même
        victoryPanel = gameObject;
        
        // Trouver le texte dans les enfants
        victoryText = GetComponentInChildren<Text>();
    }
    
    private void SetupUI()
    {
        // Configurer le texte de victoire
        if (victoryText != null)
        {
            victoryText.text = "Victoire!";
            victoryText.fontSize = 200;
            victoryText.color = HexToColor("#49FF7A");
        }
        
        // Cacher le panneau au début
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }
    
    private void OnGameVictory()
    {
        // Afficher le panneau de victoire
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        
        Debug.Log("UI: Victoire affichée!");
    }
    
    private Color HexToColor(string hex)
    {
        // Enlever le # si présent
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        // Convertir en couleur
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
        {
            return color;
        }

        // Retourner vert par défaut si la conversion échoue
        return Color.green;
    }
    
    // Méthodes publiques pour les boutons (optionnel)
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}