using UnityEngine;

namespace PhxEngine2D {

    public static class Restoration {
        public static void RestorePenetration_Circle_Cilcle(RBEntity a, RBEntity b) {
            // 1. 计算穿透深度
            // 2. 计算穿透方向
            // 3. 修正位置
            // 4. 修正速度

            bool isIntersected = IntersectionUtil.IsIntersected_Circle_Circle(a.position, a.size.x, b.position, b.size.x, out float intersectedLen);

            if (!isIntersected) {
                return;
            }
            // b指向a的方向
            Vector2 b2a_dir = (a.position - b.position).normalized;
            // a指向b的方向
            Vector2 a2b_dir = -b2a_dir;

            if (a.isStatic && !b.isStatic) {
                b.position += a2b_dir * intersectedLen;
            } else if (!a.isStatic && b.isStatic) {
                a.position += b2a_dir * intersectedLen;
            } else {
                // 恢复一半
                a.position += b2a_dir * intersectedLen / 2;
                b.position += a2b_dir * intersectedLen / 2;
            }

            // // 记录速度
            // Vector2 a_velo = a.velocity;
            // Vector2 b_velo = b.velocity;
            // 因为没有质量所以直接按一半来恢复
            // 速度方向

        }
    }
}