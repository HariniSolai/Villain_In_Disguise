using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy; 
    [SerializeField] private Button spawnButton;

    void Start()
    {
        if (enemy != null)
        {
            SetVisible(enemy, false); // hide enemy at start
        }

        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(() =>
            {
                if (enemy != null)
                {
                    SetVisible(enemy, true); // show enemy when button clicked
                }
            });
        }
    }
    private void SetVisible(GameObject obj, bool visible)
    {
        foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = visible;
        }
    }
}