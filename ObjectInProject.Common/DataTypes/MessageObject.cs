namespace ObjectInProject.Common
{
    public class MessageObject
    {        
        #region Properties

        public IoAction Action { get; set; }

        public object Data { get; set; }

        #endregion
        
        #region Constructors

        public MessageObject(IoAction action)
        {
            Action = action;
            Data = null;
        }

        public MessageObject(IoAction action, object data)
        {
            Action = action;
            Data = data;
        }

        #endregion
    }
}
