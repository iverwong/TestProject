using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour,System.IComparable<CardUI>
{
    public CardAbstract card;
    public GameObject selectedFrame;
    public Transform cardTransform;
    private int originalOrder;
    public Canvas canvas;

    /// <summary>
    /// 初始化CardUI素材
    /// </summary>
    public void Init()
    {
        if (card != null)
        {
            //UI面板信息
            Transform background = transform.Find("Card/Background");
            Transform cardBackground = transform.Find("Card/CardBackground");

            Transform image = transform.Find("Card/Image");
            Transform name = transform.Find("Card/CardName");
            Transform description = transform.Find("Card/CardDescription");
            Transform backstory = transform.Find("Card/CardBackstory");

            //设置相关信息
            switch (card.CardType)
            {
                case CardType.Attack:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/bg_red");
                    cardBackground.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/frame_back_red");
                    break;
                case CardType.Defense:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/bg_blue");
                    cardBackground.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/frame_back_blue");
                    break;
                case CardType.Buff:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/bg_green");
                    cardBackground.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/frame_back_green");
                    break;
                case CardType.Debuff:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/bg_purple");
                    cardBackground.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/CardUI/frame_back_purple");
                    break;
            }

            image.GetComponent<Image>().sprite = card.CardImage;
            name.GetComponent<TextMeshProUGUI>().text = card.CardName;
            description.GetComponent<TextMeshProUGUI>().text = card.CardDescription;
            backstory.GetComponent<TextMeshProUGUI>().text = card.CardBackstory;
        }
        else throw new System.Exception("CardUI没有card属性相关联");
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

    /// <summary>
    /// 卡牌划过，显示外框
    /// </summary>
    public void MouseEnter()
    {
        canvas.sortingOrder = 200;
        selectedFrame.SetActive(true);
    }

    public void MouseExit()
    {
        canvas.sortingOrder = originalOrder;
        selectedFrame.SetActive(false);
    }

    public void MoveToPosition(float _x)
    {
        cardTransform.localPosition = new Vector3(_x, 0f, 0f);
    }

    /// <summary>
    /// 设置该CardUI排序值
    /// </summary>
    /// <param name="_x"></param>
    public void SetSortOrder(int _x)
    {
        originalOrder = _x;
        canvas.sortingOrder = _x;
    }

    public int CompareTo(CardUI other)
    {
        int result = this.card.CardType.CompareTo(other.card.CardType);
        if (result == 0)
        {
            return this.card.CardName.CompareTo(other.card.CardName);
        }
        else return result;
    }
}
