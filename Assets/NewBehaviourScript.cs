using UnityEngine;

//  GSSReader を追加している GameObject にのみ AddComponent できます。
[RequireComponent(typeof(GSSReader))]
//  クラス名はなんでもOKです。
//  読み込むデータに合わせて適切なクラス名に変更してください。
//  MonoBehaviourを継承している場合、スクリプトファイル名と
//  クラス名を合わせる必要があるので、クラス名を変更したら
//  スクリプトファイル名も変更してください。
public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //  メソッド名もなんでもOKです。
    //  GSSReader の OnLoadEnd に追加することで
    //  GSS の読み込み完了時にコールバックされます。
    public void OnGSSLoadEnd()
    {
        var r = GetComponent<GSSReader>();
        var d = r.Datas;
        if (d != null)
        {
            //  d をゲームで使うデータに変換する処理をここに書きます。
            //  以下は d の中身をコンソールに表示するサンプルです。
            for (var row = 0; row < d.Length; row++)
            {
                for (var col = 0; col < d[row].Length; col++)
                {
                    Debug.Log("[" + row + "][" + col + "]=" + d[row][col]);
                }
            }
        }
    }
}

