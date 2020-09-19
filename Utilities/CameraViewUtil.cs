using UnityEngine;

namespace Madman.Games.Utilities
{
    public class CameraViewUtil
    {
        private const float PositiveOffScreen = 1.0f;
        private const float NegativeOffScreen = 0.0f;

        public static bool IsInCameraView(
          Camera camera,
          GameObject obj,
          out Vector3 viewVector,
          float positiveOffScreen = PositiveOffScreen,
          float negativeOffScreen = NegativeOffScreen)
        {
            viewVector = camera.WorldToViewportPoint(obj.transform.position);
            return viewVector.x <= positiveOffScreen
                && viewVector.x >= negativeOffScreen
                && viewVector.y <= positiveOffScreen
                && viewVector.y >= negativeOffScreen;
        }

        public static void ScreenViewDisplacement(
          Camera camera,
          GameObject obj,
          float positiveOffScreen = PositiveOffScreen,
          float negativeOffScreen = NegativeOffScreen)
        {
            Vector3 viewVector = new Vector3();
            if (!CameraViewUtil.IsInCameraView(camera, obj, out viewVector))
            {
                if (viewVector.x >= positiveOffScreen)
                {
                    obj.transform.position = new Vector3(
                        obj.transform.position.x * -1 + 1,
                        obj.transform.position.y,
                        obj.transform.position.z);
                }
                else if (viewVector.x <= negativeOffScreen)
                {
                    obj.transform.position = new Vector3(
                        obj.transform.position.x * -1 - 1,
                        obj.transform.position.y,
                        obj.transform.position.z);
                }
                else if (viewVector.y >= positiveOffScreen)
                {
                    obj.transform.position = new Vector3(
                        obj.transform.position.x,
                        obj.transform.position.y * -1 + 1,
                        obj.transform.position.z);
                }
                else if (viewVector.y <= negativeOffScreen)
                {
                    obj.transform.position = new Vector3(
                        obj.transform.position.x,
                        obj.transform.position.y * -1 - 1,
                        obj.transform.position.z);
                }
            }
        }
    }
}
