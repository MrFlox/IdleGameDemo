using System;
using Modules.ResourceSystem;
using TriInspector;

namespace Modules.Entities.Converter
{
    [Serializable]
    public class ConverterResourceData
    {
        [Group("res")] public ResourceSo ResourceIn;
        [Group("res")] public ResourceSo ResourceOut;
    }
}