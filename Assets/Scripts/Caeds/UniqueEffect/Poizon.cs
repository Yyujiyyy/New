using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Poizon")]
public class PoizonEffect : UniqueEffect
{
    [SerializeField] float poisonAttackInit_Status;         // 1ターン目の毒攻撃力
    [SerializeField] int poisonAttackMax_Status;            // 毒攻撃の持続ターン数上限
    [SerializeField] float poisonAttackIncrease_Status;     // ターン毎の毒攻撃力の増加量
    [SerializeField] int poisonAddTurns_Status;             // 毒攻撃の付与ターン数

    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player,Enemy enemy,Text message)
    {
        // 固定値の毒関連データをプレイヤーにコピー
        player.PoizonAttackInit = poisonAttackInit_Status;
        player.PoizonAttackMax = poisonAttackMax_Status;
        player.PoizonAttackIncrease = poisonAttackIncrease_Status;
        player.PoizonAddTurns = poisonAddTurns_Status;

        // 毒持続ターン
        // 重複している場合も毒攻撃の付与ターン数で上書き
        enemy.Base.EnemyDebufPoizonTurns = poisonAddTurns_Status;

        message.text = $"毒を付与した";
    }
    //一枚前のカードの追加効果処理
    public int FlontBuff(Card card, Card flontCard)
    {
        return (int)card.Base.CardStatus.Attack_Status;
    }
}
