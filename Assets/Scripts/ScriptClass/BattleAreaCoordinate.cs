using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleAreaCoordinate
{
    public int x, y;
    public BattleAreaCoordinate parentCoordinate;
    public int f, g, h;//F（权重值）=G（移动代价）+H（估算成本）
    public BattleAreaCoordinate(int xIndex, int yIndex)
    {
        x = xIndex;
        y = yIndex;
    }


    public int Distance(BattleAreaCoordinate start, BattleAreaCoordinate end)
    {
        int distanceX = Mathf.Abs(start.x - end.x);
        int distanceY = Mathf.Abs(start.y - end.y);
        return Mathf.Max(distanceX, distanceY);

    }
    /// <summary>
    /// 返回该点周围六个点的坐标，从右上角开始，顺时针
    /// </summary>
    /// <returns>返回数组</returns>
    public BattleAreaCoordinate[] AroundPoint()
    {
        BattleAreaCoordinate rightUpPoint, rightPoint, rightDownPoint, leftDownPoint, leftPoint, leftUpPoint;
        //按顺时针返回各点
        //奇数行,y为偶数
        if (y % 2 == 0)
        {
            //右上
            rightUpPoint = new BattleAreaCoordinate(x + 1, y + 1);
            //右
            rightPoint = new BattleAreaCoordinate(x + 1, y);
            //右下
            rightDownPoint = new BattleAreaCoordinate(x + 1, y - 1);
            //左下
            leftDownPoint = new BattleAreaCoordinate(x, y - 1);
            //左
            leftPoint = new BattleAreaCoordinate(x - 1, y);
            //左上
            leftUpPoint = new BattleAreaCoordinate(x, y + 1);
        }
        //偶数行，y为奇数
        else
        {
            //右上
            rightUpPoint = new BattleAreaCoordinate(x, y + 1);
            //右
            rightPoint = new BattleAreaCoordinate(x + 1, y);
            //右下
            rightDownPoint = new BattleAreaCoordinate(x, y - 1);
            //左下
            leftDownPoint = new BattleAreaCoordinate(x - 1, y - 1);
            //左
            leftPoint = new BattleAreaCoordinate(x - 1, y);
            //左上
            leftUpPoint = new BattleAreaCoordinate(x - 1, y + 1);
        }

        return new BattleAreaCoordinate[] { rightUpPoint, rightPoint, rightDownPoint, leftDownPoint, leftPoint, leftUpPoint };
    }
    /// <summary>
    /// 根据给定范围，返回该点周围坐标List
    /// </summary>
    /// <param name="distance">返回范围的最远距离</param>
    /// <param name="isExclude">是否包含排除项</param>
    /// <returns></returns>
    public List<BattleAreaCoordinate> AroundPoint(int distance, bool isExclude)
    {
        List<BattleAreaCoordinate> pointList = new List<BattleAreaCoordinate>();
        AddPoint(this, distance, pointList, isExclude);
        return pointList;

    }
    /// <summary>
    /// 根据给定范围，返回该点周围坐标List，忽略障碍
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    public List<BattleAreaCoordinate> AroundPoint(int distance)
    {
        return AroundPoint(distance, false);
    }
    //向列表中添加点（迭代）
    private void AddPoint(BattleAreaCoordinate currentPoint, int distance, List<BattleAreaCoordinate> pointList, bool isExclude)
    {
        if (distance > 0)
        {
            //获取该点周围的点
            List<BattleAreaCoordinate> tempList = new List<BattleAreaCoordinate>();
            BattleAreaCoordinate[] testPointList = new BattleAreaCoordinate[6]; 
            testPointList = currentPoint.AroundPoint();
            foreach (BattleAreaCoordinate each in testPointList)
            {
                each.g = distance;//g值越大表明花费的步数越小
            }

            //将满足的点放入pointList
            foreach (BattleAreaCoordinate testPoint in testPointList)
            {
                //坐标点不符合要求或已存在于testPoint中
                if (testPoint.x < 0 || testPoint.y < 0) continue;
                BattleAreaCoordinate equalPoint = null;
                //查找pointList中的相同地块
                foreach (BattleAreaCoordinate point in pointList)
                {
                    if (point.x == testPoint.x && point.y == testPoint.y)
                    {
                        equalPoint = point;
                        break;
                    }
                }
                if (equalPoint != null)
                {
                    if (equalPoint.g >= testPoint.g)//如果包含相同地块，且g值大于或等于新的点
                    {
                        continue;
                    }
                    else
                    {
                        equalPoint.g = testPoint.g;
                        tempList.Add(testPoint);
                        continue;
                    }
                }


                //该点是否可移动且地块无物体（排除项）
                if (isExclude)
                {
                    //获取该点的script
                    BattleArea_Grid_Tile script = testPoint.FindTile();
                    if (script == null) continue;
                    if (script.walkable == true && script.objectOnIt == null)
                    {
                        pointList.Add(testPoint);
                        tempList.Add(testPoint);
                    }
                }
                else
                {
                    pointList.Add(testPoint);
                    tempList.Add(testPoint);
                }

            }
            foreach (BattleAreaCoordinate temp in tempList)
            {
                int dis = distance - 1;
                AddPoint(temp, dis, pointList, isExclude);
            }
        }

    }



    /// <summary>
    /// 以该点作为目标点寻路
    /// </summary>
    /// <param name="originCoordinate">起始点</param>
    /// <returns>该点的另一个副本，通过parentCoordinate属性逐层寻找路径</returns>
    public BattleAreaCoordinate AStarPathing(BattleAreaCoordinate originCoordinate)
    {
        //如果自身寻路到自身，则返回自身
        if(originCoordinate.x == x && originCoordinate.y == y)
        {
            parentCoordinate = null;
            return this;
        }


        //原始点g为0
        originCoordinate.g = 0;
        List<BattleAreaCoordinate> openList = new List<BattleAreaCoordinate>();
        List<BattleAreaCoordinate> closeList = new List<BattleAreaCoordinate>();
        //将初始点放入openlist
        openList.Add(originCoordinate);
        originCoordinate.h = HValue(originCoordinate, this);
        originCoordinate.f = originCoordinate.g + originCoordinate.h;

        BattleAreaCoordinate choiceCoordinate = originCoordinate;//选择的下一个点
        do
        {
            //将该点相邻的点放入
            foreach (BattleAreaCoordinate eachCoordinate in choiceCoordinate.AroundPoint())
            {
                //判断该点是否可移动，或是否已包含在closelist中
                Transform eachTransform = GameObject.Find("/BattleArea/BattleArea_Grid").transform.Find(string.Format("BattleArea_Grid_Tile_{0}_{1}", eachCoordinate.x, eachCoordinate.y));
                //该点不存在的情况下，直接跳到下一个点
                if (eachTransform == null) continue;
                BattleArea_Grid_Tile eachScript = eachTransform.GetComponent<BattleArea_Grid_Tile>();

                //判断是否包含在openlist中，如包含但花费大于原openlist中的点，则不采用
                foreach (BattleAreaCoordinate openListContains in openList)
                {
                    if (eachCoordinate.x == openListContains.x && eachCoordinate.y == openListContains.y)
                    {
                        int compare = choiceCoordinate.g + 1 + HValue(eachCoordinate, this);
                        if (compare > openListContains.f)
                        {
                            goto labelContinue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //如未包含在closelist中，且可移动，且该地块无物体

                foreach (BattleAreaCoordinate closeListContains in closeList)
                {
                    if (eachCoordinate.x == closeListContains.x && eachCoordinate.y == closeListContains.y)
                    {
                        goto labelContinue;
                    }
                }


                if (eachScript.walkable == true && eachScript.objectOnIt == null)
                {
                    //记录父子关系、加入openlist，计算相关值
                    eachCoordinate.parentCoordinate = choiceCoordinate;
                    openList.Add(eachCoordinate);
                    eachCoordinate.g = choiceCoordinate.g + 1;
                    eachCoordinate.h = HValue(eachCoordinate, this);
                    eachCoordinate.f = eachCoordinate.g + eachCoordinate.h;
                    continue;
                }
                labelContinue:
                continue;
            }
            //移除原始点
            openList.Remove(choiceCoordinate);
            closeList.Add(choiceCoordinate);

            //取openlist中的最优点
            int minPoint = 999;

            foreach (BattleAreaCoordinate eachCoordinateFindMin in openList)
            {
                if (eachCoordinateFindMin.f <= minPoint)
                {
                    minPoint = eachCoordinateFindMin.f;
                    choiceCoordinate = eachCoordinateFindMin;
                }
            }
        }
        //到达目标点
        while (choiceCoordinate.x != x || choiceCoordinate.y != y);
        return choiceCoordinate;

    }
    /// <summary>
    /// 计算两点间需要耗费步数的期望值
    /// </summary>
    /// <param name="fromPoint">起始点</param>
    /// <param name="toPoint">目标点</param>
    /// <returns></returns>
    private int HValue(BattleAreaCoordinate fromPoint, BattleAreaCoordinate toPoint)
    {
        int yValue = Mathf.Abs(fromPoint.y - toPoint.y);
        //原始点y值为奇
        if (fromPoint.y % 2 != 0)
        {
            //计算x差值所需步数
            //如x值向左偏移
            if (fromPoint.x - toPoint.x > 0)
            {
                //判断是否在垂直移动覆盖范围内
                if (Mathf.CeilToInt((float)yValue / 2) >= (fromPoint.x - toPoint.x))
                {
                    return yValue;
                }
                else
                {
                    return fromPoint.x - toPoint.x - Mathf.CeilToInt((float)yValue / 2) + yValue;
                }
            }
            //如x值向右偏移
            else if (fromPoint.x - toPoint.x < 0)
            {
                //判断是否在垂直移动范围内
                if (Mathf.FloorToInt((float)yValue / 2) >= (toPoint.x - fromPoint.x))
                {
                    return yValue;
                }
                else
                {
                    return toPoint.x - fromPoint.x - Mathf.FloorToInt((float)yValue / 2) + yValue;
                }
            }
            //如未发生偏移
            else
            {
                return yValue;
            }
        }
        //原始点y值为偶
        else
        {
            //计算x差值所需步数
            //如x值向左偏移
            if (fromPoint.x - toPoint.x > 0)
            {
                //判断是否在垂直移动覆盖范围内
                if (Mathf.FloorToInt((float)yValue / 2) >= (fromPoint.x - toPoint.x))
                {
                    return yValue;
                }
                else
                {
                    return fromPoint.x - toPoint.x - Mathf.FloorToInt((float)yValue / 2) + yValue;
                }
            }
            //如x值向右偏移
            else if (fromPoint.x - toPoint.x < 0)
            {
                //判断是否在垂直移动范围内
                if (Mathf.CeilToInt((float)yValue / 2) >= (toPoint.x - fromPoint.x))
                {
                    return yValue;
                }
                else
                {
                    return toPoint.x - fromPoint.x - Mathf.CeilToInt((float)yValue / 2) + yValue;
                }
            }
            //如未发生偏移
            else
            {
                return yValue;
            }
        }
    }
    /// <summary>
    /// 返回该坐标对应的Tile
    /// </summary>
    /// <returns>返回对应Tile</returns>
    public BattleArea_Grid_Tile FindTile()
    {
        BattleArea_Grid_Tile tile;
        GameObject gameObject;
        gameObject = GameObject.Find("/BattleArea/BattleArea_Grid/BattleArea_Grid_Tile_" + x.ToString() + "_" + y.ToString());
        if (gameObject)
        {
            tile = gameObject.GetComponent<BattleArea_Grid_Tile>();
            return tile;
        }
        else return null;
    }
}
