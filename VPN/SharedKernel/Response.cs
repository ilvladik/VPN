namespace SharedKernel
{
    public class Response<TDto>
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public TDto? Dto { get; set; }
    }
}
