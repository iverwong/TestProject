using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public CardAbstract card;
    /// <summary>
    /// 初始化CardUI，根据card字段赋值及填充面板信息
    /// </summary>
    public void Init()//初始化CardUI
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
    /// <summary>
    /// 卡牌点击，通过EventSystem选中卡牌时调用
    /// </summary>
    public void Click()
    {
        //将战斗状态调整为卡牌持有状态
        StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.HOLDER;
        //点灭周围移动地块
        StateMachine.RoleMovePlaneLightOnOff(false, 20);
        StateMachine.currentCard = card;
        card.Use();
    }
}
