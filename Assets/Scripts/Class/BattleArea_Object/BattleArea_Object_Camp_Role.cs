using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleArea_Object_Camp_Role : BaseInteractableObject, IObjectCanBeHurt
{

    /// <summary>
    /// 记录该角色所在地块
    /// </summary>
    public BattleArea_Grid_Tile standOn;
    private float _HP, _SP, _MP;
    /// <summary>
    /// Health Point 生命值
    /// </summary>
    public float HP
    {
        get => _HP;
        set
        {
            //进行广播
            HPChangedBroadcast(value);
            //更改HP值
            _HP += value;
        }
    }
    /// <summary>
    /// Stamina Point 活力值
    /// </summary>
    public float SP
    {
        get => _SP;
        set
        {
            //进行广播
            SPChangedBroadcast(value);
            //更改SP值
            _SP += value;
        }
    }
    /// <summary>
    /// Morale Point 士气值
    /// </summary>
    public float MP
    {
        get => _MP;
        set
        {
            //进行广播
            MPChangedBroadcast(value);
            //更改MP值
            _MP += value;
        }
    }

    public float CON;//Constitution 体质
    public float STR;//Strengh 力量
    public float AGI;//Agility 敏捷
    public float DEX;//Dexterity 灵巧
    public float WIL;//Willpower 意志
    public float LOG;//Logic 逻辑
    public float OFF;//攻击方系数
    public float DFF;//防守方系数

    public DefenseModeAbstract defenseMode = null;//防御模式

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
    public event EventHandler.ChangeValueEventHandler HPChangedBroadcast,SPChangedBroadcast,MPChangedBroadcast;//属性发生变更时的广播

    public override BattleAreaCoordinate ObjectCoordinate
    {
        get => standOn.coordinate;
        set => standOn = value.FindTile();
    }


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
            foreach (CardAbstract each in CardLibrary)
            {
                CardReady.Add(each.Clone());
            }
        }
        else throw new System.Exception("CardLibrary为空");
    }
    /// <summary>
    /// 进行抽卡
    /// </summary>
    /// <param name="_i">抽卡张数</param>
    public void DrawCard(int _i)
    {
        //循环抽卡_i次
        for (int i = 0; i < _i; i++)
        {
            //待发牌库中没有牌时，无法抽取
            if (CardReady.Count == 0)
            {
                Debug.Log("待发牌库中没有牌，停止抽牌");
                break;
            }
            //获取牌库的第一张卡
            CardAbstract currentCard = CardReady[0];
            //添加到CardPlay中
            CardPlay.Add(currentCard);
            //从CardReady中移除
            CardReady.Remove(currentCard);
            //判断CardReady卡牌张数

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
            if (CardReady.Count == 0)
            {
                ShuffleCard();//洗牌
            }
        }
    }
    /// <summary>
    /// 进行洗牌，将弃牌堆中的所有牌组乱序洗入待发牌库
    /// </summary>
    private void ShuffleCard()
    {
        int count = Discard.Count;
        //当弃牌库中有牌时
        if (count != 0)
        {
            //将所有Discard列表中的元素赋值到CardReady中
            CardReady.AddRange(Discard);
            //清空Discard
            Discard.Clear();
            //进行CardReady排序
            for (int i = 0; i < count - 1; i++)
            {
                //随机一个整数
                int random = Random.Range(i, count);
                //将index为random的元素与第i个元素交换
                CardAbstract temp = CardReady[random];
                CardReady[random] = CardReady[i];
                CardReady[i] = temp;
            }
        }
        else
        {
            Debug.Log("弃牌库中没有牌，洗牌结束。");
        }
    }
}
