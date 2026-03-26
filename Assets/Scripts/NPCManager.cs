using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public bool mechanicRescued = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        SaveSystem.instance.LoadGame();
    }
    public void RescueNPC(string npcName)
    {
        if (npcName == "Mechanic")
        {
            mechanicRescued = true;
            SaveSystem.instance.SaveGame();
        }
    }
}
