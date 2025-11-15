using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("再生したいパーティクルプレハブ")]
    public GameObject effectPrefab;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // 左クリックでエフェクトを出す
        if (Input.GetMouseButtonDown(0))
        {
            SpawnEffectAtMouse();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ParticleSystem[] allPS = FindObjectsOfType<ParticleSystem>();

            foreach (var ps in allPS)
            {
                if (ps.isPaused)
                {
                    ps.Play();
                }
                else
                {
                    ps.Pause();
                }
            }
        }
    }

    void SpawnEffectAtMouse()
    {
        if (effectPrefab == null) return;

        // クリック位置 → ワールド座標へ
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Instantiate(effectPrefab, hit.point, Quaternion.identity);
        }
        else
        {
            // 地面がない場合、Z=0 平面に出す例
            Vector3 pos = cam.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f)
            );
            Instantiate(effectPrefab, pos, Quaternion.identity);
        }
    }
}