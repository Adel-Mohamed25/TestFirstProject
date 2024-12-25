namespace Project.BLL.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CustomAutoCompleteAttribute : Attribute
    {
        public string AutoCompleteValue { get; private set; }

        public CustomAutoCompleteAttribute(string value)
        {
            AutoCompleteValue = value;
        }
    }
}
