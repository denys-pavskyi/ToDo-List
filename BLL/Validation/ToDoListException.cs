using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    [Serializable]
    public class ToDoListException : Exception
    {

        public ToDoListException()
        { }

        public ToDoListException(string message)
            : base(message)
        { }

        public ToDoListException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected ToDoListException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
