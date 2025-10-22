using UnityEngine;
using UnityEngine.UI;

public class DefeatUI : MonoBehaviour
{
    private GameObject defeatPanel;
    private Text defeatText;
    
    private void Start()
    {
        // Trouver automatiquement les éléments UI
        FindUIElements();
        
        // S'abonner à l'événement de défaite
        Finder.EventChannels.OnGameDefeat.AddListener(OnGameDefeat);
        
        // Configurer le texte et cacher le panneau au début
        SetupUI();
    }
    
    private void OnDestroy()
    {
        // Se désabonner de l'événement
        if (Finder.EventChannels != null)
        {
            Finder.EventChannels.OnGameDefeat.RemoveListener(OnGameDefeat);
        }
    }
    
    private void FindUIElements()
    {
        // Le panneau est ce GameObject lui-même
        defeatPanel = gameObject;
        
        // Trouver le texte dans les enfants
        defeatText = GetComponentInChildren<Text>();
    }
    
    private void SetupUI()
    {
        // Configurer le texte de défaite
        if (defeatText != null)
        {
            defeatText.text = "Défaite!";
            defeatText.fontSize = 200;
            defeatText.color = HexToColor("#FF4A56");
        }
        
        // Cacher le panneau au début
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false);
        }
    }
    
    private void OnGameDefeat()
    {
        // Afficher le panneau de défaite
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
        
        Debug.Log("UI: Défaite affichée!");
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

        // Retourner rouge par défaut si la conversion échoue
        return Color.red;
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