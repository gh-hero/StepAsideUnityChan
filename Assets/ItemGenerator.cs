using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemGenerator : MonoBehaviour
{

    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;

    //Unityちゃんの位置取得用。
    //ユニティを開き、この.csがアタッチされているオブジェクトのインスペクタを開くと、
    //この変数名が表示されている。その部分に、取得したいオブジェクト（今回はユニティちゃん）をヒエラルキーから選びドラッグすると、
    //取得準備OKとなる。その後は、「generateReference.transform.position.z;」などと記述する事で位置が取得出来たり色々出来るようになる。
    public GameObject generateReference;

    //スポーンする時間の間隔（インターバル）
    public float spawnInterval = 1.0f;

    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    // 走り始めた時間
    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始時の秒数を保存
        // 「Time.time」はゲーム開始からの経過時間を返す。
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // 一点時間ごとに、targetから一定距離で生成。

        // 走り始めてからの経過時間。
        //現在のstartTimeの値をTime.time(ゲーム開始からの経過時間)から引く。それをelapsedTimeに代入。
        var elapsedTime = Time.time - startTime;

        //elapsedTimeがspawnIntervalより大きい時、障害物をスポーン。
        //最後にstartTimeをTime.time(ゲーム開始からの経過時間)と同じ値にするので、上の計算式の答えが0になる。
        //なのでelapsedTimeが0になりスポーンはいったん中止される。
        //その後、またTime.timeが、更新されたstartTimeより大きくなり、その差分がelapsedTimeに代入され、その値がspawnIntervalを
        //超えた時にスポーンがされる事になる。
        if (elapsedTime > spawnInterval​)
        {

            //一定の距離ごとにアイテムを生成。
            //まずiの値をユニティちゃんの現在の座標＋50に。iの値は障害物のスポーンする場所にも使われる値なので、
            //＋50などにして少し先にしないとユニティちゃんの座標と同じ場所にスポーンしてしまう。
            //そしてそのユニティちゃんの現在の座標より値が150大きくなるまでループを実行。
            //ループはカウンタ更新の+=の右の値を足していき、条件がfalseになる時止まる。
            //カウンタ更新を25にすると、このforは4回実行される(たぶん)ので、障害物は4回スポーンされる事になる。
            for (int i = (int)generateReference.transform.position.z + 50; i < (int)generateReference.transform.position.z + 150; i += 25)
            {

                //どのアイテムを出すのかをランダムに設定
                int num = Random.Range(1, 11);

                if (num <= 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                    }
                }
                else
                {
                    //レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くZ座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        //60%コイン配置:30%車配置:10%何もなし
                        if (1 <= item && item <= 6)
                        {
                            //コインを生成
                            GameObject coin = Instantiate(coinPrefab);
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            //車を生成
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                        }
                    }
                }
            }
            // startTimeを現在の経過時間に更新。詳細は上の記述で。
            startTime = Time.time;
        }
    }
}