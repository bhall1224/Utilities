using UnityEngine;

namespace Madman.Games.Utilities
{
    public class VectorAnalysis
    {
        public static float DistanceVectorMagnitude(GameObject objA, GameObject objB)
        {
            float xDiff = objB.transform.position.x - objA.transform.position.x;
            float zDiff = objB.transform.position.z - objA.transform.position.z;

            float diffMagnitude = Mathf.Sqrt((xDiff*xDiff) + (zDiff*zDiff));

            return diffMagnitude;
        }

        public static Vector3 CalculateDisplacementFromAngle2D(Vector3 rotation)
        {
            float angle = ConvertAngleToRadians(rotation.z * -1);
            float y = Mathf.Cos(angle);
            float x = Mathf.Sin(angle);
            return new Vector3(x, y, 0);
        }

        public static Vector3 CalculateDisplacementFromAngle(Vector3 rotation)
        {
            float angle = ConvertAngleToRadians(rotation.y);
            float z = Mathf.Cos(angle);
            float x = Mathf.Sin(angle);
            return new Vector3(x, 0, z);
        }

        public static float CalculateAngleFromDisplacement(Vector3 displacement)
        {
            float op = displacement.x;
            float adj = displacement.z;

            float angle = 0.0f;

            if (op != 0.0f && adj != 0.0f)
            {
                //Pythagorean Theorem
                float hypSqr = Mathf.Pow(op, 2) + Mathf.Pow(adj, 2);

                //get angle from inverse cosine
                float cos = adj / Mathf.Sqrt(hypSqr);
                angle = Mathf.Acos(cos);

                angle = ConvertAngleToDegrees(angle);

                if (op < 0)
                    angle *= -1;

                // Debug.Log($"Angle (degrees): {angle}");
            }

            return angle;
        }

        private static float ConvertAngleToRadians(float angle)
        {
            return angle * Mathf.PI / 180.0f;
        }

        private static float ConvertAngleToDegrees(float angle)
        {
            return angle * 180.0f / Mathf.PI;
        }
    }
}
