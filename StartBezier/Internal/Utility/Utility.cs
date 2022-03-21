using UnityEngine;

namespace StartFramework.GamePlay
{
    public static class BezierUtility
    {
        public static float AngleWithAtan2(Vector2 from, Vector2 to, Vector2 spriteForward)
        {
            float adz = to.x - from.x;
            float oppo = to.y - from.y;

            float tempAngle = Mathf.Atan2(oppo, adz) * Mathf.Rad2Deg;

            float angle = 0;
            if (spriteForward == Vector2.up)
            {
                angle = tempAngle - 90;
            }
            else if (spriteForward == Vector2.right)
            {
                angle = tempAngle;
            }
            else
            {
                Debug.LogError("方法：AngleWithAtan2。spriteForward参数错误");
            }

            return angle;
        }
    }

    public static class BezierHelper
    {
        public static Vector2 LinearInterpolate(this Vector2 v2, Vector2 to, float weight)
        {
            return new Vector2
            (
                MathLerp(v2.x, to.x, weight),
                MathLerp(v2.y, to.y, weight)
            );
        }
        public static float MathLerp(float from, float to, float weight)
        {
            return from + ((to - from) * weight);
        }
    }
}