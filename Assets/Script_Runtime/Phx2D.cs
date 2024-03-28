using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhxEngine2D {
    public class Phx2D {

        SortedList<int, RBEntity> all;

        public Vector2 gravity;

        HashSet<ulong> intersectedSet;

        public Action<RBEntity, RBEntity> OnIntersectEnterHandle;//交叉开始

        public Action<RBEntity, RBEntity> OnIntersectStayHandle;//交叉持续
        public Action<RBEntity, RBEntity> OnIntersectExitHandle;//交叉结束

        public Phx2D() {
            gravity = new Vector2(0, -9.8f);
            all = new SortedList<int, RBEntity>();
        }

        public void Tick(float dt) {
            // 1.引力应用
            foreach (RBEntity rb in all.Values) {
                if (rb.isStatic) {
                    //  静态是不受引力影响的
                    continue;
                }
                // 速度发生变化
                rb.velocity += gravity * rb.gravityScale * dt;
            }
            // 2.根据速度更新位置
            foreach (RBEntity rb in all.Values) {
                // 坐标发生变化
                rb.position += rb.velocity * dt;
            }
            // 3.交叉检测
            // aabb（中心对称，无旋转的包围盒）  obb（带旋转的包围盒） Circle  Convex凸多边形
            for (int i = 0; i < all.Count; i += 1) {
                RBEntity a = all.Values[i];
                // 和所有的刚体进行交叉检测
                for (int j = i + 1; j < all.Count; j += 1) {
                    RBEntity b = all.Values[j];
                    // aabb & aabb
                    Intersect_RB_RB(a, b);
                }
            }
            // 4、交叉检测事件触发
            // 5.穿透恢复
            // 6.穿透恢复事件触发

        }

        // 添加刚体
        public RBEntity Add(int id, ShapeType shapeType, Vector2 size) {
            RBEntity rb = new RBEntity();
            rb.id = id;
            rb.shapeType = shapeType;
            rb.size = size;
            all.Add(id, rb);
            return rb;
        }
        // 移除刚体

        // 查找刚体
        // RB &Rb 检测
        void Intersect_RB_RB(RBEntity a, RBEntity b) {
            if (a.shapeType == ShapeType.Circle && b.shapeType == ShapeType.Circle) {
                // 圆和圆
                bool isIntersected = IntersectionUtil.IsIntersected_Circle_Circle(a.position, a.size.x, b.position, b.size.x);
                if (isIntersected) {
                    a.isIntersected = true;
                    b.isIntersected = true;



                    OnIntersectEnterHandle.Invoke(a, b);
                } else {
                    a.isIntersected = false;
                    b.isIntersected = false;
                }
            } else if (a.shapeType == ShapeType.Square && b.shapeType == ShapeType.Square) {
                // 
            } else if (a.shapeType == ShapeType.Circle && b.shapeType == ShapeType.Square) {
                // 
            } else if (a.shapeType == ShapeType.Square && b.shapeType == ShapeType.Circle) {
                // 
            } else {
                Debug.LogError("未知的形状类型");
            }
        }
    }
}
