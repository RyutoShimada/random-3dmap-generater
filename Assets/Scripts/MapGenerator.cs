using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_obj = null;

    [SerializeField] int m_x = 1;
    [SerializeField] int m_y = 1;
    [SerializeField] int m_z = 1;

    [SerializeField] int m_height = 1;
    [SerializeField] int m_width = 1;

    // Start is called before the first frame update
    void Start()
    {
        //RectanglegGnerator();
        RandomGenerator();
    }

    /// <summary>
    /// オブジェクトを指定された数に応じて四角形型に生成
    /// </summary>
    void RectanglegGnerator()
    {
        Vector3[,,] mapCoordinate = new Vector3[m_x, m_y, m_z];//これに座標を入れる

        int i = 0, j = 0, l = 0;//(x, y, z)

        for (; i < m_x; i++)
        {
            for (; j < m_y; j++)
            {
                for (; l < m_z; l++)
                {
                    //オブジェクトの大きさに対応
                    float x = i * m_obj.transform.localScale.x;
                    float y = j * m_obj.transform.localScale.y;
                    float z = l * m_obj.transform.localScale.z;
                    mapCoordinate[i, j, l] = new Vector3(x, y, z);
                }
                if (l == m_z) { l = 0; }
            }
            if (j == m_y) { j = 0; }
        }

        foreach (var item in mapCoordinate)
        {
            Instantiate(m_obj, item, this.transform.rotation, this.transform);
        }
    }

    void RandomGenerator()
    {
        Vector3[,,] mapCoordinate = new Vector3[m_height, m_width, m_width];//これに座標を入れる
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

                    mapCoordinate[ly, randomX, randomZ] = new Vector3(x, y, z);
                }
            }
        }

        foreach (var item in mapCoordinate)
        {
            Instantiate(m_obj, item, this.transform.rotation, this.transform);
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
                    mapCoordinate[ly, ix, jz] = new Vector3(x, y, z);
                }
                if (jz == m_width) { jz = 0; }
            }
            if (ix == m_width) { ix = 0; }
        }
    }
}
