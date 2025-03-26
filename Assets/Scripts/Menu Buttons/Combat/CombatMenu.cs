using UnityEngine;
using System.Collections;

public class CombatMenu : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
    int currentMenu = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Test()
    {
        Debug.Log(":D");
    }

    public void goToMenu(int menu) {
        menus[currentMenu].SetActive(false);
        currentMenu = menu;
        menus[currentMenu].SetActive(true);

    }
}
