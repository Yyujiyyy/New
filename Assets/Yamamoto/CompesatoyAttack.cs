using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "UniqueEffects/CompasetotyAttack")]
public class CompasetotyEffect : UniqueEffect
{
    [SerializeField][Tooltip("代償N")] private float N;


    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        int attackValue = FlontBuff(card, flontCard);
        int Hit = (int)(attackValue * Random.Range(0.8f, 1.2f));
        float defense = 1f - enemy.Base.EnemyDefense / 100f;
        player.Life = (int)(player.Life - N);
        // player.Life - = player.Life;                                              //直す
        int damage = (int)(Hit * defense);
        enemy.Base.EnemyLife -= damage;
        message.text = $"{damage}ダメージ与えた";
        if (enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }
    }
    public int FlontBuff(Card card, Card flontCard)
    {
        float attackValue = (int)card.Base.CardStatus.Attack_Status;            //カードの元の攻撃値を取得

        if (flontCard == null)
        {
            return (int)attackValue;
        }
        else
        {
            string cardName = flontCard.Base.CardName;
            FlontBuff foundBuff = card.Base.FlontBuff.Find(buff => buff.flontCard == cardName);

            if (foundBuff == null)
            {
                return (int)attackValue;
            }
            attackValue *= foundBuff.buff;
            return (int)attackValue;
        }
    }






    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
