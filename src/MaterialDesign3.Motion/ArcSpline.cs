namespace MaterialDesignThemes.Motion;

/// <summary>
/// This provides a curve fit system that stitches the x,y path together with quarter ellipses.
/// </summary>
internal sealed class ArcSpline
{
    private static readonly bool IsExtrapolate = true;

    private readonly Arc[][] _arcs;

    /// <summary>
    /// This provides a curve fit system that stitches the x,y path together with quarter ellipses.
    /// </summary>
    /// <param name="arcModes">Array of arc mode values. Expected to be of size n - 1.</param>
    /// <param name="timePoints">Array of timestamps. Expected to be of size n. Seconds preferred.</param>
    /// <param name="y">
    /// Array of values (of size n), where each value is spread on a [FloatArray] for each of
    /// its dimensions, expected to be of even size since two values are needed to interpolate arcs.
    /// </param>
    public ArcSpline(int[] arcModes, float[] timePoints, float[][] y)
    {
        var mode = StartVertical;
        var last = StartVertical;

        var count = timePoints.Length - 1;
        _arcs = new Arc[count][];

        for (int i = 0; i < count; i++)
        {
            switch (arcModes[i])
            {
                case ArcSplineArcStartVertical:
                    mode = StartVertical;
                    last = mode;
                    break;
                case ArcSplineArcStartHorizontal:
                    mode = StartHorizontal;
                    last = mode;
                    break;
                case ArcSplineArcStartFlip:
                    mode = last == StartVertical ? StartHorizontal : StartVertical;
                    last = mode;
                    break;
                case ArcSplineArcStartLinear:
                    mode = StartLinear;
                    break;
                case ArcSplineArcBelow:
                    mode = DownArc;
                    break;
                case ArcSplineArcAbove:
                    mode = UpArc;
                    break;
            }

            var yArray = y[i];
            var yArray1 = y[i + 1];
            var time1 = timePoints[i];
            var time2 = timePoints[i + 1];

            int dim = yArray.Length / 2 + yArray.Length % 2; // matches Kotlin (expects even size)
            var arcsForSegment = new Arc[dim];
            for (int j = 0; j < dim; j++)
            {
                int k = j * 2;
                arcsForSegment[j] = new Arc(
                    mode: mode,
                    time1: time1,
                    time2: time2,
                    x1: yArray[k],
                    y1: yArray[k + 1],
                    x2: yArray1[k],
                    y2: yArray1[k + 1]);
            }
            _arcs[i] = arcsForSegment;
        }
    }

    /// <summary>
    /// get the values of the at t point in time.
    /// </summary>
    public void GetPos(float time, float[] values) => GetPos(time, values, 0);

    public void GetPos(float time, float[] values, int offset)
    {
        var arcs = _arcs;
        int lastIndex = arcs.Length - 1;
        float start = arcs[0][0].Time1;
        float end = arcs[lastIndex][0].Time2;
        int size = values.Length - offset;
        if (size <= 0)
        {
            return;
        }

        if (IsExtrapolate)
        {
            if (time < start || time > end)
            {
                int p;
                float t0;
                if (time > end)
                {
                    p = lastIndex;
                    t0 = end;
                }
                else
                {
                    p = 0;
                    t0 = start;
                }
                float dt = time - t0;

                int i = offset;
                int j = 0;
                while (i < offset + size - 1 && j < arcs[p].Length)
                {
                    var arc = arcs[p][j];
                    if (arc.IsLinear)
                    {
                        values[i] = arc.GetLinearX(t0) + dt * arc.LinearDx;
                        values[i + 1] = arc.GetLinearY(t0) + dt * arc.LinearDy;
                    }
                    else
                    {
                        arc.SetPoint(t0);
                        values[i] = arc.CalcX() + dt * arc.CalcDx();
                        values[i + 1] = arc.CalcY() + dt * arc.CalcDy();
                    }
                    i += 2;
                    j++;
                }
                return;
            }
        }
        else
        {
            if (time < start) time = start;
            if (time > end) time = end;
        }

        bool populated = false;
        for (int seg = 0; seg < arcs.Length; seg++)
        {
            int j = 0; // arc index within segment
            int i = offset; // output index
            while (i < offset + size - 1 && j < arcs[seg].Length)
            {
                var arc = arcs[seg][j];
                if (time <= arc.Time2)
                {
                    if (arc.IsLinear)
                    {
                        values[i] = arc.GetLinearX(time);
                        values[i + 1] = arc.GetLinearY(time);
                    }
                    else
                    {
                        arc.SetPoint(time);
                        values[i] = arc.CalcX();
                        values[i + 1] = arc.CalcY();
                    }
                    populated = true;
                }
                i += 2;
                j++;
            }
            if (populated)
            {
                return;
            }
        }
    }

    /// <summary>
    /// Get the differential of the curves at point t
    /// </summary>
    public void GetSlope(float time, float[] slope)
    {
        var arcs = _arcs;
        float start = arcs[0][0].Time1;
        float end = arcs[arcs.Length - 1][0].Time2;
        if (time < start) time = start; else if (time > end) time = end;

        int size = slope.Length;
        bool populated = false;

        for (int seg = 0; seg < arcs.Length; seg++)
        {
            int i = 0; // output index
            int j = 0; // arc index
            while (i < size - 1 && j < arcs[seg].Length)
            {
                var arc = arcs[seg][j];
                if (time <= arc.Time2)
                {
                    if (arc.IsLinear)
                    {
                        slope[i] = arc.LinearDx;
                        slope[i + 1] = arc.LinearDy;
                    }
                    else
                    {
                        arc.SetPoint(time);
                        slope[i] = arc.CalcDx();
                        slope[i + 1] = arc.CalcDy();
                    }
                    populated = true;
                }
                i += 2;
                j++;
            }
            if (populated)
            {
                return;
            }
        }
    }

    public float GetSlope(float time, int component)
    {
        var arcs = _arcs;
        float start = arcs[0][0].Time1;
        float end = arcs[arcs.Length - 1][0].Time2;
        if (time < start) time = start; else if (time > end) time = end;

        int pair = component / 2;
        bool xComponent = (component % 2) == 0;

        for (int seg = 0; seg < arcs.Length; seg++)
        {
            if (pair >= arcs[seg].Length)
            {
                continue;
            }

            var arc = arcs[seg][pair];
            if (time <= arc.Time2)
            {
                if (arc.IsLinear)
                {
                    return xComponent ? arc.LinearDx : arc.LinearDy;
                }

                arc.SetPoint(time);
                return xComponent ? arc.CalcDx() : arc.CalcDy();
            }
        }

        return 0f;
    }

    private sealed class Arc
    {
        private const int LutSize = 101;
        private const float Epsilon = 0.001f;
        private const float HalfPi = (float)(Math.PI * 0.5);

        private float _arcDistance;
        private float _tmpSinAngle;
        private float _tmpCosAngle;

        private readonly float[] _lut;
        private readonly float _oneOverDeltaTime;
        private readonly float _arcVelocity;
        private readonly float _vertical;

        internal readonly float EllipseA;
        internal readonly float EllipseB;

        internal readonly bool IsLinear;

        /// <summary>
        /// also used to cache the slope in the unused center
        /// </summary>
        internal readonly float EllipseCenterX;

        /// <summary>
        /// also used to cache the slope in the unused center
        /// </summary>
        internal readonly float EllipseCenterY;

        public float LinearDx => EllipseCenterX;
        public float LinearDy => EllipseCenterY;

        public float Time1 { get; }
        public float Time2 { get; }

        private readonly float _x1;
        private readonly float _y1;
        private readonly float _x2;
        private readonly float _y2;

        public Arc(int mode, float time1, float time2, float x1, float y1, float x2, float y2)
        {
            Time1 = time1;
            Time2 = time2;
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;

            float dx = x2 - x1;
            float dy = y2 - y1;
            bool isVertical = mode switch
            {
                StartVertical => true,
                UpArc => dy < 0,
                DownArc => dy > 0,
                _ => false
            };

            _vertical = isVertical ? -1.0f : 1.0f;
            _oneOverDeltaTime = 1f / (Time2 - Time1);
            _lut = new float[LutSize];

            bool isLinear = mode == StartLinear;
            if (isLinear || Math.Abs(dx) < Epsilon || Math.Abs(dy) < Epsilon)
            {
                isLinear = true;
                _arcDistance = Hypot(dy, dx);
                _arcVelocity = _arcDistance * _oneOverDeltaTime;
                EllipseCenterX = dx * _oneOverDeltaTime; // cache the slope in the unused center
                EllipseCenterY = dy * _oneOverDeltaTime; // cache the slope in the unused center
                EllipseA = float.NaN;
                EllipseB = float.NaN;
            }
            else
            {
                EllipseA = dx * _vertical;
                EllipseB = dy * -_vertical;
                EllipseCenterX = isVertical ? x2 : x1;
                EllipseCenterY = isVertical ? y1 : y2;
                BuildTable(x1, y1, x2, y2);
                _arcVelocity = _arcDistance * _oneOverDeltaTime;
            }

            IsLinear = isLinear;
        }

        public void SetPoint(float time)
        {
            float angle = CalcAngle(time);
            _tmpSinAngle = (float)Math.Sin(angle);
            _tmpCosAngle = (float)Math.Cos(angle);
        }

        private float CalcAngle(float time)
        {
            float percent = (_vertical < 0f ? Time2 - time : time - Time1) * _oneOverDeltaTime;
            return HalfPi * Lookup(percent);
        }

        public float CalcX() => EllipseCenterX + EllipseA * _tmpSinAngle;

        public float CalcY() => EllipseCenterY + EllipseB * _tmpCosAngle;

        public float CalcDx()
        {
            float vx = EllipseA * _tmpCosAngle;
            float vy = -EllipseB * _tmpSinAngle;
            float norm = _arcVelocity / Hypot(vx, vy);
            return vx * _vertical * norm;
        }

        public float CalcDy()
        {
            float vx = EllipseA * _tmpCosAngle;
            float vy = -EllipseB * _tmpSinAngle;
            float norm = _arcVelocity / Hypot(vx, vy);
            return vy * _vertical * norm;
        }

        public float GetLinearX(float time)
        {
            float t = (time - Time1) * _oneOverDeltaTime;
            return _x1 + t * (_x2 - _x1);
        }

        public float GetLinearY(float time)
        {
            float t = (time - Time1) * _oneOverDeltaTime;
            return _y1 + t * (_y2 - _y1);
        }

        private float Lookup(float v)
        {
            if (v <= 0f) return 0f;
            if (v >= 1f) return 1f;
            float pos = v * (LutSize - 1);
            int iv = (int)pos;
            float off = pos - iv;
            return _lut[iv] + off * (_lut[iv + 1] - _lut[iv]);
        }

        /// <summary>
        /// Build the lookup table for arc traversal
        /// </summary>
        private void BuildTable(float x1, float y1, float x2, float y2)
        {
            float a = x2 - x1;
            float b = y1 - y2;
            float lx = 0f;
            float ly = b; // == b * cos(0)
            float dist = 0f;

            var ourPercent = OurPercentCache;
            int lastIndex = ourPercent.Length - 1;
            float lastIndexFloat = lastIndex;

            for (int i = 1; i <= lastIndex; i++)
            {
                float angle = (float)ToRadians(90.0 * i / lastIndex);
                float s = (float)Math.Sin(angle);
                float c = (float)Math.Cos(angle);
                float px = a * s;
                float py = b * c;
                dist += Hypot((px - lx), (py - ly));
                ourPercent[i] = dist;
                lx = px;
                ly = py;
            }

            _arcDistance = dist;
            for (int i = 1; i <= lastIndex; i++)
            {
                ourPercent[i] /= dist;
            }

            float lutLastIndex = (LutSize - 1);
            for (int i = 0; i < _lut.Length; i++)
            {
                float pos = i / lutLastIndex;
                int index = BinarySearch(ourPercent, pos);
                if (index >= 0)
                {
                    _lut[i] = index / lastIndexFloat;
                }
                else if (index == -1)
                {
                    _lut[i] = 0f;
                }
                else
                {
                    int p1 = -index - 2;
                    int p2 = -index - 1;
                    float ans = (p1 + (pos - ourPercent[p1]) / (ourPercent[p2] - ourPercent[p1])) / lastIndexFloat;
                    _lut[i] = ans;
                }
            }
        }

        private static float Hypot(float x, float y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            if (x < y)
            {
                var t = x; x = y; y = t;
            }
            if (x <= 0f)
            {
                return 0f;
            }
            float r = y / x;
            return x * (float)Math.Sqrt(1f + r * r);
        }
    }

    internal const int ArcSplineArcStartLinear = 0;
    internal const int ArcSplineArcStartVertical = 1;
    internal const int ArcSplineArcStartHorizontal = 2;
    internal const int ArcSplineArcStartFlip = 3;
    internal const int ArcSplineArcBelow = 4;
    internal const int ArcSplineArcAbove = 5;

    private const int StartVertical = 1;
    private const int StartHorizontal = 2;
    private const int StartLinear = 3;
    private const int DownArc = 4;
    private const int UpArc = 5;

    private static readonly float[] OurPercentCache = new float[91];

    internal static double ToRadians(double value) => value * Math.PI / 180.0;

    /// <summary>
    /// Binary search similar to java.util.Arrays.binarySearch
    /// for floats on a partially filled array starting at 0.
    /// </summary>
    internal static int BinarySearch(float[] array, float position)
    {
        int low = 0;
        int high = array.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) >> 1;
            float midVal = array[mid];

            if (midVal < position)
            {
                low = mid + 1;
            }
            else if (midVal > position)
            {
                high = mid - 1;
            }
            else
            {
                return mid; // key found
            }
        }
        return -(low + 1); // key not found
    }
}
