using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleArea_Object_Camp_Role : BaseInteractableObject
{

    [Header("Main")]
    //记录该角色所在地块
    public BattleArea_Grid_Tile standOn;

    [Header("Base Attribute")]
    public float HP;//Health Point 生命值
    public float SP;//Stamina Point 活力值
    public float MP;//Morale Point 士气值
    [Header("Base Ability")]
    public float CON;//Constitution 体质
    public float STR;//Strengh 力量
    public float AGI;//Agility 敏捷
    public float DEX;//Dexterity 灵巧
    public float WIL;//Willpower 意志
    public float LOG;//Logic 逻辑


    private bool moveState = false;
    private Vector3 moveTarget;
    private Vector3 originalPosition;
    private float motionSpeed;//动画速度
    internal int pathIndex;
    private List<Transform> orderTileTransforms = new List<Transform>();//transform列表
    internal Animator childAnimator;
    public List<CardAbstract> CardLibrary = new List<CardAbstract>(),
        CardReady = new List<CardAbstract>(),
        CardPlay = new List<CardAbstract>(),
        Discard = new List<CardAbstract>();//牌库、待发牌、手牌、弃牌
    internal Object cardUI;//cardUI的预制件

    private void Start()
    {
        childAnimator = transform.GetChild(0).GetComponent<Animator>();
        cardUI = Resources.Load("UI/CardUI");
    }
    /// <summary>
    /// 角色移动到指定坐标
    /// </summary>
    /// <param name="targetPath">目标点</param>
    /// <param name="speed">移动速度，每0.02s移动的距离</param>
    public void MoveToPosition(BattleAreaCoordinate targetPath, float speed)
    {
        //禁止操作
        StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.NONE;
        //将tile存入列表
        targetPath = targetPath.AStarPathing(StateMachine.waitCommand.standOn.coordinate);
        orderTileTransforms = new List<Transform>();
        BattleAreaCoordinate currentCoordinate = targetPath;
        while (currentCoordinate.parentCoordinate != null)
        {
            Transform currentTransform = currentCoordinate.FindTile().transform;
            orderTileTransforms.Add(currentTransform);
            currentCoordinate = currentCoordinate.parentCoordinate;
        }
        //tile列表倒序
        orderTileTransforms.Reverse();
        //初始化

        pathIndex = 0;//重置index
        motionSpeed = speed;//动画速度
        moveState = true;//激活update

    }


    private void FixedUpdate()
    {
        //角色移动动画及position位移
        FixedUpdate_MoveMotion();

    }

    private void FixedUpdate_MoveMotion()
    {
        //角色移动
        if (moveState)
        {
            childAnimator.SetBool("Walking", true);
            transform.LookAt(orderTileTransforms[pathIndex]);
            transform.position = Vector3.MoveTowards(transform.position, orderTileTransforms[pathIndex].position, motionSpeed * GameSettings.GameSpeed);
            //角色移动到某个位置
            if (Vector3.Distance(this.transform.position, orderTileTransforms[pathIndex].position) < 0.05f)
            {
                transform.position = orderTileTransforms[pathIndex++].position;
            }
            //index超出时，停止移动
            if (pathIndex == orderTileTransforms.Count)
            {
                moveState = false;
                childAnimator.SetBool("Walking", false);
                //StateMachine.state = BattleArea_Grid.BattleArea_Grid_State.COMMANDER;
            }
        }
    }
    /// <summary>
    /// 将CardLibrary中的卡牌复制到CardReady中
    /// </summary>
    public void CardLibraryToCardReady()
    {
        if (CardLibrary != null)
        {
            foreach(CardAbstract each in CardLibrary)
            {
                CardReady.Add(each.Clone());
            }
        }
        else throw new System.Exception("CardLibrary为空");
    }

    public void DrawCard(int _i)
    {
        //循环抽卡_i次
        for (int i = 0; i < _i; i++)
        {
            if(CardReady.Count > 0)//待发牌库中有牌时
            {
                //随机一个index
                int randomIndex = Random.Range(0, CardReady.Count - 1);
                CardAbstract currentCard = CardReady[randomIndex];
                //添加到CardPlay中
                CardPlay.Add(currentCard);
                //从CardReady中移除
                CardReady.Remove(currentCard);
                //实例化CardUI预制件
                GameObject prefebCardUI;
                prefebCardUI = (GameObject)PrefabUtility.InstantiatePrefab(cardUI);
                //载入UI资源
                CardUI cardUIScript = prefebCardUI.GetComponent<CardUI>();
                cardUIScript.card = currentCard;
                cardUIScript.Init();
                //将该UI资源传入StateMachine中统一管理
                StateMachine.handleCardUI.Add(cardUIScript);
                //排列handleCardUI列表，并移动到正确位置
                StateMachine.OrderHandleCardUI();
            }
        }
    }
}
