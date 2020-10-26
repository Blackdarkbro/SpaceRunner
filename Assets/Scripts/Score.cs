using System;
using System.Globalization;
using System.Text;
using UnityEngine;

public class Score
{
    public Score()
    {
        Value = 0;
    }

    public Score(string score)
    {
        if (int.TryParse(score, out var res))
        {
            Debug.Log("No correct value of score");
        }

        Value = res;
    }

    public int Value { get; private set; }
    private int _value;
    StringBuilder _sb = new StringBuilder();

    public void ResetValue()
    {
        Value = 0;
    }

    public static Score operator ++(Score obj)
    {
        obj.Value++;
        return obj;
    }

    public static Score operator --(Score obj)
    {
        obj.Value--;
        return obj;
    }

    public static bool operator !=(Score obj1, Score obj2)
    {
        return obj1?.Value != obj2?.Value;
    }

    public static bool operator ==(Score obj1, Score obj2)
    {
        return obj1?.Value == obj2?.Value;
    }

    public static explicit operator int(Score score)
    {
        return score.Value;
    }

    public override string ToString()
    {
        _sb.Clear().Append($"Score: {Value}");
        return _sb.ToString();
    }
}