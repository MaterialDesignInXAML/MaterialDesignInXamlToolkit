namespace MaterialDesignThemes.Motion;

internal sealed class MonoSpline
{
    private readonly float[] _timePoints;
    private readonly float[][] _values;
    private readonly float[][] _tangents;

    public MonoSpline(float[] time, float[][] y, float periodicBias)
    {
        var n = time.Length;
        var dim = y[0].Length;

        var slope = MakeFloatArray(n - 1, dim);
        var tangent = MakeFloatArray(n, dim);

        for (var j = 0; j < dim; j++)
        {
            for (var i = 0; i < n - 1; i++)
            {
                var dt = time[i + 1] - time[i];
                slope[i][j] = (y[i + 1][j] - y[i][j]) / dt;
                tangent[i][j] = i == 0 ? slope[i][j] : 0.5f * (slope[i - 1][j] + slope[i][j]);
            }

            tangent[n - 1][j] = slope[n - 2][j];
        }

        if (!float.IsNaN(periodicBias))
        {
            for (var j = 0; j < dim; j++)
            {
                var adjustedSlope = (slope[n - 2][j] * (1 - periodicBias)) + (slope[0][j] * periodicBias);
                slope[0][j] = adjustedSlope;
                slope[n - 2][j] = adjustedSlope;
                tangent[n - 1][j] = adjustedSlope;
                tangent[0][j] = adjustedSlope;
            }
        }

        for (var i = 0; i < n - 1; i++)
        {
            for (var j = 0; j < dim; j++)
            {
                if (slope[i][j] == 0.0f)
                {
                    tangent[i][j] = 0.0f;
                    tangent[i + 1][j] = 0.0f;
                }
                else
                {
                    var a = tangent[i][j] / slope[i][j];
                    var b = tangent[i + 1][j] / slope[i][j];
                    var h = (float)Math.Sqrt(a * a + b * b);
                    if (h > 9.0f)
                    {
                        var t = 3.0f / h;
                        tangent[i][j] = t * a * slope[i][j];
                        tangent[i + 1][j] = t * b * slope[i][j];
                    }
                }
            }
        }

        _timePoints = time;
        _values = y;
        _tangents = tangent;
    }

    public float GetPos(float time, int component)
    {
        var n = _timePoints.Length;
        var index = time <= _timePoints[0]
            ? 0
            : (time >= _timePoints[n - 1] ? n - 1 : -1);

        if (index != -1)
        {
            return _values[index][component] + (time - _timePoints[index]) * GetSlope(_timePoints[index], component);
        }

        for (var i = 0; i < n - 1; i++)
        {
            if (Math.Abs(time - _timePoints[i]) < float.Epsilon)
            {
                return _values[i][component];
            }

            if (time < _timePoints[i + 1])
            {
                var h = _timePoints[i + 1] - _timePoints[i];
                var x = (time - _timePoints[i]) / h;
                var y1 = _values[i][component];
                var y2 = _values[i + 1][component];
                var t1 = _tangents[i][component];
                var t2 = _tangents[i + 1][component];
                return Hermite.Interpolate(h, x, y1, y2, t1, t2);
            }
        }

        return 0f;
    }

    public void GetPos(float time, AnimationVector vector, int index = 0)
    {
        var n = _timePoints.Length;
        var dim = _values[0].Length;
        if (vector.Size < dim)
        {
            return;
        }

        var t = Clamp(time, _timePoints[0], _timePoints[n - 1]);
        for (var i = index; i < n - 1; i++)
        {
            if (t <= _timePoints[i + 1])
            {
                var h = _timePoints[i + 1] - _timePoints[i];
                var x = (t - _timePoints[i]) / h;
                for (var j = 0; j < dim; j++)
                {
                    var y1 = _values[i][j];
                    var y2 = _values[i + 1][j];
                    var t1 = _tangents[i][j];
                    var t2 = _tangents[i + 1][j];
                    vector[j] = Hermite.Interpolate(h, x, y1, y2, t1, t2);
                }

                break;
            }
        }
    }

    public float GetSlope(float time, int component)
    {
        var n = _timePoints.Length;
        var t = Clamp(time, _timePoints[0], _timePoints[n - 1]);
        for (var i = 0; i < n - 1; i++)
        {
            if (t <= _timePoints[i + 1])
            {
                var y1 = _values[i][component];
                var y2 = _values[i + 1][component];
                var t1 = _tangents[i][component];
                var t2 = _tangents[i + 1][component];
                var h = _timePoints[i + 1] - _timePoints[i];
                var x = (t - _timePoints[i]) / h;
                return Hermite.Differential(h, x, y1, y2, t1, t2) / h;
            }
        }

        return 0f;
    }

    public void GetSlope(float time, float[] slope)
    {
        var dim = _values[0].Length;
        if (slope.Length < dim)
        {
            return;
        }

        var n = _timePoints.Length;
        var t = Clamp(time, _timePoints[0], _timePoints[n - 1]);
        for (var i = 0; i < n - 1; i++)
        {
            if (t <= _timePoints[i + 1])
            {
                var h = _timePoints[i + 1] - _timePoints[i];
                var x = (t - _timePoints[i]) / h;
                for (var j = 0; j < dim; j++)
                {
                    var y1 = _values[i][j];
                    var y2 = _values[i + 1][j];
                    var t1 = _tangents[i][j];
                    var t2 = _tangents[i + 1][j];
                    slope[j] = Hermite.Differential(h, x, y1, y2, t1, t2) / h;
                }

                break;
            }
        }
    }

    public void GetSlope(float time, AnimationVector vector, int index = 0)
    {
        var n = _timePoints.Length;
        var dim = _values[0].Length;
        if (vector.Size < dim)
        {
            return;
        }

        var tangentIndex = time <= _timePoints[0]
            ? 0
            : (time >= _timePoints[n - 1] ? n - 1 : -1);

        if (tangentIndex != -1)
        {
            var tangent = _tangents[tangentIndex];
            if (tangent.Length < dim)
            {
                return;
            }

            for (var j = 0; j < dim; j++)
            {
                vector[j] = tangent[j];
            }

            return;
        }

        for (var i = index; i < n - 1; i++)
        {
            if (time <= _timePoints[i + 1])
            {
                var h = _timePoints[i + 1] - _timePoints[i];
                var x = (time - _timePoints[i]) / h;
                for (var j = 0; j < dim; j++)
                {
                    var y1 = _values[i][j];
                    var y2 = _values[i + 1][j];
                    var t1 = _tangents[i][j];
                    var t2 = _tangents[i + 1][j];
                    vector[j] = Hermite.Differential(h, x, y1, y2, t1, t2) / h;
                }

                break;
            }
        }
    }

    private static float[][] MakeFloatArray(int count, int dimension)
    {
        var array = new float[count][];
        for (var i = 0; i < count; i++)
        {
            array[i] = new float[dimension];
        }

        return array;
    }

    private static float Clamp(float value, float min, float max)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }
}
