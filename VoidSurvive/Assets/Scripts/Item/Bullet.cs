using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : PoolAble
{    

    public float speed = 5f;

    void Update()
    {

        // �Ѿ��� ���� ���󰡸� ���� ���ֱ�
        if (this.transform.position.y > 5)
        {
            // ���� �ڽ��� Destroy�� ���� �ʴ´�.
            //Destroy(this.gameObject);

            // ������Ʈ Ǯ�� ��ȯ
            Pool.Release(this.gameObject);
        }

        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
