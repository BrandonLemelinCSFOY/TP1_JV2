using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private Music backgroundMusic;
    [SerializeField] private Music victoryMusic;
    [SerializeField] private Music defeatMusic;

    private void Start()
    {
        // Commencer la musique de fond
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
        
        // S'abonner aux événements
        Finder.EventChannels.OnGameVictory.AddListener(OnGameVictory);
        Finder.EventChannels.OnGameDefeat.AddListener(OnGameDefeat);
    }
    
    private void OnDestroy()
    {
        // Se désabonner des événements pour éviter les erreurs
        if (Finder.EventChannels != null)
        {
            Finder.EventChannels.OnGameVictory.RemoveListener(OnGameVictory);
            Finder.EventChannels.OnGameDefeat.RemoveListener(OnGameDefeat);
        }
    }
    
    private void OnGameVictory()
    {
        // Arrêter la musique de fond et jouer la musique de victoire
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
        
        if (victoryMusic != null)
        {
            victoryMusic.Play();
        }
    }
    
    private void OnGameDefeat()
    {
        // Arrêter la musique de fond et jouer la musique de défaite
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
        
        if (defeatMusic != null)
        {
            defeatMusic.Play();
        }
    }
}
