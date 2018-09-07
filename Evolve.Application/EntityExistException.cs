using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Application
{
    [Serializable]
    public class EntityExistException : Exception
    {
        public EntityExistException() 
        {
        }
        public EntityExistException( string message ) : base( message ) 
        {
        }
        public EntityExistException( string message, Exception inner ) : base( message, inner ) 
        {
        }
        protected EntityExistException( 
        System.Runtime.Serialization.SerializationInfo info, 
        System.Runtime.Serialization.StreamingContext context ) : base( info, context ) 
        { 
        }
    }
}
