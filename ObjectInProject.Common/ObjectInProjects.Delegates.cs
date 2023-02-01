namespace ObjectInProject.Common
{
    public delegate void AuditMessage(string message, string method, string module, int line, AuditSeverity auditSeverity);
    public delegate bool CreateUpdateSearchProjectReply(CrudAction crudAction, SearchProject searchProject, out string result);
    public delegate bool EventMessage(object message, out object reply, out string result);
}
