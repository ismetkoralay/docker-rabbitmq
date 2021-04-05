namespace Customer.Api.Infrastructure
{
    public static class Extensions
    {
        public static object GetPropertyValue(this object source, string propertyName)
        {
            var propertyInfo = source.GetType().GetProperty(propertyName);
            return propertyInfo == null ? (object) null : propertyInfo.GetValue(source);
        }
    }
}