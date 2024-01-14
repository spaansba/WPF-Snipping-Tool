using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace Snipping_Tool_V4.Modules
{

    public static class PenStruct
    {
        public readonly record struct PenData(
            Color color,
            float Thickness = 1f,
            LineCap StartCap = LineCap.Round,
            LineCap EndCap = LineCap.Round,
            LineJoin LineJoin = LineJoin.Round,
            DashStyle DashStyle = DashStyle.Solid,
            float DashOffset = 5f
        );
    }

    public static class PenCache
    {
        private const int AllowedCacheMissesBeforePrune = 5;
        private static int cacheMisses;
        private static List<PenStruct.PenData>? tempList;
        private static List<PenStruct.PenData> TempList => tempList ??= new();


        /// <summary>
        /// https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/weak-references
        /// </summary>
        private static readonly Dictionary<PenStruct.PenData, WeakReference<Pen>> customPens = new();
        private static readonly ReadOnlyDictionary<PenStruct.PenData, Pen> builtInPens = CreateBuiltinPenDictionary().AsReadOnly();

        private static bool TryGetPen(PenStruct.PenData data, [NotNullWhen(true)] out Pen? pen)
        {
            pen = default;
            if (!customPens.TryGetValue(data, out var reference))
            {
                return false;
            }
            if (reference.TryGetTarget(out pen))
            {
                return true;
            }
            return false;
        }

        private static Dictionary<PenStruct.PenData, Pen> CreateBuiltinPenDictionary()
        {
            var pens = typeof(Pens)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Select(static property => property.GetValue(null) as Pen);
            var dictionary = new Dictionary<PenStruct.PenData, Pen>();
            foreach (var pen in pens)
            {
                if (pen is null)
                    continue;
                var penData = GetPenData(pen);
                dictionary.Add(penData, pen);
            }
            return dictionary;
        }
        private static PenStruct.PenData GetPenData(Pen pen)
        {
            return new PenStruct.PenData(
                color: pen.Color,
                Thickness: pen.Width,
                StartCap: pen.StartCap,
                EndCap: pen.EndCap,
                LineJoin: pen.LineJoin,
                DashStyle: pen.DashStyle,
                DashOffset: pen.DashOffset
                );
        }

        private static Pen CreatePen(PenStruct.PenData data)
        {
            return new Pen(data.color, data.Thickness)
            {
                StartCap = data.StartCap,
                EndCap = data.EndCap,
                LineJoin = data.LineJoin,
                DashStyle = data.DashStyle,
                DashOffset = data.DashOffset
            };
        }

        public static Pen GetPen(PenStruct.PenData data)
        {
            if (TryGetPen(data, out var pen))
            {
                return pen;
            }
            if (builtInPens.TryGetValue(data, out pen))
            {
                return pen;
            }
            Prune();
            pen = CreatePen(data);
            if (customPens.TryAdd(data, new WeakReference<Pen>(pen)) is false)
            {
                customPens[data].SetTarget(pen);
            }
            return pen;
        }

        private static void Prune()
        {
            ++cacheMisses;
            if (cacheMisses <= AllowedCacheMissesBeforePrune)
                return;
            TempList.AddRange(customPens.Keys);
            foreach (var key in TempList)
            {
                var reference = customPens[key];
                if (reference.TryGetTarget(out _) is false)
                {
                    customPens.Remove(key);
                }
            }
            TempList.Clear();
            cacheMisses = 0;
        }
        public static Pen GetPen(Color color, int thickness = 1) => GetPen(new PenStruct.PenData(color, thickness));
    }
}
