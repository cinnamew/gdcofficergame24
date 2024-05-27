using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class ExtensionMethods
{
    public static void RemoveCloneName(this GameObject gameObject) =>
        gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);

    public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
    {
        foreach (var mType in dict)
            action?.Invoke(mType.Key, mType.Value);
    }

    public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TValue> value)
    {
        foreach (var mType in dict)
            value?.Invoke(mType.Value);
    }

    public static T GetOrAddComponent<T>(this GameObject tGO) where T : Component
    {
        if (null == tGO)
        {
            return null;
        }

        var type = typeof(T);
        var component = tGO.GetComponent(type);
        if (null == component)
        {
            component = tGO.AddComponent(type);
        }

        return component as T;
    }

    public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length < 1;

    public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count < 1;

    public static bool IsNullOrEmpty<T>(this Queue<T> queue) => queue == null || queue.Count < 1;

    public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> dictionary) =>
        dictionary == null || dictionary.Count < 1;

    public static bool IsNull(this GameObject go) => go == null;

    public static bool IsNull(this Component component) => component == null;

    public static T SelectRandom<T>(this T[] array)
    {
        return array.IsNullOrEmpty() ? default : array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static T SelectRandom<T>(this List<T> list)
    {
        return list.IsNullOrEmpty() ? default : list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static Vector2Int ToVector2Int(this Vector2 vector2) => new Vector2Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));

    public static Vector2 ToVector2(this Vector2Int vector2Int) => new Vector2(vector2Int.x, vector2Int.y);
    
    public static Coroutine DoAfter(this MonoBehaviour monoBehaviour, float seconds, Action action) => monoBehaviour.StartCoroutine(DoAfterCoroutine(seconds, action));
    
    private static IEnumerator DoAfterCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
    
    public static void DoAfter(this MonoBehaviour monoBehaviour, Func<bool> condition, Action action) => monoBehaviour.StartCoroutine(DoAfterCoroutine(condition, action));
    
    private static IEnumerator DoAfterCoroutine(Func<bool> condition, Action action)
    {
        yield return new WaitUntil(condition);
        action?.Invoke();
    }

    public static void FadeIn(this CanvasGroup canvasGroup, float duration)
    {
        canvasGroup.DOFade(1f, duration).OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    
    public static void FadeOut(this CanvasGroup canvasGroup, float duration)
    {
        canvasGroup.DOFade(0f, duration).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
}