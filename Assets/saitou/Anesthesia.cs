using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Anesthesia")]
public class Anesthesia : UniqueEffect
{
    [SerializeField]
    public List<float> sleepRate = new List<float> { 0.66f, 0.33f, 0f }; // ターンごとの眠り確率

    // 睡眠中の Count を記録する辞書（敵ごとに記録）
    private Dictionary<EnemyBase, int> countLock = new Dictionary<EnemyBase, int>();

    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        if (enemy.Base.IsSleeping)
        {
            message.text = "すでに敵は眠っている！";
            return;
        }

        float rate = sleepRate[0]; // 最初の眠り判定だけなので 0
        if (enemy.TrySleep(rate))
        {
            message.text = "敵を眠らせた！";
        }
        else
        {
            message.text = "眠らせに失敗した…";
        }
    }

    // 眠り継続中は敵のCount1を固定して行動停止
    public void FixCountDuringSleep(Enemy enemy)
    {
        if (enemy.Base.IsSleeping && countLock.ContainsKey(enemy.Base))
        {
            enemy.Base.Count1 = countLock[enemy.Base];
        }
    }

    // 眠り解除処理
    public void WakeUp(Enemy enemy, Text message)
    {
        if (enemy.Base.IsSleeping)
        {
            enemy.Base.IsSleeping = false;
            enemy.Base.SleepTurn = 0;

            if (countLock.ContainsKey(enemy.Base))
            {
                countLock.Remove(enemy.Base);
            }

            message.text += "\n敵は目を覚ました！";
        }
    }

}

