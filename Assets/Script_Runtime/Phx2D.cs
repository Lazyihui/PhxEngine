using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhxEngine2D {
    public class Phx2D {

        Dictionary<int, RBEntity> all;

        public Vector2 gravity;
        public Phx2D() {
            gravity = new Vector2(0, -9.8f);
            all = new Dictionary<int, RBEntity>();
        }

        public void Tick(float dt) {
            // 1.引力应用
            foreach (RBEntity rb in all.Values) {
                // 速度发生变化
                rb.velocity += gravity * rb.gravityScale * dt;
            }
            // 2.根据速度更新位置
            foreach (RBEntity rb in all.Values) {
                // 坐标发生变化
                rb.position += rb.velocity * dt;
            }
            // 3.交叉检测
            // 4、交叉检测事件触发
            // 5.穿透恢复
            // 6.穿透恢复事件触发

        }

        // 添加刚体
        public RBEntity Add(int id) {
            RBEntity rb = new RBEntity();
            rb.id = id;
            all.Add(id, rb);
            return rb;
        }
        // 移除刚体

        // 查找刚体
    }
}
