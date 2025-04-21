using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public enum CombatState
{
    PlayerTurn,
    AttackCommence,
    ItemCommence,
    EnemyTurn,
}

public enum PlayerAttack
{
    Hug,
    Push,
    Tease
}



public class Combat : MonoBehaviour
{
    public static Combat Instance { get; private set; }
    private int GameCombatNumb = 0;
    public TextDisplay display;

    //[SerializeField] private bool IsPlayerTurn;

    public GameObject inputBlockerPanel;

    [SerializeField] private int playerMaxHP;
    [SerializeField] private int playerCurrHP;
    public UnityEngine.UI.Image PlayerCurrBar;
    [SerializeField] private int enemyMaxHP;
    [SerializeField] private int enemyCurrHP;
    public UnityEngine.UI.Image EnemyCurrBar;

    [SerializeField] private bool playerIsWeakened;
    [SerializeField] private bool playerIsBuffed;

    [SerializeField] private GameObject sliderBarPrefab;
    [SerializeField] private Text enemyDescriptionText;
    private AttackBarController[] activeAttackBarControllers = new AttackBarController[3];




    private Enemy[] enemyArray = new Enemy[4];

    public static EnemyAction ba1 = new EnemyAction("Spoon", EnemyActionType.ATTACK, 6);
    public static EnemyAction ba2 = new EnemyAction("Cake", EnemyActionType.HEAL, 5);
    public static EnemyAction ba3 = new EnemyAction("Mix", EnemyActionType.BUFF, 0);
    public static EnemyAction bba1 = new EnemyAction("Push", EnemyActionType.ATTACK, 5);
    public static EnemyAction bba2 = new EnemyAction("Slap", EnemyActionType.ATTACK, 8);
    public static EnemyAction bba3 = new EnemyAction("Condemn", EnemyActionType.WEAKEN, 0);
    public Enemy Buck = new Enemy("Buck", 30, ba1, ba2, ba3, "Buck the Buck is the friendly neighborhood baker, and the owner of Doenuts. His baking is legendary; if he gets low on health, he might just grab a pastry and heal himself back up!");
    public Enemy Otter = new Enemy("Bibbleboo", 40, bba1, bba2, bba3, "Bibbleboo is one of Meanie's best friends. She's a little shy. If Meanie thinks too hard about it, she might lose her fighting spirit and get a debuff...");

    //flow
    CombatState currentState;
    public bool isPlayerTurn;
    public static int tempSliderRead;
    public int[] hitLogs = new int[3];
    //ui refs
    public GameObject barPrefab;


    //prefab of the slider eric made 
    public GameObject sliderPrefab;

    public float barSpeed = 15.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetupBattle();

        //flow logic
        //isPlayerTurn = true;
        currentState = CombatState.PlayerTurn;  
    }

    void SetupBattle()
    {
        SetInputBlocked(false);
        
        enemyArray[0] = Buck;
        enemyArray[1] = Otter;

        enemyDescriptionText.text = enemyArray[GameCombatNumb].CombatInfo;
        playerMaxHP = 25;
        playerCurrHP = playerMaxHP;
        PlayerCurrBar.fillAmount = (float)playerCurrHP / (float)playerMaxHP;

        enemyMaxHP = enemyArray[GameCombatNumb].HP;
        enemyCurrHP = enemyMaxHP;
        EnemyCurrBar.fillAmount = (float)enemyCurrHP / (float)enemyMaxHP;
    }

    private void Update() 
    {
        
    }

    public void AttackButton1Press()
    {
        DoPlayerAttack(PlayerAttack.Push);
    }

    public void AttackButton2Press()
    {
        DoPlayerAttack(PlayerAttack.Hug);
    }

    public void AttackButton3Press()
    {
        DoPlayerAttack(PlayerAttack.Tease);
    }



    public static void SetTempSliderRead(int i) => tempSliderRead = i;


    

    void SetInputBlocked(bool isBlocked)
    {
        inputBlockerPanel.SetActive(isBlocked);
    }

    //button calls
    public void DoPlayerAttack(PlayerAttack chosenAttack)
    {
        currentState = CombatState.AttackCommence;

        int damage = 0;
        int crit = 0;
        int amount = 0;
        switch (chosenAttack)
        {
            case (PlayerAttack.Hug):
                damage += 2;
                crit += 4;
                amount += 2;    
                break;
            case (PlayerAttack.Push):
                damage += 5;
                crit += 7;
                amount += 1;
                break;
            case (PlayerAttack.Tease):
                damage += 1;
                crit += 3;
                amount += 3;
                break;
            default:
                break;
        }
        if (playerIsBuffed)
        {
            damage += 3;
            crit += 3;
        }

        if (playerIsWeakened)
        {
            damage -= 2;
            if (damage > 1)
            {
                damage = 1;
            }
            crit -= 2;
            if (crit > 1)
            {
                crit = 1;
            }
        }
        SetInputBlocked(true);
        StartCoroutine(WaitForSlider(damage, crit, amount));
    }

    IEnumerator WaitForSlider(int dam, int crit, int amou)
    {
    
        for (int i = 0; i < amou; i++)
        {
            activeAttackBarControllers[i] = Instantiate(sliderBarPrefab).GetComponentInChildren<AttackBarController>();

            while (!activeAttackBarControllers[i].Triggered)
            {
                yield return null;
            }

            hitLogs[i] = tempSliderRead;
        }

        for (int i = 0; i < amou; i++)
        {
            yield return new WaitForSeconds(1f);
            if (hitLogs[i] == 0)
            {
                //animation
                display.ShowText("miss", Color.red);
                //Debug.Log(0);
            }
            else if (hitLogs[i] == 1)
            {
                //animation
                display.ShowNumber(dam, Color.red);
                enemyCurrHP -= dam;
                EnemyCurrBar.fillAmount = (float)enemyCurrHP / (float)enemyMaxHP;
                //Debug.Log(dam);
            }
            else
            {
                //animation
                display.ShowNumber(crit, Color.red);
                enemyCurrHP -= crit;
                EnemyCurrBar.fillAmount = (float)enemyCurrHP / (float)enemyMaxHP;
                //Debug.Log(crit);
            }
            
        }
        yield return new WaitForSeconds(1f);
        if (enemyCurrHP <= 0 )
        {
            GoToWin();
        }
        isPlayerTurn = false;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        
        
        int moveNumber = Random.Range(0, 3);
        int numbMod = 0;
        display.ShowText(enemyArray[GameCombatNumb].actionArray[moveNumber].Name, Color.white);
        yield return new WaitForSeconds(1.2f);
        EnemyActionType tempType = enemyArray[GameCombatNumb].actionArray[moveNumber].Type;

        if (enemyArray[GameCombatNumb].isWeakened == true)
        {
            numbMod -= 4;
            enemyArray[GameCombatNumb].isWeakened = false;
        }
        if (enemyArray[GameCombatNumb].isBuffed == true)
        {
            numbMod += 4;
            enemyArray[GameCombatNumb].isBuffed = false;
        }


        if (tempType == EnemyActionType.ATTACK)
        {
            //animation
            display.ShowNumber(enemyArray[GameCombatNumb].actionArray[moveNumber].Value + numbMod, Color.red);
            playerCurrHP -= enemyArray[GameCombatNumb].actionArray[moveNumber].Value + numbMod;
            PlayerCurrBar.fillAmount = (float)playerCurrHP / (float)playerMaxHP;
            Debug.Log("Did " + (enemyArray[GameCombatNumb].actionArray[moveNumber].Value + numbMod) + " damage");
        }
        else if (tempType == EnemyActionType.HEAL)
        {
            //animation
            display.ShowNumber(enemyArray[GameCombatNumb].actionArray[moveNumber].Value, Color.green);
            enemyCurrHP += enemyArray[GameCombatNumb].actionArray[moveNumber].Value;
            if (enemyCurrHP < 0)
            {
                enemyCurrHP = 0;

            }
            EnemyCurrBar.fillAmount = (float)enemyCurrHP / (float)enemyMaxHP;
            Debug.Log("Healed " + enemyArray[GameCombatNumb].actionArray[moveNumber].Value + " health");
        }
        else if (tempType == EnemyActionType.WEAKEN)
        {
            playerIsWeakened = true;
            display.ShowText("weaken", Color.magenta);
        }
        else
        {
            enemyArray[GameCombatNumb].isBuffed = true;
            display.ShowText("buffed", Color.yellow);
        }


        yield return new WaitForSeconds(1f);
        if (playerCurrHP <= 0)
        {
            GoToLose();
        }
        
        Debug.Log("your turn");
        SetInputBlocked(false);
    }

    public void GoToWin()
    {
        GameCombatNumb++;
        //leave combat
    }

    public void GoToLose()
    { 
        //go back to start of combat and reinitialize
    }

}

