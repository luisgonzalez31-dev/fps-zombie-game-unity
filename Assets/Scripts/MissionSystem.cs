using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    public int zombiesToKill = 30;
    private int zombiesKilled = 0;
    private bool missionComplete = false;

    public TextMeshProUGUI missionText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateMissionUI();
    }

    public void ZombieKilled()
    {
        zombiesKilled++;

        UpdateMissionUI();

        if (zombiesKilled >= zombiesToKill)
        {
            MissionComplete();
        }
    }

    void UpdateMissionUI()
    {
        missionText.text = "Kill Zombies: " + zombiesKilled + " / " + zombiesToKill;
    }

    void MissionComplete()
    {
        missionComplete = true;

        missionText.text = "MISSION COMPLETE! GO TO EXTRACTION";

        GameManager.instance.AddMoney(100);
    }
    public bool IsMissionComplete()
    {
        return missionComplete;
    }
}
