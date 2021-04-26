using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDelete : MonoBehaviour
{

    //（追加）壁を移動させるコンポーネントを入れる。Rigidbody型の変数名myRgibody。
    private Rigidbody myRigidbody;
    //（追加）前方向の速度
    private float velocityZ = 16f;
    //動きを減速させる係数
    private float coefficient = 0.99f;
    //ゲーム終了の判定
    private bool isEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        //Rigibodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了なら壁の動きを減衰する
        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
        }

        //壁に速度を与える
        //Rigidbodyクラスの「velocity」は、Vector3型の変数で、物体の持つ速度を表します。
        //inputVelocityX,0inputVelocityY,this.velocityZとあるが、this.velocityZは変数名。中身は16f。3つの値に入れた数値だけ移動する。
        //x,y,zの順に対応している。
        this.myRigidbody.velocity = new Vector3(0, 0, this.velocityZ);


        //トリガーモードで他のオブジェクトと接触した場合の処理
        void OnTriggerEnter(Collider other)
        {

            //障害物に衝突した場合（追加）
            if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag" || other.gameObject.tag == "CoinTag")
            {
                Destroy(other.gameObject);
            }

            //ゴール地点に到達した場合（追加）
            if (other.gameObject.tag == "GoalTag")
            {
                this.isEnd = true;

            }


        }



    }
}
