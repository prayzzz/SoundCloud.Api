using System.Linq.Expressions;
using Moq;
using Moq.Protected;

namespace SoundCloud.Api.Test
{
    public class ArgumentCaptor<T>
    {
        public T Capture()
        {
            return It.Is<T>(t => SaveValue(t));
        }
        
        public Expression CaptureExpr()
        {
            return ItExpr.Is<T>(t => SaveValue(t));
        }

        private bool SaveValue(T t)
        {
            Value = t;
            return true;
        }

        public T Value { get; private set; }
    }
}