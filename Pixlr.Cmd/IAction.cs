namespace Pixlr.Cmd
{
    public interface IAction<TRequest, TResult>
    {
        TResult Execute(TRequest request);
    }
}