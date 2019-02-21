using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureAreaController : MonoBehaviour
{
    [SerializeField] GameObject gObjGesturePanel; //!< パネルのオブジェクト
    public bool isGestureAreaDisplayed = true; //!< ジェスチャパネルが表示されているかどうか

    List<int> userGestureInputs = new List<int>(); //!< ジェスチャのユーザ入力 - 2つで1組
    bool isInputEnabled = false; //!< 入力を受け付けているかどうか

    // Start is called before the first frame update
    void Start()
    {
        gObjGesturePanel.SetActive(isGestureAreaDisplayed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * 入力開始
     */
    public void startInput(string point)
    {
        string[] tempArr = point.Split(',');
        userGestureInputs.Add(int.Parse(tempArr[0]));
        userGestureInputs.Add(int.Parse(tempArr[1]));

        isInputEnabled = true;
        Debug.Log("Input Start");

        changePointColor(point);
    }

    /*
     * 入力追加
     */
    public void addInput(string point)
    {
        if (isInputEnabled) {
            string[] tempArr = point.Split(',');
            userGestureInputs.Add(int.Parse(tempArr[0]));
            userGestureInputs.Add(int.Parse(tempArr[1]));

            Debug.Log("Input Added");
            changePointColor(point);
        }
    }

    /*
     * 入力終了
     */
    public void endInput()
    {
        isInputEnabled = false;

        foreach (int i in userGestureInputs)
        {
            Debug.Log(i);
        }

        userGestureInputs.Clear();
        Debug.Log("Input Ended");
    }

    /*
     * 指定されたStringの座標のImageの色を変更
     * @params string 座標(x,y)
     */
     void changePointColor(string point)
     {
        GameObject gObjPoint = GameObject.Find(point.Replace(',', '-'));
        gObjPoint.GetComponent<SVGImage>().color = new Color(1.0f, 1.0f, 0.0f);
    }

    /*
     * パネルの表示・非表示切り替え
     */
    public void togglePanel()
    {
        isGestureAreaDisplayed = !isGestureAreaDisplayed;
        gObjGesturePanel.SetActive(isGestureAreaDisplayed);
    }
}
