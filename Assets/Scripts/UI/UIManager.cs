using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<SimpleUI> uis = null;

	private void Awake()
    {
        for (int i = 0; i < uis.Count;++i)
        {
            uis[i].Initialize();
        }
    }
}
