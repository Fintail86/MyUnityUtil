using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    #region GameObject
    public static T GetOrAddComponent<T>(this Component pComp) where T : Component
    {
        if(pComp != null)
        {
            return GetOrAddComponent<T>(pComp.gameObject);
        }
        return null;
    }

    public static T GetOrAddComponent<T>(this GameObject pGameObj) where T : Component
    {
        var lComp = pGameObj.GetComponent<T>();
        if(lComp == null)
        {
            lComp = pGameObj.AddComponent<T>();
        }
        return lComp;
    }

    public static bool RemoveComponent<T>(this Component pComp) where T : Component
    {
        if (pComp != null)
        {
            return RemoveComponent<T>(pComp.gameObject);
        }
        return false;
    }


    public static bool RemoveComponent<T>(this GameObject pGameObj) where T : Component
    {
        var lComp = pGameObj.GetComponent<T>();
        if (lComp != null)
        {
            GameObject.Destroy(lComp);
            return true;
        }
        return false;
    }
    #endregion

    #region Transform
    public static void Init(this Transform pTrans, Transform parent)
    {
        if(pTrans != null)
        {
            pTrans.SetParent(parent);
            pTrans.localPosition = Vector3.zero;
            pTrans.localRotation = Quaternion.identity;
            pTrans.localScale = Vector3.one;
        }
    }

    public static void DestroyAllChildren(this Transform _tf)
    {
        finder.Clear();

        FindAllChildren(_tf);

        Debug.LogFormat("DestroyAllChildren will Deleate {0} child", finder.Count);

        foreach (var targetTf in finder.ToArray())
        {
            GameObject.Destroy(targetTf.gameObject);
        }
    }

    static Stack<Transform> finder = new Stack<Transform>();
    private static void FindAllChildren(Transform _tf)
    {
        for (int i = 0; i < _tf.childCount; i++)
        {
            if (_tf.GetChild(i).childCount > 0)
                FindAllChildren(_tf.GetChild(i));

            finder.Push(_tf.GetChild(i));
        }
    }

    public static Transform FindParent(this Transform pTrans, string pParentName)
    {
        if(pTrans != null && string.IsNullOrEmpty(pParentName) == false)
        {
            Transform lParent = pTrans.parent;
            while (lParent != null)
            {
                if(string.Equals(pParentName, lParent.name))
                {
                    return lParent;
                }
                lParent = lParent.parent;
            }
        }
        return null;
    }

    public static T FindParent<T>(this Transform pTrans) where T : Component
    {
        if (pTrans != null)
        {
            Transform lParent = pTrans.parent;
            while (lParent != null)
            {
                var lComponent = lParent.GetComponent<T>();
                if (lComponent != null)
                {
                    return lComponent;
                }
                lParent = lParent.parent;
            }
        }
        return null;
    }

    public static Vector2 GetPositionXY(this Transform pTrans)
    {
        var lPos = pTrans.position;
        return new Vector2(lPos.x, lPos.y);
    }

    public static Vector2 GetPositionXZ(this Transform pTrans)
    {
        var lPos = pTrans.position;
        return new Vector2(lPos.x, lPos.z);
    }

    public static Vector2 GetPositioYX(this Transform pTrans)
    {
        var lPos = pTrans.position;
        return new Vector2(lPos.y, lPos.x);
    }

    public static Vector2 GetPositioYZ(this Transform pTrans)
    {
        var lPos = pTrans.position;
        return new Vector2(lPos.y, lPos.z);
    }

    public static Vector2 GetPositioZX(this Transform pTrans)
    {
        var lPos = pTrans.position;
        return new Vector2(lPos.z, lPos.x);
    }

    public static Vector2 GetPositioZY(this Transform pTrans)
    {
        var lPos = pTrans.position;
        return new Vector2(lPos.z, lPos.y);
    }
    #endregion

    #region Animator
    public static void ResetParameters(this Animator pAnimator)
    {
        foreach (var lParam in pAnimator.parameters)
        {
            switch (lParam.type)
            {
                case AnimatorControllerParameterType.Float:
                    pAnimator.SetFloat(lParam.name, 0f);
                    break;
                case AnimatorControllerParameterType.Int:
                    pAnimator.SetInteger(lParam.name, 0);
                    break;
                case AnimatorControllerParameterType.Bool:
                    pAnimator.SetBool(lParam.name, false);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    pAnimator.ResetTrigger(lParam.name);
                    break;
            }
        }
    }

    public static void ResetTriggers(this Animator pAnimator)
    {
        foreach (var lParam in pAnimator.parameters)
        {
            switch (lParam.type)
            {
                case AnimatorControllerParameterType.Trigger:
                    pAnimator.ResetTrigger(lParam.name);
                    break;
            }
        }
    }
    #endregion
}
