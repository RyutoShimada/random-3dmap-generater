using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_obj = null;

    [SerializeField] int m_height = 1;
    [SerializeField] int m_width = 1;

    Vector3[,,] m_mapCoordinate;

    // Start is called before the first frame update
    void Start()
    {
        RandomGenerator();
    }

    void RandomGenerator()
    {
        m_mapCoordinate = new Vector3[m_height, m_width, m_width];//これに座標を入れる
        int ly = 0, ix = 0, jz = 0;
        float x, y, z;

        for ( ; ly < m_height; ly++)
        {
            if (ly == 0)
            {
                Foundation();//土台作り
            }
            else
            {
                int randomCount = Random.Range(1, m_width);
                for (int i = 0; i < randomCount; i++)
                {
                    int randomX = Random.Range(0, m_width);
                    int randomZ = Random.Range(0, m_width);

                    x = randomX * m_obj.transform.localScale.x;
                    z = randomZ * m_obj.transform.localScale.z;
                    y = ly * m_obj.transform.localScale.y;

                    m_mapCoordinate[ly, randomX, randomZ] = new Vector3(x, y, z);

                    //空中にオブジェクトが生成されないように、その下を埋める処理
                    if (ly > 1 && m_mapCoordinate[ly - 1, randomX, randomZ] == Vector3.zero)
                    {
                        for (int n = 1; n < ly; n++)
                        {
                            if (m_mapCoordinate[n, randomX, randomZ] == Vector3.zero)
                            {
                                m_mapCoordinate[n, randomX, randomZ] = new Vector3(x, n * m_obj.transform.localScale.y, z);
                            }
                        }
                    }
                }
            }
        }

        //下の処理で省かれないために、最初の配列にある座標のオブジェクトは生成する
        GameObject go = Instantiate(m_obj, m_mapCoordinate[0, 0, 0], this.transform.rotation, this.transform);
        int num = 0;
        go.name = $"MapObject{num}";
        foreach (var item in m_mapCoordinate)
        {
            //初期化の時点ですべての配列に0座標が入っているので、それらのオブジェクトは生成しない
            if (item != Vector3.zero)
            {
                GameObject gO = Instantiate(m_obj, item, this.transform.rotation, this.transform);
                num++;
                gO.name = $"MapObject{num}";
            }
        }

        void Foundation()
        {
            for (; ix < m_width; ix++)
            {
                for (; jz < m_width; jz++)
                {
                    //オブジェクトの大きさに対応
                    x = ix * m_obj.transform.localScale.x;
                    z = jz * m_obj.transform.localScale.z;
                    y = ly * m_obj.transform.localScale.y;
                    m_mapCoordinate[ly, ix, jz] = new Vector3(x, y, z);
                }
                if (jz == m_width) { jz = 0; }
            }
            if (ix == m_width) { ix = 0; }
        }
    }

    /// <summary>
    /// ボタンで呼ぶ
    /// </summary>
    public void RebuildingMap()
    {
        m_mapCoordinate = new Vector3[m_height, m_width, m_width];//リセット
        foreach (var item in GameObject.FindGameObjectsWithTag("MapObjectTag"))
        {
            Destroy(item.gameObject);
        }
        RandomGenerator();
    }
}
