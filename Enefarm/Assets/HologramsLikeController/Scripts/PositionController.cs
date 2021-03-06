﻿using System;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Unity.SpatialMapping;
using System.Linq;

namespace HologramsLikeController
{
    public class PositionController : MonoBehaviour, IInputHandler, ISourceStateHandler
    {
        // コントロール対象のGameObject
        public GameObject target;
        private Interpolator interpolator;
        private Rigidbody rigidbody;

        public bool IsDraggingEnable = true;
        private bool isDragging;

        private Camera mainCamera;

        private float objRefDistance;
        private float handRefDistance;
        private Quaternion gazeAngularOffset;

        private Vector3 draggingPosition;

        private IInputSource currentInputSource = null;
        private uint currentInputSourceId;
        private ColliderManager colliderManager;

        private Renderer[] rotationRenderers;
        private Renderer[] positionRenderers;
        private Renderer[] completeRenderers;

        private void OnEnable()
        {
            target = transform.GetComponentInParent<TransformController>().Target;
            GameObject.Find("TextManager");

            colliderManager = target.GetComponent<ColliderManager>();

            if (target == null)
            {
#if UNITY_EDITOR
                Debug.LogError("PositionController-OnEnable: target is not set.");
                return;
#endif
            }

            rigidbody = target.GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            if (interpolator == null)
            {
                interpolator = target.GetComponent<Interpolator>();
            }

            if (interpolator == null)
            {
#if UNITY_EDITOR
                Debug.LogError("PositionController-OnEnabled: Target object isn't attached Interpolator component.");
#endif
            }

            initRenderer();
            mainCamera = Camera.main;
        }

        private void initRenderer()
        {
            var controller = target.transform.Find("TransformController").gameObject;
            rotationRenderers = controller.transform.Find("RotationControlManager").GetComponentsInChildren<Renderer>();
            positionRenderers = controller.transform.Find("PositionControlManager").GetComponentsInChildren<Renderer>();
            completeRenderers = controller.transform.Find("Complete").GetComponentsInChildren<Renderer>();

        }

        private void changeRendererColor(Color color)
        {
            rotationRenderers.ToList().ForEach(r => r.material.SetColor("_EmissionColor", color));
            completeRenderers.ToList().ForEach(r => r.material.SetColor("_EmissionColor", color));
            positionRenderers.ToList().ForEach(r => r.material.SetColor("_LineColor", color));

        }

        private void Update()
        {
            if (IsDraggingEnable && isDragging)
            {
                UpdatedDragging();
                return;
            }

            if (colliderManager.isCollision)
            {
                changeRendererColor(Color.red);
                return;
            }

            changeRendererColor(Color.yellow);


        }

        public void StartDragging()
        {
            if (!IsDraggingEnable)
                return;
            if (isDragging)
                return;

            InputManager.Instance.PushModalInputHandler(gameObject);
            isDragging = true;

            Vector3 targetPosition = target.transform.position;

            Vector3 handPosition;
            currentInputSource.TryGetPosition(currentInputSourceId, out handPosition);

            Vector3 pivotPosition = GetHandPivotPosition();

            handRefDistance = Vector3.Magnitude(handPosition - pivotPosition);
            objRefDistance = Vector3.Magnitude(targetPosition - pivotPosition);


            Vector3 objDirection = Vector3.Normalize(targetPosition - pivotPosition);
            Vector3 handDirection = Vector3.Normalize(handPosition - pivotPosition);

            objDirection = mainCamera.transform.InverseTransformDirection(objDirection);
            handDirection = mainCamera.transform.InverseTransformDirection(handDirection);

            gazeAngularOffset = Quaternion.FromToRotation(handDirection, objDirection);
            draggingPosition = targetPosition;

            rigidbody.useGravity = false;

        }

        public void UpdatedDragging()
        {
            Vector3 newHandPosition;
            currentInputSource.TryGetPosition(currentInputSourceId, out newHandPosition);

            Vector3 pivotPosition = GetHandPivotPosition();

            Vector3 newHandDirection = Vector3.Normalize(newHandPosition - pivotPosition);
            newHandDirection = mainCamera.transform.InverseTransformDirection(newHandDirection);

            Vector3 targetDirection = Vector3.Normalize(gazeAngularOffset * newHandDirection);
            targetDirection = mainCamera.transform.TransformDirection(targetDirection);

            float currentHandDistance = Vector3.Magnitude(newHandPosition - pivotPosition);
            float distanceRatio = currentHandDistance / handRefDistance;
            float distanceOffset = distanceRatio > 0 ?
                (distanceRatio - 1f) * TransformControlManager.Instance.distanceScale : 0;
            float targetDistance = objRefDistance + distanceOffset;

            draggingPosition = pivotPosition + (targetDirection * targetDistance);

            if (colliderManager.isCollision)
            {

                var p = (target.transform.position - draggingPosition).normalized;
                rigidbody.AddForce(p * (-1f));

                changeRendererColor(Color.red);

                return;
            }

            changeRendererColor(Color.blue);
            //rigidbody.MovePosition(draggingPosition);
            interpolator.SetTargetPosition(draggingPosition);

        }

        public void StopDragging()
        {
            if (!isDragging)
                return;

            InputManager.Instance.PopModalInputHandler();
            isDragging = false;
            currentInputSource = null;

            rigidbody.velocity = Vector3.zero;

        }

        private Vector3 GetHandPivotPosition()
        {
            return mainCamera.transform.position + new Vector3(0, -0.2f, 0) - mainCamera.transform.forward * 0.2f;
        }

        #region IInputHandler
        public void OnInputUp(InputEventData eventData)
        {
            if (currentInputSource != null && eventData.SourceId == currentInputSourceId)
                StopDragging();
        }

        public void OnInputDown(InputEventData eventData)
        {
            if (isDragging)
                return;

            if (!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position))
                return;

            currentInputSource = eventData.InputSource;
            currentInputSourceId = eventData.SourceId;

            StartDragging();
        }
        #endregion

        #region ISourceStateHandler
        public void OnSourceDetected(SourceStateEventData eventData)
        {
            // Nothing to do.
        }

        public void OnSourceLost(SourceStateEventData eventData)
        {
            if (currentInputSource != null && eventData.SourceId == currentInputSourceId)
                StopDragging();
        }




        #endregion
    }
}