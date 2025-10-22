using UnityEngine;
using UnityEngine.Events;

// TODO : Ajouter tous vos canaux événementiels ici.
//        Consultez les notes de cours si vous avez oublié comment faire.
public class EventChannels : MonoBehaviour
{
    [Header("Game Events")]
    public UnityEvent OnGameVictory;
    public UnityEvent OnGameDefeat;
    public UnityEvent<Portal> OnPortalDestroyed;
    public UnityEvent<SpaceMarine> OnPlayerDeath;
}