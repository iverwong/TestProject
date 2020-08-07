using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public CardAbstract card;
    public void Initialize()//初始化CardUI
    {

        //UI面板信息
        Transform image = transform.Find("Panel/Image");
        Transform name = transform.Find("Panel/Name");
        Transform description = transform.Find("Panel/Description");

        //设置相关信息
        image.GetComponent<RawImage>().texture = card.CardImage;
        name.GetComponent<Text>().text = card.CardName;
        description.GetComponent<Text>().text = card.CardDescription;
    }

    public void Click()
    {
        //将战斗状态调整为卡牌持有状态
        StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.HOLDER;
        //点灭周围移动地块
        StateMachine.RoleMovePlaneLightOnOff(false, 20);
        card.Use();
    }
}
