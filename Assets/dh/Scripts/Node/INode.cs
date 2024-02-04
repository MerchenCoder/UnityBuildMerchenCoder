using System.Collections;

public interface INode
{
    public IEnumerator Execute();
    public IEnumerator ProcessData();
}