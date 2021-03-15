using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    [SerializeField] GameObject m_obj = null;
    [SerializeField] int m_x = 1;
    [SerializeField] int m_y = 1;
    [SerializeField] int m_z = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3[,] mapCoordinate = new Vector3[m_x, m_y];//これに座標を入れる
        Vector3[,,] mapCoordinate = new Vector3[m_x, m_y, m_z];//これに座標を入れる

        int i = 0;//x
        int j = 0;//y
        int l = 0;//z

        for ( ; i < m_x; i++)
        {
            for ( ; j < m_y; j++)
            {
                for ( ; l < m_z; l++)
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

        //for (; i < m_x; i++)//行
        //{
        //    for (; j < m_y; j++)
        //    {
        //        mapCoordinate[i, j] = new Vector3(i, j);
        //    }
        //    if (j == m_y) { j = 0; }
        //}

        foreach (var item in mapCoordinate)
        {
            Instantiate(m_obj, item, this.transform.rotation, this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
