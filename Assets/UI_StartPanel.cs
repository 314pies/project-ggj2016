using UnityEngine;

public class UI_StartPanel : MonoBehaviour
{
    private void Awake()
    {
        EventManager.StartListening("OnEnterGameState", delegate { gameObject.SetActive(false); });
    }
}
