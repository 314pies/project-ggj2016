using UnityEngine;

public class UI_SuccessPanel : MonoBehaviour
{
    private void Awake()
    {
        EventManager.StartListening("OnEnterSuccessState", delegate { gameObject.SetActive(true); });
        EventManager.StartListening("OnEnterGameState", delegate { gameObject.SetActive(false); });

        gameObject.SetActive(false);
    }
}