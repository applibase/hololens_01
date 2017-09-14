using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HologramsLikeController {
    public class TransformControllerCompletion : MonoBehaviour, IInputClickHandler {

        public float correction;
        private TargetObjectManager targetObjectManager;

        public void OnInputClicked(InputClickedEventData eventData) {
            Transform transformController = transform.parent;
            GameObject target = transformController.parent.gameObject;
            // Cubeのレンダラーとコライダーを有効か
            target.GetComponent<Collider>().enabled = true;
            // 親オブジェクトを無効化

            if (targetObjectManager.mode == TargetMode.Other)
            {
                transformController.gameObject.SetActive(false);
            }

            targetObjectManager.Change();
            target.GetComponent<Rigidbody>().useGravity = true;
            
            
            // debug
            Debug.Log("TransformController disabled.");
        }

        private void Start() {
            TransformController tc = transform.GetComponentInParent<TransformController>();

            float posY = tc.PositionControlerScale.y * correction;

            transform.localPosition = new Vector3(0, posY + TransformControlManager.Instance.completePanelPositionY, 0);
            targetObjectManager = GameObject.Find("ObjectManager").GetComponent<TargetObjectManager>();
        }

        private void Update() {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}