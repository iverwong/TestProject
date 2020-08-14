using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour, System.IComparable<CardUI>
{
    public CardAbstract card;
    public GameObject selectedFrame;
    public RectTransform cardTransform;
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
        if (StateMachine.currentCard != this)
        {
            //将战斗状态调整为卡牌持有状态
            StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.HOLDER;
            //点灭周围移动地块
            StateMachine.RoleMovePlaneLightOnOff(false, 20);
            MouseExit();
            //卡牌进入待选取
            if (StateMachine.currentCard == null)
            {
                MoveToCurrentCard();
            }
            else
            {
                StateMachine.currentCard.MoveToHandleCard();
                MoveToCurrentCard();
            }
            card.Use();//调用卡抽象类的Use方法
        }
    }
    /// <summary>
    /// 将CardUI物体移动至手持状态（UI左侧）
    /// </summary>
    public void MoveToCurrentCard()
    {
        //移动至屏幕左边
        cardTransform.anchorMin = new Vector2(0f, 0.5f);
        cardTransform.anchorMax = new Vector2(0f, 0.5f);
        cardTransform.pivot = new Vector2(0f, 0.5f);
        cardTransform.anchoredPosition = Vector3.zero;
        //配置当前选中卡牌
        StateMachine.currentCard = this;
        //将该卡片从CardUI列表移出
        StateMachine.handleCardUI.Remove(this);
        //重排序
        StateMachine.OrderHandleCardUI();
    }
    /// <summary>
    /// 将CardUI物体移动至手牌持有状态（UI底部）
    /// </summary>
    public void MoveToHandleCard()
    {
        //移至屏幕底部
        cardTransform.anchorMin = new Vector2(0.5f, 0f);
        cardTransform.anchorMax = new Vector2(0.5f, 0f);
        cardTransform.pivot = new Vector2(0.5f, 0f);
        //将卡牌移入CardUI列表
        StateMachine.handleCardUI.Add(this);
        //重置当前卡牌
        StateMachine.currentCard = null;
        //重排序
        StateMachine.OrderHandleCardUI();
    }

    /// <summary>
    /// 鼠标划入卡牌，显示外框
    /// </summary>
    public void MouseEnter()
    {
        if (StateMachine.currentCard != this)
        {
            canvas.sortingOrder = 200;
            selectedFrame.SetActive(true);
        }
    }

    /// <summary>
    /// 鼠标划出卡牌，隐藏外框
    /// </summary>
    public void MouseExit()
    {
        if (StateMachine.currentCard != this)
        {
            canvas.sortingOrder = originalOrder;
            selectedFrame.SetActive(false);
        }
    }

    public void MoveToPosition(float _x)
    {
        cardTransform.anchoredPosition = new Vector3(_x, 0f, 0f);

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
    /// <summary>
    /// 实现CompareTo接口，实现以卡牌种类、卡牌名称进行排序
    /// </summary>
    /// <param name="other">另一张卡牌</param>
    /// <returns>int返回结果，小于、等于、大于0的情况</returns>
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
