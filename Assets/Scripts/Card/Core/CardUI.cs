using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public void Initialize(CardAbstract card)//初始化CardUI
    {
        //UI面板信息
        Transform image = transform.Find("Image");
        Transform name = transform.Find("Name");
        Transform description = transform.Find("Description");

        //设置相关信息
        image.GetComponent<RawImage>().texture = card.CardImage;
        name.GetComponent<Text>().text = card.CardName;
        description.GetComponent<Text>().text = card.CardDescription;
    }
}
