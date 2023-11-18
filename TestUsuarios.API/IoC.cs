using TestUsuarios.Business.IServices;
using TestUsuarios.Business.Services;
using TestUsuarios.Lib.Utils;
using TesUsuarios.Data.Repository.Usuarios;

namespace TestUsuarios.API
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUtils, Utils>();
            services.AddScoped<IUsuariosServices, UsuarioServices>();
            services.AddCors();
            return services;
        }
    }
}
