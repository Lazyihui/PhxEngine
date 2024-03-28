using UnityEngine;
namespace PhxEngine2D {

    public static class IntersectionUtil {
        // AABB & AABB
        public static bool IntersectionUtil_AABB_AABB(Vector2 amin, Vector2 amax, Vector2 bmin, Vector2 bmax) {
            return amin.x <= bmax.x && amax.x >= bmin.x && amin.y <= bmax.y && amax.y >= bmin.y;
        }
        // circle & circle
        // public static bool IntersectionUtil_Circle_Circle(Vector2 aCenter, float aRadius, Vector2 bCenter, float bRadius) {

        //     Vector2  diff = aCenter - bCenter;
        //     float distSqr = diff.magnitude;
        //     return distSqr <= (aRadius + bRadius)*(aRadius + bRadius);
        // }
        public static bool IsIntersected_Circle_Circle(Vector2 aCenter, float aRadius, Vector2 bCenter, float bRadius) {
            Vector2 diff = aCenter - bCenter;
            // float dis = diff.magnitude; // 得到开平方后的结果
            float disSqr = diff.sqrMagnitude; // 不开平方
            return disSqr <= (aRadius + bRadius) * (aRadius + bRadius);
        }
    }
}