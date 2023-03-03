namespace Numerology.API.Managers.Models
{
    public class ManagerResult<T>
    {
        public T Value { get; set; }
        public string[] Message { get; set; }

        public ManagerResult(T value, string[] message)
        {
            this.Value = value;
            this.Message = message;
        }
    }
}
