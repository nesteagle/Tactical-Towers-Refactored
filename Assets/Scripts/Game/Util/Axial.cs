using System;

public class Axial
{
    public int Q { get; set; }
    public int R { get; set; }

    public Axial(int q, int r)
    {
        Q = q;
        R = r;
    }

    public override bool Equals(object obj)
    {
        return obj is Axial axial &&
               Q == axial.Q &&
               R == axial.R;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Q, R);
    }
}