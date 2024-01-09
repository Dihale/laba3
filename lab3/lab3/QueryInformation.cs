using System.Runtime.Serialization;

namespace lab3;

[Serializable]
public class QueryInformation
{
    public QueryInformation(int indexOperation, string input, string result)
    {
        IndexOperation = indexOperation;
        Input = input;
        Result = result;
    }

    [DataMember] public int IndexOperation { get; private set; }
    [DataMember] public string Input { get; private set; }
    [DataMember] public string Result { get; private set; }
}