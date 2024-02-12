namespace SnippingToolWPF
{
    public static class ServiceProviderExtensions
    {
        public static T? GetService<T>(this IServiceProvider serviceProvider)
        where T : class
        {
            return serviceProvider.GetService(typeof(T)) as T;
        }

        public static T? GetRequiredService<T>(this IServiceProvider serviceProvider)
        where T : class
        {
            return serviceProvider.GetRequiredService(typeof(T)) as T;
        }
        public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(provider);
            ArgumentNullException.ThrowIfNull(serviceType);
            return provider.GetService(serviceType) ?? throw new InvalidOperationException($"No service for type '{serviceType}' has been registered.");
        }
    }
}
