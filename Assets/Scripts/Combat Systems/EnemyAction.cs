using UnityEngine;

public class EnemyAction
{
    [SerializeField] public string Name;
    [SerializeField] public EnemyActionType Type;
    [SerializeField] public float Value;

    public EnemyAction(string name, EnemyActionType type, float val)
    {
        Name = name;
        Type = type;
        Value = val;
    }

}

public enum EnemyActionType
{
    ATTACK, HEAL, BUFF, WEAKEN
}





