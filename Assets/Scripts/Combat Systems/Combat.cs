using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Combat : MonoBehaviour
{
    private int GameCombatNumb = 0;

    [SerializeField] private bool IsPlayerTurn;

    [SerializeField] private int playerMaxHP;
    [SerializeField] private int playerCurrHP;
    [SerializeField] private int enemyMaxHP;
    [SerializeField] private int enemyCurrHP;
    [SerializeField] private int enemyMinDamage;

    readonly PlayerAttacks[] AttackArray = new PlayerAttacks[4];

    public PlayerAttacks hug;
    public PlayerAttacks push;
    public PlayerAttacks tease;



    void Start()
    {
        hug = new PlayerAttacks(1, 5, 7);
        push = new PlayerAttacks(2, 2, 4);
        tease = new PlayerAttacks(3, 1, 3);
        AttackArray[0] = hug;
        AttackArray[1] = push;
        AttackArray[2] = tease;

        SetupBattle();

    }

    void SetupBattle()
    {
        playerMaxHP = 25;
        playerCurrHP = playerMaxHP;

        enemyMaxHP = 20;
        enemyCurrHP = enemyMaxHP;
    }

    void PlayerTurn()
    {

    }



    public void PlayerAttack(int currAttack)
    {

        for (int i = 0; i < AttackArray[currAttack].attacks; i++)
        { // 
            //activate slider
            //read results of slider
            int sliderInput = 1;

            if (sliderInput == 1)
            {
                enemyCurrHP -= AttackArray[currAttack].damage;
                //damage animation and number on screen
            }
            else if (sliderInput == 2)
            {
                enemyCurrHP -= AttackArray[currAttack].crit;
                //damage animation and number on screen
            }
            else
            {
                //miss animation and zero on screen
            }
            //yield return new WaitForSeconds(.2f);
        }



        //yield return new WaitForSeconds(1f);

        if (enemyCurrHP <= 0)
        {
            GoToWin();
        }
        else
        {
            //gotoEnemyTurn();
        }
    }

    public void EnemyTurn()
    {
        //enemy do attack

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
}

