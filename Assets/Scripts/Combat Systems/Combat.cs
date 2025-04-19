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

    //[SerializeField] private bool IsPlayerTurn;

    [SerializeField] private int playerMaxHP;
    [SerializeField] private int playerCurrHP;
    public UnityEngine.UI.Image PlayerCurrBar;
    [SerializeField] private int enemyMaxHP;
    [SerializeField] private int enemyCurrHP;
    public UnityEngine.UI.Image EnemyCurrBar;

    [SerializeField] private bool playerIsWeakened;
    [SerializeField] private bool playerIsBuffed;

    [SerializeField] private GameObject sliderBarPrefab;
    private AttackBarController[] activeAttackBarControllers = new AttackBarController[3];




    private Enemy[] enemyArray = new Enemy[4];

    public static EnemyAction ba1 = new EnemyAction("Wooden Spoon", EnemyActionType.ATTACK, 6);
    public static EnemyAction ba2 = new EnemyAction("Cake", EnemyActionType.HEAL, 5);
    public static EnemyAction ba3 = new EnemyAction("Mix", EnemyActionType.BUFF, 0);
    public static EnemyAction bba1 = new EnemyAction("Push", EnemyActionType.ATTACK, 5);
    public static EnemyAction bba2 = new EnemyAction("Slap", EnemyActionType.ATTACK, 8);
    public static EnemyAction bba3 = new EnemyAction("Condemn", EnemyActionType.WEAKEN, 0);
    public Enemy Buck = new Enemy("Buck", 30, ba1, ba2, ba3, "This mf is Buck");
    public Enemy Otter = new Enemy("Bibbleboo", 40, bba1, bba2, bba3, "This mf is Bibbleboo");

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

    private void Update() 
    {
        //switch (currentState)
        //{
        //    case (CombatState.PlayerTurn):
        //        awaitMenuInput();
        //        break;
        //    case (CombatState.EnemyTurn):
        //        break;
        //    case (CombatState.AttackCommence):
                
        //        break;
        //    case (CombatState.ItemCommence):
        //        break;
        //    default:
        //        break;
        //}
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


    void SetupBattle()
    {
        enemyArray[0] = Buck;
        enemyArray[1] = Otter;


        playerMaxHP = 25;
        playerCurrHP = playerMaxHP;
        PlayerCurrBar.fillAmount = (float)playerCurrHP / (float)playerMaxHP;

        enemyMaxHP = enemyArray[GameCombatNumb].HP;
        enemyCurrHP = enemyMaxHP;
        EnemyCurrBar.fillAmount = (float)enemyCurrHP / (float)enemyMaxHP;
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
                //print 0 on screen
                //Debug.Log(0);
            }
            else if (hitLogs[i] == 1)
            {
                //animation
                //print damage# on screen
                enemyCurrHP -= dam;
                EnemyCurrBar.fillAmount = (float)enemyCurrHP / (float)enemyMaxHP;
                //Debug.Log(dam);
            }
            else
            {
                //animation
                //print crit# on screen
                enemyCurrHP -= crit;
                EnemyCurrBar.fillAmount = (float)enemyCurrHP / (float)enemyMaxHP;
                //Debug.Log(crit);
            }
            
        }
        yield return new WaitForSeconds(1f);
    }

    public void EnemyTurn()
    {
        int moveNumber = Random.Range(0, 2);
        int numbMod = 0;
        EnemyActionType tempType = enemyArray[GameCombatNumb].actionArray[moveNumber].Type;

        if (enemyArray[GameCombatNumb].isWeakened == true) 
        {
            numbMod -= 4;
        }
        if (enemyArray[GameCombatNumb].isBuffed == true) 
        {
            numbMod += 4;
        }


        if (tempType == EnemyActionType.ATTACK)
        {
            //animation
            //print enemyArray[GameCombatNumb].actionArray[moveNumber].Value + numbMod on screen
            playerCurrHP -= enemyArray[GameCombatNumb].actionArray[moveNumber].Value + numbMod;
        } else if (tempType == EnemyActionType.HEAL) {
            //animation
            //print enemyArray[GameCombatNumb].actionArray[moveNumber].Value  on screen
            enemyCurrHP += enemyArray[GameCombatNumb].actionArray[moveNumber].Value;
            if (enemyCurrHP < 0)
            {
                enemyCurrHP = 0;
            }
        }

        else if (tempType == EnemyActionType.WEAKEN)
        {

        }


            //yield return new WaitForSeconds(1f);

            if (playerCurrHP <= 0)
        {

        }
        else
        {

        }


    }

    public void GoToWin()
    {
        GameCombatNumb++;
    }


    ////ui motion, anims, sequence
    //IEnumerator SpawnBars(int barCount)
    //{
    //    for (int i = 0; i < barCount; i++)
    //    {
    //        Instantiate(barPrefab, spawnPoint.position, Quaternion.identity);
    //        yield return new WaitForSeconds(delayBetweenBars);
    //    }
    //}
}

