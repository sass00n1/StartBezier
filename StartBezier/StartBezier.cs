using UnityEngine;
using UnityEngine.Events;

namespace StartFramework.GamePlay
{
    public class StartBezier : MonoBehaviour
    {
        [Header("Posistion")]
        [SerializeField] public Vector2 startPos;
        [SerializeField] public Vector2 tangent0;
        [SerializeField] public Vector2 tangent1;
        [SerializeField] public Vector2 endPos;
        [Header("Speed")]
        [Range(0,1)]
        [SerializeField] private float speed = 0.1f;
        [Header("Visualization")]
        [SerializeField] private bool debug = true;
        [Header("Style")]
        [SerializeField] public Color color = Color.red;
        [SerializeField] public float width = 3;
        [Header("Event")]
        public UnityEvent OnMoveEnd;

        //内部变量
        float weight = 0;
        bool useEvent;

        private void Update()
        {
            weight = Mathf.MoveTowards(weight, 1f, Time.deltaTime * speed);

            Vector2 a0 = startPos.LinearInterpolate(tangent0, weight);
            Vector2 a1 = tangent0.LinearInterpolate(tangent1, weight);
            Vector2 a2 = tangent1.LinearInterpolate(endPos, weight);

            Vector2 b0 = a0.LinearInterpolate(a1, weight);
            Vector2 b1 = a1.LinearInterpolate(a2, weight);

            Vector2 s = b0.LinearInterpolate(b1, weight);

            transform.position = s;

            if (weight != 1)
            {
                //旋转
                float z = BezierUtility.AngleWithAtan2(transform.position, b1, Vector2.right);
                transform.eulerAngles = new Vector3(0, 0, z);
            }

            if (useEvent == false && weight == 1)
            {
                OnMoveEnd?.Invoke();
                useEvent = true;
            }

            if (debug)
            {
                Debug.DrawLine(startPos, tangent0, Color.gray, 0.3f);
                Debug.DrawLine(tangent0, tangent1, Color.gray, 0.3f);
                Debug.DrawLine(tangent1, endPos, Color.gray, 0.3f);

                Debug.DrawLine(a0, a1, Color.green, 0.3f);
                Debug.DrawLine(a1, a2, Color.green, 0.3f);
                Debug.DrawLine(b0, b1, Color.blue, 0.3f);
            }
        }

        [ContextMenu("同步Transformd到曲线开始位置")]
        public void SyncTransformToStartPos()
        {
            transform.position = startPos;
        }

        [ContextMenu("重置为标准位置")]
        public void Default()
        {
            startPos = Vector2.zero;
            endPos = new Vector2(10, 0);
            tangent0 = new Vector2(0, 10);
            tangent1 = new Vector2(10, 10);
            transform.position = Vector2.zero;
        }
    }
}