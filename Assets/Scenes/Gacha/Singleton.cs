using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T obj;
        
        public static T Obj
        {
            get
            {
                if (obj) return obj;
                
                obj = FindObjectOfType<T>();

                if (!obj) obj = new GameObject(typeof(T).ToString()).AddComponent<T>();

                return obj;
            }
        }

        public static bool HasInstance => obj != null;

        protected virtual void Awake()
        {
            if (!HasInstance)
            {
                obj = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
