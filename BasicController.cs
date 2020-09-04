using Constants;
using UnityEngine;


namespace Utilties
{
    public class BasicController
    {
        public delegate void ActionCallback();

        public delegate void AnimationCallback(string input);

        public static void DefaultActionCallback()
        {
            Debug.Log("no action implemented...");
        }

        public static void DefaultAnimationCallback(string input)
        {
            Debug.Log($"animation state: {input}");
        }

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

            if (Input.GetKey(ControlConstants.Forward) || Input.GetKey(ControlConstants.ForwardAlt))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle(rotation);
                animationCallback(AnimationConstants.WalkState);
            }

            if (Input.GetKey(ControlConstants.Reverse) || Input.GetKey(ControlConstants.ReverseAlt))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle(rotation) * -1;
                animationCallback(AnimationConstants.WalkState);
            }

            if (Input.GetKey(ControlConstants.Left))
            {
                rotation.y += -5.0f;
            }

            if (Input.GetKey(ControlConstants.Right))
            {
                rotation.y += 5.0f;
            }

            if (Input.GetKey(ControlConstants.Space))
            {
                action = true;
                actionCallback();
            }

            if (displacement == Vector3.zero && !action)
            {
                animationCallback(AnimationConstants.IdleState);
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

            if (Input.GetKey(ControlConstants.Forward))
            {
                rotationalVector.y = 0.0f;
                displacementVector.z = 1;
                animationCallback(AnimationConstants.WalkState);
            }
            else if (Input.GetKey(ControlConstants.Reverse))
            {
                rotationalVector.y = 180.0f;
                displacementVector.z = -1;
                animationCallback(AnimationConstants.WalkState);
            }
            else if (Input.GetKey(ControlConstants.Left))
            {
                rotationalVector.y = 270;
                displacementVector.x = -1;
                animationCallback(AnimationConstants.WalkState);
            }
            else if (Input.GetKey(ControlConstants.Right))
            {
                rotationalVector.y = 90.0f;
                displacementVector.x = 1;
                animationCallback(AnimationConstants.WalkState);
            }
            else if (Input.GetKey(ControlConstants.Space))
            {
                actionCallback();
            }
            else
            {
                animationCallback(AnimationConstants.IdleState);
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
            }

            rotationalVector.y += xVelocity;

            displacementVector = VectorAnalysis.CalculateDisplacementFromAngle(rotationalVector);

            if (displacementVector != Vector3.zero
                && rotationalVector != transform.rotation.eulerAngles)
            {
                animationCallback(AnimationConstants.WalkState);
            }
            else
            {
                animationCallback(AnimationConstants.IdleState);
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
                actionCallback();

            if (displacementVector != Vector3.zero)
            {
                animationCallback(AnimationConstants.WalkState);
            }
            else
            {
                animationCallback(AnimationConstants.IdleState);
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

            if (Input.GetKey(ControlConstants.Forward))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle2D(rotation);
                animationCallback(AnimationConstants.WalkState);
            }

            if (Input.GetKey(ControlConstants.Reverse))
            {
                displacement = VectorAnalysis.CalculateDisplacementFromAngle2D(rotation) * -1;
                animationCallback(AnimationConstants.WalkState);
            }

            if (Input.GetKey(ControlConstants.Left))
            {
                rotation.z += rotationAmount;
            }

            if (Input.GetKey(ControlConstants.Right))
            {
                rotation.z -= rotationAmount;
            }

            if (Input.GetKey(ControlConstants.Space))
            {
                action = true;
                actionCallback();
            }

            if (displacement == Vector3.zero && !action)
            {
                animationCallback(AnimationConstants.IdleState);
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
            }

            rotationalVector.z -= xVelocity * rotationAmount;

            displacementVector = VectorAnalysis.CalculateDisplacementFromAngle2D(rotationalVector) * zVelocity;

            if (displacementVector != Vector3.zero
                && ((transform != null
                && rotationalVector != transform.rotation.eulerAngles)
                || rotationalVector != Vector3.zero))
            {
                animationCallback(AnimationConstants.WalkState);
            }
            else
            {
                animationCallback(AnimationConstants.IdleState);
            }
        }
    }
}
