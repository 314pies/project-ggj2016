using UnityEngine;

public class UI_FailedPanel : MonoBehaviour
{
    private void Awake()
    {
        EventManager.StartListening("OnEnterFailState", delegate { gameObject.SetActive(true); });
        EventManager.StartListening("OnEnterGameState", delegate { gameObject.SetActive(false); });

        gameObject.SetActive(false);
    }
}