using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCoordinator : MonoBehaviour
{
    public GameObject skillsMenu;

    private Character player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Character>();
        HideSkillsMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCoordinator()
    {
        // This is run when the player is asking what to do.
        ShowSkillsMenu();
        Camera.main.GetComponent<CameraFollow>().FollowCursor();
    }

    public void ShowSkillsMenu()
    {
        if (!skillsMenu.activeSelf)
        {
            skillsMenu.SetActive(true);
        }
        // Spawn in available buttons.
    }

    public void HideSkillsMenu()
    {
        skillsMenu.SetActive(false);
    }

    // This will pseudo spawn in a wall, and let the player choose where to put it.
    public void OutlineBuildItem(GameObject prefab)
    {
        // hover the game object in the worldspace

    }

    public void SendEndTurn()
    {
        HideSkillsMenu();
        player.EndTurn();
    }

}
