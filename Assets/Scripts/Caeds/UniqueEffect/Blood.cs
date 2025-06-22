using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Blood")]
public class BloodEffect : UniqueEffect
{
    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        int bloodValue = FlontBuff(card, flontCard);

        int hit = (int)(bloodValue * Random.Range(0.8f, 1.2f));
        float defense = 1f - enemy.Base.EnemyDefense / 100f;

        int damage = (int)(hit * defense);


        // 敵にダメージを与える
        enemy.Base.EnemyLife -= damage;
        if (enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }

        // 吸血処理
        int healValue = damage;
        if ((player.Life + damage) > player.LifeMax)
        {
            healValue = player.LifeMax - player.Life;
        }
        player.Life += healValue;
        message.text = $"{healValue}HPを吸い取った";
    }
    //一枚前のカードの追加効果処理
    public int FlontBuff(Card card, Card flontCard)
    {

        float attackValue = (int)card.Base.CardStatus.Attack_Status;


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
}
