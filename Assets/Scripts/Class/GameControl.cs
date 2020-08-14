using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch (StateMachine.state)
            {
                case BattleArea_Grid.BattleArea_Grid_State.NONE:
                    break;
                case BattleArea_Grid.BattleArea_Grid_State.COMMANDER:
                    break;
                case BattleArea_Grid.BattleArea_Grid_State.HOLDER:
                    //取消触发器显示
                    StateMachine.currentCard.card.CardTrigger.EndTrigger();
                    //cardUI移动至手牌(会导致currentCard被清空）
                    StateMachine.currentCard.MoveToHandleCard();
                    //点亮移动区块
                    StateMachine.RoleMovePlaneLightOnOff(true, 10);
                    //更改状态
                    StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.COMMANDER;
                    break;
            }
        }
    }
}
