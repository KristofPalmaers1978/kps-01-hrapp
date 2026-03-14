namespace HrApp.Services.Results
{
    public abstract class BaseResult
    {
        protected BaseResult()
        {
            Succeeded = true;
        }
        List<string> _errors = new List<string>();
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors => _errors;
        public string ErrorString => string.Join(", ", _errors);
        public void Failed(string error)
        {
            Succeeded = false;
            _errors.Add(error); 
        }
    }
}
