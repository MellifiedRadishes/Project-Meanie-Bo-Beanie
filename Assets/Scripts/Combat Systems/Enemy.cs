using UnityEngine;

public class Enemy
{
    [SerializeField] public string Name;
    [SerializeField] public EnemyAction[] actionArray = new EnemyAction[3];
    [SerializeField] public bool isWeakened;
    [SerializeField] public bool isBuffed;

    public Enemy(string name, EnemyAction ac1, EnemyAction ac2, EnemyAction ac3)
    {
        Name = name;
        actionArray[0] = ac1;
        actionArray[1] = ac2;
        actionArray[2] = ac3;
        isWeakened = false;
        isBuffed = false;
    }
}


