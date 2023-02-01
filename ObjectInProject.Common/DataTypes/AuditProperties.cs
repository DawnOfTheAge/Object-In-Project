namespace ObjectInProject.Common
{
    public class AuditProperties
    {
        #region Properties

        public bool AuditOn { get; set; }

        public bool ShowMethod { get; set; }

        public bool ShowModule { get; set; }

        public bool ShowLine { get; set; }

        public bool ShowDateTime { get; set; }

        public bool ShowSeverity { get; set; }

        #endregion

        #region Constructor

        public AuditProperties(bool auditOn,
                               bool showMethod,
                               bool showModule,
                               bool showLine,
                               bool showDateTime,
                               bool showSeverity)
        {
            AuditOn = auditOn;
            ShowMethod = showMethod;
            ShowModule = showModule;
            ShowLine = showLine;
            ShowDateTime = showDateTime;
            ShowSeverity = showSeverity;
        }

        public AuditProperties(bool all)
        {
            AuditOn = all;
            ShowMethod = all;
            ShowModule = all;
            ShowLine = all;
            ShowDateTime = all;
            ShowSeverity = all;
        }

        public AuditProperties()
        {
        }

        #endregion
    }
}
