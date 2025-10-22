using TMPro;
using UnityEngine;

public class MissileUI : MonoBehaviour
{
    [SerializeField] private string format = "{0}";
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        int currentMissiles = Finder.GameController.GetCurrentMissileCount();
        text.text = string.Format(format, currentMissiles);
    }
}
