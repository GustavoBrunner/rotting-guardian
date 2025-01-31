
public enum Phases
{
    first,
    second, 
    third
}
public class PhaseArrayBuilder 
{

    public int[] GetPhaseArray(Phases phase)
    {
        int[] result;
        switch (phase)
        {
            case Phases.first:
                result = new int[]
                    {
                        2, 2, 2, 2, 2, 2, 2, 2,
                        2, 1, 1, 2, 1, 1, 1, 2,
                        2, 1, 2, 2, 2, 1, 1, 2,
                        2, 1, 1, 2, 1, 1, 1, 2,
                        2, 1, 1, 1, 1, 1, 0, 2,
                        2, 0, 1, 1, 1, 0, 0, 2,
                        2, 0, 0, 1, 1, 0, 0, 2,
                        2, 2, 2, 2, 2, 2, 2, 2
                    };
                return result;
            case Phases.second:
                result = new int[]
                    {
                        0, 1, 1, 1, 1, 1, 0,
                        1, 1, 1, 2, 1, 1, 1,
                        1, 1, 2, 2, 2, 1, 1,
                        1, 1, 1, 2, 1, 1, 1,
                        0, 1, 1, 1, 1, 1, 0,
                        0, 0, 1, 1, 1, 0, 0,
                        0, 0, 0, 1, 1, 0, 0,
                        0, 0, 0, 1, 0, 0, 0
                    };
                return result;

            default:
                return new int[] { };
        }
    }
}
