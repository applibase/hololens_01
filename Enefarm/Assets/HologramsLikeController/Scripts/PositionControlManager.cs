using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: エディタ上で見た目がわかるようにしたい
namespace HologramsLikeController
{
    public class PositionControlManager : MonoBehaviour
    {

        public GameObject cubeController;

        private void Start()
        {

            cubeController.SetActive(false);
            cubeController.transform.localPosition = Vector3.zero;
            cubeController.transform.rotation = Quaternion.identity;

            TransformController tc = transform.GetComponentInParent<TransformController>();
            GameObject target = tc.Target;

            Active();

        }

        public void Active()
        {
            TransformController tc = transform.GetComponentInParent<TransformController>();
            // コントロール対象を囲むワイヤーフレームの大きさを設定する
            cubeController.transform.localScale =
                tc.PositionControlerScale * TransformControlManager.Instance.positionCubeScale;

            cubeController.AddComponent<PositionController>();
            cubeController.GetComponent<MeshRenderer>().sharedMaterial =
                TransformControlManager.Instance.positionCubeMaterial;
            cubeController.SetActive(true);

            cubeController.transform.rotation = Quaternion.Euler(0, tc.Target.transform.localEulerAngles.y, 0);
        }
    }
}
