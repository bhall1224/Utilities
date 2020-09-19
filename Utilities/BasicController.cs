using UnityEngine;

namespace Madman.Games.Utilities
{
    public class BasicController
    {
        public delegate void ActionCallback();

        public delegate void AnimationCallback(
            bool travelAnimation = false,
            bool actionAnimation = false,
            bool idleAnimation = false);

        public static void DefaultActionCallback() { }

        public static void DefaultAnimationCallback(
            bool travelAnimation = false,
            bool actionAnimation = false,
            bool idleAnimation = false) { }

        public static void FirstPersonControlHandler(
            out Vector3 displacement,
            out Vector3 rotation,
            Transform transform = null,
            ActionCallback actionCallback = null,
            AnimationCallback animationCallback = null)
        {

            displacement = Vector3.zero;

            if (transform == null)
                rotation = Vector3.zero;
            else
                rotation = transform.rotation.eulerAngles;

            bool action = false;

            if (animationCallback == null)
                animationCallback = DefaultAnimationCallback;

            if (actionCallback == null)
                actionCallback = DefaultActionCallback;

            if (Input.GetKey(Controls.Forward) || Input.GetKey(Controls.ForwardAlt))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle(rotation);
                animationCallback(travelAnimation: true);
            }

            if (Input.GetKey(Controls.Reverse) || Input.GetKey(Controls.ReverseAlt))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle(rotation) * -1;
                animationCallback(travelAnimation: true);
            }

            if (Input.GetKey(Controls.Left))
            {
                rotation.y += -5.0f;
            }

            if (Input.GetKey(Controls.Right))
            {
                rotation.y += 5.0f;
            }

            if (Input.GetKey(Controls.Space))
            {
                action = true;
                actionCallback();
                animationCallback(actionAnimation: true);
            }

            if (displacement == Vector3.zero && !action)
            {
                animationCallback(idleAnimation: true);
            }
        }

        public static void TopViewControlHandler(
            out Vector3 displacementVector,
            out Vector3 rotationalVector,
            Transform transform = null,
            ActionCallback actionCallback = null,
            AnimationCallback animationCallback = null)
        {

            displacementVector = Vector3.zero;

            if (transform == null)
                rotationalVector = Vector3.zero;
            else
                rotationalVector = transform.rotation.eulerAngles;

            if (animationCallback == null)
                animationCallback = DefaultAnimationCallback;

            if (actionCallback == null)
                actionCallback = DefaultActionCallback;

            if (Input.GetKey(Controls.Forward))
            {
                rotationalVector.y = 0.0f;
                displacementVector.z = 1;
                animationCallback(travelAnimation: true);
            }
            else if (Input.GetKey(Controls.Reverse))
            {
                rotationalVector.y = 180.0f;
                displacementVector.z = -1;
                animationCallback(travelAnimation: true);
            }
            else if (Input.GetKey(Controls.Left))
            {
                rotationalVector.y = 270;
                displacementVector.x = -1;
                animationCallback(travelAnimation: true);
            }
            else if (Input.GetKey(Controls.Right))
            {
                rotationalVector.y = 90.0f;
                displacementVector.x = 1;
                animationCallback(travelAnimation: true);
            }
            else if (Input.GetKey(Controls.Space))
            {
                actionCallback();
                animationCallback(actionAnimation: true);
            }
            else
            {
                animationCallback(idleAnimation: true);
            }
        }

        public static void FirstPersonTouchScreenControlHandler(
            float xVelocity,
            float zVelocity,
            bool buttonPressed,
            out Vector3 displacementVector,
            out Vector3 rotationalVector,
            Transform transform = null,
            ActionCallback actionCallback = null,
            AnimationCallback animationCallback = null)
        {
            displacementVector = Vector3.zero;

            if (transform == null)
                rotationalVector = Vector3.zero;
            else
                rotationalVector = transform.rotation.eulerAngles;

            if (animationCallback == null)
                animationCallback = DefaultAnimationCallback;

            if (actionCallback == null)
                actionCallback = DefaultActionCallback;

            if (buttonPressed)
            {
                actionCallback();
                animationCallback(actionAnimation: true);
            }

            rotationalVector.y += xVelocity;

            displacementVector = VectorAnalysis.CalculateDisplacementFromAngle(rotationalVector) * zVelocity;

            if (displacementVector != Vector3.zero
                && rotationalVector != transform.rotation.eulerAngles)
            {
                animationCallback(travelAnimation: true);
            }
            else
            {
                animationCallback(idleAnimation: true);
            }
        }

        public static void TopDownTouchScreenControlHandler(
            float xVelocity,
            float zVelocity,
            bool buttonPressed,
            out Vector3 displacementVector,
            out Vector3 rotationalVector,
            Transform transform = null,
            ActionCallback actionCallback = null,
            AnimationCallback animationCallback = null)
        {
            displacementVector = new Vector3(xVelocity, 0, zVelocity);

            if (transform == null)
                rotationalVector = Vector3.zero;
            else
                rotationalVector = transform.rotation.eulerAngles;

            rotationalVector.y = VectorAnalysis.CalculateAngleFromDisplacement(displacementVector);

            if (animationCallback == null)
                animationCallback = DefaultAnimationCallback;

            if (actionCallback == null)
                actionCallback = DefaultActionCallback;

            if (buttonPressed)
            {
                actionCallback();
                animationCallback(actionAnimation: true);
            }

            if (displacementVector != Vector3.zero)
            {
                animationCallback(travelAnimation: true);
            }
            else
            {
                animationCallback(idleAnimation: true);
            }
        }


        public static void TopDown2DVectorControl(
            float rotationAmount,
            out Vector3 displacement,
            out Vector3 rotation,
            Transform transform = null,
            ActionCallback actionCallback = null,
            AnimationCallback animationCallback = null)
        {
            displacement = Vector3.zero;

            if (transform == null)
                rotation = Vector3.zero;
            else
                rotation = transform.rotation.eulerAngles;

            bool action = false;

            if (animationCallback == null)
                animationCallback = DefaultAnimationCallback;

            if (actionCallback == null)
                actionCallback = DefaultActionCallback;

            if (Input.GetKey(Controls.Forward))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle2D(rotation);
                animationCallback(travelAnimation: true);
            }

            if (Input.GetKey(Controls.Reverse))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle2D(rotation) * -1;
                animationCallback(travelAnimation: true);
            }

            if (Input.GetKey(Controls.Left))
            {
                rotation.z += rotationAmount;
            }

            if (Input.GetKey(Controls.Right))
            {
                rotation.z -= rotationAmount;
            }

            if (Input.GetKey(Controls.Space))
            {
                action = true;
                actionCallback();
                animationCallback(actionAnimation: true);
            }

            if (displacement == Vector3.zero && !action)
            {
                animationCallback(idleAnimation: true);
            }
        }

        public static void TopDown2DVectorTouchControl(
            float rotationAmount,
            float xVelocity,
            float zVelocity,
            bool buttonPressed,
            out Vector3 displacementVector,
            out Vector3 rotationalVector,
            Transform transform = null,
            ActionCallback actionCallback = null,
            AnimationCallback animationCallback = null)
        {
            displacementVector = Vector3.zero;

            if (transform == null)
                rotationalVector = Vector3.zero;
            else
                rotationalVector = transform.rotation.eulerAngles;

            if (animationCallback == null)
                animationCallback = DefaultAnimationCallback;

            if (actionCallback == null)
                actionCallback = DefaultActionCallback;

            if (buttonPressed)
            {
                actionCallback();
                animationCallback(actionAnimation: true);
            }

            rotationalVector.z -= xVelocity * rotationAmount;

            displacementVector = VectorAnalysis.CalculateDisplacementFromAngle2D(rotationalVector) * zVelocity;

            if (displacementVector != Vector3.zero
                && ((transform != null
                && rotationalVector != transform.rotation.eulerAngles)
                || rotationalVector != Vector3.zero))
            {
                animationCallback(travelAnimation: true);
            }
            else
            {
                animationCallback(idleAnimation: true);
            }
        }
    }
}
