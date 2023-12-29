using System;
using Cinemachine;
using UnityEngine;

namespace Cameras
{
    public class CameraFollow : CinemachineExtension
    {
        private const float MinY = 9f;
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body) return;
            Vector3 pos = state.RawPosition;
            pos.y = Mathf.Max(pos.y, MinY);
            state.RawPosition = pos;
        }
    }
}