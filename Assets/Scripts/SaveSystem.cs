using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("MechanicRescued", NPCManager.instance.mechanicRescued ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        NPCManager.instance.mechanicRescued = PlayerPrefs.GetInt("MechanicRescued", 0) == 1;
    }
}
