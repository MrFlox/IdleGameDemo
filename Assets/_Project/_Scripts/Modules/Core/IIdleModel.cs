using System;
using UnityEngine;

namespace Modules.Core
{

    public interface ISavableAndLoadableModel
    {
        public void Save();
        public void Load();
    }


    public partial interface IIdleModel<TActionType, SType> where SType : ScriptableObject
    {
        void Subscribe(Action<TActionType> onChange);
        void Unsubscribe(Action<TActionType> onChange);
        void LoadFromSettings(SType settings);

    }
}