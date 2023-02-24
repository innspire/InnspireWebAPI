namespace InnspireWebAPI.Util
{
    public class ValueResult<TValue>
    {
        public TValue Value { get; set; }

        public bool Success { get; set; }

        public ValueResult(TValue value, bool success)
        {
            Value = value;
            Success = success;
        }

        public static ValueResult<TValue> AccessDenied()
        {
            return new ValueResult<TValue>(default, false); 
        }
    }
}
