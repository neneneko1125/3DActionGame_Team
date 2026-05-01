using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image HPBarGreen;
    public Image HPBarRed;

    public static UIManager Instance {  get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
