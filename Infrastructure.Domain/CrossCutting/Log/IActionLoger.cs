namespace Infrastructure.Domain.CrossCutting.Log
{
    public interface IActionLoger
    {
        void LogAction(string userName, string actionName, string[] parameterNameList, object[] parameterList, object result);
    }
}
