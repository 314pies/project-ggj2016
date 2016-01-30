using UnityEngine;

public abstract class SimpleUI : MonoBehaviour
{
    public abstract void Initialize();
    
    protected void Show()
    {
        gameObject.SetActive(true);
    }

    protected void Hide()
    {
        gameObject.SetActive(false);
    }
}