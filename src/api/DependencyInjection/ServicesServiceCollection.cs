using api.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Defines the method to register our services.
    /// </summary>
    public static class ServicesServiceCollection
    {
        /// <summary>
        /// Adds our services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddScoped<IUserRoleService, UserRoleService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRoleService, RoleService>();
    }
}